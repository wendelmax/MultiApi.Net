using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data.Context;
using StarWars.Api.Models;
using StarWars.Api.Repositories.Interfaces;

namespace StarWars.Api.Repositories.Implementations;

public class VehicleRepository : IVehicleRepository
{
    private readonly StarWarsDbContext _context;

    public VehicleRepository(StarWarsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _context.Vehicles
            .OrderBy(v => v.Name)
            .ToListAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await _context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<IEnumerable<Vehicle>> SearchAsync(string term)
    {
        return await _context.Vehicles
            .Where(v => v.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       v.Model.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                       v.Manufacturer.Contains(term, StringComparison.OrdinalIgnoreCase))
            .OrderBy(v => v.Name)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Vehicles.CountAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetByClassAsync(string vehicleClass)
    {
        return await _context.Vehicles
            .Where(v => v.VehicleClass.Contains(vehicleClass, StringComparison.OrdinalIgnoreCase))
            .OrderBy(v => v.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetByManufacturerAsync(string manufacturer)
    {
        return await _context.Vehicles
            .Where(v => v.Manufacturer.Contains(manufacturer, StringComparison.OrdinalIgnoreCase))
            .OrderBy(v => v.Name)
            .ToListAsync();
    }
}
