using Asisste.Data.Communication;
using Asisste.Services.Abstractions.Absences;
using Asisste.Services.Abstractions.Authentication;
using Microsoft.Maui.Storage;
using System.Globalization;
using System.Net.Http.Headers;

namespace Asisste.Data.Absences;

public sealed class LegacyAbsenceService : IAbsenceService
{
    private const string LocalAbsenceDateKey = "asisste.local-absence.date";
    private const string LocalAbsenceFileNameKey = "asisste.local-absence.file-name";

    private readonly IAuthenticationService authenticationService;
    private readonly LegacyApiClient apiClient;

    public LegacyAbsenceService(IAuthenticationService authenticationService, LegacyApiClient apiClient)
    {
        this.authenticationService = authenticationService;
        this.apiClient = apiClient;
    }

    public async Task<AbsenceSnapshot> GetTodaySnapshotAsync(CancellationToken cancellationToken = default)
    {
        var session = authenticationService.CurrentSession;

        if (session is null)
        {
            return new AbsenceSnapshot(false, false, "Inicia sesión para gestionar faltas.");
        }

        if (session.IsLocalMode)
        {
            return CreateLocalSnapshot();
        }

        try
        {
            var today = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            var response = await apiClient.GetAsync<LegacyAbsenceResponse>(
                $"api/Faltas?ID_Usuario={session.UserId}&Fecha={today}",
                cancellationToken);

            var alreadySubmitted = IsSameDay(response?.Fecha, DateTime.Today);

            return new AbsenceSnapshot(
                true,
                alreadySubmitted,
                alreadySubmitted
                    ? "Ya registraste una falta para hoy."
                    : "Selecciona un archivo para registrar la falta del día.");
        }
        catch (HttpRequestException)
        {
            return new AbsenceSnapshot(true, false, "No fue posible consultar el estado de faltas.");
        }
    }

    public async Task<AbsenceActionResult> SubmitAsync(AbsenceAttachment attachment, CancellationToken cancellationToken = default)
    {
        var session = authenticationService.CurrentSession;

        if (session is null)
        {
            var snapshotWithoutSession = new AbsenceSnapshot(false, false, "Inicia sesión para gestionar faltas.");
            return new AbsenceActionResult(false, "No hay una sesión activa.", snapshotWithoutSession);
        }

        if (session.IsLocalMode)
        {
            return SubmitLocal(attachment);
        }

        if (attachment.Content.Length >= 4_000_000)
        {
            var oversizedSnapshot = await GetTodaySnapshotAsync(cancellationToken);
            return new AbsenceActionResult(false, "El archivo no debe pesar más de 4 MB.", oversizedSnapshot);
        }

        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(attachment.Content);

        if (!string.IsNullOrWhiteSpace(attachment.ContentType))
        {
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(attachment.ContentType);
        }

        content.Add(fileContent, "file", attachment.FileName);

        var today = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        var path = $"api/Faltas?ID_Usuario={session.UserId}&Usuario={Uri.EscapeDataString(session.Username)}&fechaactual={today}&ID_Estado=3";
        var message = await apiClient.PostMultipartAsync(path, content, cancellationToken);
        var snapshot = await GetTodaySnapshotAsync(cancellationToken);

        return new AbsenceActionResult(true, message, snapshot);
    }

    private static AbsenceSnapshot CreateLocalSnapshot()
    {
        var today = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        var storedDate = Preferences.Default.Get(LocalAbsenceDateKey, string.Empty);

        if (!string.Equals(storedDate, today, StringComparison.Ordinal))
        {
            Preferences.Default.Remove(LocalAbsenceDateKey);
            Preferences.Default.Remove(LocalAbsenceFileNameKey);

            return new AbsenceSnapshot(
                true,
                false,
                "Modo local activo. Puedes enviar una evidencia simulada sin llamar al API.");
        }

        var fileName = Preferences.Default.Get(LocalAbsenceFileNameKey, "archivo-demo.pdf");

        return new AbsenceSnapshot(
            true,
            true,
            $"Modo local: ya registraste una falta con el archivo {fileName}.");
    }

    private static AbsenceActionResult SubmitLocal(AbsenceAttachment attachment)
    {
        if (attachment.Content.Length >= 4_000_000)
        {
            var oversizedSnapshot = CreateLocalSnapshot();
            return new AbsenceActionResult(false, "El archivo no debe pesar más de 4 MB.", oversizedSnapshot);
        }

        var snapshot = CreateLocalSnapshot();

        if (snapshot.AlreadySubmittedToday)
        {
            return new AbsenceActionResult(false, snapshot.StatusMessage, snapshot);
        }

        Preferences.Default.Set(LocalAbsenceDateKey, DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        Preferences.Default.Set(LocalAbsenceFileNameKey, attachment.FileName);

        var updatedSnapshot = CreateLocalSnapshot();
        return new AbsenceActionResult(true, $"Archivo {attachment.FileName} registrado en modo local.", updatedSnapshot);
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

    private sealed class LegacyAbsenceResponse
    {
        public string Fecha { get; set; } = string.Empty;
    }
}