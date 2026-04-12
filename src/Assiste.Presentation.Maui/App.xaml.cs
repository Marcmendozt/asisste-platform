namespace Assiste.Presentation.Maui;

public partial class App : Microsoft.Maui.Controls.Application
{
    private readonly AppShell appShell;

    public App(AppShell appShell)
    {
        InitializeComponent();
        this.appShell = appShell;
    }

    protected override Microsoft.Maui.Controls.Window CreateWindow(Microsoft.Maui.IActivationState? activationState)
    {
        return new Microsoft.Maui.Controls.Window(appShell);
    }
}
