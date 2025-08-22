using Microsoft.AspNetCore.Mvc;
using StarWars.Api.Services.Interfaces;

namespace StarWars.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly IDatabaseSeedService _seedService;

    public DatabaseController(IDatabaseSeedService seedService)
    {
        _seedService = seedService;
    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedDatabase()
    {
        try
        {
            await _seedService.SeedAsync();
            return Ok(new { message = "Banco de dados populado com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetDatabaseStatus()
    {
        try
        {
            var isSeeded = await _seedService.IsDatabaseSeededAsync();
            var count = await _seedService.GetSeedCountAsync();
            
            return Ok(new
            {
                isSeeded,
                entityCount = count,
                status = isSeeded ? "Populado" : "Vazio"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
