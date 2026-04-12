using Asisste.Domain.Entities;

namespace Asisste.API.Contracts.Responses;

public sealed class LegacyWsLoginResponse
{
    public int ID_Usuario { get; init; }

    public string Nombre { get; init; } = string.Empty;

    public string HoraEntrada { get; init; } = string.Empty;

    public int Tolerancia { get; init; }

    public int ID_Movil { get; init; }

    public bool SessionMovil { get; init; }

    public static LegacyWsLoginResponse From(MobileUserProfile? profile)
    {
        if (profile is null)
        {
            return new LegacyWsLoginResponse();
        }

        return new LegacyWsLoginResponse
        {
            ID_Usuario = profile.UserId,
            Nombre = profile.FullName,
            HoraEntrada = profile.EntryTime,
            Tolerancia = profile.ToleranceMinutes,
            ID_Movil = profile.MobileId,
            SessionMovil = profile.HasMobileSession
        };
    }
}