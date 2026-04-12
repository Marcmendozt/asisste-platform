using AppAsisste.Maui.Models;
using AppAsisste.Maui.Services;
using AppAsisste.Maui.ViewModels;
using AppAsisste.Maui.Views;

namespace AppAsisste.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>();

        builder.Services
            .AddSingleton<AppShell>()
            .AddSingleton<IAppNavigator, AppNavigator>()
            .AddSingleton<IDataStore<Item>, InMemoryItemDataStore>()
            .AddSingleton<IBrowserService, BrowserService>()
            .AddSingleton<ILocationService, LocationService>()
            .AddTransient<LoginViewModel>()
            .AddTransient<ItemsViewModel>()
            .AddTransient<AboutViewModel>()
            .AddTransient<NewItemViewModel>()
            .AddTransient<ItemDetailViewModel>()
            .AddTransient<UbicacionViewModel>()
            .AddTransient<LoginPage>()
            .AddTransient<ItemsPage>()
            .AddTransient<AboutPage>()
            .AddTransient<NewItemPage>()
            .AddTransient<ItemDetailPage>()
            .AddTransient<UbicacionPage>();

        return builder.Build();
    }
}
