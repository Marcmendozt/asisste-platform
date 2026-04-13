namespace Asisste.Services.Profiles;

public sealed record ProfileDto(
    int Id,
    string Description,
    bool IsActive);