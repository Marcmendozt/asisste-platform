namespace Asisste.MAUI;

public partial class App : Microsoft.Maui.Controls.Application
{
    private readonly IServiceProvider serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        this.serviceProvider = serviceProvider;
    }

    protected override Microsoft.Maui.Controls.Window CreateWindow(Microsoft.Maui.IActivationState? activationState)
    {
        return new Microsoft.Maui.Controls.Window(serviceProvider.GetRequiredService<AppShell>());
    }
}
