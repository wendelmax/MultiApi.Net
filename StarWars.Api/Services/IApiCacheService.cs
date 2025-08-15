using StarWars.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace StarWars.Api.Services;

public interface IApiCacheService
{
    Task<(bool Hit, string? Payload, string ContentType)> TryGetAsync(string key, CancellationToken ct = default);
    Task SetAsync(string key, string payload, string contentType, TimeSpan ttl, CancellationToken ct = default);
}

public class ApiCacheService : IApiCacheService
{
    private readonly StarWarsDbContext _db;

    public ApiCacheService(StarWarsDbContext db)
    {
        _db = db;
    }

    public async Task<(bool Hit, string? Payload, string ContentType)> TryGetAsync(string key, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var entry = await _db.CacheEntries.Where(e => e.Key == key && e.ExpiresAtUtc > now).FirstOrDefaultAsync(ct);
        if (entry is null) return (false, null, "application/json");
        return (true, entry.Payload, entry.ContentType);
    }

    public async Task SetAsync(string key, string payload, string contentType, TimeSpan ttl, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var expires = now.Add(ttl);
        var existing = await _db.CacheEntries.Where(e => e.Key == key).FirstOrDefaultAsync(ct);
        if (existing is null)
        {
            _db.CacheEntries.Add(new ApiCacheEntry
            {
                Key = key,
                Payload = payload,
                ContentType = contentType,
                CreatedAtUtc = now,
                ExpiresAtUtc = expires
            });
        }
        else
        {
            existing.Payload = payload;
            existing.ContentType = contentType;
            existing.ExpiresAtUtc = expires;
        }
        await _db.SaveChangesAsync(ct);
    }
}


