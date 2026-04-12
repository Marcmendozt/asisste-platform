using Assiste.Presentation.Maui.Features.Location.ViewModels;

namespace Assiste.Presentation.Maui.Features.Location.Pages;

public partial class UbicacionPage : ContentPage
{
    public UbicacionPage(UbicacionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
