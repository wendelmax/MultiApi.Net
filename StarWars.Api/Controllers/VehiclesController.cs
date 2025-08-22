using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using StarWars.Api.Services.Interfaces;
using StarWars.Api.DTOs;

namespace StarWars.Api.Controllers;

/// <summary>
/// Endpoints para consultar veículos do banco local.
/// </summary>
[ApiController]
[Route("v1/veiculos")]
[Produces("application/json")]
[SwaggerTag("Veículos")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    /// <summary>
    /// Lista todos os veículos do banco local.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Lista veículos")]
    public async Task<IActionResult> GetAll([FromQuery] int? page = null, [FromQuery] int? pageSize = null)
    {
        var result = await _vehicleService.GetVehiclesAsync(page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Obtém um veículo específico por id do banco local.
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Veículo por id")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        
        if (vehicle == null)
            return NotFound(new { error = "Veículo não encontrado" });

        return Ok(vehicle);
    }

    /// <summary>
    /// Pesquisa veículos por termo no banco local.
    /// </summary>
    /// <param name="termo">Termo de busca (case-insensitive).</param>
    [HttpGet("buscar")]
    [SwaggerOperation(Summary = "Busca veículos")]
    public async Task<IActionResult> Search([FromQuery(Name = "termo")] string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return BadRequest(new { error = "Parâmetro 'termo' é obrigatório." });
        }

        var result = await _vehicleService.SearchVehiclesAsync(termo);
        return Ok(result);
    }

    /// <summary>
    /// Busca veículos por classe.
    /// </summary>
    /// <param name="classe">Classe do veículo para filtrar.</param>
    [HttpGet("classe/{classe}")]
    [SwaggerOperation(Summary = "Veículos por classe")]
    public async Task<IActionResult> GetByClass([FromRoute] string classe)
    {
        var vehicles = await _vehicleService.GetVehiclesByClassAsync(classe);
        return Ok(new { count = vehicles.Count(), results = vehicles });
    }

    /// <summary>
    /// Busca veículos por fabricante.
    /// </summary>
    /// <param name="fabricante">Fabricante para filtrar.</param>
    [HttpGet("fabricante/{fabricante}")]
    [SwaggerOperation(Summary = "Veículos por fabricante")]
    public async Task<IActionResult> GetByManufacturer([FromRoute] string fabricante)
    {
        var vehicles = await _vehicleService.GetVehiclesByManufacturerAsync(fabricante);
        return Ok(new { count = vehicles.Count(), results = vehicles });
    }
}
