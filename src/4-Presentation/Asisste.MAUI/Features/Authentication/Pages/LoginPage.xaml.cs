using Asisste.MAUI.Features.Authentication.ViewModels;

namespace Asisste.MAUI.Features.Authentication.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is LoginViewModel viewModel)
        {
            viewModel.Reset();
        }
    }
}
