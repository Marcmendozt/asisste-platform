namespace Asisste.Domain.Entities;

public sealed record UserSession(
    int UserId,
    string Username,
    string DisplayName,
    int StatusId,
    bool MobileSessionEnabled,
    string AttendanceUsername,
    bool IsLocalMode = false);