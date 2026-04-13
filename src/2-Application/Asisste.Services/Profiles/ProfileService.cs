using Asisste.Domain.Entities;
using Asisste.Domain.Repositories;
using Asisste.Services.Abstractions.Profiles;
using Asisste.Services.Common.Exceptions;

namespace Asisste.Services.Profiles;

public sealed class ProfileService : IProfileService
{
    private const int MinimumDescriptionLength = 4;

    private readonly IProfileRepository profileRepository;

    public ProfileService(IProfileRepository profileRepository)
    {
        this.profileRepository = profileRepository;
    }

    public async Task<IReadOnlyList<ProfileDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        var profiles = await profileRepository.ListAsync(cancellationToken);
        return profiles
            .OrderBy(profile => profile.Description)
            .Select(Map)
            .ToArray();
    }

    public async Task<ProfileDto> GetByIdAsync(int profileId, CancellationToken cancellationToken = default)
    {
        var profile = await profileRepository.GetByIdAsync(profileId, cancellationToken);
        if (profile is null)
        {
            throw new NotFoundException("El perfil solicitado no existe.");
        }

        return Map(profile);
    }

    public async Task<ProfileDto> CreateAsync(CreateProfileDto request, CancellationToken cancellationToken = default)
    {
        var normalizedDescription = NormalizeDescription(request.Description);

        var createdProfile = await profileRepository.CreateAsync(
            new Profile(0, normalizedDescription, true),
            cancellationToken);

        if (createdProfile is null)
        {
            throw new ConflictException("EL PERFIL YA EXISTE.");
        }

        return Map(createdProfile);
    }

    public async Task<ProfileDto> UpdateAsync(
        int profileId,
        UpdateProfileDto request,
        CancellationToken cancellationToken = default)
    {
        var currentProfile = await profileRepository.GetByIdAsync(profileId, cancellationToken);
        if (currentProfile is null)
        {
            throw new NotFoundException("El perfil que desea actualizar no existe.");
        }

        var normalizedDescription = NormalizeDescription(request.Description);
        var profileWithSameDescription = await profileRepository.GetByDescriptionAsync(normalizedDescription, cancellationToken);
        if (profileWithSameDescription is not null && profileWithSameDescription.Id != profileId)
        {
            throw new ConflictException("EL PERFIL YA EXISTE.");
        }

        var updatedProfile = currentProfile with
        {
            Description = normalizedDescription
        };

        var wasUpdated = await profileRepository.UpdateAsync(updatedProfile, cancellationToken);
        if (!wasUpdated)
        {
            throw new NotFoundException("No fue posible actualizar el perfil solicitado.");
        }

        return Map(updatedProfile);
    }

    public async Task DeleteAsync(int profileId, CancellationToken cancellationToken = default)
    {
        var existingProfile = await profileRepository.GetByIdAsync(profileId, cancellationToken);
        if (existingProfile is null)
        {
            throw new NotFoundException("El perfil que desea eliminar no existe.");
        }

        var wasDeleted = await profileRepository.DeleteAsync(profileId, cancellationToken);
        if (!wasDeleted)
        {
            throw new NotFoundException("No fue posible eliminar el perfil solicitado.");
        }
    }

    private static ProfileDto Map(Profile profile)
    {
        return new ProfileDto(profile.Id, profile.Description, profile.IsActive);
    }

    private static string NormalizeDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ApplicationValidationException("La descripción del perfil es obligatoria.");
        }

        var normalizedDescription = description.Trim();
        if (normalizedDescription.Length < MinimumDescriptionLength)
        {
            throw new ApplicationValidationException(
                $"La descripción del perfil debe tener al menos {MinimumDescriptionLength} caracteres.");
        }

        return normalizedDescription;
    }
}