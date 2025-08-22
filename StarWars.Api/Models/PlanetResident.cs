namespace StarWars.Api.Models;

public class PlanetResident
{
    public int Id { get; set; }
    public int PlanetId { get; set; }
    public int PersonId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Planet Planet { get; set; } = null!;
    public Person Person { get; set; } = null!;
}
