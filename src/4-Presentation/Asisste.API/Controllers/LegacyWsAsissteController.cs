using Asisste.API.Contracts.Responses;
using Asisste.Services.Abstractions.MobileAccess;
using Microsoft.AspNetCore.Mvc;

namespace Asisste.API.Controllers;

[ApiController]
[Route("WSAsisste.asmx")]
public sealed class LegacyWsAsissteController : ControllerBase
{
    private readonly IMobileUserProfileService mobileUserProfileService;

    public LegacyWsAsissteController(IMobileUserProfileService mobileUserProfileService)
    {
        this.mobileUserProfileService = mobileUserProfileService;
    }

    [HttpGet("WSLogin")]
    [ProducesResponseType<LegacyWsLoginResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<LegacyWsLoginResponse>> WSLogin(
        [FromQuery] string? Usuario,
        [FromQuery] string? Clave,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Clave))
        {
            return Ok(new LegacyWsLoginResponse());
        }

        var profile = await mobileUserProfileService.GetProfileAsync(Usuario, Clave, cancellationToken);
        return Ok(LegacyWsLoginResponse.From(profile));
    }
}