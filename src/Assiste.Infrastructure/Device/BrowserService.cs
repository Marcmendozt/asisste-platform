using Assiste.Application.Abstractions.Device;
using Microsoft.Maui.ApplicationModel;

namespace Assiste.Infrastructure.Device;

public sealed class BrowserService : IBrowserService
{
    public Task OpenAsync(string uri)
    {
        return Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
}
