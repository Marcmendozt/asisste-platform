using Asisste.ApiData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddApiData(builder.Configuration.GetConnectionString("LegacyAssiste"));

var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/", () => Results.Ok(new
{
    service = "Asisste.API",
    status = "running",
    slices = new[]
    {
        "mobile-auth-login",
        "legacy-wslogin-compat"
    }
}));

app.MapControllers();

app.Run();

public partial class Program;