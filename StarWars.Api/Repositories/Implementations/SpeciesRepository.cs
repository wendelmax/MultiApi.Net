using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data.Context;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;

namespace StarWars.Api.Repositories.Implementations;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly StarWarsDbContext _context;

    public SpeciesRepository(StarWarsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Species>> GetAllAsync()
    {
        return await _context.Species
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<Species?> GetByIdAsync(int id)
    {
        return await _context.Species
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Species>> SearchAsync(string term)
    {
        return await _context.Species
            .Where(s => s.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       s.Classification.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       s.Designation.Contains(term, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Species.CountAsync();
    }

    public async Task<IEnumerable<Species>> GetByClassificationAsync(string classification)
    {
        return await _context.Species
            .Where(s => s.Classification.Contains(classification, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Species>> GetByDesignationAsync(string designation)
    {
        return await _context.Species
            .Where(s => s.Designation.Contains(designation, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.Name)
            .ToListAsync();
    }
}
