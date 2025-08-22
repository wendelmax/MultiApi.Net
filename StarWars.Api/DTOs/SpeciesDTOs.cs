namespace StarWars.Api.DTOs;

public record SpeciesResponse(
    int Id,
    string Name,
    string? Classification,
    string? Designation,
    string? AverageHeight,
    string? SkinColors,
    string? HairColors,
    string? EyeColors,
    string? AverageLifespan,
    string? Homeworld,
    string? Language,
    string? People,
    string? Films,
    DateTime CreatedAt
);

public record SpeciesListResponse(
    int Total,
    int Page,
    int PageSize,
    IEnumerable<SpeciesResponse> Species
);

public record SpeciesSearchResponse(
    int Count,
    IEnumerable<SpeciesResponse> Results
);
