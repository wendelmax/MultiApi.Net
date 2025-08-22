using StarWars.Api.Models;

namespace StarWars.Api.Repositories.Interfaces;

public interface ISpeciesRepository
{
    Task<IEnumerable<Species>> GetAllAsync();
    Task<Species?> GetByIdAsync(int id);
    Task<IEnumerable<Species>> SearchAsync(string term);
    Task<int> GetCountAsync();
    Task<IEnumerable<Species>> GetByClassificationAsync(string classification);
    Task<IEnumerable<Species>> GetByDesignationAsync(string designation);
}
