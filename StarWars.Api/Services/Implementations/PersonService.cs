using StarWars.Api.DTOs;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;
using StarWars.Api.Services.Interfaces;

namespace StarWars.Api.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<PersonListResponse> GetPeopleAsync(int? page = null, int? pageSize = null)
    {
        var people = await _personRepository.GetAllAsync();
        var total = await _personRepository.GetCountAsync();

        var personResponses = people.Select(MapToPersonResponse);

        var currentPage = page ?? 1;
        var currentPageSize = pageSize ?? people.Count();

        var pagedPeople = personResponses
            .Skip((currentPage - 1) * currentPageSize)
            .Take(currentPageSize);

        return new PersonListResponse(total, currentPage, currentPageSize, pagedPeople);
    }

    public async Task<PersonResponse?> GetPersonByIdAsync(int id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        return person != null ? MapToPersonResponse(person) : null;
    }

    public async Task<PersonSearchResponse> SearchPeopleAsync(string term)
    {
        var people = await _personRepository.SearchAsync(term);
        var personResponses = people.Select(MapToPersonResponse);

        return new PersonSearchResponse(people.Count(), personResponses);
    }

    public async Task<IEnumerable<PersonResponse>> GetPeopleBySpeciesAsync(string species)
    {
        var people = await _personRepository.GetBySpeciesAsync(species);
        return people.Select(MapToPersonResponse);
    }

    public async Task<IEnumerable<PersonResponse>> GetPeopleByHomeworldAsync(string homeworld)
    {
        var people = await _personRepository.GetByHomeworldAsync(homeworld);
        return people.Select(MapToPersonResponse);
    }

    private static PersonResponse MapToPersonResponse(Person person)
    {
        // Extrair IDs das tabelas de junção
        var filmIds = person.FilmCharacters?.Select(fc => fc.FilmId).ToList() ?? new List<int>();
        var speciesIds = person.PersonSpecies?.Select(ps => ps.SpeciesId).ToList() ?? new List<int>();
        var vehicleIds = person.PersonVehicles?.Select(pv => pv.VehicleId).ToList() ?? new List<int>();
        var starshipIds = person.PersonStarships?.Select(ps => ps.StarshipId).ToList() ?? new List<int>();

        return new PersonResponse(
            person.Id,
            person.Name,
            person.Height,
            person.Mass,
            person.HairColor,
            person.SkinColor,
            person.EyeColor,
            person.BirthYear,
            person.Gender,
            person.HomeworldPlanetId?.ToString() ?? "",
            string.Join(",", filmIds),
            string.Join(",", speciesIds),
            string.Join(",", vehicleIds),
            string.Join(",", starshipIds),
            person.CreatedAt
        );
    }
}
