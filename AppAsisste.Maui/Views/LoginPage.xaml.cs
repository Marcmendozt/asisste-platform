using AppAsisste.Maui.ViewModels;

namespace AppAsisste.Maui.Views;

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
