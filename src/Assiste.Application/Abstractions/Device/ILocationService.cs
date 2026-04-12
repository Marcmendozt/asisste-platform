namespace Assiste.Application.Abstractions.Device;

public interface ILocationService
{
    Task<LocationResult?> GetCurrentLocationAsync(CancellationToken cancellationToken = default);
}

public sealed record LocationResult(
    double Latitude,
    double Longitude,
    double AccuracyMeters,
    DateTimeOffset Timestamp);
