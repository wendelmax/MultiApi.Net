using StarWars.Api.DTOs;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;
using StarWars.Api.Services.Interfaces;

namespace StarWars.Api.Services.Implementations;

public class SpeciesService : ISpeciesService
{
    private readonly ISpeciesRepository _speciesRepository;

    public SpeciesService(ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }

    public async Task<SpeciesListResponse> GetSpeciesAsync(int? page = null, int? pageSize = null)
    {
        var species = await _speciesRepository.GetAllAsync();
        var total = await _speciesRepository.GetCountAsync();

        var speciesResponses = species.Select(MapToSpeciesResponse);

        var currentPage = page ?? 1;
        var currentPageSize = pageSize ?? species.Count();

        var pagedSpecies = speciesResponses
            .Skip((currentPage - 1) * currentPageSize)
            .Take(currentPageSize);

        return new SpeciesListResponse(total, currentPage, currentPageSize, pagedSpecies);
    }

    public async Task<SpeciesResponse?> GetSpeciesByIdAsync(int id)
    {
        var species = await _speciesRepository.GetByIdAsync(id);
        return species != null ? MapToSpeciesResponse(species) : null;
    }

    public async Task<SpeciesSearchResponse> SearchSpeciesAsync(string term)
    {
        var species = await _speciesRepository.SearchAsync(term);
        var speciesResponses = species.Select(MapToSpeciesResponse);

        return new SpeciesSearchResponse(species.Count(), speciesResponses);
    }

    public async Task<IEnumerable<SpeciesResponse>> GetSpeciesByClassificationAsync(string classification)
    {
        var species = await _speciesRepository.GetByClassificationAsync(classification);
        return species.Select(MapToSpeciesResponse);
    }

    public async Task<IEnumerable<SpeciesResponse>> GetSpeciesByDesignationAsync(string designation)
    {
        var species = await _speciesRepository.GetByDesignationAsync(designation);
        return species.Select(MapToSpeciesResponse);
    }

    private static SpeciesResponse MapToSpeciesResponse(Species species)
    {
        // Extrair IDs das tabelas de junção
        var peopleIds = species.PersonSpecies?.Select(ps => ps.PersonId).ToList() ?? new List<int>();
        var filmIds = species.FilmSpecies?.Select(fs => fs.FilmId).ToList() ?? new List<int>();

        return new SpeciesResponse(
            species.Id,
            species.Name,
            species.Classification,
            species.Designation,
            species.AverageHeight,
            species.SkinColors,
            species.HairColors,
            species.EyeColors,
            species.AverageLifespan,
            species.HomeworldPlanetId?.ToString() ?? "",
            species.Language,
            string.Join(",", peopleIds),
            string.Join(",", filmIds),
            species.CreatedAt
        );
    }
}
