namespace Asisste.Domain.Entities;

public sealed record MobileUserProfile(
    int UserId,
    string FullName,
    string EntryTime,
    int ToleranceMinutes,
    int MobileId,
    bool HasMobileSession);