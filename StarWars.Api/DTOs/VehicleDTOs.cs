namespace StarWars.Api.DTOs;

public record VehicleResponse(
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
    string? VehicleClass,
    string? Pilots,
    string? Films,
    DateTime CreatedAt
);

public record VehicleListResponse(
    int Total,
    int Page,
    int PageSize,
    IEnumerable<VehicleResponse> Vehicles
);

public record VehicleSearchResponse(
    int Count,
    IEnumerable<VehicleResponse> Results
);
