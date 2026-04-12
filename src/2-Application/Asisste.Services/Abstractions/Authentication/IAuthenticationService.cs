using Asisste.Domain.Entities;

namespace Asisste.Services.Abstractions.Authentication;

public interface IAuthenticationService
{
    UserSession? CurrentSession { get; }

    Task<SignInResult> SignInAsync(string username, string password, CancellationToken cancellationToken = default);

    Task<SignInResult> SignInLocalAsync(CancellationToken cancellationToken = default);

    Task SignOutAsync(CancellationToken cancellationToken = default);
}

public sealed record SignInResult(
    bool Success,
    string Message,
    UserSession? Session = null);