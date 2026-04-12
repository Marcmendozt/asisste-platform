using Assiste.Application.Abstractions.Device;
using Assiste.Presentation.Maui.Common.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace Assiste.Presentation.Maui.Features.About.ViewModels;

public partial class AboutViewModel : ViewModelBase
{
    private readonly IBrowserService browserService;

    public AboutViewModel(IBrowserService browserService)
    {
        this.browserService = browserService;
        Title = "Acerca de";
    }

    public string Headline => "Assiste sobre .NET MAUI";

    public string VersionText => ".NET 10 | Single Project | CommunityToolkit.Mvvm";

    public string Summary => "La aplicación fue reestructurada para usar Shell, inyección de dependencias nativa y bindings compilados en todas las vistas.";

    public string Details => "DependencyService y Xamarin.Essentials se sustituyeron por servicios inyectados y por las APIs modernas de Microsoft.Maui.ApplicationModel y Microsoft.Maui.Devices.Sensors.";

    [RelayCommand]
    private Task OpenDocumentationAsync()
    {
        return browserService.OpenAsync("https://learn.microsoft.com/dotnet/maui/");
    }
}
