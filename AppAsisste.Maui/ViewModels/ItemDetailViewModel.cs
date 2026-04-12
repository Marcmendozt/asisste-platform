using AppAsisste.Maui.Models;
using AppAsisste.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppAsisste.Maui.ViewModels;

public partial class ItemDetailViewModel : ViewModelBase, IPageInitializer<string>
{
    private readonly IDataStore<Item> dataStore;

    public ItemDetailViewModel(IDataStore<Item> dataStore)
    {
        this.dataStore = dataStore;
        Title = "Detalle";
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Identifier))]
    private string itemId = string.Empty;

    [ObservableProperty]
    private string heading = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    public string Identifier => string.IsNullOrWhiteSpace(ItemId) ? string.Empty : $"Id: {ItemId}";

    public async Task InitializeAsync(string parameter)
    {
        ItemId = parameter;

        var item = await dataStore.GetItemAsync(parameter);

        if (item is null)
        {
            Heading = "Elemento no encontrado";
            Description = "No fue posible recuperar la información solicitada.";
            return;
        }

        Title = item.Title;
        Heading = item.Title;
        Description = item.Description;
    }
}
