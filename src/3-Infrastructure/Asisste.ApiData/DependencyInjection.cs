using Asisste.ApiData.Configuration;
using Asisste.ApiData.MobileAccess;
using Asisste.ApiData.Persistence;
using Asisste.ApiData.Profiles;
using Asisste.Domain.Repositories;
using Asisste.Services.Abstractions.MobileAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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

        services.AddDbContext<LegacyAsissteDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IMobileUserProfileService, SqlMobileUserProfileService>();
        services.AddScoped<IProfileRepository, EfProfileRepository>();

        return services;
    }
}