using Microsoft.AspNetCore.Mvc;
using CollectionManager.Api.Models;
using CollectionManager.Api.Services;

namespace CollectionManager.Api.Controllers;

/// <summary>
/// Controlador para gerenciar coleções MongoDB
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly MongoService _mongoService;

    public CollectionsController(MongoService mongoService)
    {
        _mongoService = mongoService;
    }

    /// <summary>
    /// Cria uma nova coleção com uma chave de API única
    /// </summary>
    /// <param name="request">Solicitação de criação da coleção</param>
    /// <returns>Coleção criada com chave de API</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateCollectionResponse), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<CreateCollectionResponse>> CreateCollection(CreateCollectionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Collection name is required");
        }

        var existingCollection = await _mongoService.GetCollectionByNameAsync(request.Name);
        if (existingCollection != null)
        {
            return Conflict($"Collection with name '{request.Name}' already exists");
        }

        var collection = await _mongoService.CreateCollectionAsync(request);
        
        var response = new CreateCollectionResponse
        {
            CollectionId = collection.Id,
            Name = collection.Name,
            ApiKey = collection.ApiKey,
            CreatedAt = collection.CreatedAt,
            Message = "Collection created successfully"
        };

        return CreatedAtAction(nameof(GetCollection), new { id = collection.Id }, response);
    }

    /// <summary>
    /// Obtém todas as coleções
    /// </summary>
    /// <returns>Lista de todas as coleções</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Collection>), 200)]
    public async Task<ActionResult<List<Collection>>> GetCollections()
    {
        var collections = await _mongoService.GetAllCollectionsAsync();
        return Ok(collections);
    }

    /// <summary>
    /// Obtém uma coleção específica por nome
    /// </summary>
    /// <param name="id">Nome da coleção</param>
    /// <returns>Informações da coleção</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Collection), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Collection>> GetCollection(string id)
    {
        var collection = await _mongoService.GetCollectionByNameAsync(id);
        if (collection == null)
        {
            return NotFound();
        }

        return Ok(collection);
    }

    /// <summary>
    /// Exclui uma coleção e todos os seus documentos
    /// </summary>
    /// <param name="id">ID da coleção</param>
    /// <returns>Sem conteúdo em caso de sucesso</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteCollection(string id)
    {
        var success = await _mongoService.DeleteCollectionAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
