using AppAsisste.Maui.Models;
using AppAsisste.Maui.Services;
using AppAsisste.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AppAsisste.Maui.ViewModels;

public partial class ItemsViewModel : ViewModelBase
{
    private readonly IDataStore<Item> dataStore;
    private readonly IAppNavigator navigator;

    public ItemsViewModel(IDataStore<Item> dataStore, IAppNavigator navigator)
    {
        this.dataStore = dataStore;
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
            var items = await dataStore.GetItemsAsync();

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
