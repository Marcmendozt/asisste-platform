using Asisste.Data.Absences;
using Asisste.Services.Abstractions.Authentication;
using Asisste.Services.Abstractions.Absences;
using Asisste.Services.Abstractions.Attendance;
using Asisste.Services.Abstractions.Device;
using Asisste.Services.Abstractions.Persistence;
using Asisste.Data.Authentication;
using Asisste.Data.Attendance;
using Asisste.Data.Communication;
using Asisste.Data.Data;
using Asisste.Data.Device;
using Microsoft.Extensions.DependencyInjection;

namespace Asisste.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<LegacyApiClient>();
        services.AddSingleton<MobileAuthApiClient>();
        services.AddSingleton<IAuthenticationService, LegacyAuthenticationService>();
        services.AddSingleton<IAttendanceService, LegacyAttendanceService>();
        services.AddSingleton<IAbsenceService, LegacyAbsenceService>();
        services.AddSingleton<IItemRepository, InMemoryItemRepository>();
        services.AddSingleton<IBrowserService, BrowserService>();
        services.AddSingleton<ILocationService, LocationService>();

        return services;
    }
}
