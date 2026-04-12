using Assiste.Presentation.Maui.Features.Items.ViewModels;

namespace Assiste.Presentation.Maui.Features.Items.Pages;

public partial class NewItemPage : ContentPage
{
    public NewItemPage(NewItemViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
