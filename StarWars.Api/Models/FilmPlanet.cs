namespace StarWars.Api.Models;

public class FilmPlanet
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int PlanetId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Film Film { get; set; } = null!;
    public Planet Planet { get; set; } = null!;
}
