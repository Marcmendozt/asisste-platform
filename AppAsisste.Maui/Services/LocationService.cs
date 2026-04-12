using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace AppAsisste.Maui.Services;

public sealed class LocationService : ILocationService
{
    public async Task<LocationResult?> GetCurrentLocationAsync(CancellationToken cancellationToken = default)
    {
        var permission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (permission != PermissionStatus.Granted)
        {
            permission = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        if (permission != PermissionStatus.Granted)
        {
            return null;
        }

        var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
        var location = await Geolocation.Default.GetLocationAsync(request, cancellationToken);

        if (location is null)
        {
            return null;
        }

        return new LocationResult(
            location.Latitude,
            location.Longitude,
            location.Accuracy ?? 0,
            location.Timestamp);
    }
}
