namespace StarWars.Api.DTOs;

public record PersonResponse(
    int Id,
    string Name,
    string? Height,
    string? Mass,
    string? HairColor,
    string? SkinColor,
    string? EyeColor,
    string? BirthYear,
    string? Gender,
    string? Homeworld,
    string? Films,
    string? Species,
    string? Vehicles,
    string? Starships,
    DateTime CreatedAt
);

public record PersonListResponse(
    int Total,
    int Page,
    int PageSize,
    IEnumerable<PersonResponse> People
);

public record PersonSearchResponse(
    int Count,
    IEnumerable<PersonResponse> Results
);
