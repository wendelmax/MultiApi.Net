namespace CollectionManager.Api.Models;

public class CreateCollectionRequest
{
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Owner { get; set; } = string.Empty;
}
