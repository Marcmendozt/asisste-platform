using Asisste.Domain.Entities;

namespace Asisste.Domain.Repositories;

public interface IProfileRepository
{
    Task<IReadOnlyList<Profile>> ListAsync(CancellationToken cancellationToken = default);

    Task<Profile?> GetByIdAsync(int profileId, CancellationToken cancellationToken = default);

    Task<Profile?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default);

    Task<Profile?> CreateAsync(Profile profile, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Profile profile, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int profileId, CancellationToken cancellationToken = default);
}