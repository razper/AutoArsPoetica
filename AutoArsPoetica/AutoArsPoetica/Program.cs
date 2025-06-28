using AutoArsPoetica.Components;
using AutoArsPoetica.Services;
using Microsoft.EntityFrameworkCore;
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

// Add mongo db
builder.Services.AddDbContext<ArsPoeticaDbContext>(options =>
options.UseMongoDB(builder.Configuration["MongoDB:ConnectionString"], builder.Configuration["MongoDB:DatabaseName"]));

// Add MudBlazor services

builder.Services.AddMudServices();
builder.Services.AddScoped<IArsPoeticaService, ArsPoeticaServiceBackEnd>();

// Add Weather services
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<ICryptoService, CryptoService>();

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
app.MapGet("/api/weather/generate", async (IArsPoeticaService arsPoeticaService, ArsPoeticaDbContext dbContext) =>
{
    try
    {
        var createdAt = DateTime.UtcNow;
        var poemContent = await arsPoeticaService.GenerateWeatherPoemAsync();
        var poem = new Poem(poemContent, createdAt);

        dbContext.Poems.Add(poem);
        await dbContext.SaveChangesAsync();

        return Results.Ok(poem);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/crypto/generate", async (IArsPoeticaService arsPoeticaService, ArsPoeticaDbContext dbContext) =>
{
    try
    {
        var createdAt = DateTime.UtcNow;
        var poemContent = await arsPoeticaService.GenerateCryptoPoemAsync();
        var poem = new Poem(poemContent, createdAt);

        dbContext.Poems.Add(poem);
        await dbContext.SaveChangesAsync();

        return Results.Ok(poem);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/poems", async (ArsPoeticaDbContext dbContext) =>
{
    return await dbContext.Poems.OrderByDescending(p => p.Epoch).Take(23).ToListAsync();
});

app.Run();