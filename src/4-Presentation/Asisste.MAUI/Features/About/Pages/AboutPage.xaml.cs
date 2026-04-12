using Asisste.MAUI.Features.About.ViewModels;

namespace Asisste.MAUI.Features.About.Pages;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
