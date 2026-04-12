using Asisste.Services.Abstractions.Persistence;
using Asisste.MAUI.Common.Navigation;
using Asisste.MAUI.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Asisste.MAUI.Features.Items.ViewModels;

public partial class ItemDetailViewModel : ViewModelBase, IPageInitializer<string>
{
    private readonly IItemRepository itemRepository;

    public ItemDetailViewModel(IItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
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

        var item = await itemRepository.GetByIdAsync(parameter);

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
