using Asisste.Domain.Entities;
using Xunit;

namespace Asisste.Domain.Tests;

public sealed class ItemTests
{
    [Fact]
    public void Item_ShouldStoreAssignedValues()
    {
        var item = new Item
        {
            Id = "item-1",
            Title = "Marcacion",
            Description = "Entidad base de ejemplo"
        };

        Assert.Equal("item-1", item.Id);
        Assert.Equal("Marcacion", item.Title);
        Assert.Equal("Entidad base de ejemplo", item.Description);
    }
}