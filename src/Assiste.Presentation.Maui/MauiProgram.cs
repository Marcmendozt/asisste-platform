using Assiste.Infrastructure;

namespace Assiste.Presentation.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>();

        builder.Services
            .AddPresentation()
            .AddInfrastructure();

        return builder.Build();
    }
}
