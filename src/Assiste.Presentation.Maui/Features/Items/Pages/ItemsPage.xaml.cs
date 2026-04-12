using Assiste.Presentation.Maui.Features.Items.ViewModels;

namespace Assiste.Presentation.Maui.Features.Items.Pages;

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
