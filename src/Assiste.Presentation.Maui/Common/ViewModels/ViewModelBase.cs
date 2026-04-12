using CommunityToolkit.Mvvm.ComponentModel;

namespace Assiste.Presentation.Maui.Common.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string title = string.Empty;

    public bool IsNotBusy => !IsBusy;
}
