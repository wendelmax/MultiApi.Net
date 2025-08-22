using StarWars.Api.DTOs;

namespace StarWars.Api.Services.Interfaces;

public interface IPlanetService
{
    Task<PlanetListResponse> GetPlanetsAsync(int? page = null, int? pageSize = null);
    Task<PlanetResponse?> GetPlanetByIdAsync(int id);
    Task<PlanetSearchResponse> SearchPlanetsAsync(string term);
    Task<IEnumerable<PlanetResponse>> GetPlanetsByClimateAsync(string climate);
    Task<IEnumerable<PlanetResponse>> GetPlanetsByTerrainAsync(string terrain);
}
