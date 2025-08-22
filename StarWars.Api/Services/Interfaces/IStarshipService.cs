using StarWars.Api.DTOs;

namespace StarWars.Api.Services.Interfaces;

public interface IStarshipService
{
    Task<StarshipListResponse> GetStarshipsAsync(int? page = null, int? pageSize = null);
    Task<StarshipResponse?> GetStarshipByIdAsync(int id);
    Task<StarshipSearchResponse> SearchStarshipsAsync(string term);
    Task<IEnumerable<StarshipResponse>> GetStarshipsByClassAsync(string starshipClass);
    Task<IEnumerable<StarshipResponse>> GetStarshipsByManufacturerAsync(string manufacturer);
}
