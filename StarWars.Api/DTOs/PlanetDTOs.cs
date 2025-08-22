namespace StarWars.Api.DTOs;

public record PlanetResponse(
    int Id,
    string Name,
    string? RotationPeriod,
    string? OrbitalPeriod,
    string? Diameter,
    string? Climate,
    string? Gravity,
    string? Terrain,
    string? SurfaceWater,
    string? Population,
    string? Residents,
    string? Films,
    DateTime CreatedAt
);

public record PlanetListResponse(
    int Total,
    int Page,
    int PageSize,
    IEnumerable<PlanetResponse> Planets
);

public record PlanetSearchResponse(
    int Count,
    IEnumerable<PlanetResponse> Results
);
