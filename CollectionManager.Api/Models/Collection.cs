using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CollectionManager.Api.Models;

public class Collection
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public string ApiKey { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string Description { get; set; } = string.Empty;
    
    public string Owner { get; set; } = string.Empty;
}
