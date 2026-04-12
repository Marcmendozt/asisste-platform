using AppAsisste.Maui.Models;

namespace AppAsisste.Maui.Services;

public sealed class InMemoryItemDataStore : IDataStore<Item>
{
    private readonly List<Item> items =
    [
        new Item
        {
            Id = Guid.NewGuid().ToString("N"),
            Title = "Primer caso",
            Description = "Registro inicial migrado a .NET MAUI con inyección de dependencias."
        },
        new Item
        {
            Id = Guid.NewGuid().ToString("N"),
            Title = "Seguimiento",
            Description = "Elemento de ejemplo para validar CollectionView y compiled bindings."
        },
        new Item
        {
            Id = Guid.NewGuid().ToString("N"),
            Title = "Ubicación",
            Description = "Caso listo para integrarse con Microsoft.Maui.Devices.Sensors.Geolocation."
        }
    ];

    public Task<bool> AddItemAsync(Item item)
    {
        items.Add(item);
        return Task.FromResult(true);
    }

    public Task<bool> UpdateItemAsync(Item item)
    {
        var index = items.FindIndex(current => current.Id == item.Id);

        if (index < 0)
        {
            return Task.FromResult(false);
        }

        items[index] = item;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteItemAsync(string id)
    {
        var item = items.FirstOrDefault(current => current.Id == id);

        if (item is null)
        {
            return Task.FromResult(false);
        }

        items.Remove(item);
        return Task.FromResult(true);
    }

    public Task<Item?> GetItemAsync(string id)
    {
        var item = items.FirstOrDefault(current => current.Id == id);
        return Task.FromResult(item);
    }

    public Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
    {
        return Task.FromResult<IEnumerable<Item>>(items);
    }
}
