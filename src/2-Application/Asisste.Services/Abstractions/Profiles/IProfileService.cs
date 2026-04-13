using Asisste.Services.Profiles;

namespace Asisste.Services.Abstractions.Profiles;

public interface IProfileService
{
    Task<IReadOnlyList<ProfileDto>> ListAsync(CancellationToken cancellationToken = default);

    Task<ProfileDto> GetByIdAsync(int profileId, CancellationToken cancellationToken = default);

    Task<ProfileDto> CreateAsync(CreateProfileDto request, CancellationToken cancellationToken = default);

    Task<ProfileDto> UpdateAsync(int profileId, UpdateProfileDto request, CancellationToken cancellationToken = default);

    Task DeleteAsync(int profileId, CancellationToken cancellationToken = default);
}