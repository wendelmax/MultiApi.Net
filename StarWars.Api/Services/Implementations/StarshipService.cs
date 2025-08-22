using StarWars.Api.DTOs;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;
using StarWars.Api.Services.Interfaces;

namespace StarWars.Api.Services.Implementations;

public class StarshipService : IStarshipService
{
    private readonly IStarshipRepository _starshipRepository;

    public StarshipService(IStarshipRepository starshipRepository)
    {
        _starshipRepository = starshipRepository;
    }

    public async Task<StarshipListResponse> GetStarshipsAsync(int? page = null, int? pageSize = null)
    {
        var starships = await _starshipRepository.GetAllAsync();
        var total = await _starshipRepository.GetCountAsync();

        var starshipResponses = starships.Select(MapToStarshipResponse);

        var currentPage = page ?? 1;
        var currentPageSize = pageSize ?? starships.Count();

        var pagedStarships = starshipResponses
            .Skip((currentPage - 1) * currentPageSize)
            .Take(currentPageSize);

        return new StarshipListResponse(total, currentPage, currentPageSize, pagedStarships);
    }

    public async Task<StarshipResponse?> GetStarshipByIdAsync(int id)
    {
        var starship = await _starshipRepository.GetByIdAsync(id);
        return starship != null ? MapToStarshipResponse(starship) : null;
    }

    public async Task<StarshipSearchResponse> SearchStarshipsAsync(string term)
    {
        var starships = await _starshipRepository.SearchAsync(term);
        var starshipResponses = starships.Select(MapToStarshipResponse);

        return new StarshipSearchResponse(starships.Count(), starshipResponses);
    }

    public async Task<IEnumerable<StarshipResponse>> GetStarshipsByClassAsync(string starshipClass)
    {
        var starships = await _starshipRepository.GetByClassAsync(starshipClass);
        return starships.Select(MapToStarshipResponse);
    }

    public async Task<IEnumerable<StarshipResponse>> GetStarshipsByManufacturerAsync(string manufacturer)
    {
        var starships = await _starshipRepository.GetByManufacturerAsync(manufacturer);
        return starships.Select(MapToStarshipResponse);
    }

    private static StarshipResponse MapToStarshipResponse(Starship starship)
    {
        // Extrair IDs das tabelas de junção
        var pilotIds = starship.PersonStarships?.Select(ps => ps.PersonId).ToList() ?? new List<int>();
        var filmIds = starship.FilmStarships?.Select(fs => fs.FilmId).ToList() ?? new List<int>();

        return new StarshipResponse(
            starship.Id,
            starship.Name,
            starship.Model,
            starship.Manufacturer,
            starship.CostInCredits,
            starship.Length,
            starship.MaxAtmospheringSpeed,
            starship.Crew,
            starship.Passengers,
            starship.CargoCapacity,
            starship.Consumables,
            starship.HyperdriveRating,
            starship.MGLT,
            starship.StarshipClass,
            string.Join(",", pilotIds),
            string.Join(",", filmIds),
            starship.CreatedAt
        );
    }
}
