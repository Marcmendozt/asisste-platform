using Microsoft.Maui.ApplicationModel;

namespace AppAsisste.Maui.Services;

public sealed class BrowserService : IBrowserService
{
    public Task OpenAsync(string uri)
    {
        return Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
}
