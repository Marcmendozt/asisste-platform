using Asisste.Services.Abstractions.Authentication;
using Asisste.MAUI.Features.Absences.Pages;
using Asisste.MAUI.Features.Attendance.Pages;
using Asisste.MAUI.Common.Navigation;
using Asisste.MAUI.Features.Authentication.Pages;
using Asisste.MAUI.Features.Location.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Asisste.MAUI;

public partial class AppShell : Shell
{
    private readonly IServiceProvider serviceProvider;
    private readonly FlyoutItem loginItem;
    private readonly FlyoutItem attendanceItem;
    private readonly FlyoutItem absencesItem;
    private readonly FlyoutItem locationItem;

    public AppShell(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        this.serviceProvider = serviceProvider;

        loginItem = CreateFlyoutItem<LoginPage>("Acceso", "lock.svg", nameof(LoginPage));
        attendanceItem = CreateFlyoutItem<AttendancePage>("Asistencia", "feed.svg", nameof(AttendancePage));
        absencesItem = CreateFlyoutItem<AbsencesPage>("Faltas", "about.svg", nameof(AbsencesPage));
        locationItem = CreateFlyoutItem<UbicacionPage>("Ubicación", "location.svg", nameof(UbicacionPage));

        Shell.SetFlyoutItemIsVisible(loginItem, false);

        Items.Add(loginItem);
        Items.Add(attendanceItem);
        Items.Add(absencesItem);
        Items.Add(locationItem);

        var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
        SetAuthenticated(authenticationService.CurrentSession is not null);
    }

    public void SetAuthenticated(bool isAuthenticated)
    {
        FlyoutBehavior = isAuthenticated ? FlyoutBehavior.Flyout : FlyoutBehavior.Disabled;
        Shell.SetFlyoutItemIsVisible(LogoutMenuItem, isAuthenticated);

        loginItem.IsVisible = !isAuthenticated;
        attendanceItem.IsVisible = isAuthenticated;
        absencesItem.IsVisible = isAuthenticated;
        locationItem.IsVisible = isAuthenticated;

        CurrentItem = isAuthenticated ? attendanceItem : loginItem;
    }

    private FlyoutItem CreateFlyoutItem<TPage>(string title, string icon, string route) where TPage : Page
    {
        return new FlyoutItem
        {
            Title = title,
            Route = route,
            Icon = icon,
            Items =
            {
                new ShellContent
                {
                    Route = route,
                    ContentTemplate = new DataTemplate(() => serviceProvider.GetRequiredService<TPage>())
                }
            }
        };
    }

    private async void OnLogoutClicked(object? sender, EventArgs e)
    {
        var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
        var navigator = serviceProvider.GetRequiredService<IAppNavigator>();
        await authenticationService.SignOutAsync();
        await navigator.ShowLoginAsync();
    }
}
