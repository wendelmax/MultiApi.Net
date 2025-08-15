namespace CollectionManager.Api.Models;

public class CreateCollectionResponse
{
    public string CollectionId { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public string ApiKey { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    public string Message { get; set; } = string.Empty;
}
