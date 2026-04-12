using Asisste.Data.Communication;
using Asisste.Domain.Entities;
using Asisste.Services.Abstractions.Authentication;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using System.Globalization;
using System.Text.Json;

namespace Asisste.Data.Authentication;

public sealed class LegacyAuthenticationService : IAuthenticationService
{
    private const string SessionKey = "asisste.current-session";
    private const string OriginalUsernameKey = "asisste.original-username";
    private const string DeviceIdKey = "asisste.device-id";
    private const string LocalUsername = "qa.local";
    private const string LocalDisplayName = "Usuario local QA";

    private readonly LegacyApiClient apiClient;
    private readonly MobileAuthApiClient mobileAuthApiClient;

    public LegacyAuthenticationService(LegacyApiClient apiClient, MobileAuthApiClient mobileAuthApiClient)
    {
        this.apiClient = apiClient;
        this.mobileAuthApiClient = mobileAuthApiClient;
        CurrentSession = LoadSession();
    }

    public UserSession? CurrentSession { get; private set; }

    public Task<SignInResult> SignInLocalAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var session = new UserSession(
            -1,
            LocalUsername,
            LocalDisplayName,
            1,
            true,
            LocalUsername,
            true);

        SaveSession(session);
        CurrentSession = session;

        return Task.FromResult(new SignInResult(
            true,
            "Modo local activado. Ya puedes probar la app sin conectarte al backend legado.",
            session));
    }

    public async Task<SignInResult> SignInAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return new SignInResult(false, "Ingresa usuario y contraseña.");
        }

        try
        {
            var normalizedUsername = username.Trim();
            var loginResult = await mobileAuthApiClient.SignInAsync(
                normalizedUsername,
                password,
                cancellationToken);

            if (loginResult.Response is null)
            {
                return new SignInResult(false, loginResult.Message);
            }

            var attendanceUsername = Preferences.Default.Get(OriginalUsernameKey, string.Empty);

            if (string.IsNullOrWhiteSpace(attendanceUsername))
            {
                attendanceUsername = normalizedUsername;
                Preferences.Default.Set(OriginalUsernameKey, attendanceUsername);
            }

            var mobileSessionEnabled = loginResult.Response.HasMobileSession;
            var signInMessage = "Sesión iniciada correctamente.";

            if (!mobileSessionEnabled)
            {
                try
                {
                    await RegisterDeviceAsync(loginResult.Response.UserId, cancellationToken);
                    mobileSessionEnabled = true;
                }
                catch (HttpRequestException)
                {
                    signInMessage = "Sesión iniciada, pero no fue posible registrar el dispositivo en el backend legado.";
                }
                catch (TaskCanceledException)
                {
                    signInMessage = "Sesión iniciada, pero el registro del dispositivo agotó el tiempo de espera.";
                }
            }

            var displayName = string.IsNullOrWhiteSpace(loginResult.Response.FullName)
                ? normalizedUsername
                : loginResult.Response.FullName;

            var session = new UserSession(
                loginResult.Response.UserId,
                normalizedUsername,
                displayName,
                1,
                mobileSessionEnabled,
                attendanceUsername,
                false);

            SaveSession(session);
            CurrentSession = session;

            return new SignInResult(true, signInMessage, session);
        }
        catch (HttpRequestException)
        {
            return new SignInResult(false, "No fue posible conectarse con el API de autenticación.");
        }
        catch (TaskCanceledException)
        {
            return new SignInResult(false, "La autenticación superó el tiempo de espera.");
        }
    }

    public Task SignOutAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Preferences.Default.Remove(SessionKey);
        CurrentSession = null;

        return Task.CompletedTask;
    }

    private async Task RegisterDeviceAsync(int userId, CancellationToken cancellationToken)
    {
        var payload = new
        {
            ID_Usuario = userId,
            Model = DeviceInfo.Current.Model,
            Manufacturer = DeviceInfo.Current.Manufacturer,
            Name = DeviceInfo.Name,
            Version = DeviceInfo.Current.VersionString,
            Platform = DeviceInfo.Current.Platform.ToString(),
            Idiom = CultureInfo.CurrentUICulture.Name,
            DeviceType = DeviceInfo.Current.DeviceType.ToString(),
            IMEI = GetOrCreateDeviceId()
        };

        await apiClient.PostJsonAsync("api/Usuarios", payload, cancellationToken);
    }

    private static string GetOrCreateDeviceId()
    {
        var deviceId = Preferences.Default.Get(DeviceIdKey, string.Empty);

        if (!string.IsNullOrWhiteSpace(deviceId))
        {
            return deviceId;
        }

        deviceId = Guid.NewGuid().ToString("N");
        Preferences.Default.Set(DeviceIdKey, deviceId);
        return deviceId;
    }

    private static UserSession? LoadSession()
    {
        var serializedSession = Preferences.Default.Get(SessionKey, string.Empty);

        if (string.IsNullOrWhiteSpace(serializedSession))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<UserSession>(serializedSession);
        }
        catch (JsonException)
        {
            Preferences.Default.Remove(SessionKey);
            return null;
        }
    }

    private static void SaveSession(UserSession session)
    {
        Preferences.Default.Set(SessionKey, JsonSerializer.Serialize(session));
    }
}