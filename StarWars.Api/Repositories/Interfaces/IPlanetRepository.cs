using StarWars.Api.Models;

namespace StarWars.Api.Repositories.Interfaces;

public interface IPlanetRepository
{
    Task<IEnumerable<Planet>> GetAllAsync();
    Task<Planet?> GetByIdAsync(int id);
    Task<IEnumerable<Planet>> SearchAsync(string term);
    Task<int> GetCountAsync();
    Task<IEnumerable<Planet>> GetByClimateAsync(string climate);
    Task<IEnumerable<Planet>> GetByTerrainAsync(string terrain);
}
