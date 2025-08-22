using StarWars.Api.Models;

namespace StarWars.Api.Repositories.Interfaces;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task<IEnumerable<Person>> SearchAsync(string term);
    Task<int> GetCountAsync();
    Task<IEnumerable<Person>> GetBySpeciesAsync(string species);
    Task<IEnumerable<Person>> GetByHomeworldAsync(string homeworld);
}
