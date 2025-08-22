namespace StarWars.Api.Services.Interfaces;

public interface IDatabaseSeedService
{
    Task SeedAsync();
    Task<bool> IsDatabaseSeededAsync();
    Task<int> GetSeedCountAsync();
}
