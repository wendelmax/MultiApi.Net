using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data;
using StarWars.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Star Wars",
        Version = "v1",
        Description = "API de estudos em português que realiza proxy para a SWAPI (Star Wars API).",
        Contact = new OpenApiContact
        {
            Name = "MultiApi.Net",
            Url = new Uri("https://swapi.py4e.com/documentation")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);

    // Agrupar por tags PT-BR com base nos nomes dos controladores
    options.TagActionsBy(api =>
    {
        var controller = api.GroupName ?? api.ActionDescriptor.RouteValues["controller"];
        return new[] { controller switch
        {
            "People" => "Pessoas",
            "Planets" => "Planetas",
            "Films" => "Filmes",
            "Starships" => "Naves",
            "Vehicles" => "Veículos",
            "Species" => "Espécies",
            _ => controller
        }};
    });
    options.DocInclusionPredicate((_, _) => true);
});

builder.Services.AddHttpClient("swapi", client =>
{
    client.BaseAddress = new Uri("https://swapi.py4e.com/api/");
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddHealthChecks();

// EF Core + SQLite
builder.Services.AddDbContext<StarWarsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StarWars")));

// Cache service
builder.Services.AddScoped<IApiCacheService, ApiCacheService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(setup =>
{
    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "API Star Wars (PT-BR) v1");
    setup.DocumentTitle = "API Star Wars (Estudos)";
});

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

// Create database on startup (simple EnsureCreated for cache persistence)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StarWarsDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.Run();
