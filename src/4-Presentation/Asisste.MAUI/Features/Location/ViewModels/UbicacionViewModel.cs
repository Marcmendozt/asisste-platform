using Asisste.Services.Abstractions.Device;
using Asisste.MAUI.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using System.Globalization;

namespace Asisste.MAUI.Features.Location.ViewModels;

public partial class UbicacionViewModel : ViewModelBase
{
    private readonly ILocationService locationService;

    public UbicacionViewModel(ILocationService locationService)
    {
        this.locationService = locationService;
        Title = "Ubicación";
    }

    [ObservableProperty]
    private string statusMessage = "Solicita la ubicación actual del dispositivo.";

    [ObservableProperty]
    private string latitude = "-";

    [ObservableProperty]
    private string longitude = "-";

    [ObservableProperty]
    private string accuracy = "-";

    [RelayCommand]
    private async Task LocateAsync()
    {
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;
        StatusMessage = "Buscando ubicación...";

        try
        {
            var result = await locationService.GetCurrentLocationAsync();

            if (result is null)
            {
                Latitude = "-";
                Longitude = "-";
                Accuracy = "-";
                StatusMessage = "No fue posible obtener la ubicación. Revisa permisos y GPS.";
                return;
            }

            Latitude = result.Latitude.ToString("F6", CultureInfo.InvariantCulture);
            Longitude = result.Longitude.ToString("F6", CultureInfo.InvariantCulture);
            Accuracy = $"{result.AccuracyMeters:F0} m";
            StatusMessage = $"Actualizado {result.Timestamp.LocalDateTime:dd/MM/yyyy HH:mm}";
        }
        catch (FeatureNotSupportedException)
        {
            StatusMessage = "La geolocalización no está soportada en este dispositivo.";
        }
        catch (FeatureNotEnabledException)
        {
            StatusMessage = "Activa los servicios de ubicación para continuar.";
        }
        catch (PermissionException)
        {
            StatusMessage = "La aplicación no tiene permisos de ubicación.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
