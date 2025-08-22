using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data.Context;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;

namespace StarWars.Api.Repositories.Implementations;

public class PlanetRepository : IPlanetRepository
{
    private readonly StarWarsDbContext _context;

    public PlanetRepository(StarWarsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Planet>> GetAllAsync()
    {
        return await _context.Planets
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Planet?> GetByIdAsync(int id)
    {
        return await _context.Planets
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Planet>> SearchAsync(string term)
    {
        return await _context.Planets
            .Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       p.Climate.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       p.Terrain.Contains(term, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Planets.CountAsync();
    }

    public async Task<IEnumerable<Planet>> GetByClimateAsync(string climate)
    {
        return await _context.Planets
            .Where(p => p.Climate.Contains(climate, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Planet>> GetByTerrainAsync(string terrain)
    {
        return await _context.Planets
            .Where(p => p.Terrain.Contains(terrain, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
}
