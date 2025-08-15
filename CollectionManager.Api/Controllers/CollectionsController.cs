using Microsoft.AspNetCore.Mvc;
using CollectionManager.Api.Models;
using CollectionManager.Api.Services;

namespace CollectionManager.Api.Controllers;

/// <summary>
/// Controller for managing MongoDB collections
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
    /// Creates a new collection with a unique API key
    /// </summary>
    /// <param name="request">Collection creation request</param>
    /// <returns>Created collection with API key</returns>
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
    /// Gets all collections
    /// </summary>
    /// <returns>List of all collections</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Collection>), 200)]
    public async Task<ActionResult<List<Collection>>> GetCollections()
    {
        var collections = await _mongoService.GetAllCollectionsAsync();
        return Ok(collections);
    }

    /// <summary>
    /// Gets a specific collection by name
    /// </summary>
    /// <param name="id">Collection name</param>
    /// <returns>Collection information</returns>
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
    /// Deletes a collection and all its documents
    /// </summary>
    /// <param name="id">Collection ID</param>
    /// <returns>No content on success</returns>
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
