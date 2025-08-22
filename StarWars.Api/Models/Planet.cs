namespace StarWars.Api.Models;

public class Planet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? RotationPeriod { get; set; }
    public string? OrbitalPeriod { get; set; }
    public string? Diameter { get; set; }
    public string? Climate { get; set; }
    public string? Gravity { get; set; }
    public string? Terrain { get; set; }
    public string? SurfaceWater { get; set; }
    public string? Population { get; set; }
    public string? Created { get; set; }
    public string? Edited { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação para tabelas de junção
    public ICollection<FilmPlanet> FilmPlanets { get; set; } = new List<FilmPlanet>();
    public ICollection<PlanetResident> PlanetResidents { get; set; } = new List<PlanetResident>();
    
    // Relacionamento com Person (homeworld)
    public ICollection<Person> HomeworldPeople { get; set; } = new List<Person>();
    
    // Relacionamento com Species (homeworld)
    public ICollection<Species> HomeworldSpecies { get; set; } = new List<Species>();
}
