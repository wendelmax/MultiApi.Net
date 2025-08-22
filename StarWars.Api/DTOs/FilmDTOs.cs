namespace StarWars.Api.DTOs;

public record FilmResponse(
    int Id, 
    string Title, 
    int? EpisodeId, 
    string? OpeningCrawl,
    string? Director, 
    string? Producer, 
    string? ReleaseDate,
    string? Characters,
    string? Planets,
    string? Starships,
    string? Vehicles,
    string? Species,
    DateTime CreatedAt
);

public record FilmListResponse(
    int Total, 
    int Page, 
    int PageSize, 
    IEnumerable<FilmResponse> Films
);

public record FilmSearchResponse(
    int Count, 
    IEnumerable<FilmResponse> Results
);

public record FilmQueryParameters(
    string? Term = null,
    int? Page = null,
    int? PageSize = null,
    string? OrderBy = null,
    string? OrderDirection = "asc"
);
