using AppAsisste.Maui.ViewModels;

namespace AppAsisste.Maui.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
