using Asisste.Services.Abstractions.Attendance;
using Asisste.Services.Abstractions.Device;
using Asisste.MAUI.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace Asisste.MAUI.Features.Attendance.ViewModels;

public partial class AttendanceViewModel : ViewModelBase
{
    private readonly IAttendanceService attendanceService;
    private readonly ILocationService locationService;

    public AttendanceViewModel(IAttendanceService attendanceService, ILocationService locationService)
    {
        this.attendanceService = attendanceService;
        this.locationService = locationService;
        Title = "Asistencia";
    }

    [ObservableProperty]
    private string userDisplayName = "Sin sesión";

    [ObservableProperty]
    private string currentTime = DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture);

    [ObservableProperty]
    private string statusMessage = "Consultando estado de asistencia...";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(MarkAttendanceCommand))]
    private bool canMark;

    [ObservableProperty]
    private string actionLabel = "Marcar entrada";

    [ObservableProperty]
    private string exitTimeText = "Sin datos";

    [ObservableProperty]
    private string latitude = "-";

    [ObservableProperty]
    private string longitude = "-";

    [ObservableProperty]
    private string accuracy = "-";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasFeedbackMessage))]
    private string feedbackMessage = string.Empty;

    public bool HasFeedbackMessage => !string.IsNullOrWhiteSpace(FeedbackMessage);

    public Task LoadAsync()
    {
        return RefreshAsync();
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;
        CurrentTime = DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        MarkAttendanceCommand.NotifyCanExecuteChanged();

        try
        {
            var snapshot = await attendanceService.GetTodaySnapshotAsync();
            ApplySnapshot(snapshot);
            await RefreshLocationPreviewAsync();
        }
        finally
        {
            IsBusy = false;
            MarkAttendanceCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand]
    private async Task MarkAttendanceAsync()
    {
        if (IsBusy)
        {
            return;
        }

        if (!CanMark)
        {
            return;
        }

        IsBusy = true;
        CurrentTime = DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        MarkAttendanceCommand.NotifyCanExecuteChanged();

        try
        {
            var result = await attendanceService.MarkAsync();
            ApplySnapshot(result.Snapshot);
            FeedbackMessage = result.Message;
            await RefreshLocationPreviewAsync();
        }
        finally
        {
            IsBusy = false;
            MarkAttendanceCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand]
    private Task OpenLocationSettingsAsync()
    {
        return locationService.OpenLocationSettingsAsync();
    }

    private void ApplySnapshot(AttendanceSnapshot snapshot)
    {
        UserDisplayName = snapshot.UserDisplayName;
        StatusMessage = snapshot.StatusMessage;
        ActionLabel = snapshot.ActionLabel;
        CanMark = snapshot.CanMark;
        ExitTimeText = snapshot.ExitTimeText;
    }

    private async Task RefreshLocationPreviewAsync()
    {
        var location = await locationService.GetCurrentLocationAsync();

        if (location is null)
        {
            Latitude = "-";
            Longitude = "-";
            Accuracy = "-";
            return;
        }

        Latitude = location.Latitude.ToString("F6", CultureInfo.InvariantCulture);
        Longitude = location.Longitude.ToString("F6", CultureInfo.InvariantCulture);
        Accuracy = $"{location.AccuracyMeters:F0} m";
    }
}