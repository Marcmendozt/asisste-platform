namespace Assiste.Application.Abstractions.Device;

public interface IBrowserService
{
    Task OpenAsync(string uri);
}
