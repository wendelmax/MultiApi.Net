using MongoDB.Driver;
using CollectionManager.Api.Models;

namespace CollectionManager.Api.Services;

public class MongoService
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Collection> _collections;

    private static string? GetDatabaseNameFromConnectionString(string connectionString)
    {
        try
        {
            var uri = new Uri(connectionString);
            var databaseName = uri.AbsolutePath.TrimStart('/');
            return string.IsNullOrEmpty(databaseName) ? null : databaseName;
        }
        catch
        {
            return null;
        }
    }
    private readonly IConfiguration _configuration;

    public MongoService(IConfiguration configuration)
    {
        _configuration = configuration;
        
        // Prioriza variável de ambiente, depois configuração, depois fallback
        var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__MONGODB") 
            ?? configuration.GetConnectionString("MongoDB") 
            ?? "mongodb://localhost:27017";
            
        var client = new MongoClient(connectionString);
        
        // Extrair o nome do database da connection string ou usar padrão
        var databaseName = GetDatabaseNameFromConnectionString(connectionString) ?? "multiapi";
        _database = client.GetDatabase(databaseName);
        _collections = _database.GetCollection<Collection>("Collections");
    }

    public async Task<Collection> CreateCollectionAsync(CreateCollectionRequest request)
    {
        var collection = new Collection
        {
            Name = request.Name,
            Description = request.Description,
            Owner = request.Owner,
            ApiKey = GenerateApiKey(),
            CreatedAt = DateTime.UtcNow
        };

        await _collections.InsertOneAsync(collection);
        
        await _database.CreateCollectionAsync(request.Name);
        
        return collection;
    }

    public async Task<Collection?> GetCollectionByApiKeyAsync(string apiKey)
    {
        var filter = Builders<Collection>.Filter.Eq(c => c.ApiKey, apiKey);
        return await _collections.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Collection?> GetCollectionByNameAsync(string name)
    {
        var filter = Builders<Collection>.Filter.Eq(c => c.Name, name);
        return await _collections.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Collection>> GetAllCollectionsAsync()
    {
        return await _collections.Find(_ => true).ToListAsync();
    }

    public async Task<bool> DeleteCollectionAsync(string collectionId)
    {
        var filter = Builders<Collection>.Filter.Eq(c => c.Id, collectionId);
        var result = await _collections.DeleteOneAsync(filter);
        
        if (result.DeletedCount > 0)
        {
            await _database.DropCollectionAsync(collectionId);
            return true;
        }
        
        return false;
    }

    public async Task<string> InsertDocumentAsync(string collectionName, Dictionary<string, object> data)
    {
        var collection = _database.GetCollection<DynamicDocument>(collectionName);
        var document = new DynamicDocument
        {
            Data = data,
            CreatedAt = DateTime.UtcNow
        };

        await collection.InsertOneAsync(document);
        return document.Id;
    }

    public async Task<DynamicDocument?> GetDocumentAsync(string collectionName, string documentId)
    {
        var collection = _database.GetCollection<DynamicDocument>(collectionName);
        var filter = Builders<DynamicDocument>.Filter.Eq(d => d.Id, documentId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<DynamicDocument>> GetDocumentsAsync(string collectionName, Dictionary<string, object>? filter = null)
    {
        var collection = _database.GetCollection<DynamicDocument>(collectionName);
        
        if (filter == null || !filter.Any())
        {
            return await collection.Find(_ => true).ToListAsync();
        }

        var builder = Builders<DynamicDocument>.Filter;
        var filters = new List<FilterDefinition<DynamicDocument>>();
        
        foreach (var kvp in filter)
        {
            filters.Add(builder.Eq($"Data.{kvp.Key}", kvp.Value));
        }

        var combinedFilter = filters.Count > 1 ? builder.And(filters) : filters.FirstOrDefault();
        return await collection.Find(combinedFilter).ToListAsync();
    }

    public async Task<bool> UpdateDocumentAsync(string collectionName, string documentId, Dictionary<string, object> data)
    {
        var collection = _database.GetCollection<DynamicDocument>(collectionName);
        var filter = Builders<DynamicDocument>.Filter.Eq(d => d.Id, documentId);
        var update = Builders<DynamicDocument>.Update
            .Set(d => d.Data, data)
            .Set(d => d.UpdatedAt, DateTime.UtcNow);

        var result = await collection.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteDocumentAsync(string collectionName, string documentId)
    {
        var collection = _database.GetCollection<DynamicDocument>(collectionName);
        var filter = Builders<DynamicDocument>.Filter.Eq(d => d.Id, documentId);
        var result = await collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

    private string GenerateApiKey()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("MongoDB") ?? "mongodb://localhost:27017";
    }
}
