using Microsoft.EntityFrameworkCore;
using CollectionManager.Api.Data;
using CollectionManager.Api.Models;
using System.Text.Json;

namespace CollectionManager.Api.Services;

public class CollectionService
{
    private readonly CollectionManagerContext _context;

    public CollectionService(CollectionManagerContext context)
    {
        _context = context;
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

        _context.Collections.Add(collection);
        await _context.SaveChangesAsync();
        
        return collection;
    }

    public async Task<Collection?> GetCollectionByApiKeyAsync(string apiKey)
    {
        return await _context.Collections
            .Include(c => c.Tables)
            .FirstOrDefaultAsync(c => c.ApiKey == apiKey && c.IsActive);
    }

    public async Task<Collection?> GetCollectionByNameAsync(string name)
    {
        return await _context.Collections
            .Include(c => c.Tables)
            .FirstOrDefaultAsync(c => c.Name == name && c.IsActive);
    }

    public async Task<List<Collection>> GetAllCollectionsAsync()
    {
        return await _context.Collections
            .Include(c => c.Tables)
            .Where(c => c.IsActive)
            .ToListAsync();
    }

    public async Task<bool> DeleteCollectionAsync(int collectionId)
    {
        var collection = await _context.Collections.FindAsync(collectionId);
        if (collection == null) return false;

        collection.IsActive = false;
        collection.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CollectionTable> CreateTableAsync(int collectionId, string tableName, string? description = null)
    {
        var table = new CollectionTable
        {
            CollectionId = collectionId,
            TableName = tableName,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tables.Add(table);
        await _context.SaveChangesAsync();
        
        return table;
    }

    public async Task<string> InsertRecordAsync(int tableId, Dictionary<string, object> data)
    {
        var jsonData = JsonSerializer.Serialize(data);
        
        var record = new TableRecord
        {
            TableId = tableId,
            JsonData = jsonData,
            CreatedAt = DateTime.UtcNow
        };

        _context.Records.Add(record);
        await _context.SaveChangesAsync();
        
        return record.Id.ToString();
    }

    public async Task<TableRecord?> GetRecordAsync(int tableId, int recordId)
    {
        return await _context.Records
            .FirstOrDefaultAsync(r => r.TableId == tableId && r.Id == recordId);
    }

    public async Task<List<TableRecord>> GetRecordsAsync(int tableId, Dictionary<string, object>? filter = null)
    {
        var query = _context.Records.Where(r => r.TableId == tableId);
        
        if (filter != null && filter.Any())
        {
            foreach (var kvp in filter)
            {
                var value = JsonSerializer.Serialize(kvp.Value);
                query = query.Where(r => r.JsonData.Contains(value));
            }
        }

        return await query.ToListAsync();
    }

    public async Task<bool> UpdateRecordAsync(int tableId, int recordId, Dictionary<string, object> data)
    {
        var record = await _context.Records
            .FirstOrDefaultAsync(r => r.TableId == tableId && r.Id == recordId);
        
        if (record == null) return false;

        record.JsonData = JsonSerializer.Serialize(data);
        record.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRecordAsync(int tableId, int recordId)
    {
        var record = await _context.Records
            .FirstOrDefaultAsync(r => r.TableId == tableId && r.Id == recordId);
        
        if (record == null) return false;

        _context.Records.Remove(record);
        await _context.SaveChangesAsync();
        return true;
    }

    private string GenerateApiKey()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
