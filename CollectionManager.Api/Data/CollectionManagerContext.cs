using Microsoft.EntityFrameworkCore;
using CollectionManager.Api.Models;

namespace CollectionManager.Api.Data;

public class CollectionManagerContext : DbContext
{
    public CollectionManagerContext(DbContextOptions<CollectionManagerContext> options)
        : base(options)
    {
    }

    public DbSet<Collection> Collections { get; set; }
    public DbSet<CollectionTable> Tables { get; set; }
    public DbSet<TableRecord> Records { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.ApiKey).IsUnique();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).IsRequired(false);
        });

        modelBuilder.Entity<CollectionTable>(entity =>
        {
            entity.HasIndex(e => new { e.CollectionId, e.TableName }).IsUnique();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).IsRequired(false);
            
            entity.HasOne(e => e.Collection)
                .WithMany(e => e.Tables)
                .HasForeignKey(e => e.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TableRecord>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).IsRequired(false);
            entity.Property(e => e.JsonData).HasColumnType("NVARCHAR(MAX)");
            
            entity.HasOne(e => e.Table)
                .WithMany(e => e.Records)
                .HasForeignKey(e => e.TableId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
