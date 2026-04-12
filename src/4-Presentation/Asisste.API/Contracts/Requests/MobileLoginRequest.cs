namespace Asisste.API.Contracts.Requests;

public sealed class MobileLoginRequest
{
    public string Username { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}