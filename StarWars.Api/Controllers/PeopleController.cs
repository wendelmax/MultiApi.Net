using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using Swashbuckle.AspNetCore.Annotations;
using StarWars.Api.Services;
using Microsoft.Extensions.Configuration;

namespace StarWars.Api.Controllers;

/// <summary>
/// Endpoints para consultar pessoas (personagens) da SWAPI.
/// </summary>
[ApiController]
[Route("v1/pessoas")]
[Produces("application/json")]
[SwaggerTag("Pessoas")]
public class PeopleController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IApiCacheService _cache;
    private readonly IConfiguration _config;

    public PeopleController(IHttpClientFactory httpClientFactory, IApiCacheService cache, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _config = config;
    }

    private async Task<(string Content, string ContentType)> GetUpstreamWithCacheAsync(string path)
    {
        var cacheKey = $"people::{path}";
        var (hit, payload, contentType) = await _cache.TryGetAsync(cacheKey);
        if (hit) return (payload!, contentType);

        var client = _httpClientFactory.CreateClient("swapi");
        var response = await client.GetAsync(path);
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var ttlSec = _config.GetValue<int>("Cache:SecondsToLive", 3600);
            await _cache.SetAsync(cacheKey, content, "application/json", TimeSpan.FromSeconds(ttlSec));
        }
        return (content, "application/json");
    }

    /// <summary>
    /// Lista pessoas com paginação nativa da SWAPI.
    /// </summary>
    /// <param name="pagina">Página desejada (opcional).</param>
    [HttpGet]
    [SwaggerOperation(Summary = "Lista pessoas", Description = "Lista pessoas com paginação nativa da SWAPI.")]
    public async Task<IActionResult> GetAll([FromQuery(Name = "pagina")] int? pagina = null)
    {
        var url = pagina is null ? "people/" : $"people/?page={pagina}";
        var (content, contentType) = await GetUpstreamWithCacheAsync(url);
        return Content(content, contentType);
    }

    /// <summary>
    /// Obtém uma pessoa específica por id.
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Pessoa por id")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var url = $"people/{id}/";
        var (content, contentType) = await GetUpstreamWithCacheAsync(url);
        return Content(content, contentType);
    }

    /// <summary>
    /// Pesquisa pessoas por termo.
    /// </summary>
    /// <param name="termo">Termo de busca (case-insensitive).</param>
    [HttpGet("buscar")]
    [SwaggerOperation(Summary = "Busca pessoas", Description = "Pesquisa pessoas por termo (case-insensitive).")]
    public async Task<IActionResult> Search([FromQuery(Name = "termo")] string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return BadRequest(new { error = "Parâmetro q é obrigatório." });
        }

        var url = $"people/?search={Uri.EscapeDataString(termo)}";
        var (content, contentType) = await GetUpstreamWithCacheAsync(url);
        return Content(content, contentType);
    }

    /// <summary>
    /// Filtra pessoas com suporte a paginação e ordenação locais.
    /// </summary>
    /// <param name="termo">Termo de busca (opcional).</param>
    /// <param name="pagina">Página desejada (opcional).</param>
    /// <param name="tamanhoPagina">Tamanho da página aplicado localmente (opcional).</param>
    /// <param name="ordenarPor">Campo para ordenar localmente (ex.: name, height) (opcional).</param>
    /// <param name="ordenarDirecao">Direção de ordenação: asc ou desc (padrão asc).</param>
    [HttpGet("filtrar")]
    [SwaggerOperation(Summary = "Filtra pessoas", Description = "Filtra pessoas com paginação e ordenação locais (campos de string). Retorna envelope padronizado.")]
    public async Task<IActionResult> Filter(
        [FromQuery(Name = "termo")] string? termo,
        [FromQuery(Name = "pagina")] int? pagina = null,
        [FromQuery(Name = "tamanhoPagina")] int? tamanhoPagina = null,
        [FromQuery(Name = "ordenarPor")] string? ordenarPor = null,
        [FromQuery(Name = "ordenarDirecao")] string? ordenarDirecao = "asc")
    {
        var client = _httpClientFactory.CreateClient("swapi");
        var queryParts = new List<string>();
        if (pagina is not null) queryParts.Add($"page={pagina}");
        if (!string.IsNullOrWhiteSpace(termo)) queryParts.Add($"search={Uri.EscapeDataString(termo)}");
        var url = "people/" + (queryParts.Count > 0 ? "?" + string.Join("&", queryParts) : string.Empty);

        var (text, _) = await GetUpstreamWithCacheAsync(url);

        using var doc = JsonDocument.Parse(text);
        var root = doc.RootElement;
        var count = root.TryGetProperty("count", out var countProp) ? countProp.GetInt32() : 0;
        var results = root.TryGetProperty("results", out var resultsProp) ? resultsProp.EnumerateArray().ToList() : new List<JsonElement>();

        if (!string.IsNullOrWhiteSpace(ordenarPor))
        {
            var key = ordenarPor.Trim();
            results = results
                .OrderBy(e => e.TryGetProperty(key, out var p) ? p.ToString() : null, StringComparer.OrdinalIgnoreCase)
                .ToList();
            if (string.Equals(ordenarDirecao, "desc", StringComparison.OrdinalIgnoreCase))
            {
                results.Reverse();
            }
        }

        if (tamanhoPagina is not null && tamanhoPagina.Value > 0)
        {
            results = results.Take(tamanhoPagina.Value).ToList();
        }

        var itensArray = new JsonArray();
        foreach (var item in results)
        {
            itensArray.Add(JsonNode.Parse(item.GetRawText()));
        }

        var shaped = new JsonObject
        {
            ["total"] = count,
            ["pagina"] = pagina ?? 1,
            ["tamanhoPagina"] = tamanhoPagina ?? results.Count,
            ["ordenarPor"] = ordenarPor ?? string.Empty,
            ["ordenarDirecao"] = ordenarDirecao ?? "asc",
            ["itens"] = itensArray
        };

        return Content(shaped.ToJsonString(), "application/json");
    }
}


