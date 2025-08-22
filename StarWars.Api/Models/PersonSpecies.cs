namespace StarWars.Api.Models;

public class PersonSpecies
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int SpeciesId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Person Person { get; set; } = null!;
    public Species Species { get; set; } = null!;
}
