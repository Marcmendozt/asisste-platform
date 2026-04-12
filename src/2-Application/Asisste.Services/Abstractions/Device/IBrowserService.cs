namespace Asisste.Services.Abstractions.Device;

public interface IBrowserService
{
    Task OpenAsync(string uri);
}
