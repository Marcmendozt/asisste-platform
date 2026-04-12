using AppAsisste.Maui.ViewModels;

namespace AppAsisste.Maui.Views;

public partial class NewItemPage : ContentPage
{
    public NewItemPage(NewItemViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
