namespace StarWars.Api.Models;

public class FilmStarship
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int StarshipId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Film Film { get; set; } = null!;
    public Starship Starship { get; set; } = null!;
}
