using Asisste.MAUI.Features.Attendance.ViewModels;

namespace Asisste.MAUI.Features.Attendance.Pages;

public partial class AttendancePage : ContentPage
{
    private readonly AttendanceViewModel viewModel;

    public AttendancePage(AttendanceViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadAsync();
    }
}