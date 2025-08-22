using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data.Context;
using StarWars.Api.Repositories.Interfaces;
using StarWars.Api.Repositories.Implementations;
using StarWars.Api.Services.Interfaces;
using StarWars.Api.Services.Implementations;

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

// HTTP Client removido - agora usamos apenas banco local

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddHealthChecks();

// Database configuration - SQL Server LocalDB
builder.Services.AddDbContext<StarWarsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StarWars")));

// Repositories
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();
builder.Services.AddScoped<IStarshipRepository, StarshipRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<ISpeciesRepository, SpeciesRepository>();

// Services
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPlanetService, PlanetService>();
builder.Services.AddScoped<IStarshipService, StarshipService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ISpeciesService, SpeciesService>();

builder.Services.AddScoped<IDatabaseSeedService, DatabaseSeedService>();

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

// Initialize database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StarWarsDbContext>();
    var seedService = scope.ServiceProvider.GetRequiredService<IDatabaseSeedService>();
    
    try
    {
        await db.Database.EnsureCreatedAsync();
        
        // Forçar seed sempre para debug
        Console.WriteLine("🔄 Forçando seed do banco de dados...");
        await seedService.SeedAsync();
        
        var count = await seedService.GetSeedCountAsync();
        Console.WriteLine($"✅ Banco de dados populado com {count} entidades");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro durante inicialização do banco: {ex.Message}");
        throw;
    }
}

app.Run();
