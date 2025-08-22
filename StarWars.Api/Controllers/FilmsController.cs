using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using StarWars.Api.Services.Interfaces;
using StarWars.Api.DTOs;

namespace StarWars.Api.Controllers;

/// <summary>
/// Endpoints para consultar filmes da SWAPI.
/// </summary>
[ApiController]
[Route("v1/filmes")]
[Produces("application/json")]
[SwaggerTag("Filmes")]
public class FilmsController : ControllerBase
{
    private readonly IFilmService _filmService;

    public FilmsController(IFilmService filmService)
    {
        _filmService = filmService;
    }

    // Método removido - agora usamos apenas banco local

    /// <summary>
    /// Lista todos os filmes do banco local.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Lista filmes")]
    public async Task<IActionResult> GetAll()
    {
        var query = new FilmQueryParameters();
        var result = await _filmService.GetFilmsAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// Obtém um filme específico por id do banco local.
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Filme por id")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var film = await _filmService.GetFilmByIdAsync(id);
        
        if (film == null)
            return NotFound(new { error = "Filme não encontrado" });

        return Ok(film);
    }

    /// <summary>
    /// Pesquisa filmes por termo no banco local.
    /// </summary>
    /// <param name="termo">Termo de busca (case-insensitive).</param>
    [HttpGet("buscar")]
    [SwaggerOperation(Summary = "Busca filmes")]
    public async Task<IActionResult> Search([FromQuery(Name = "termo")] string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return BadRequest(new { error = "Parâmetro 'termo' é obrigatório." });
        }

        var result = await _filmService.SearchFilmsAsync(termo);
        return Ok(result);
    }

    /// <summary>
    /// Filtra filmes com suporte a paginação e ordenação locais.
    /// </summary>
    /// <param name="termo">Termo de busca (opcional).</param>
    /// <param name="pagina">Página desejada (opcional).</param>
    /// <param name="tamanhoPagina">Tamanho da página aplicado localmente (opcional).</param>
    /// <param name="ordenarPor">Campo para ordenar localmente (ex.: title, episode_id) (opcional).</param>
    /// <param name="ordenarDirecao">Direção de ordenação: asc ou desc (padrão asc).</param>
    [HttpGet("filtrar")]
    [SwaggerOperation(Summary = "Filtra filmes", Description = "Filtra filmes com paginação e ordenação locais. Retorna envelope padronizado.")]
    public async Task<IActionResult> Filter(
        [FromQuery(Name = "termo")] string? termo,
        [FromQuery(Name = "pagina")] int? pagina = null,
        [FromQuery(Name = "tamanhoPagina")] int? tamanhoPagina = null,
        [FromQuery(Name = "ordenarPor")] string? ordenarPor = null,
        [FromQuery(Name = "ordenarDirecao")] string? ordenarDirecao = "asc")
    {
        var query = new FilmQueryParameters(termo, pagina, tamanhoPagina, ordenarPor, ordenarDirecao);
        var result = await _filmService.GetFilmsAsync(query);
        return Ok(result);
    }
}


