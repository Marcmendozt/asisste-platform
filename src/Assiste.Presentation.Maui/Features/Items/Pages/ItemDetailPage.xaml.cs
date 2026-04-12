using Assiste.Presentation.Maui.Features.Items.ViewModels;

namespace Assiste.Presentation.Maui.Features.Items.Pages;

public partial class ItemDetailPage : ContentPage
{
    public ItemDetailPage(ItemDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
