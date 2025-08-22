using StarWars.Api.DTOs;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;
using StarWars.Api.Services.Interfaces;

namespace StarWars.Api.Services.Implementations;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<VehicleListResponse> GetVehiclesAsync(int? page = null, int? pageSize = null)
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        var total = await _vehicleRepository.GetCountAsync();

        var vehicleResponses = vehicles.Select(MapToVehicleResponse);

        var currentPage = page ?? 1;
        var currentPageSize = pageSize ?? vehicles.Count();

        var pagedVehicles = vehicleResponses
            .Skip((currentPage - 1) * currentPageSize)
            .Take(currentPageSize);

        return new VehicleListResponse(total, currentPage, currentPageSize, pagedVehicles);
    }

    public async Task<VehicleResponse?> GetVehicleByIdAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        return vehicle != null ? MapToVehicleResponse(vehicle) : null;
    }

    public async Task<VehicleSearchResponse> SearchVehiclesAsync(string term)
    {
        var vehicles = await _vehicleRepository.SearchAsync(term);
        var vehicleResponses = vehicles.Select(MapToVehicleResponse);

        return new VehicleSearchResponse(vehicles.Count(), vehicleResponses);
    }

    public async Task<IEnumerable<VehicleResponse>> GetVehiclesByClassAsync(string vehicleClass)
    {
        var vehicles = await _vehicleRepository.GetByClassAsync(vehicleClass);
        return vehicles.Select(MapToVehicleResponse);
    }

    public async Task<IEnumerable<VehicleResponse>> GetVehiclesByManufacturerAsync(string manufacturer)
    {
        var vehicles = await _vehicleRepository.GetByManufacturerAsync(manufacturer);
        return vehicles.Select(MapToVehicleResponse);
    }

    private static VehicleResponse MapToVehicleResponse(Vehicle vehicle)
    {
        // Extrair IDs das tabelas de junção
        var pilotIds = vehicle.PersonVehicles?.Select(pv => pv.PersonId).ToList() ?? new List<int>();
        var filmIds = vehicle.FilmVehicles?.Select(fv => fv.FilmId).ToList() ?? new List<int>();

        return new VehicleResponse(
            vehicle.Id,
            vehicle.Name,
            vehicle.Model,
            vehicle.Manufacturer,
            vehicle.CostInCredits,
            vehicle.Length,
            vehicle.MaxAtmospheringSpeed,
            vehicle.Crew,
            vehicle.Passengers,
            vehicle.CargoCapacity,
            vehicle.Consumables,
            vehicle.VehicleClass,
            string.Join(",", pilotIds),
            string.Join(",", filmIds),
            vehicle.CreatedAt
        );
    }
}
