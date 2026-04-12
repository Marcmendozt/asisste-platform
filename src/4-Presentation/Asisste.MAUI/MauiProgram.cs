using Asisste.Data;

namespace Asisste.MAUI;

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
