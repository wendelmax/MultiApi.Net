namespace StarWars.Api.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Height { get; set; }
    public string? Mass { get; set; }
    public string? HairColor { get; set; }
    public string? SkinColor { get; set; }
    public string? EyeColor { get; set; }
    public string? BirthYear { get; set; }
    public string? Gender { get; set; }
    public int? HomeworldPlanetId { get; set; }
    public string? Created { get; set; }
    public string? Edited { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Planet? HomeworldPlanet { get; set; }
    
    // Propriedades de navegação para tabelas de junção
    public ICollection<FilmCharacter> FilmCharacters { get; set; } = new List<FilmCharacter>();
    public ICollection<PersonSpecies> PersonSpecies { get; set; } = new List<PersonSpecies>();
    public ICollection<PersonVehicle> PersonVehicles { get; set; } = new List<PersonVehicle>();
    public ICollection<PersonStarship> PersonStarships { get; set; } = new List<PersonStarship>();
    public ICollection<PlanetResident> PlanetResidents { get; set; } = new List<PlanetResident>();
}
