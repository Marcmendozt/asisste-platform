using Asisste.Domain.Entities;

namespace Asisste.API.Contracts.Responses;

public sealed record MobileLoginResponse(
    int UserId,
    string FullName,
    string EntryTime,
    int ToleranceMinutes,
    int MobileId,
    bool HasMobileSession)
{
    public static MobileLoginResponse From(MobileUserProfile profile)
    {
        return new MobileLoginResponse(
            profile.UserId,
            profile.FullName,
            profile.EntryTime,
            profile.ToleranceMinutes,
            profile.MobileId,
            profile.HasMobileSession);
    }
}