using Asisste.ApiData.Configuration;
using Asisste.ApiData.MobileAccess;
using Asisste.Services.Abstractions.MobileAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Asisste.ApiData;

public static class DependencyInjection
{
    public static IServiceCollection AddApiData(this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "No se configuró ConnectionStrings:LegacyAssiste para Asisste.API.");
        }

        services.AddSingleton(new LegacySqlOptions
        {
            ConnectionString = connectionString
        });

        services.AddScoped<IMobileUserProfileService, SqlMobileUserProfileService>();

        return services;
    }
}