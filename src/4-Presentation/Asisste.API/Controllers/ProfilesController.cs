using Asisste.Services.Abstractions.Profiles;
using Asisste.Services.Common.Exceptions;
using Asisste.Services.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace Asisste.API.Controllers;

[ApiController]
[Route("api/profiles")]
public sealed class ProfilesController : ControllerBase
{
    private readonly IProfileService profileService;

    public ProfilesController(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    [HttpGet]
    [ProducesResponseType<IReadOnlyList<ProfileDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProfileDto>>> GetProfiles(CancellationToken cancellationToken)
    {
        var profiles = await profileService.ListAsync(cancellationToken);
        return Ok(profiles);
    }

    [HttpGet("{profileId:int}")]
    [ProducesResponseType<ProfileDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProfileDto>> GetProfileById(int profileId, CancellationToken cancellationToken)
    {
        try
        {
            var profile = await profileService.GetByIdAsync(profileId, cancellationToken);
            return Ok(profile);
        }
        catch (Exception exception)
        {
            return MapException(exception);
        }
    }

    [HttpPost]
    [ProducesResponseType<ProfileDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProfileDto>> CreateProfile(
        [FromBody] CreateProfileDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var createdProfile = await profileService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetProfileById), new { profileId = createdProfile.Id }, createdProfile);
        }
        catch (Exception exception)
        {
            return MapException(exception);
        }
    }

    [HttpPut("{profileId:int}")]
    [ProducesResponseType<ProfileDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProfileDto>> UpdateProfile(
        int profileId,
        [FromBody] UpdateProfileDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var updatedProfile = await profileService.UpdateAsync(profileId, request, cancellationToken);
            return Ok(updatedProfile);
        }
        catch (Exception exception)
        {
            return MapException(exception);
        }
    }

    [HttpDelete("{profileId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProfile(int profileId, CancellationToken cancellationToken)
    {
        try
        {
            await profileService.DeleteAsync(profileId, cancellationToken);
            return NoContent();
        }
        catch (Exception exception)
        {
            return MapException(exception);
        }
    }

    private ActionResult MapException(Exception exception)
    {
        return exception switch
        {
            ApplicationValidationException validationException => BadRequest(new
            {
                message = validationException.Message
            }),
            ConflictException conflictException => Conflict(new
            {
                message = conflictException.Message
            }),
            NotFoundException notFoundException => NotFound(new
            {
                message = notFoundException.Message
            }),
            _ => Problem(title: "No fue posible procesar la solicitud.", detail: exception.Message)
        };
    }
}