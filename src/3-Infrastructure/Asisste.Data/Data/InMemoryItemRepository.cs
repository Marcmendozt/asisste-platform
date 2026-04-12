using Asisste.Services.Abstractions.Persistence;
using Asisste.Domain.Entities;

namespace Asisste.Data.Data;

public sealed class InMemoryItemRepository : IItemRepository
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

    public Task<bool> AddAsync(Item item)
    {
        items.Add(item);
        return Task.FromResult(true);
    }

    public Task<bool> UpdateAsync(Item item)
    {
        var index = items.FindIndex(current => current.Id == item.Id);

        if (index < 0)
        {
            return Task.FromResult(false);
        }

        items[index] = item;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(string id)
    {
        var item = items.FirstOrDefault(current => current.Id == id);

        if (item is null)
        {
            return Task.FromResult(false);
        }

        items.Remove(item);
        return Task.FromResult(true);
    }

    public Task<Item?> GetByIdAsync(string id)
    {
        var item = items.FirstOrDefault(current => current.Id == id);
        return Task.FromResult(item);
    }

    public Task<IReadOnlyCollection<Item>> ListAsync(bool forceRefresh = false)
    {
        return Task.FromResult<IReadOnlyCollection<Item>>(items.ToArray());
    }
}
