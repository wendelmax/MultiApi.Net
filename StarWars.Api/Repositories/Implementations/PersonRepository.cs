using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data.Context;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;

namespace StarWars.Api.Repositories.Implementations;

public class PersonRepository : IPersonRepository
{
    private readonly StarWarsDbContext _context;

    public PersonRepository(StarWarsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.People
            .Include(p => p.HomeworldPlanet)
            .Include(p => p.FilmCharacters)
            .Include(p => p.PersonSpecies)
            .Include(p => p.PersonVehicles)
            .Include(p => p.PersonStarships)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.People
            .Include(p => p.HomeworldPlanet)
            .Include(p => p.FilmCharacters)
            .Include(p => p.PersonSpecies)
            .Include(p => p.PersonVehicles)
            .Include(p => p.PersonStarships)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Person>> SearchAsync(string term)
    {
        return await _context.People
            .Include(p => p.HomeworldPlanet)
            .Include(p => p.PersonSpecies)
            .Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       p.PersonSpecies.Any(ps => ps.Species.Name.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                       (p.HomeworldPlanet != null && p.HomeworldPlanet.Name.Contains(term, StringComparison.OrdinalIgnoreCase)))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.People.CountAsync();
    }

    public async Task<IEnumerable<Person>> GetBySpeciesAsync(string species)
    {
        return await _context.People
            .Include(p => p.PersonSpecies)
            .ThenInclude(ps => ps.Species)
            .Where(p => p.PersonSpecies.Any(ps => ps.Species.Name.Contains(species, StringComparison.OrdinalIgnoreCase)))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Person>> GetByHomeworldAsync(string homeworld)
    {
        return await _context.People
            .Include(p => p.HomeworldPlanet)
            .Where(p => p.HomeworldPlanet != null && p.HomeworldPlanet.Name.Contains(homeworld, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
}
