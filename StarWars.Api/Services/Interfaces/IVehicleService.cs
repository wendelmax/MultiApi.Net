using StarWars.Api.DTOs;

namespace StarWars.Api.Services.Interfaces;

public interface IVehicleService
{
    Task<VehicleListResponse> GetVehiclesAsync(int? page = null, int? pageSize = null);
    Task<VehicleResponse?> GetVehicleByIdAsync(int id);
    Task<VehicleSearchResponse> SearchVehiclesAsync(string term);
    Task<IEnumerable<VehicleResponse>> GetVehiclesByClassAsync(string vehicleClass);
    Task<IEnumerable<VehicleResponse>> GetVehiclesByManufacturerAsync(string manufacturer);
}
