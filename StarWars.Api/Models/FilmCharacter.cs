namespace StarWars.Api.Models;

public class FilmCharacter
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int PersonId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Film Film { get; set; } = null!;
    public Person Person { get; set; } = null!;
}
