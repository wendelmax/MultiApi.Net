using StarWars.Api.DTOs;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;
using StarWars.Api.Services.Interfaces;

namespace StarWars.Api.Services.Implementations;

public class FilmService : IFilmService
{
    private readonly IFilmRepository _filmRepository;

    public FilmService(IFilmRepository filmRepository)
    {
        _filmRepository = filmRepository;
    }

    public async Task<FilmListResponse> GetFilmsAsync(FilmQueryParameters query)
    {
        var films = await _filmRepository.GetAllAsync();
        var total = await _filmRepository.GetCountAsync();

        var filmResponses = films.Select(MapToFilmResponse);

        var page = query.Page ?? 1;
        var pageSize = query.PageSize ?? films.Count();

        var pagedFilms = filmResponses
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return new FilmListResponse(total, page, pageSize, pagedFilms);
    }

    public async Task<FilmResponse?> GetFilmByIdAsync(int id)
    {
        var film = await _filmRepository.GetByIdAsync(id);
        return film != null ? MapToFilmResponse(film) : null;
    }

    public async Task<FilmSearchResponse> SearchFilmsAsync(string term)
    {
        var films = await _filmRepository.SearchAsync(term);
        var filmResponses = films.Select(MapToFilmResponse);

        return new FilmSearchResponse(films.Count(), filmResponses);
    }

    public async Task<IEnumerable<FilmResponse>> GetFilmsByEpisodeAsync(int episodeId)
    {
        var films = await _filmRepository.GetByEpisodeAsync(episodeId);
        return films.Select(MapToFilmResponse);
    }

    public async Task<IEnumerable<FilmResponse>> GetFilmsByDirectorAsync(string director)
    {
        var films = await _filmRepository.GetByDirectorAsync(director);
        return films.Select(MapToFilmResponse);
    }

    private static FilmResponse MapToFilmResponse(Film film)
    {
        // Extrair IDs das tabelas de junção
        var characterIds = film.FilmCharacters?.Select(fc => fc.PersonId).ToList() ?? new List<int>();
        var planetIds = film.FilmPlanets?.Select(fp => fp.PlanetId).ToList() ?? new List<int>();
        var starshipIds = film.FilmStarships?.Select(fs => fs.StarshipId).ToList() ?? new List<int>();
        var vehicleIds = film.FilmVehicles?.Select(fv => fv.VehicleId).ToList() ?? new List<int>();
        var speciesIds = film.FilmSpecies?.Select(fs => fs.SpeciesId).ToList() ?? new List<int>();

        return new FilmResponse(
            film.Id,
            film.Title,
            film.EpisodeId,
            film.OpeningCrawl,
            film.Director,
            film.Producer,
            film.ReleaseDate,
            string.Join(",", characterIds),
            string.Join(",", planetIds),
            string.Join(",", starshipIds),
            string.Join(",", vehicleIds),
            string.Join(",", speciesIds),
            film.CreatedAt
        );
    }
}
