using AppAsisste.Maui.Services;
using AppAsisste.Maui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AppAsisste.Maui;

public partial class AppShell : Shell
{
    private readonly IServiceProvider serviceProvider;
    private readonly FlyoutItem loginItem;
    private readonly FlyoutItem itemsItem;
    private readonly FlyoutItem locationItem;
    private readonly FlyoutItem aboutItem;

    public AppShell(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        this.serviceProvider = serviceProvider;

        loginItem = CreateFlyoutItem<LoginPage>("Acceso", "lock.svg", nameof(LoginPage));
        itemsItem = CreateFlyoutItem<ItemsPage>("Explorar", "feed.svg", nameof(ItemsPage));
        locationItem = CreateFlyoutItem<UbicacionPage>("Ubicación", "location.svg", nameof(UbicacionPage));
        aboutItem = CreateFlyoutItem<AboutPage>("Acerca de", "about.svg", nameof(AboutPage));

        Shell.SetFlyoutItemIsVisible(loginItem, false);

        Items.Add(loginItem);
        Items.Add(itemsItem);
        Items.Add(locationItem);
        Items.Add(aboutItem);

        SetAuthenticated(false);
    }

    public void SetAuthenticated(bool isAuthenticated)
    {
        FlyoutBehavior = isAuthenticated ? FlyoutBehavior.Flyout : FlyoutBehavior.Disabled;
        Shell.SetFlyoutItemIsVisible(LogoutMenuItem, isAuthenticated);

        loginItem.IsVisible = !isAuthenticated;
        itemsItem.IsVisible = isAuthenticated;
        locationItem.IsVisible = isAuthenticated;
        aboutItem.IsVisible = isAuthenticated;

        CurrentItem = isAuthenticated ? itemsItem : loginItem;
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
        var navigator = serviceProvider.GetRequiredService<IAppNavigator>();
        await navigator.ShowLoginAsync();
    }
}
