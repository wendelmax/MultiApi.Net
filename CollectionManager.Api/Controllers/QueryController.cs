using Microsoft.AspNetCore.Mvc;
using CollectionManager.Api.Models;
using CollectionManager.Api.Services;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CollectionManager.Api.Controllers;

/// <summary>
/// Controller para executar consultas MongoDB personalizadas
/// </summary>
[ApiController]
[Route("api/query")]
public class QueryController : ControllerBase
{
    private readonly MongoService _mongoService;

    public QueryController(MongoService mongoService)
    {
        _mongoService = mongoService;
    }

    /// <summary>
    /// Executa consultas MongoDB personalizadas na coleção especificada
    /// </summary>
    /// <param name="collectionName">Nome da coleção</param>
    /// <param name="request">Requisição de consulta</param>
    /// <param name="apiKey">Chave de API para autenticação</param>
    /// <returns>Resultado da consulta</returns>
    [HttpPost("{collectionName}")]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult<object>> ExecuteQuery(string collectionName, [FromBody] QueryRequest request, [FromHeader(Name = "X-API-Key")] string apiKey)
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

        if (request == null)
        {
            return BadRequest("Query request is required");
        }

        try
        {
            var result = await ExecuteMongoQuery(collectionName, request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Query execution failed: {ex.Message}");
        }
    }

    private async Task<object> ExecuteMongoQuery(string collectionName, QueryRequest request)
    {
        var database = GetMongoDatabase();
        var collection = database.GetCollection<BsonDocument>(collectionName);

        switch (request.Operation?.ToLower())
        {
            case "find":
                return await ExecuteFind(collection, request);
            case "aggregate":
                return await ExecuteAggregate(collection, request);
            case "count":
                return await ExecuteCount(collection, request);
            case "distinct":
                return await ExecuteDistinct(collection, request);
            default:
                throw new ArgumentException($"Unsupported operation: {request.Operation}");
        }
    }

    private async Task<object> ExecuteFind(IMongoCollection<BsonDocument> collection, QueryRequest request)
    {
        var filter = request.Filter != null ? BsonDocument.Parse(request.Filter) : new BsonDocument();
        var sort = request.Sort != null ? BsonDocument.Parse(request.Sort) : new BsonDocument();
        var projection = request.Projection != null ? BsonDocument.Parse(request.Projection) : new BsonDocument();
        
        var limit = request.Limit ?? 100;
        var skip = request.Skip ?? 0;

        var cursor = collection.Find(filter)
            .Sort(sort)
            .Project(projection)
            .Skip(skip)
            .Limit(limit);

        var documents = await cursor.ToListAsync();
        return documents.Select(doc => BsonTypeMapper.MapToDotNetValue(doc));
    }

    private async Task<object> ExecuteAggregate(IMongoCollection<BsonDocument> collection, QueryRequest request)
    {
        if (request.Pipeline == null)
        {
            throw new ArgumentException("Pipeline is required for aggregate operation");
        }

        var pipeline = request.Pipeline.Select(stage => BsonDocument.Parse(stage)).ToList();
        var cursor = collection.Aggregate<BsonDocument>(pipeline);
        var documents = await cursor.ToListAsync();
        return documents.Select(doc => BsonTypeMapper.MapToDotNetValue(doc));
    }

    private async Task<object> ExecuteCount(IMongoCollection<BsonDocument> collection, QueryRequest request)
    {
        var filter = request.Filter != null ? BsonDocument.Parse(request.Filter) : new BsonDocument();
        var count = await collection.CountDocumentsAsync(filter);
        return new { count };
    }

    private async Task<object> ExecuteDistinct(IMongoCollection<BsonDocument> collection, QueryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Field))
        {
            throw new ArgumentException("Field is required for distinct operation");
        }

        var filter = request.Filter != null ? BsonDocument.Parse(request.Filter) : new BsonDocument();
        var distinctValues = await collection.DistinctAsync<string>(request.Field, filter);
        var values = await distinctValues.ToListAsync();
        return values;
    }

    private IMongoDatabase GetMongoDatabase()
    {
        var connectionString = _mongoService.GetConnectionString();
        var client = new MongoClient(connectionString);
        return client.GetDatabase("CollectionManager");
    }
}

/// <summary>
/// Modelo para requisições de consulta personalizada
/// </summary>
public class QueryRequest
{
    /// <summary>
    /// Operação a ser executada (find, aggregate, count, distinct)
    /// </summary>
    public string? Operation { get; set; }
    
    /// <summary>
    /// Filtro MongoDB em formato JSON
    /// </summary>
    public string? Filter { get; set; }
    
    /// <summary>
    /// Ordenação MongoDB em formato JSON
    /// </summary>
    public string? Sort { get; set; }
    
    /// <summary>
    /// Projeção MongoDB em formato JSON
    /// </summary>
    public string? Projection { get; set; }
    
    /// <summary>
    /// Campo para operação distinct
    /// </summary>
    public string? Field { get; set; }
    
    /// <summary>
    /// Pipeline de agregação MongoDB
    /// </summary>
    public List<string>? Pipeline { get; set; }
    
    /// <summary>
    /// Limite de documentos retornados
    /// </summary>
    public int? Limit { get; set; }
    
    /// <summary>
    /// Número de documentos para pular
    /// </summary>
    public int? Skip { get; set; }
}
