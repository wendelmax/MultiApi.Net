using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CollectionManager.Api.Models;

public class DynamicDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    
    [BsonExtraElements]
    public Dictionary<string, object> Data { get; set; } = new();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
}
