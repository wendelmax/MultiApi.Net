using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data.Context;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;

namespace StarWars.Api.Repositories.Implementations;

public class FilmRepository : IFilmRepository
{
    private readonly StarWarsDbContext _context;

    public FilmRepository(StarWarsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Film>> GetAllAsync()
    {
        return await _context.Films
            .OrderBy(f => f.EpisodeId)
            .ToListAsync();
    }

    public async Task<Film?> GetByIdAsync(int id)
    {
        return await _context.Films
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<Film>> SearchAsync(string term)
    {
        return await _context.Films
            .Where(f => f.Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       f.Director.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       f.Producer.Contains(term, StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f.EpisodeId)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Films.CountAsync();
    }

    public async Task<IEnumerable<Film>> GetByEpisodeAsync(int episodeId)
    {
        return await _context.Films
            .Where(f => f.EpisodeId == episodeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Film>> GetByDirectorAsync(string director)
    {
        return await _context.Films
            .Where(f => f.Director.Contains(director, StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f.EpisodeId)
            .ToListAsync();
    }
}
