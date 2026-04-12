using Asisste.Services.Abstractions.Device;
using Microsoft.Maui.ApplicationModel;

namespace Asisste.Data.Device;

public sealed class BrowserService : IBrowserService
{
    public Task OpenAsync(string uri)
    {
        return Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
}
