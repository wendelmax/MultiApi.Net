using StarWars.Api.Models;

namespace StarWars.Api.Repositories.Interfaces;

public interface IFilmRepository
{
    Task<IEnumerable<Film>> GetAllAsync();
    Task<Film?> GetByIdAsync(int id);
    Task<IEnumerable<Film>> SearchAsync(string term);
    Task<int> GetCountAsync();
    Task<IEnumerable<Film>> GetByEpisodeAsync(int episodeId);
    Task<IEnumerable<Film>> GetByDirectorAsync(string director);
}
