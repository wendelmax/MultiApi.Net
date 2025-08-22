using StarWars.Api.DTOs;

namespace StarWars.Api.Services.Interfaces;

public interface IPersonService
{
    Task<PersonListResponse> GetPeopleAsync(int? page = null, int? pageSize = null);
    Task<PersonResponse?> GetPersonByIdAsync(int id);
    Task<PersonSearchResponse> SearchPeopleAsync(string term);
    Task<IEnumerable<PersonResponse>> GetPeopleBySpeciesAsync(string species);
    Task<IEnumerable<PersonResponse>> GetPeopleByHomeworldAsync(string homeworld);
}
