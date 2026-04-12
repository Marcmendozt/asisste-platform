using Assiste.Presentation.Maui.Features.About.ViewModels;

namespace Assiste.Presentation.Maui.Features.About.Pages;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
