using Microsoft.Extensions.DependencyInjection;

namespace Assiste.Presentation.Maui.Common.Navigation;

public sealed class AppNavigator : IAppNavigator
{
    private readonly AppShell shell;
    private readonly IServiceProvider serviceProvider;

    public AppNavigator(AppShell shell, IServiceProvider serviceProvider)
    {
        this.shell = shell;
        this.serviceProvider = serviceProvider;
    }

    public async Task ShowMainShellAsync()
    {
        await ResetNavigationStackAsync();
        shell.SetAuthenticated(true);
    }

    public async Task ShowLoginAsync()
    {
        await ResetNavigationStackAsync();
        shell.SetAuthenticated(false);
    }

    public Task PopAsync()
    {
        return shell.Navigation.PopAsync();
    }

    public async Task PushAsync<TPage>() where TPage : Page
    {
        var page = serviceProvider.GetRequiredService<TPage>();
        await shell.Navigation.PushAsync(page);
    }

    public async Task PushAsync<TPage, TParameter>(TParameter parameter) where TPage : Page
    {
        var page = serviceProvider.GetRequiredService<TPage>();

        if (page.BindingContext is IPageInitializer<TParameter> initializer)
        {
            await initializer.InitializeAsync(parameter);
        }

        await shell.Navigation.PushAsync(page);
    }

    private async Task ResetNavigationStackAsync()
    {
        while (shell.Navigation.NavigationStack.Count > 1)
        {
            await shell.Navigation.PopAsync(false);
        }
    }
}
