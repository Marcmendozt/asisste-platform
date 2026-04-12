using Assiste.Domain.Entities;

namespace Assiste.Application.Abstractions.Persistence;

public interface IItemRepository
{
    Task<bool> AddAsync(Item item);

    Task<bool> UpdateAsync(Item item);

    Task<bool> DeleteAsync(string id);

    Task<Item?> GetByIdAsync(string id);

    Task<IReadOnlyCollection<Item>> ListAsync(bool forceRefresh = false);
}
