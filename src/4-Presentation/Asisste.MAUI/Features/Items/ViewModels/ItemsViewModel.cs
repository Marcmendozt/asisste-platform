using Asisste.Services.Abstractions.Persistence;
using Asisste.Domain.Entities;
using Asisste.MAUI.Common.Navigation;
using Asisste.MAUI.Common.ViewModels;
using Asisste.MAUI.Features.Items.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Asisste.MAUI.Features.Items.ViewModels;

public partial class ItemsViewModel : ViewModelBase
{
    private readonly IItemRepository itemRepository;
    private readonly IAppNavigator navigator;

    public ItemsViewModel(IItemRepository itemRepository, IAppNavigator navigator)
    {
        this.itemRepository = itemRepository;
        this.navigator = navigator;
        Title = "Explorar";
    }

    public ObservableCollection<Item> Items { get; } = [];

    [ObservableProperty]
    private Item? selectedItem;

    partial void OnSelectedItemChanged(Item? value)
    {
        if (value is null)
        {
            return;
        }

        OpenItemCommand.Execute(value);
    }

    [RelayCommand]
    private async Task LoadItemsAsync()
    {
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;

        try
        {
            Items.Clear();
            var items = await itemRepository.ListAsync();

            foreach (var item in items.OrderBy(current => current.Title, StringComparer.OrdinalIgnoreCase))
            {
                Items.Add(item);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private Task AddItemAsync()
    {
        return navigator.PushAsync<NewItemPage>();
    }

    [RelayCommand]
    private async Task OpenItemAsync(Item? item)
    {
        if (item is null)
        {
            return;
        }

        await navigator.PushAsync<ItemDetailPage, string>(item.Id);
        SelectedItem = null;
    }
}
