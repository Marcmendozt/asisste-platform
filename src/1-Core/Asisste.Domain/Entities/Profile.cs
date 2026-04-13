namespace Asisste.Domain.Entities;

public sealed record Profile(
    int Id,
    string Description,
    bool IsActive);