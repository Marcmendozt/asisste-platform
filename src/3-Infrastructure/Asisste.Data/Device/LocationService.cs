using Asisste.Services.Abstractions.Device;
using Android.Content;
using Android.Locations;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using Platform = Microsoft.Maui.ApplicationModel.Platform;

namespace Asisste.Data.Device;

public sealed class LocationService : ILocationService
{
    public Task<bool> IsLocationEnabledAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var locationManager = (LocationManager?)Platform.AppContext.GetSystemService(Context.LocationService);
        var isEnabled = locationManager?.IsProviderEnabled(LocationManager.GpsProvider) == true ||
                        locationManager?.IsProviderEnabled(LocationManager.NetworkProvider) == true;

        return Task.FromResult(isEnabled);
    }

    public Task OpenLocationSettingsAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
        intent.AddFlags(ActivityFlags.NewTask);

        if (Platform.CurrentActivity is not null)
        {
            Platform.CurrentActivity.StartActivity(intent);
        }
        else
        {
            Platform.AppContext.StartActivity(intent);
        }

        return Task.CompletedTask;
    }

    public async Task<LocationResult?> GetCurrentLocationAsync(CancellationToken cancellationToken = default)
    {
        if (!await IsLocationEnabledAsync(cancellationToken))
        {
            return null;
        }

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
