using Asisste.MAUI.Features.Items.ViewModels;

namespace Asisste.MAUI.Features.Items.Pages;

public partial class ItemDetailPage : ContentPage
{
    public ItemDetailPage(ItemDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
