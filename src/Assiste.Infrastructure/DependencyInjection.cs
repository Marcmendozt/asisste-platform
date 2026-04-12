using Assiste.Application.Abstractions.Device;
using Assiste.Application.Abstractions.Persistence;
using Assiste.Infrastructure.Data;
using Assiste.Infrastructure.Device;
using Microsoft.Extensions.DependencyInjection;

namespace Assiste.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IItemRepository, InMemoryItemRepository>();
        services.AddSingleton<IBrowserService, BrowserService>();
        services.AddSingleton<ILocationService, LocationService>();

        return services;
    }
}
