using Asisste.MAUI.Features.Absences.Pages;
using Asisste.MAUI.Features.Absences.ViewModels;
using Asisste.MAUI.Features.Attendance.Pages;
using Asisste.MAUI.Features.Attendance.ViewModels;
using Asisste.MAUI.Common.Navigation;
using Asisste.MAUI.Features.About.Pages;
using Asisste.MAUI.Features.About.ViewModels;
using Asisste.MAUI.Features.Authentication.Pages;
using Asisste.MAUI.Features.Authentication.ViewModels;
using Asisste.MAUI.Features.Items.Pages;
using Asisste.MAUI.Features.Items.ViewModels;
using Asisste.MAUI.Features.Location.Pages;
using Asisste.MAUI.Features.Location.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Asisste.MAUI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<AppShell>();
        services.AddSingleton<IAppNavigator, AppNavigator>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<AttendanceViewModel>();
        services.AddTransient<AbsencesViewModel>();
        services.AddTransient<ItemsViewModel>();
        services.AddTransient<AboutViewModel>();
        services.AddTransient<NewItemViewModel>();
        services.AddTransient<ItemDetailViewModel>();
        services.AddTransient<UbicacionViewModel>();

        services.AddTransient<LoginPage>();
    services.AddTransient<AttendancePage>();
    services.AddTransient<AbsencesPage>();
        services.AddTransient<ItemsPage>();
        services.AddTransient<AboutPage>();
        services.AddTransient<NewItemPage>();
        services.AddTransient<ItemDetailPage>();
        services.AddTransient<UbicacionPage>();

        return services;
    }
}
