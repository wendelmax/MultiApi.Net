using Microsoft.AspNetCore.Mvc;
using CollectionManager.Api.Models;
using CollectionManager.Api.Services;

namespace CollectionManager.Api.Controllers;

/// <summary>
/// Controller for performing CRUD operations on documents within collections
/// </summary>
[ApiController]
[Route("api/documents")]
public class DocumentsController : ControllerBase
{
    private readonly MongoService _mongoService;

    public DocumentsController(MongoService mongoService)
    {
        _mongoService = mongoService;
    }

    /// <summary>
    /// Creates a new document in the specified collection
    /// </summary>
    /// <param name="collectionName">Name of the collection</param>
    /// <param name="data">Document data as key-value pairs</param>
    /// <param name="apiKey">API key for authentication</param>
    /// <returns>Created document information</returns>
    [HttpPost("{collectionName}")]
    [ProducesResponseType(typeof(object), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult<object>> CreateDocument(string collectionName, [FromBody] Dictionary<string, object> data, [FromHeader(Name = "X-API-Key")] string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return Unauthorized("API key is required");
        }

        var collection = await _mongoService.GetCollectionByApiKeyAsync(apiKey);
        if (collection == null || collection.Name != collectionName)
        {
            return Forbid("Invalid API key or collection access denied");
        }

        if (data == null || !data.Any())
        {
            return BadRequest("Document data is required");
        }

        var documentId = await _mongoService.InsertDocumentAsync(collectionName, data);
        
        return CreatedAtAction(nameof(GetDocument), new { collectionName, documentId }, new { id = documentId, message = "Document created successfully" });
    }

    /// <summary>
    /// Retrieves a specific document by ID
    /// </summary>
    /// <param name="collectionName">Name of the collection</param>
    /// <param name="documentId">Document ID</param>
    /// <param name="apiKey">API key for authentication</param>
    /// <returns>Document data</returns>
    [HttpGet("{collectionName}/{documentId}")]
    [ProducesResponseType(typeof(DynamicDocument), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<DynamicDocument>> GetDocument(string collectionName, string documentId, [FromHeader(Name = "X-API-Key")] string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return Unauthorized("API key is required");
        }

        var collection = await _mongoService.GetCollectionByApiKeyAsync(apiKey);
        if (collection == null || collection.Name != collectionName)
        {
            return Forbid("Invalid API key or collection access denied");
        }

        var document = await _mongoService.GetDocumentAsync(collectionName, documentId);
        if (document == null)
        {
            return NotFound();
        }

        return Ok(document);
    }

    /// <summary>
    /// Retrieves documents from a collection with optional filtering
    /// </summary>
    /// <param name="collectionName">Name of the collection</param>
    /// <param name="apiKey">API key for authentication</param>
    /// <param name="filter">Optional filter parameters as query string</param>
    /// <returns>List of documents</returns>
    [HttpGet("{collectionName}")]
    [ProducesResponseType(typeof(List<DynamicDocument>), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult<List<DynamicDocument>>> GetDocuments(string collectionName, [FromHeader(Name = "X-API-Key")] string apiKey, [FromQuery] Dictionary<string, object>? filter)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return Unauthorized("API key is required");
        }

        var collection = await _mongoService.GetCollectionByApiKeyAsync(apiKey);
        if (collection == null || collection.Name != collectionName)
        {
            return Forbid("Invalid API key or collection access denied");
        }

        var documents = await _mongoService.GetDocumentsAsync(collectionName, filter);
        return Ok(documents);
    }

    /// <summary>
    /// Updates an existing document
    /// </summary>
    /// <param name="collectionName">Name of the collection</param>
    /// <param name="documentId">Document ID</param>
    /// <param name="data">Updated document data</param>
    /// <param name="apiKey">API key for authentication</param>
    /// <returns>No content on success</returns>
    [HttpPut("{collectionName}/{documentId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateDocument(string collectionName, string documentId, [FromBody] Dictionary<string, object> data, [FromHeader(Name = "X-API-Key")] string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return Unauthorized("API key is required");
        }

        var collection = await _mongoService.GetCollectionByApiKeyAsync(apiKey);
        if (collection == null || collection.Name != collectionName)
        {
            return Forbid("Invalid API key or collection access denied");
        }

        if (data == null || !data.Any())
        {
            return BadRequest("Document data is required");
        }

        var success = await _mongoService.UpdateDocumentAsync(collectionName, documentId, data);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a document from a collection
    /// </summary>
    /// <param name="collectionName">Name of the collection</param>
    /// <param name="documentId">Document ID</param>
    /// <param name="apiKey">API key for authentication</param>
    /// <returns>No content on success</returns>
    [HttpDelete("{collectionName}/{documentId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteDocument(string collectionName, string documentId, [FromHeader(Name = "X-API-Key")] string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return Unauthorized("API key is required");
        }

        var collection = await _mongoService.GetCollectionByApiKeyAsync(apiKey);
        if (collection == null || collection.Name != collectionName)
        {
            return Forbid("Invalid API key or collection access denied");
        }

        var success = await _mongoService.DeleteDocumentAsync(collectionName, documentId);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
