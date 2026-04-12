namespace Assiste.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    DemoCredentials DemoCredentials { get; }

    Task<bool> SignInAsync(string email, string password, CancellationToken cancellationToken = default);
}

public sealed record DemoCredentials(string Email, string Password);