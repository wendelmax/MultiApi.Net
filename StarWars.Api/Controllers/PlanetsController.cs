using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using StarWars.Api.Services.Interfaces;
using StarWars.Api.DTOs;

namespace StarWars.Api.Controllers;

/// <summary>
/// Endpoints para consultar planetas do banco local.
/// </summary>
[ApiController]
[Route("v1/planetas")]
[Produces("application/json")]
[SwaggerTag("Planetas")]
public class PlanetsController : ControllerBase
{
    private readonly IPlanetService _planetService;

    public PlanetsController(IPlanetService planetService)
    {
        _planetService = planetService;
    }

    /// <summary>
    /// Lista todos os planetas do banco local.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Lista planetas")]
    public async Task<IActionResult> GetAll([FromQuery] int? page = null, [FromQuery] int? pageSize = null)
    {
        var result = await _planetService.GetPlanetsAsync(page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Obtém um planeta específico por id do banco local.
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Planeta por id")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var planet = await _planetService.GetPlanetByIdAsync(id);
        
        if (planet == null)
            return NotFound(new { error = "Planeta não encontrado" });

        return Ok(planet);
    }

    /// <summary>
    /// Pesquisa planetas por termo no banco local.
    /// </summary>
    /// <param name="termo">Termo de busca (case-insensitive).</param>
    [HttpGet("buscar")]
    [SwaggerOperation(Summary = "Busca planetas")]
    public async Task<IActionResult> Search([FromQuery(Name = "termo")] string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return BadRequest(new { error = "Parâmetro 'termo' é obrigatório." });
        }

        var result = await _planetService.SearchPlanetsAsync(termo);
        return Ok(result);
    }

    /// <summary>
    /// Busca planetas por clima.
    /// </summary>
    /// <param name="clima">Clima para filtrar.</param>
    [HttpGet("clima/{clima}")]
    [SwaggerOperation(Summary = "Planetas por clima")]
    public async Task<IActionResult> GetByClimate([FromRoute] string clima)
    {
        var planets = await _planetService.GetPlanetsByClimateAsync(clima);
        return Ok(new { count = planets.Count(), results = planets });
    }

    /// <summary>
    /// Busca planetas por terreno.
    /// </summary>
    /// <param name="terreno">Terreno para filtrar.</param>
    [HttpGet("terreno/{terreno}")]
    [SwaggerOperation(Summary = "Planetas por terreno")]
    public async Task<IActionResult> GetByTerrain([FromRoute] string terreno)
    {
        var planets = await _planetService.GetPlanetsByTerrainAsync(terreno);
        return Ok(new { count = planets.Count(), results = planets });
    }
}
