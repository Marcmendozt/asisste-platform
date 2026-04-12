using Assiste.Application.Abstractions.Authentication;
using Assiste.Presentation.Maui.Common.Navigation;
using Assiste.Presentation.Maui.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assiste.Presentation.Maui.Features.Authentication.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly IAuthenticationService authenticationService;
    private readonly IAppNavigator navigator;

    public LoginViewModel(IAppNavigator navigator, IAuthenticationService authenticationService)
    {
        this.navigator = navigator;
        this.authenticationService = authenticationService;
        Title = "Acceso";

        Reset();
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SignInCommand))]
    private string email = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SignInCommand))]
    private string password = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasValidationMessage))]
    private string validationMessage = string.Empty;

    public bool HasValidationMessage => !string.IsNullOrWhiteSpace(ValidationMessage);

    public void Reset()
    {
        Email = authenticationService.DemoCredentials.Email;
        Password = authenticationService.DemoCredentials.Password;
        ValidationMessage = string.Empty;
        SignInCommand.NotifyCanExecuteChanged();
    }

    partial void OnEmailChanged(string value)
    {
        ValidationMessage = string.Empty;
    }

    partial void OnPasswordChanged(string value)
    {
        ValidationMessage = string.Empty;
    }

    private bool CanSignIn()
    {
        return !IsBusy &&
               !string.IsNullOrWhiteSpace(Email) &&
               !string.IsNullOrWhiteSpace(Password);
    }

    [RelayCommand(CanExecute = nameof(CanSignIn))]
    private async Task SignInAsync()
    {
        if (IsBusy)
        {
            return;
        }

        if (!Email.Contains('@', StringComparison.Ordinal))
        {
            ValidationMessage = "Ingresa un correo electrónico válido.";
            return;
        }

        IsBusy = true;
        SignInCommand.NotifyCanExecuteChanged();

        try
        {
            var isAuthenticated = await authenticationService.SignInAsync(Email, Password);

            if (!isAuthenticated)
            {
                ValidationMessage = "Credenciales incorrectas. Usa el acceso demo configurado.";
                return;
            }

            await navigator.ShowMainShellAsync();
        }
        finally
        {
            IsBusy = false;
            SignInCommand.NotifyCanExecuteChanged();
        }
    }
}
