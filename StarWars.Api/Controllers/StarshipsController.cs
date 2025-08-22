using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using StarWars.Api.Services.Interfaces;
using StarWars.Api.DTOs;

namespace StarWars.Api.Controllers;

/// <summary>
/// Endpoints para consultar naves estelares do banco local.
/// </summary>
[ApiController]
[Route("v1/naves")]
[Produces("application/json")]
[SwaggerTag("Naves Estelares")]
public class StarshipsController : ControllerBase
{
    private readonly IStarshipService _starshipService;

    public StarshipsController(IStarshipService starshipService)
    {
        _starshipService = starshipService;
    }

    /// <summary>
    /// Lista todas as naves estelares do banco local.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Lista naves estelares")]
    public async Task<IActionResult> GetAll([FromQuery] int? page = null, [FromQuery] int? pageSize = null)
    {
        var result = await _starshipService.GetStarshipsAsync(page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Obtém uma nave estelar específica por id do banco local.
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Nave estelar por id")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var starship = await _starshipService.GetStarshipByIdAsync(id);
        
        if (starship == null)
            return NotFound(new { error = "Nave estelar não encontrada" });

        return Ok(starship);
    }

    /// <summary>
    /// Pesquisa naves estelares por termo no banco local.
    /// </summary>
    /// <param name="termo">Termo de busca (case-insensitive).</param>
    [HttpGet("buscar")]
    [SwaggerOperation(Summary = "Busca naves estelares")]
    public async Task<IActionResult> Search([FromQuery(Name = "termo")] string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return BadRequest(new { error = "Parâmetro 'termo' é obrigatório." });
        }

        var result = await _starshipService.SearchStarshipsAsync(termo);
        return Ok(result);
    }

    /// <summary>
    /// Busca naves estelares por classe.
    /// </summary>
    /// <param name="classe">Classe da nave para filtrar.</param>
    [HttpGet("classe/{classe}")]
    [SwaggerOperation(Summary = "Naves estelares por classe")]
    public async Task<IActionResult> GetByClass([FromRoute] string classe)
    {
        var starships = await _starshipService.GetStarshipsByClassAsync(classe);
        return Ok(new { count = starships.Count(), results = starships });
    }

    /// <summary>
    /// Busca naves estelares por fabricante.
    /// </summary>
    /// <param name="fabricante">Fabricante para filtrar.</param>
    [HttpGet("fabricante/{fabricante}")]
    [SwaggerOperation(Summary = "Naves estelares por fabricante")]
    public async Task<IActionResult> GetByManufacturer([FromRoute] string fabricante)
    {
        var starships = await _starshipService.GetStarshipsByManufacturerAsync(fabricante);
        return Ok(new { count = starships.Count(), results = starships });
    }
}
