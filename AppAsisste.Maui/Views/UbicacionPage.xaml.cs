using AppAsisste.Maui.ViewModels;

namespace AppAsisste.Maui.Views;

public partial class UbicacionPage : ContentPage
{
    public UbicacionPage(UbicacionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
