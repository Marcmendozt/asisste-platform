using Asisste.ApiData;
using Asisste.Services.Abstractions.Profiles;
using Asisste.Services.Profiles;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalWeb", policy =>
    {
        policy
            .SetIsOriginAllowed(static origin => IsAllowedLocalWebOrigin(origin))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddApiData(builder.Configuration.GetConnectionString("LegacyAssiste"));

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors("LocalWeb");

app.MapGet("/", () => Results.Ok(new
{
    service = "Asisste.API",
    status = "running",
    slices = new[]
    {
        "mobile-auth-login",
        "legacy-wslogin-compat",
        "profiles-catalog"
    }
}));

app.MapControllers();

app.Run();

public partial class Program
{
    private static bool IsAllowedLocalWebOrigin(string origin)
    {
        if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
        {
            return false;
        }

        return uri.Scheme is "http" or "https"
            && (uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
                || uri.Host.Equals("127.0.0.1"));
    }
}