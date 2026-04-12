using System.Text;
using System.Text.Json;

namespace Asisste.Data.Communication;

public sealed class LegacyApiClient
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient httpClient;

    public LegacyApiClient()
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://201.240.192.217/"),
            Timeout = TimeSpan.FromSeconds(20)
        };
    }

    public async Task<T?> GetAsync<T>(string relativePath, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, relativePath);
        using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        if (stream.CanSeek && stream.Length == 0)
        {
            return default;
        }

        return await JsonSerializer.DeserializeAsync<T>(stream, SerializerOptions, cancellationToken);
    }

    public async Task<string> PostJsonAsync(string relativePath, object payload, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(payload, SerializerOptions);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await httpClient.PostAsync(relativePath, content, cancellationToken);
        return await ReadMessageAsync(response, cancellationToken);
    }

    public async Task<string> PostMultipartAsync(string relativePath, MultipartFormDataContent content, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsync(relativePath, content, cancellationToken);
        return await ReadMessageAsync(response, cancellationToken);
    }

    private static async Task<string> ReadMessageAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(body))
        {
            return body.Trim().Trim('"');
        }

        return response.IsSuccessStatusCode
            ? "Operación completada correctamente."
            : $"Error HTTP {(int)response.StatusCode}.";
    }
}