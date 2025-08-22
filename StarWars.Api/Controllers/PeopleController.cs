using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using StarWars.Api.Services.Interfaces;
using StarWars.Api.DTOs;

namespace StarWars.Api.Controllers;

/// <summary>
/// Endpoints para consultar pessoas (personagens) do banco local.
/// </summary>
[ApiController]
[Route("v1/pessoas")]
[Produces("application/json")]
[SwaggerTag("Pessoas")]
public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService)
    {
        _personService = personService;
    }

    /// <summary>
    /// Lista todas as pessoas do banco local.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Lista pessoas")]
    public async Task<IActionResult> GetAll([FromQuery] int? page = null, [FromQuery] int? pageSize = null)
    {
        var result = await _personService.GetPeopleAsync(page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Obtém uma pessoa específica por id do banco local.
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Pessoa por id")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        
        if (person == null)
            return NotFound(new { error = "Pessoa não encontrada" });

        return Ok(person);
    }

    /// <summary>
    /// Pesquisa pessoas por termo no banco local.
    /// </summary>
    /// <param name="termo">Termo de busca (case-insensitive).</param>
    [HttpGet("buscar")]
    [SwaggerOperation(Summary = "Busca pessoas")]
    public async Task<IActionResult> Search([FromQuery(Name = "termo")] string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return BadRequest(new { error = "Parâmetro 'termo' é obrigatório." });
        }

        var result = await _personService.SearchPeopleAsync(termo);
        return Ok(result);
    }

    /// <summary>
    /// Busca pessoas por espécie.
    /// </summary>
    /// <param name="especie">Espécie para filtrar.</param>
    [HttpGet("especie/{especie}")]
    [SwaggerOperation(Summary = "Pessoas por espécie")]
    public async Task<IActionResult> GetBySpecies([FromRoute] string especie)
    {
        var people = await _personService.GetPeopleBySpeciesAsync(especie);
        return Ok(new { count = people.Count(), results = people });
    }

    /// <summary>
    /// Busca pessoas por planeta natal.
    /// </summary>
    /// <param name="planeta">Planeta natal para filtrar.</param>
    [HttpGet("planeta/{planeta}")]
    [SwaggerOperation(Summary = "Pessoas por planeta natal")]
    public async Task<IActionResult> GetByHomeworld([FromRoute] string planeta)
    {
        var people = await _personService.GetPeopleByHomeworldAsync(planeta);
        return Ok(new { count = people.Count(), results = people });
    }
}
