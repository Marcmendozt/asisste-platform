using Asisste.Services.Abstractions.Authentication;
using Asisste.MAUI.Common.Navigation;
using Asisste.MAUI.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Asisste.MAUI.Features.Authentication.ViewModels;

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
    private string username = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SignInCommand))]
    private string password = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasValidationMessage))]
    private string validationMessage = string.Empty;

    public bool HasValidationMessage => !string.IsNullOrWhiteSpace(ValidationMessage);

    public void Reset()
    {
        Username = string.Empty;
        Password = string.Empty;
        ValidationMessage = string.Empty;
        SignInCommand.NotifyCanExecuteChanged();
        SignInLocalCommand.NotifyCanExecuteChanged();
    }

    partial void OnUsernameChanged(string value)
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
               !string.IsNullOrWhiteSpace(Username) &&
               !string.IsNullOrWhiteSpace(Password);
    }

    private bool CanSignInLocal()
    {
        return !IsBusy;
    }

    [RelayCommand(CanExecute = nameof(CanSignIn))]
    private async Task SignInAsync()
    {
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;
        SignInCommand.NotifyCanExecuteChanged();
        SignInLocalCommand.NotifyCanExecuteChanged();

        try
        {
            var result = await authenticationService.SignInAsync(Username, Password);

            if (!result.Success)
            {
                ValidationMessage = result.Message;
                return;
            }

            ValidationMessage = string.Empty;
            await navigator.ShowMainShellAsync();
        }
        finally
        {
            IsBusy = false;
            SignInCommand.NotifyCanExecuteChanged();
            SignInLocalCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand(CanExecute = nameof(CanSignInLocal))]
    private async Task SignInLocalAsync()
    {
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;
        ValidationMessage = string.Empty;
        SignInCommand.NotifyCanExecuteChanged();
        SignInLocalCommand.NotifyCanExecuteChanged();

        try
        {
            var result = await authenticationService.SignInLocalAsync();

            if (!result.Success)
            {
                ValidationMessage = result.Message;
                return;
            }

            await navigator.ShowMainShellAsync();
        }
        finally
        {
            IsBusy = false;
            SignInCommand.NotifyCanExecuteChanged();
            SignInLocalCommand.NotifyCanExecuteChanged();
        }
    }
}
