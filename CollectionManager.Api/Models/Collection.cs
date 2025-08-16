using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionManager.Api.Models;

public class Collection
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Owner { get; set; } = string.Empty;
    
    [Required]
    [StringLength(64)]
    public string ApiKey { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public virtual ICollection<CollectionTable> Tables { get; set; } = new List<CollectionTable>();
}

public class CollectionTable
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string TableName { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    [ForeignKey("CollectionId")]
    public int CollectionId { get; set; }
    
    public virtual Collection Collection { get; set; } = null!;
    
    public virtual ICollection<TableRecord> Records { get; set; } = new List<TableRecord>();
}

public class TableRecord
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string JsonData { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    [ForeignKey("TableId")]
    public int TableId { get; set; }
    
    public virtual CollectionTable Table { get; set; } = null!;
}
