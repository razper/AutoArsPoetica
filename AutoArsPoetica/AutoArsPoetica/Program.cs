using AutoArsPoetica.Components;
using AutoArsPoetica.Services;
using MudBlazor.Services;
using OpenAI;
using OpenAI.Chat;

var builder = WebApplication.CreateBuilder(args);

// Add HttpClient
builder.Services.AddHttpClient();

// Add OpenAI configuration
builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidOperationException("OpenAI API key not found in configuration");
    return new ChatClient("gpt-3.5-turbo", apiKey);
});

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddScoped<IArsPoeticaService, ArsPoeticaServiceBackEnd>();

// Add Flight and Weather services
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

// Add services to the container.

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AutoArsPoetica.Client._Imports).Assembly);

// Add minimal API endpoint for poem generation
app.MapGet("/api/poem/generate", async (IArsPoeticaService arsPoeticaService) =>
{
    try
    {
        var poem = await arsPoeticaService.GeneratePoemAsync();

        return Results.Ok(poem);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();