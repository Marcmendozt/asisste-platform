using Microsoft.Maui.Devices;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Asisste.Data.Communication;

public sealed class MobileAuthApiClient
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient httpClient;

    public MobileAuthApiClient()
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(GetBaseAddress()),
            Timeout = TimeSpan.FromSeconds(20)
        };
    }

    public async Task<MobileAuthApiResult> SignInAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        var payload = JsonSerializer.Serialize(new MobileLoginRequest
        {
            Username = username,
            Password = password
        }, SerializerOptions);

        using var content = new StringContent(payload, Encoding.UTF8, "application/json");
        using var response = await httpClient.PostAsync("api/mobile/auth/login", content, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return new MobileAuthApiResult(null, ReadMessage(body) ?? CreateFallbackMessage(response.StatusCode));
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            return new MobileAuthApiResult(null, "La API de autenticación no devolvió contenido.");
        }

        var loginResponse = JsonSerializer.Deserialize<MobileAuthResponse>(body, SerializerOptions);

        if (loginResponse is null)
        {
            return new MobileAuthApiResult(null, "La respuesta del API de autenticación no se pudo interpretar.");
        }

        return new MobileAuthApiResult(loginResponse, string.Empty);
    }

    private static string GetBaseAddress()
    {
        return DeviceInfo.Current.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5075/"
            : "http://localhost:5075/";
    }

    private static string? ReadMessage(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return null;
        }

        try
        {
            var error = JsonSerializer.Deserialize<ApiErrorResponse>(body, SerializerOptions);

            if (!string.IsNullOrWhiteSpace(error?.Message))
            {
                return error.Message;
            }
        }
        catch (JsonException)
        {
        }

        return body.Trim().Trim('"');
    }

    private static string CreateFallbackMessage(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.BadRequest => "Usuario y clave son obligatorios.",
            HttpStatusCode.Unauthorized => "Credenciales inválidas.",
            _ => $"Error HTTP {(int)statusCode}."
        };
    }

    public sealed record MobileAuthApiResult(MobileAuthResponse? Response, string Message);

    private sealed class MobileLoginRequest
    {
        public string Username { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;
    }

    public sealed class MobileAuthResponse
    {
        public int UserId { get; init; }

        public string FullName { get; init; } = string.Empty;

        public string EntryTime { get; init; } = string.Empty;

        public int ToleranceMinutes { get; init; }

        public int MobileId { get; init; }

        public bool HasMobileSession { get; init; }
    }

    private sealed class ApiErrorResponse
    {
        public string Message { get; init; } = string.Empty;
    }
}