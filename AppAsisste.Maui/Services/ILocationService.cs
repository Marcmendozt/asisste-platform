using Microsoft.Maui.Devices.Sensors;

namespace AppAsisste.Maui.Services;

public interface ILocationService
{
    Task<LocationResult?> GetCurrentLocationAsync(CancellationToken cancellationToken = default);
}

public sealed record LocationResult(
    double Latitude,
    double Longitude,
    double AccuracyMeters,
    DateTimeOffset Timestamp);
