namespace StarWars.Api.DTOs;

public record StarshipResponse(
    int Id,
    string Name,
    string? Model,
    string? Manufacturer,
    string? CostInCredits,
    string? Length,
    string? MaxAtmospheringSpeed,
    string? Crew,
    string? Passengers,
    string? CargoCapacity,
    string? Consumables,
    string? HyperdriveRating,
    string? MGLT,
    string? StarshipClass,
    string? Pilots,
    string? Films,
    DateTime CreatedAt
);

public record StarshipListResponse(
    int Total,
    int Page,
    int PageSize,
    IEnumerable<StarshipResponse> Starships
);

public record StarshipSearchResponse(
    int Count,
    IEnumerable<StarshipResponse> Results
);
