namespace StarWars.Api.Models;

public class PersonStarship
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int StarshipId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Person Person { get; set; } = null!;
    public Starship Starship { get; set; } = null!;
}
