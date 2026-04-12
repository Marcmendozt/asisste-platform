using Asisste.Data.Communication;
using Asisste.Services.Abstractions.Attendance;
using Asisste.Services.Abstractions.Authentication;
using Asisste.Services.Abstractions.Device;
using Microsoft.Maui.Storage;
using System.Globalization;

namespace Asisste.Data.Attendance;

public sealed class LegacyAttendanceService : IAttendanceService
{
    private const double GeofenceMeters = 61.3988238130978;
    private const string LocalAttendanceDateKey = "asisste.local-attendance.date";
    private const string LocalAttendanceEntryKey = "asisste.local-attendance.entry";
    private const string LocalAttendanceExitKey = "asisste.local-attendance.exit";

    private readonly IAuthenticationService authenticationService;
    private readonly ILocationService locationService;
    private readonly LegacyApiClient apiClient;

    public LegacyAttendanceService(
        IAuthenticationService authenticationService,
        ILocationService locationService,
        LegacyApiClient apiClient)
    {
        this.authenticationService = authenticationService;
        this.locationService = locationService;
        this.apiClient = apiClient;
    }

    public async Task<AttendanceSnapshot> GetTodaySnapshotAsync(CancellationToken cancellationToken = default)
    {
        var session = authenticationService.CurrentSession;

        if (session is null)
        {
            return CreateNoSessionSnapshot();
        }

        if (session.IsLocalMode)
        {
            return CreateLocalSnapshot(session);
        }

        try
        {
            var today = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            var response = await apiClient.GetAsync<LegacyAttendanceResponse>(
                $"api/Asistencia?ID_Usuario={session.UserId}&Fecha={today}",
                cancellationToken);

            if (!IsSameDay(response?.Fecha, DateTime.Today))
            {
                return new AttendanceSnapshot(
                    true,
                    session.DisplayName,
                    "Aún no registras asistencia hoy.",
                    "Marcar entrada",
                    true,
                    false,
                    false,
                    "No registrada");
            }

            if (string.IsNullOrWhiteSpace(response?.HoraSalida) || response.HoraSalida == "00:00:00")
            {
                return new AttendanceSnapshot(
                    true,
                    session.DisplayName,
                    "Tu entrada ya fue registrada. Falta marcar la salida.",
                    "Marcar salida",
                    true,
                    true,
                    false,
                    "Pendiente");
            }

            return new AttendanceSnapshot(
                true,
                session.DisplayName,
                "La asistencia de hoy ya está completa.",
                "Ya no se puede marcar",
                false,
                false,
                true,
                response.HoraSalida);
        }
        catch (HttpRequestException)
        {
            return new AttendanceSnapshot(
                true,
                session.DisplayName,
                "No fue posible consultar la asistencia actual.",
                "Reintentar",
                false,
                false,
                false,
                "Sin datos");
        }
    }

