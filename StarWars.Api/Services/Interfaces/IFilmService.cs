using StarWars.Api.DTOs;

namespace StarWars.Api.Services.Interfaces;

public interface IFilmService
{
    Task<FilmListResponse> GetFilmsAsync(FilmQueryParameters query);
    Task<FilmResponse?> GetFilmByIdAsync(int id);
    Task<FilmSearchResponse> SearchFilmsAsync(string term);
    Task<IEnumerable<FilmResponse>> GetFilmsByEpisodeAsync(int episodeId);
    Task<IEnumerable<FilmResponse>> GetFilmsByDirectorAsync(string director);
}
