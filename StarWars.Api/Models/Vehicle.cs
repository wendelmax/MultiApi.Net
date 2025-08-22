namespace StarWars.Api.Models;

public class Vehicle
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }
    public string? CostInCredits { get; set; }
    public string? Length { get; set; }
    public string? MaxAtmospheringSpeed { get; set; }
    public string? Crew { get; set; }
    public string? Passengers { get; set; }
    public string? CargoCapacity { get; set; }
    public string? Consumables { get; set; }
    public string? VehicleClass { get; set; }
    public string? Created { get; set; }
    public string? Edited { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de navegação para tabelas de junção
    public ICollection<FilmVehicle> FilmVehicles { get; set; } = new List<FilmVehicle>();
    public ICollection<PersonVehicle> PersonVehicles { get; set; } = new List<PersonVehicle>();
}
