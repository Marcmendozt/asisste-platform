using Asisste.MAUI.Common.ViewModels;
using Asisste.Services.Abstractions.Absences;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Asisste.MAUI.Features.Absences.ViewModels;

public partial class AbsencesViewModel : ViewModelBase
{
    private readonly IAbsenceService absenceService;
    private AbsenceAttachment? selectedAttachment;

    public AbsencesViewModel(IAbsenceService absenceService)
    {
        this.absenceService = absenceService;
        Title = "Faltas";
    }

    [ObservableProperty]
    private string fileName = "Ningún archivo seleccionado";

    [ObservableProperty]
    private string statusMessage = "Cargando estado de faltas...";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private bool alreadySubmitted;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasFeedbackMessage))]
    private string feedbackMessage = string.Empty;

    public bool HasFeedbackMessage => !string.IsNullOrWhiteSpace(FeedbackMessage);

    public Task LoadAsync()
    {
        return RefreshAsync();
    }

    public void SetAttachment(AbsenceAttachment attachment)
    {
        selectedAttachment = attachment;
        FileName = attachment.FileName;
        FeedbackMessage = string.Empty;
        SubmitCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;
        SubmitCommand.NotifyCanExecuteChanged();

        try
        {
            ApplySnapshot(await absenceService.GetTodaySnapshotAsync());
        }
        finally
        {
            IsBusy = false;
            SubmitCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private async Task SubmitAsync()
    {
        if (IsBusy)
        {
            return;
        }

        if (selectedAttachment is null)
        {
            FeedbackMessage = "Selecciona un archivo antes de enviarlo.";
            return;
        }

        IsBusy = true;
        SubmitCommand.NotifyCanExecuteChanged();

        try
        {
            var result = await absenceService.SubmitAsync(selectedAttachment);
            FeedbackMessage = result.Message;
            ApplySnapshot(result.Snapshot);

            if (result.Success)
            {
                selectedAttachment = null;
                FileName = "Ningún archivo seleccionado";
            }
        }
        finally
        {
            IsBusy = false;
            SubmitCommand.NotifyCanExecuteChanged();
        }
    }

    private bool CanSubmit()
    {
        return !IsBusy && !AlreadySubmitted && selectedAttachment is not null;
    }

    private void ApplySnapshot(AbsenceSnapshot snapshot)
    {
        AlreadySubmitted = snapshot.AlreadySubmittedToday;
        StatusMessage = snapshot.StatusMessage;
        SubmitCommand.NotifyCanExecuteChanged();
    }
}