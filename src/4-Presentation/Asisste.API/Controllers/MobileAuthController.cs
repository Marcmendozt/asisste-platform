using Asisste.API.Contracts.Requests;
using Asisste.API.Contracts.Responses;
using Asisste.Services.Abstractions.MobileAccess;
using Microsoft.AspNetCore.Mvc;

namespace Asisste.API.Controllers;

[ApiController]
[Route("api/mobile/auth")]
public sealed class MobileAuthController : ControllerBase
{
    private readonly IMobileUserProfileService mobileUserProfileService;

    public MobileAuthController(IMobileUserProfileService mobileUserProfileService)
    {
        this.mobileUserProfileService = mobileUserProfileService;
    }

    [HttpPost("login")]
    [ProducesResponseType<MobileLoginResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<MobileLoginResponse>> Login(
        [FromBody] MobileLoginRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new
            {
                message = "Usuario y clave son obligatorios."
            });
        }

        var profile = await mobileUserProfileService.GetProfileAsync(
            request.Username,
            request.Password,
            cancellationToken);

        if (profile is null)
        {
            return Unauthorized(new
            {
                message = "Credenciales inválidas."
            });
        }

        return Ok(MobileLoginResponse.From(profile));
    }
}