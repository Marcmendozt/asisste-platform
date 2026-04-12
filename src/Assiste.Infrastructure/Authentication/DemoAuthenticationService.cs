using Assiste.Application.Abstractions.Authentication;

namespace Assiste.Infrastructure.Authentication;

public sealed class DemoAuthenticationService : IAuthenticationService
{
    public DemoCredentials DemoCredentials { get; } = new("demo@assiste.app", "Assiste123!");

    public Task<bool> SignInAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var isValidEmail = string.Equals(email.Trim(), DemoCredentials.Email, StringComparison.OrdinalIgnoreCase);
        var isValidPassword = string.Equals(password, DemoCredentials.Password, StringComparison.Ordinal);

        return Task.FromResult(isValidEmail && isValidPassword);
    }
}