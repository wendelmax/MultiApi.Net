namespace StarWars.Api.Models;

public class PersonVehicle
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int VehicleId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação
    public Person Person { get; set; } = null!;
    public Vehicle Vehicle { get; set; } = null!;
}
