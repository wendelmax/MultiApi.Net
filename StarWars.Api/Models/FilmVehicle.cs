namespace StarWars.Api.Models;

public class FilmVehicle
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int VehicleId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Film Film { get; set; } = null!;
    public Vehicle Vehicle { get; set; } = null!;
}
