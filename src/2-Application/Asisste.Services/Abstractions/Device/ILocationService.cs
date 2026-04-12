namespace Asisste.Services.Abstractions.Device;

public interface ILocationService
{
    Task<bool> IsLocationEnabledAsync(CancellationToken cancellationToken = default);

    Task OpenLocationSettingsAsync(CancellationToken cancellationToken = default);

    Task<LocationResult?> GetCurrentLocationAsync(CancellationToken cancellationToken = default);
}

public sealed record LocationResult(
    double Latitude,
    double Longitude,
    double AccuracyMeters,
    DateTimeOffset Timestamp);
