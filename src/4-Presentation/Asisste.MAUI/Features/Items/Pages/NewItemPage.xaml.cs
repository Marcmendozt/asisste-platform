using Asisste.MAUI.Features.Items.ViewModels;

namespace Asisste.MAUI.Features.Items.Pages;

public partial class NewItemPage : ContentPage
{
    public NewItemPage(NewItemViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
