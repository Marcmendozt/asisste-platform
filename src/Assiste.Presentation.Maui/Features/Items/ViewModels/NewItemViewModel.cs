using Assiste.Application.Abstractions.Persistence;
using Assiste.Domain.Entities;
using Assiste.Presentation.Maui.Common.Navigation;
using Assiste.Presentation.Maui.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assiste.Presentation.Maui.Features.Items.ViewModels;

public partial class NewItemViewModel : ViewModelBase
{
    private readonly IItemRepository itemRepository;
    private readonly IAppNavigator navigator;

    public NewItemViewModel(IItemRepository itemRepository, IAppNavigator navigator)
    {
        this.itemRepository = itemRepository;
        this.navigator = navigator;
        Title = "Nuevo elemento";
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string titleInput = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string descriptionInput = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasValidationMessage))]
    private string validationMessage = string.Empty;

    public bool HasValidationMessage => !string.IsNullOrWhiteSpace(ValidationMessage);

    partial void OnTitleInputChanged(string value)
    {
        ValidationMessage = string.Empty;
    }

    partial void OnDescriptionInputChanged(string value)
    {
        ValidationMessage = string.Empty;
    }

    private bool CanSave()
    {
        return !IsBusy &&
               !string.IsNullOrWhiteSpace(TitleInput) &&
               !string.IsNullOrWhiteSpace(DescriptionInput);
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveAsync()
    {
        if (IsBusy)
        {
            return;
        }

        if (!CanSave())
        {
            ValidationMessage = "Completa el título y la descripción antes de guardar.";
            return;
        }

        IsBusy = true;
        SaveCommand.NotifyCanExecuteChanged();

        try
        {
            var item = new Item
            {
                Id = Guid.NewGuid().ToString("N"),
                Title = TitleInput.Trim(),
                Description = DescriptionInput.Trim()
            };

            await itemRepository.AddAsync(item);
            Reset();
            await navigator.PopAsync();
        }
        finally
        {
            IsBusy = false;
            SaveCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        Reset();
        await navigator.PopAsync();
    }

    public void Reset()
    {
        TitleInput = string.Empty;
        DescriptionInput = string.Empty;
        ValidationMessage = string.Empty;
        SaveCommand.NotifyCanExecuteChanged();
    }
}
