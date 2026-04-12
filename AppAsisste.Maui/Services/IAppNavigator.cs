namespace AppAsisste.Maui.Services;

public interface IAppNavigator
{
    Task ShowMainShellAsync();

    Task ShowLoginAsync();

    Task PushAsync<TPage>() where TPage : Page;

    Task PushAsync<TPage, TParameter>(TParameter parameter) where TPage : Page;

    Task PopAsync();
}

public interface IPageInitializer<in TParameter>
{
    Task InitializeAsync(TParameter parameter);
}
