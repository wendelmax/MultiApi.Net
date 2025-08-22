using StarWars.Api.DTOs;

namespace StarWars.Api.Services.Interfaces;

public interface ISpeciesService
{
    Task<SpeciesListResponse> GetSpeciesAsync(int? page = null, int? pageSize = null);
    Task<SpeciesResponse?> GetSpeciesByIdAsync(int id);
    Task<SpeciesSearchResponse> SearchSpeciesAsync(string term);
    Task<IEnumerable<SpeciesResponse>> GetSpeciesByClassificationAsync(string classification);
    Task<IEnumerable<SpeciesResponse>> GetSpeciesByDesignationAsync(string designation);
}
