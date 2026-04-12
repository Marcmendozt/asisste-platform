using Asisste.MAUI.Features.Location.ViewModels;

namespace Asisste.MAUI.Features.Location.Pages;

public partial class UbicacionPage : ContentPage
{
    public UbicacionPage(UbicacionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
