namespace StarWars.Api.Models;

public class Species
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Classification { get; set; }
    public string? Designation { get; set; }
    public string? AverageHeight { get; set; }
    public string? SkinColors { get; set; }
    public string? HairColors { get; set; }
    public string? EyeColors { get; set; }
    public string? AverageLifespan { get; set; }
    public int? HomeworldPlanetId { get; set; }
    public string? Language { get; set; }
    public string? Created { get; set; }
    public string? Edited { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Planet? HomeworldPlanet { get; set; }
    
    // Propriedades de navegação para tabelas de junção
    public ICollection<FilmSpecies> FilmSpecies { get; set; } = new List<FilmSpecies>();
    public ICollection<PersonSpecies> PersonSpecies { get; set; } = new List<PersonSpecies>();
}
