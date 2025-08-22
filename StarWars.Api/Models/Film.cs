namespace StarWars.Api.Models;

public class Film
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int? EpisodeId { get; set; }
    public string? OpeningCrawl { get; set; }
    public string? Director { get; set; }
    public string? Producer { get; set; }
    public string? ReleaseDate { get; set; }
    public string? Created { get; set; }
    public string? Edited { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação para tabelas de junção
    public ICollection<FilmCharacter> FilmCharacters { get; set; } = new List<FilmCharacter>();
    public ICollection<FilmPlanet> FilmPlanets { get; set; } = new List<FilmPlanet>();
    public ICollection<FilmStarship> FilmStarships { get; set; } = new List<FilmStarship>();
    public ICollection<FilmVehicle> FilmVehicles { get; set; } = new List<FilmVehicle>();
    public ICollection<FilmSpecies> FilmSpecies { get; set; } = new List<FilmSpecies>();
}
