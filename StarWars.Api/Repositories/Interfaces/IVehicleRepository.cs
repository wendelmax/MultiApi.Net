using StarWars.Api.Models;

namespace StarWars.Api.Repositories.Interfaces;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<IEnumerable<Vehicle>> SearchAsync(string term);
    Task<int> GetCountAsync();
    Task<IEnumerable<Vehicle>> GetByClassAsync(string vehicleClass);
    Task<IEnumerable<Vehicle>> GetByManufacturerAsync(string manufacturer);
}
