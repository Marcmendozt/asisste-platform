using Assiste.Presentation.Maui.Common.Navigation;
using Assiste.Presentation.Maui.Features.About.Pages;
using Assiste.Presentation.Maui.Features.About.ViewModels;
using Assiste.Presentation.Maui.Features.Authentication.Pages;
using Assiste.Presentation.Maui.Features.Authentication.ViewModels;
using Assiste.Presentation.Maui.Features.Items.Pages;
using Assiste.Presentation.Maui.Features.Items.ViewModels;
using Assiste.Presentation.Maui.Features.Location.Pages;
using Assiste.Presentation.Maui.Features.Location.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Assiste.Presentation.Maui;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<AppShell>();
        services.AddSingleton<IAppNavigator, AppNavigator>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<ItemsViewModel>();
        services.AddTransient<AboutViewModel>();
        services.AddTransient<NewItemViewModel>();
        services.AddTransient<ItemDetailViewModel>();
        services.AddTransient<UbicacionViewModel>();

        services.AddTransient<LoginPage>();
        services.AddTransient<ItemsPage>();
        services.AddTransient<AboutPage>();
        services.AddTransient<NewItemPage>();
        services.AddTransient<ItemDetailPage>();
        services.AddTransient<UbicacionPage>();

        return services;
    }
}
