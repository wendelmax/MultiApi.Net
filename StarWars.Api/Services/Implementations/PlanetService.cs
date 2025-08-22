using StarWars.Api.DTOs;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;
using StarWars.Api.Services.Interfaces;

namespace StarWars.Api.Services.Implementations;

public class PlanetService : IPlanetService
{
    private readonly IPlanetRepository _planetRepository;

    public PlanetService(IPlanetRepository planetRepository)
    {
        _planetRepository = planetRepository;
    }

    public async Task<PlanetListResponse> GetPlanetsAsync(int? page = null, int? pageSize = null)
    {
        var planets = await _planetRepository.GetAllAsync();
        var total = await _planetRepository.GetCountAsync();

        var planetResponses = planets.Select(MapToPlanetResponse);

        var currentPage = page ?? 1;
        var currentPageSize = pageSize ?? planets.Count();

        var pagedPlanets = planetResponses
            .Skip((currentPage - 1) * currentPageSize)
            .Take(currentPageSize);

        return new PlanetListResponse(total, currentPage, currentPageSize, pagedPlanets);
    }

    public async Task<PlanetResponse?> GetPlanetByIdAsync(int id)
    {
        var planet = await _planetRepository.GetByIdAsync(id);
        return planet != null ? MapToPlanetResponse(planet) : null;
    }

    public async Task<PlanetSearchResponse> SearchPlanetsAsync(string term)
    {
        var planets = await _planetRepository.SearchAsync(term);
        var planetResponses = planets.Select(MapToPlanetResponse);

        return new PlanetSearchResponse(planets.Count(), planetResponses);
    }

    public async Task<IEnumerable<PlanetResponse>> GetPlanetsByClimateAsync(string climate)
    {
        var planets = await _planetRepository.GetByClimateAsync(climate);
        return planets.Select(MapToPlanetResponse);
    }

    public async Task<IEnumerable<PlanetResponse>> GetPlanetsByTerrainAsync(string terrain)
    {
        var planets = await _planetRepository.GetByTerrainAsync(terrain);
        return planets.Select(MapToPlanetResponse);
    }

    private static PlanetResponse MapToPlanetResponse(Planet planet)
    {
        // Extrair IDs das tabelas de junção
        var residentIds = planet.PlanetResidents?.Select(pr => pr.PersonId).ToList() ?? new List<int>();
        var filmIds = planet.FilmPlanets?.Select(fp => fp.FilmId).ToList() ?? new List<int>();

        return new PlanetResponse(
            planet.Id,
            planet.Name,
            planet.RotationPeriod,
            planet.OrbitalPeriod,
            planet.Diameter,
            planet.Climate,
            planet.Gravity,
            planet.Terrain,
            planet.SurfaceWater,
            planet.Population,
            string.Join(",", residentIds),
            string.Join(",", filmIds),
            planet.CreatedAt
        );
    }
}