    public async Task<AttendanceActionResult> MarkAsync(CancellationToken cancellationToken = default)
    {
        var session = authenticationService.CurrentSession;

        if (session is null)
        {
            var snapshotWithoutSession = CreateNoSessionSnapshot();
            return new AttendanceActionResult(false, "Inicia sesión para continuar.", snapshotWithoutSession);
        }

        if (session.IsLocalMode)
        {
            return MarkLocal(session);
        }

        var snapshot = await GetTodaySnapshotAsync(cancellationToken);

        if (!snapshot.CanMark)
        {
            return new AttendanceActionResult(false, snapshot.StatusMessage, snapshot);
        }

        if (!await locationService.IsLocationEnabledAsync(cancellationToken))
        {
            return new AttendanceActionResult(false, "Activa el GPS para registrar la marcación.", snapshot);
        }

        var location = await locationService.GetCurrentLocationAsync(cancellationToken);

        if (location is null)
        {
            return new AttendanceActionResult(false, "No fue posible obtener la ubicación actual.", snapshot);
        }

        var coordinates = FormatCoordinates(location.Latitude, location.Longitude);
        string message;

        if (snapshot.CanMarkExit)
        {
            message = await apiClient.PostJsonAsync(
                "api/AsistenciaSalida",
                new
                {
                    ID_Usuario = session.UserId,
                    HoraSalida = DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture),
                    UbicacionSalida = coordinates
                },
                cancellationToken);
        }
        else
        {
            var workLocation = await apiClient.GetAsync<LegacyWorkLocationResponse>(
                $"api/Usuarios?ID_Usuario={session.UserId}",
                cancellationToken);

            var geofenceStatus = "DENTRO DEL RANGO PERMITIDO";

            if (string.IsNullOrWhiteSpace(workLocation?.Ubicacion))
            {
                await apiClient.PostJsonAsync(
                    "api/Asistencia",
                    new
                    {
                        ID_Usuario = session.UserId,
                        Ubicacion = coordinates
                    },
                    cancellationToken);
            }
            else
            {
                geofenceStatus = CalculateGeofenceStatus(workLocation.Ubicacion, location.Latitude, location.Longitude);
            }

            message = await apiClient.PostJsonAsync(
                "api/Maestros",
                new
                {
                    ID_Usuario = session.UserId,
                    HoraIngreso = DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture),
                    UbicacionIngreso = coordinates,
                    NombreUsuario = session.AttendanceUsername,
                    Geovalla = geofenceStatus
                },
                cancellationToken);
        }

        var updatedSnapshot = await GetTodaySnapshotAsync(cancellationToken);
        return new AttendanceActionResult(true, message, updatedSnapshot);
    }

    private static AttendanceSnapshot CreateNoSessionSnapshot()
    {
        return new AttendanceSnapshot(
            false,
            "Sin sesión",
            "Inicia sesión para consultar tu asistencia.",
            "Marcar entrada",
            false,
            false,
            false,
            "Sin datos");
    }

    private static AttendanceSnapshot CreateLocalSnapshot(Domain.Entities.UserSession session)
    {
        var today = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        var storedDate = Preferences.Default.Get(LocalAttendanceDateKey, string.Empty);

        if (!string.Equals(storedDate, today, StringComparison.Ordinal))
        {
            Preferences.Default.Remove(LocalAttendanceEntryKey);
            Preferences.Default.Remove(LocalAttendanceExitKey);
            Preferences.Default.Set(LocalAttendanceDateKey, today);
        }

        var entryTime = Preferences.Default.Get(LocalAttendanceEntryKey, string.Empty);
        var exitTime = Preferences.Default.Get(LocalAttendanceExitKey, string.Empty);

        if (string.IsNullOrWhiteSpace(entryTime))
        {
            return new AttendanceSnapshot(
                true,
                session.DisplayName,
                "Modo local activo. Puedes simular una marcación sin llamar al API.",
                "Marcar entrada",
                true,
                false,
                false,
                "No registrada");
        }

        if (string.IsNullOrWhiteSpace(exitTime))
        {
            return new AttendanceSnapshot(
                true,
                session.DisplayName,
                $"Entrada local registrada a las {entryTime}. Falta marcar la salida.",
                "Marcar salida",
                true,
                true,
                false,
                "Pendiente");
        }

        return new AttendanceSnapshot(
            true,
            session.DisplayName,
            $"Modo local: asistencia completa. Entrada {entryTime}, salida {exitTime}.",
            "Ya no se puede marcar",
            false,
            false,
            true,
            exitTime);
    }

    private static AttendanceActionResult MarkLocal(Domain.Entities.UserSession session)
    {
        var snapshot = CreateLocalSnapshot(session);

        if (!snapshot.CanMark)
        {
            return new AttendanceActionResult(false, snapshot.StatusMessage, snapshot);
        }

        var now = DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        var today = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        Preferences.Default.Set(LocalAttendanceDateKey, today);

        string message;

        if (snapshot.CanMarkExit)
        {
            Preferences.Default.Set(LocalAttendanceExitKey, now);
            message = $"Salida local registrada a las {now}.";
        }
        else
        {
            Preferences.Default.Set(LocalAttendanceEntryKey, now);
            Preferences.Default.Remove(LocalAttendanceExitKey);
            message = $"Entrada local registrada a las {now}.";
        }

        var updatedSnapshot = CreateLocalSnapshot(session);
        return new AttendanceActionResult(true, message, updatedSnapshot);
    }

    private static bool IsSameDay(string? value, DateTime expectedDate)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate)
               && parsedDate.Date == expectedDate.Date;
    }

    private static string FormatCoordinates(double latitude, double longitude)
    {
        return string.Create(
            CultureInfo.InvariantCulture,
            $"{latitude:F6},{longitude:F6}");
    }

    private static string CalculateGeofenceStatus(string coordinates, double currentLatitude, double currentLongitude)
    {
        var parts = coordinates.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2 ||
            !double.TryParse(parts[0], CultureInfo.InvariantCulture, out var registeredLatitude) ||
            !double.TryParse(parts[1], CultureInfo.InvariantCulture, out var registeredLongitude))
        {
            return "DENTRO DEL RANGO PERMITIDO";
        }

        var distance = CalculateDistanceMeters(registeredLatitude, registeredLongitude, currentLatitude, currentLongitude);
        return distance < GeofenceMeters ? "DENTRO DEL RANGO PERMITIDO" : "ESTÁ FUERA DEL RANGO PERMITIDO";
    }

    private static double CalculateDistanceMeters(double latitude1, double longitude1, double latitude2, double longitude2)
    {
        const double earthRadiusKm = 6371;
        var latitudeDelta = (latitude2 - latitude1) * (Math.PI / 180);
        var longitudeDelta = (longitude2 - longitude1) * (Math.PI / 180);

        var a = Math.Sin(latitudeDelta / 2) * Math.Sin(latitudeDelta / 2) +
                Math.Cos(latitude1 * (Math.PI / 180)) *
                Math.Cos(latitude2 * (Math.PI / 180)) *
                Math.Sin(longitudeDelta / 2) * Math.Sin(longitudeDelta / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadiusKm * c * 1000;
    }

    private sealed class LegacyAttendanceResponse
    {
        public string Fecha { get; set; } = string.Empty;

        public string HoraSalida { get; set; } = string.Empty;
    }

    private sealed class LegacyWorkLocationResponse
    {
        public string Ubicacion { get; set; } = string.Empty;
    }
}