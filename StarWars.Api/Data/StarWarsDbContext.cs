using Microsoft.EntityFrameworkCore;

namespace StarWars.Api.Data;

public class StarWarsDbContext : DbContext
{
    public DbSet<ApiCacheEntry> CacheEntries => Set<ApiCacheEntry>();

    public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StarWarsDbContext).Assembly);
        modelBuilder.Entity<ApiCacheEntry>(entity =>
        {
            entity.ToTable("api_cache");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Key).IsRequired();
            entity.Property(e => e.ContentType).IsRequired();
            entity.HasIndex(e => e.Key).IsUnique();
        });
    }
}

public class ApiCacheEntry
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string ContentType { get; set; } = "application/json";
    public string Payload { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAtUtc { get; set; }
}


