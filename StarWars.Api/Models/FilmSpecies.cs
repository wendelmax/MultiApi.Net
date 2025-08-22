namespace StarWars.Api.Models;

public class FilmSpecies
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int SpeciesId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Film Film { get; set; } = null!;
    public Species Species { get; set; } = null!;
}
