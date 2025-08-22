using Microsoft.AspNetCore.Mvc;

namespace StarWars.Api.Controllers;

/// <summary>
/// Raiz da API de estudos. Redireciona para documentação e mapeia recursos disponíveis.
/// </summary>
[ApiController]
[Route("/")]
public class RootController : ControllerBase
{
    /// <summary>
    /// Apresenta links para recursos e documentação.
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        return Ok(new
        {
            documentacao = $"{baseUrl}/swagger",
            recursos = new
            {
                filmes = $"{baseUrl}/v1/filmes",
                pessoas = $"{baseUrl}/v1/pessoas",
                planetas = $"{baseUrl}/v1/planetas",
                naves = $"{baseUrl}/v1/naves",
                veiculos = $"{baseUrl}/v1/veiculos",
                especies = $"{baseUrl}/v1/especies"
            },
            status = "API local - dados do universo Star Wars",
            total_endpoints = 6,
            nota = "Todos os endpoints implementados e funcionando!"
        });
    }
}


