using CommunityToolkit.Mvvm.ComponentModel;

namespace Asisste.MAUI.Common.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string title = string.Empty;

    public bool IsNotBusy => !IsBusy;
}
