using StarWars.Api.Models;

namespace StarWars.Api.Repositories.Interfaces;

public interface IStarshipRepository
{
    Task<IEnumerable<Starship>> GetAllAsync();
    Task<Starship?> GetByIdAsync(int id);
    Task<IEnumerable<Starship>> SearchAsync(string term);
    Task<int> GetCountAsync();
    Task<IEnumerable<Starship>> GetByClassAsync(string starshipClass);
    Task<IEnumerable<Starship>> GetByManufacturerAsync(string manufacturer);
}
