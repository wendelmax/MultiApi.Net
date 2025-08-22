using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using StarWars.Api.Services.Interfaces;
using StarWars.Api.DTOs;

namespace StarWars.Api.Controllers;

/// <summary>
/// Endpoints para consultar espécies do banco local.
/// </summary>
[ApiController]
[Route("v1/especies")]
[Produces("application/json")]
[SwaggerTag("Espécies")]
public class SpeciesController : ControllerBase
{
    private readonly ISpeciesService _speciesService;

    public SpeciesController(ISpeciesService speciesService)
    {
        _speciesService = speciesService;
    }

    /// <summary>
    /// Lista todas as espécies do banco local.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Lista espécies")]
    public async Task<IActionResult> GetAll([FromQuery] int? page = null, [FromQuery] int? pageSize = null)
    {
        var result = await _speciesService.GetSpeciesAsync(page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Obtém uma espécie específica por id do banco local.
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Espécie por id")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var species = await _speciesService.GetSpeciesByIdAsync(id);
        
        if (species == null)
            return NotFound(new { error = "Espécie não encontrada" });

        return Ok(species);
    }

    /// <summary>
    /// Pesquisa espécies por termo no banco local.
    /// </summary>
    /// <param name="termo">Termo de busca (case-insensitive).</param>
    [HttpGet("buscar")]
    [SwaggerOperation(Summary = "Busca espécies")]
    public async Task<IActionResult> Search([FromQuery(Name = "termo")] string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return BadRequest(new { error = "Parâmetro 'termo' é obrigatório." });
        }

        var result = await _speciesService.SearchSpeciesAsync(termo);
        return Ok(result);
    }

    /// <summary>
    /// Busca espécies por classificação.
    /// </summary>
    /// <param name="classificacao">Classificação para filtrar.</param>
    [HttpGet("classificacao/{classificacao}")]
    [SwaggerOperation(Summary = "Espécies por classificação")]
    public async Task<IActionResult> GetByClassification([FromRoute] string classificacao)
    {
        var species = await _speciesService.GetSpeciesByClassificationAsync(classificacao);
        return Ok(new { count = species.Count(), results = species });
    }

    /// <summary>
    /// Busca espécies por designação.
    /// </summary>
    /// <param name="designacao">Designação para filtrar.</param>
    [HttpGet("designacao/{designacao}")]
    [SwaggerOperation(Summary = "Espécies por designação")]
    public async Task<IActionResult> GetByDesignation([FromRoute] string designacao)
    {
        var species = await _speciesService.GetSpeciesByDesignationAsync(designacao);
        return Ok(new { count = species.Count(), results = species });
    }
}
