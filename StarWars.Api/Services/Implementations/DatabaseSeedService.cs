using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarWars.Api.Data.Context;
using StarWars.Api.Services.Interfaces;
using System.Data;

namespace StarWars.Api.Services.Implementations;

public class DatabaseSeedService : IDatabaseSeedService
{
    private readonly StarWarsDbContext _context;
    private readonly ILogger<DatabaseSeedService> _logger;

    public DatabaseSeedService(StarWarsDbContext context, ILogger<DatabaseSeedService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation("Iniciando seed do banco de dados via SQL...");

            if (await IsDatabaseSeededAsync())
            {
                _logger.LogInformation("Banco de dados já foi populado. Pulando seed.");
                return;
            }

            await ExecuteSeedSqlAsync();
            
            _logger.LogInformation("Seed do banco de dados concluído com sucesso!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro durante o seed do banco de dados");
            throw;
        }
    }

    private async Task ExecuteSeedSqlAsync()
    {
        try
        {
            // Caminho para o arquivo SQL de seed
            var sqlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Scripts", "Data", "starwars_seed.sql");
            
            if (!File.Exists(sqlFilePath))
            {
                _logger.LogWarning("Arquivo SQL de seed não encontrado em: {Path}", sqlFilePath);
                return;
            }

            var sqlContent = await File.ReadAllTextAsync(sqlFilePath);
            
            // Dividir o SQL em comandos individuais
            var commands = sqlContent.Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Where(cmd => !string.IsNullOrWhiteSpace(cmd.Trim()))
                .Select(cmd => cmd.Trim())
                .ToList();

            _logger.LogInformation("Executando {Count} comandos SQL...", commands.Count);

            foreach (var command in commands)
            {
                if (!string.IsNullOrWhiteSpace(command) && !command.StartsWith("--"))
                {
                    try
                    {
                        await _context.Database.ExecuteSqlRawAsync(command);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("Erro ao executar comando SQL: {Command}. Erro: {Error}", 
                            command.Substring(0, Math.Min(50, command.Length)), ex.Message);
                    }
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("SQL de seed executado com sucesso!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao executar SQL de seed");
            throw;
        }
    }

    public async Task<bool> IsDatabaseSeededAsync()
    {
        try
        {
            // Verificar se há dados em alguma tabela principal
            var hasFilms = await _context.Films.AnyAsync();
            var hasPeople = await _context.People.AnyAsync();
            var hasPlanets = await _context.Planets.AnyAsync();

            return hasFilms && hasPeople && hasPlanets;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Erro ao verificar se banco está populado");
            return false;
        }
    }

    public async Task<int> GetSeedCountAsync()
    {
        try
        {
            var filmCount = await _context.Films.CountAsync();
            var peopleCount = await _context.People.CountAsync();
            var planetCount = await _context.Planets.CountAsync();
            var speciesCount = await _context.Species.CountAsync();
            var starshipCount = await _context.Starships.CountAsync();
            var vehicleCount = await _context.Vehicles.CountAsync();

            return filmCount + peopleCount + planetCount + speciesCount + starshipCount + vehicleCount;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Erro ao contar entidades do seed");
            return 0;
        }
    }
}
