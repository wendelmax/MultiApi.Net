using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data.Context;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;

namespace StarWars.Api.Repositories.Implementations;

public class StarshipRepository : IStarshipRepository
{
    private readonly StarWarsDbContext _context;

    public StarshipRepository(StarWarsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Starship>> GetAllAsync()
    {
        return await _context.Starships
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<Starship?> GetByIdAsync(int id)
    {
        return await _context.Starships
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Starship>> SearchAsync(string term)
    {
        return await _context.Starships
            .Where(s => s.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       s.Model.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       s.Manufacturer.Contains(term, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Starships.CountAsync();
    }

    public async Task<IEnumerable<Starship>> GetByClassAsync(string starshipClass)
    {
        return await _context.Starships
            .Where(s => s.StarshipClass.Contains(starshipClass, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Starship>> GetByManufacturerAsync(string manufacturer)
    {
        return await _context.Starships
            .Where(s => s.Manufacturer.Contains(manufacturer, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.Name)
            .ToListAsync();
    }
}
