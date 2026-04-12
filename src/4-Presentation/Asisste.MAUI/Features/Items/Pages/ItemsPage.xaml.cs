using Asisste.MAUI.Features.Items.ViewModels;

namespace Asisste.MAUI.Features.Items.Pages;

public partial class ItemsPage : ContentPage
{
    public ItemsPage(ItemsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ItemsViewModel viewModel)
        {
            await viewModel.LoadItemsCommand.ExecuteAsync(null);
        }
    }
}
