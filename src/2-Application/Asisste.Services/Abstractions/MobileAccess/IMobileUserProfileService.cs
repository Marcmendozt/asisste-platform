using Asisste.Domain.Entities;

namespace Asisste.Services.Abstractions.MobileAccess;

public interface IMobileUserProfileService
{
    Task<MobileUserProfile?> GetProfileAsync(string username, string password, CancellationToken cancellationToken = default);
}