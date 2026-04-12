using Asisste.MAUI.Features.Absences.ViewModels;
using Asisste.Services.Abstractions.Absences;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;

namespace Asisste.MAUI.Features.Absences.Pages;

public partial class AbsencesPage : ContentPage
{
    private readonly AbsencesViewModel viewModel;

    public AbsencesPage(AbsencesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadAsync();
    }

    private async void OnPickFileClicked(object? sender, EventArgs e)
    {
        if (viewModel.AlreadySubmitted)
        {
            viewModel.FeedbackMessage = "Solo puedes enviar un archivo de falta por día.";
            return;
        }

        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Selecciona la evidencia",
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    [DevicePlatform.Android] = new[] { "application/pdf", "image/png", "image/jpeg" }
                })
            });

            if (result is null)
            {
                return;
            }

            await using var stream = await result.OpenReadAsync();
            using var memory = new MemoryStream();
            await stream.CopyToAsync(memory);

            var contentType = string.IsNullOrWhiteSpace(result.ContentType)
                ? InferContentType(result.FileName)
                : result.ContentType;

            viewModel.SetAttachment(new AbsenceAttachment(result.FileName, contentType, memory.ToArray()));
        }
        catch (Exception ex)
        {
            viewModel.FeedbackMessage = ex.Message;
        }
    }

    private static string InferContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        return extension switch
        {
            ".pdf" => "application/pdf",
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            _ => "application/octet-stream"
        };
    }
}