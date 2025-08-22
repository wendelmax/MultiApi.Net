using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data.Context;
using StarWars.Api.Services.Interfaces;

namespace StarWars.Api.Services.Implementations;

public class DatabaseSeedService : IDatabaseSeedService
{
    private readonly StarWarsDbContext _context;
    private readonly ILogger<DatabaseSeedService> _logger;

    public DatabaseSeedService(StarWarsDbContext context, ILogger<DatabaseSeedService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation("Iniciando seed do banco de dados...");

            if (await IsDatabaseSeededAsync())
            {
                _logger.LogInformation("Banco de dados já foi populado. Pulando seed.");
                return;
            }

            _logger.LogInformation("Populando banco de dados com dados do Star Wars...");
            
            await SeedFilmsAsync();
            await SeedPeopleAsync();
            await SeedPlanetsAsync();
            await SeedSpeciesAsync();
            await SeedStarshipsAsync();
            await SeedVehiclesAsync();
            await SeedJunctionTablesAsync();
            
            _logger.LogInformation("Seed do banco de dados concluído com sucesso!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro durante o seed do banco de dados");
            throw;
        }
    }

    private async Task SeedFilmsAsync()
    {
        if (await _context.Films.AnyAsync()) return;

        var films = new[]
        {
            new { Title = "A New Hope", EpisodeId = 4, OpeningCrawl = "It is a period of civil war...", Director = "George Lucas", Producer = "Gary Kurtz, Rick McCallum", ReleaseDate = "1977-05-25" },
            new { Title = "The Empire Strikes Back", EpisodeId = 5, OpeningCrawl = "It is a dark time for the Rebellion...", Director = "Irvin Kershner", Producer = "Gary Kurtz, Rick McCallum", ReleaseDate = "1980-05-17" },
            new { Title = "Return of the Jedi", EpisodeId = 6, OpeningCrawl = "Luke Skywalker has returned to his home planet...", Director = "Richard Marquand", Producer = "Howard G. Kazanjian, George Lucas, Rick McCallum", ReleaseDate = "1983-05-25" },
            new { Title = "The Phantom Menace", EpisodeId = 1, OpeningCrawl = "Turmoil has engulfed the Galactic Republic...", Director = "George Lucas", Producer = "Rick McCallum", ReleaseDate = "1999-05-19" },
            new { Title = "Attack of the Clones", EpisodeId = 2, OpeningCrawl = "There is unrest in the Galactic Senate...", Director = "George Lucas", Producer = "Rick McCallum", ReleaseDate = "2002-05-16" },
            new { Title = "Revenge of the Sith", EpisodeId = 3, OpeningCrawl = "War! The Republic is crumbling...", Director = "George Lucas", Producer = "Rick McCallum", ReleaseDate = "2005-05-19" },
            new { Title = "The Force Awakens", EpisodeId = 7, OpeningCrawl = "Luke Skywalker has vanished...", Director = "J.J. Abrams", Producer = "Kathleen Kennedy, J.J. Abrams, Bryan Burk", ReleaseDate = "2015-12-18" }
        };

        foreach (var film in films)
        {
            await _context.Films.AddAsync(new Models.Film
            {
                Title = film.Title,
                EpisodeId = film.EpisodeId,
                OpeningCrawl = film.OpeningCrawl,
                Director = film.Director,
                Producer = film.Producer,
                ReleaseDate = film.ReleaseDate,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Films populados: {Count}", films.Length);
    }

    private async Task SeedPeopleAsync()
    {
        if (await _context.People.AnyAsync()) return;

        var people = new[]
        {
            new { Name = "Luke Skywalker", Height = "172", Mass = "77", HairColor = "blond", SkinColor = "fair", EyeColor = "blue", BirthYear = "19BBY", Gender = "male" },
            new { Name = "Leia Organa", Height = "150", Mass = "49", HairColor = "brown", SkinColor = "light", EyeColor = "brown", BirthYear = "19BBY", Gender = "female" },
            new { Name = "Han Solo", Height = "180", Mass = "80", HairColor = "brown", SkinColor = "fair", EyeColor = "brown", BirthYear = "29BBY", Gender = "male" },
            new { Name = "Darth Vader", Height = "202", Mass = "136", HairColor = "none", SkinColor = "white", EyeColor = "yellow", BirthYear = "41.9BBY", Gender = "male" },
            new { Name = "Obi-Wan Kenobi", Height = "182", Mass = "77", HairColor = "auburn, white", SkinColor = "fair", EyeColor = "blue-gray", BirthYear = "57BBY", Gender = "male" },
            new { Name = "Yoda", Height = "66", Mass = "17", HairColor = "white", SkinColor = "green", EyeColor = "brown", BirthYear = "896BBY", Gender = "male" },
            new { Name = "Chewbacca", Height = "228", Mass = "112", HairColor = "brown", SkinColor = "unknown", EyeColor = "blue", BirthYear = "200BBY", Gender = "male" },
            new { Name = "R2-D2", Height = "96", Mass = "32", HairColor = "n/a", SkinColor = "white, blue", EyeColor = "red", BirthYear = "33BBY", Gender = "n/a" },
            new { Name = "C-3PO", Height = "167", Mass = "75", HairColor = "n/a", SkinColor = "gold", EyeColor = "yellow", BirthYear = "112BBY", Gender = "n/a" },
            new { Name = "Anakin Skywalker", Height = "188", Mass = "84", HairColor = "blond", SkinColor = "fair", EyeColor = "blue", BirthYear = "41.9BBY", Gender = "male" },
            new { Name = "Padmé Amidala", Height = "165", Mass = "45", HairColor = "brown", SkinColor = "light", EyeColor = "brown", BirthYear = "46BBY", Gender = "female" },
            new { Name = "Mace Windu", Height = "188", Mass = "84", HairColor = "none", SkinColor = "dark", EyeColor = "brown", BirthYear = "72BBY", Gender = "male" },
            new { Name = "Qui-Gon Jinn", Height = "193", Mass = "89", HairColor = "brown", SkinColor = "fair", EyeColor = "blue", BirthYear = "92BBY", Gender = "male" },
            new { Name = "Count Dooku", Height = "193", Mass = "80", HairColor = "white", SkinColor = "fair", EyeColor = "brown", BirthYear = "102BBY", Gender = "male" },
            new { Name = "Palpatine", Height = "170", Mass = "75", HairColor = "brown", SkinColor = "pale", EyeColor = "yellow", BirthYear = "82BBY", Gender = "male" },
            new { Name = "Boba Fett", Height = "183", Mass = "78.2", HairColor = "black", SkinColor = "fair", EyeColor = "brown", BirthYear = "31.5BBY", Gender = "male" },
            new { Name = "Jango Fett", Height = "183", Mass = "79", HairColor = "black", SkinColor = "tan", EyeColor = "brown", BirthYear = "66BBY", Gender = "male" },
            new { Name = "Jabba Desilijic Tiure", Height = "175", Mass = "1,358", HairColor = "n/a", SkinColor = "green-tan, brown", EyeColor = "orange", BirthYear = "600BBY", Gender = "hermaphrodite" },
            new { Name = "Wedge Antilles", Height = "170", Mass = "77", HairColor = "brown", SkinColor = "fair", EyeColor = "hazel", BirthYear = "21BBY", Gender = "male" },
            new { Name = "Lando Calrissian", Height = "177", Mass = "79", HairColor = "black", SkinColor = "dark", EyeColor = "brown", BirthYear = "31BBY", Gender = "male" }
        };

        foreach (var person in people)
        {
            await _context.People.AddAsync(new Models.Person
            {
                Name = person.Name,
                Height = person.Height,
                Mass = person.Mass,
                HairColor = person.HairColor,
                SkinColor = person.SkinColor,
                EyeColor = person.EyeColor,
                BirthYear = person.BirthYear,
                Gender = person.Gender,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("People populados: {Count}", people.Length);
    }

    private async Task SeedPlanetsAsync()
    {
        if (await _context.Planets.AnyAsync()) return;

        var planets = new[]
        {
            new { Name = "Tatooine", RotationPeriod = "23", OrbitalPeriod = "304", Diameter = "10465", Climate = "arid", Gravity = "1 standard", Terrain = "desert", SurfaceWater = "1", Population = "200000" },
            new { Name = "Alderaan", RotationPeriod = "24", OrbitalPeriod = "364", Diameter = "12500", Climate = "temperate", Gravity = "1 standard", Terrain = "grasslands, mountains", SurfaceWater = "40", Population = "2000000000" },
            new { Name = "Yavin IV", RotationPeriod = "24", OrbitalPeriod = "4818", Diameter = "10200", Climate = "temperate, tropical", Gravity = "1 standard", Terrain = "jungle, rainforests", SurfaceWater = "8", Population = "1000" },
            new { Name = "Hoth", RotationPeriod = "23", OrbitalPeriod = "549", Diameter = "7200", Climate = "frozen", Gravity = "1.1 standard", Terrain = "tundra, ice caves, mountain ranges", SurfaceWater = "100", Population = "unknown" },
            new { Name = "Dagobah", RotationPeriod = "23", OrbitalPeriod = "341", Diameter = "8900", Climate = "murky", Gravity = "N/A", Terrain = "swamp, jungles", SurfaceWater = "8", Population = "unknown" },
            new { Name = "Bespin", RotationPeriod = "12", OrbitalPeriod = "5110", Diameter = "118000", Climate = "temperate", Gravity = "1.5 (surface), 1 standard (Cloud City)", Terrain = "gas giant", SurfaceWater = "0", Population = "6000000" },
            new { Name = "Endor", RotationPeriod = "18", OrbitalPeriod = "402", Diameter = "4900", Climate = "temperate", Gravity = "0.85 standard", Terrain = "forests, mountains, lakes", SurfaceWater = "8", Population = "30000000" },
            new { Name = "Naboo", RotationPeriod = "26", OrbitalPeriod = "312", Diameter = "12120", Climate = "temperate", Gravity = "1 standard", Terrain = "grassy hills, swamps, forests, mountains", SurfaceWater = "12", Population = "4500000000" },
            new { Name = "Coruscant", RotationPeriod = "24", OrbitalPeriod = "368", Diameter = "12240", Climate = "temperate", Gravity = "1 standard", Terrain = "cityscape, mountains", SurfaceWater = "unknown", Population = "1000000000000" },
            new { Name = "Kamino", RotationPeriod = "27", OrbitalPeriod = "463", Diameter = "19720", Climate = "temperate", Gravity = "1 standard", Terrain = "ocean", SurfaceWater = "100", Population = "1000000000" },
            new { Name = "Geonosis", RotationPeriod = "30", OrbitalPeriod = "256", Diameter = "11370", Climate = "temperate, arid", Gravity = "0.9 standard", Terrain = "rock, desert, mountain, barren", SurfaceWater = "5", Population = "100000000000" },
            new { Name = "Utapau", RotationPeriod = "27", OrbitalPeriod = "351", Diameter = "12900", Climate = "temperate, arid, windy", Gravity = "0.8 standard", Terrain = "scrublands, savanna, canyons, sinkholes", SurfaceWater = "0.9", Population = "95000000" },
            new { Name = "Mustafar", RotationPeriod = "36", OrbitalPeriod = "412", Diameter = "4200", Climate = "hot", Gravity = "1 standard", Terrain = "volcanoes, lava rivers, mountains, caves", SurfaceWater = "0", Population = "20000" },
            new { Name = "Kashyyyk", RotationPeriod = "26", OrbitalPeriod = "381", Diameter = "12765", Climate = "tropical", Gravity = "1 standard", Terrain = "jungle, forests, lakes, rivers", SurfaceWater = "60", Population = "45000000" },
            new { Name = "Polis Massa", RotationPeriod = "24", OrbitalPeriod = "590", Diameter = "0", Climate = "artificial temperate", Gravity = "0.56 standard", Terrain = "airless asteroid", SurfaceWater = "0", Population = "1000000" }
        };

        foreach (var planet in planets)
        {
            await _context.Planets.AddAsync(new Models.Planet
            {
                Name = planet.Name,
                RotationPeriod = planet.RotationPeriod,
                OrbitalPeriod = planet.OrbitalPeriod,
                Diameter = planet.Diameter,
                Climate = planet.Climate,
                Gravity = planet.Gravity,
                Terrain = planet.Terrain,
                SurfaceWater = planet.SurfaceWater,
                Population = planet.Population,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Planets populados: {Count}", planets.Length);
    }

    private async Task SeedSpeciesAsync()
    {
        if (await _context.Species.AnyAsync()) return;

        var species = new[]
        {
            new { Name = "Human", Classification = "mammal", Designation = "sentient", AverageHeight = "180", SkinColors = "caucasian, black, asian, hispanic", HairColors = "blonde, brown, black, red", EyeColors = "brown, blue, green, hazel, grey, amber", AverageLifespan = "120", Language = "Galactic Basic" },
            new { Name = "Droid", Classification = "artificial", Designation = "sentient", AverageHeight = "0", SkinColors = "n/a", HairColors = "n/a", EyeColors = "n/a", AverageLifespan = "0", Language = "n/a" },
            new { Name = "Wookie", Classification = "mammal", Designation = "sentient", AverageHeight = "210", SkinColors = "gray", HairColors = "black, brown", EyeColors = "blue, green, yellow, brown, golden, red", AverageLifespan = "400", Language = "Shyriiwook" },
            new { Name = "Rodian", Classification = "sentient", Designation = "reptilian", AverageHeight = "170", SkinColors = "green, blue", HairColors = "n/a", EyeColors = "black", AverageLifespan = "unknown", Language = "Galatic Basic" },
            new { Name = "Hutt", Classification = "gastropod", Designation = "sentient", AverageHeight = "300", SkinColors = "green, brown, tan", HairColors = "n/a", EyeColors = "yellow, red", AverageLifespan = "1000", Language = "Huttese" },
            new { Name = "Yoda's species", Classification = "mammal", Designation = "sentient", AverageHeight = "66", SkinColors = "green, yellow", HairColors = "white, brown", EyeColors = "brown, green, yellow", AverageLifespan = "900", Language = "Galactic basic" },
            new { Name = "Trandoshan", Classification = "reptile", Designation = "sentient", AverageHeight = "200", SkinColors = "brown, green", HairColors = "none", EyeColors = "yellow, orange", AverageLifespan = "unknown", Language = "Dosh" },
            new { Name = "Mon Calamari", Classification = "amphibian", Designation = "sentient", AverageHeight = "160", SkinColors = "red, blue, brown, magenta", HairColors = "none", EyeColors = "yellow", AverageLifespan = "unknown", Language = "Mon Calamarian" },
            new { Name = "Ewok", Classification = "mammal", Designation = "sentient", AverageHeight = "100", SkinColors = "brown", HairColors = "white, brown, black", EyeColors = "orange, brown", AverageLifespan = "unknown", Language = "Ewokese" },
            new { Name = "Sullustan", Classification = "mammal", Designation = "sentient", AverageHeight = "180", SkinColors = "pale", HairColors = "none", EyeColors = "black", AverageLifespan = "unknown", Language = "Sullutese" },
            new { Name = "Neimoidian", Classification = "unknown", Designation = "sentient", AverageHeight = "180", SkinColors = "grey, green", HairColors = "none", EyeColors = "red, pink", AverageLifespan = "unknown", Language = "Neimoidia" },
            new { Name = "Gungan", Classification = "amphibian", Designation = "sentient", AverageHeight = "196", SkinColors = "brown, green", HairColors = "none", EyeColors = "orange", AverageLifespan = "unknown", Language = "Gungan basic" },
            new { Name = "Toydarian", Classification = "mammal", Designation = "sentient", AverageHeight = "120", SkinColors = "blue, green, grey", HairColors = "none", EyeColors = "yellow", AverageLifespan = "91", Language = "Toydarian" },
            new { Name = "Dug", Classification = "mammal", Designation = "sentient", AverageHeight = "100", SkinColors = "brown, purple, grey, red", HairColors = "none", EyeColors = "yellow, blue", AverageLifespan = "unknown", Language = "Dugese" },
            new { Name = "Twi'lek", Classification = "mammals", Designation = "sentient", AverageHeight = "200", SkinColors = "orange, yellow, blue, green, pink, purple, tan", HairColors = "none", EyeColors = "blue, brown, orange, pink", AverageLifespan = "unknown", Language = "Twi'leki" }
        };

        foreach (var spec in species)
        {
            await _context.Species.AddAsync(new Models.Species
            {
                Name = spec.Name,
                Classification = spec.Classification,
                Designation = spec.Designation,
                AverageHeight = spec.AverageHeight,
                SkinColors = spec.SkinColors,
                HairColors = spec.HairColors,
                EyeColors = spec.EyeColors,
                AverageLifespan = spec.AverageLifespan,
                Language = spec.Language,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Species populados: {Count}", species.Length);
    }

    private async Task SeedStarshipsAsync()
    {
        if (await _context.Starships.AnyAsync()) return;

        var starships = new[]
        {
            new { Name = "Millennium Falcon", Model = "YT-1300 light freighter", Manufacturer = "Corellian Engineering Corporation", CostInCredits = "100000", Length = "34.37", MaxAtmospheringSpeed = "1050", Crew = "4", Passengers = "6", CargoCapacity = "100000", Consumables = "2 months", HyperdriveRating = "0.5", MGLT = "75", StarshipClass = "Light freighter" },
            new { Name = "X-wing", Model = "T-65 X-wing", Manufacturer = "Incom Corporation", CostInCredits = "149999", Length = "12.5", MaxAtmospheringSpeed = "1050", Crew = "1", Passengers = "0", CargoCapacity = "110", Consumables = "1 week", HyperdriveRating = "1.0", MGLT = "100", StarshipClass = "Starfighter" },
            new { Name = "TIE Advanced x1", Model = "Twin Ion Engine Advanced x1", Manufacturer = "Sienar Fleet Systems", CostInCredits = "unknown", Length = "9.2", MaxAtmospheringSpeed = "1200", Crew = "1", Passengers = "0", CargoCapacity = "150", Consumables = "5 days", HyperdriveRating = "1.0", MGLT = "105", StarshipClass = "Starfighter" },
            new { Name = "Death Star", Model = "DS-1 Orbital Battle Station", Manufacturer = "Imperial Department of Military Research, Sienar Fleet Systems", CostInCredits = "1000000000000", Length = "120000", MaxAtmospheringSpeed = "n/a", Crew = "342,953", Passengers = "843,342", CargoCapacity = "1000000000000", Consumables = "3 years", HyperdriveRating = "4.0", MGLT = "10", StarshipClass = "Deep Space Mobile Battlestation" },
            new { Name = "Star Destroyer", Model = "Imperial I-class Star Destroyer", Manufacturer = "Kuat Drive Yards", CostInCredits = "150000000", Length = "1,600", MaxAtmospheringSpeed = "975", Crew = "47,060", Passengers = "n/a", CargoCapacity = "36000000", Consumables = "2 years", HyperdriveRating = "2.0", MGLT = "60", StarshipClass = "Star Destroyer" },
            new { Name = "Naboo Royal Starship", Model = "J-type 327 Nubian royal starship", Manufacturer = "Theed Palace Space Vessel Engineering Corps, Nubia Star Drives, Incorporated", CostInCredits = "unknown", Length = "76", MaxAtmospheringSpeed = "920", Crew = "8", Passengers = "unknown", CargoCapacity = "unknown", Consumables = "unknown", HyperdriveRating = "1.8", MGLT = "unknown", StarshipClass = "yacht" },
            new { Name = "Naboo fighter", Model = "N-1 starfighter", Manufacturer = "Theed Palace Space Vessel Engineering Corps", CostInCredits = "200000", Length = "11", MaxAtmospheringSpeed = "1100", Crew = "1", Passengers = "0", CargoCapacity = "65", Consumables = "7 days", HyperdriveRating = "1.0", MGLT = "unknown", StarshipClass = "Starfighter" },
            new { Name = "Jedi Interceptor", Model = "Eta-2 Actis-class light interceptor", Manufacturer = "Kuat Systems Engineering", CostInCredits = "320000", Length = "5.47", MaxAtmospheringSpeed = "1500", Crew = "1", Passengers = "0", CargoCapacity = "60", Consumables = "2 days", HyperdriveRating = "1.0", MGLT = "150", StarshipClass = "Starfighter" },
            new { Name = "Slave 1", Model = "Firespray-31-class patrol and attack", Manufacturer = "Kuat Systems Engineering", CostInCredits = "unknown", Length = "21.5", MaxAtmospheringSpeed = "1000", Crew = "1", Passengers = "6", CargoCapacity = "70000", Consumables = "1 month", HyperdriveRating = "3.0", MGLT = "70", StarshipClass = "Patrol craft" },
            new { Name = "Imperial shuttle", Model = "Lambda-class T-4a shuttle", Manufacturer = "Sienar Fleet Systems", CostInCredits = "240000", Length = "20", MaxAtmospheringSpeed = "850", Crew = "6", Passengers = "20", CargoCapacity = "80000", Consumables = "2 months", HyperdriveRating = "1.0", MGLT = "50", StarshipClass = "Armed government transport" }
        };

        foreach (var starship in starships)
        {
            await _context.Starships.AddAsync(new Models.Starship
            {
                Name = starship.Name,
                Model = starship.Model,
                Manufacturer = starship.Manufacturer,
                CostInCredits = starship.CostInCredits,
                Length = starship.Length,
                MaxAtmospheringSpeed = starship.MaxAtmospheringSpeed,
                Crew = starship.Crew,
                Passengers = starship.Passengers,
                CargoCapacity = starship.CargoCapacity,
                Consumables = starship.Consumables,
                HyperdriveRating = starship.HyperdriveRating,
                MGLT = starship.MGLT,
                StarshipClass = starship.StarshipClass,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Starships populados: {Count}", starships.Length);
    }

    private async Task SeedVehiclesAsync()
    {
        if (await _context.Vehicles.AnyAsync()) return;

        var vehicles = new[]
        {
            new { Name = "Sand Crawler", Model = "Digger Crawler", Manufacturer = "Corellia Mining Corporation", CostInCredits = "150000", Length = "36.8", MaxAtmospheringSpeed = "30", Crew = "46", Passengers = "30", CargoCapacity = "50000", Consumables = "2 months", VehicleClass = "wheeled" },
            new { Name = "T-16 skyhopper", Model = "T-16 skyhopper", Manufacturer = "Incom Corporation", CostInCredits = "14500", Length = "10.4", MaxAtmospheringSpeed = "1200", Crew = "1", Passengers = "1", CargoCapacity = "50", Consumables = "0", VehicleClass = "repulsorcraft" },
            new { Name = "X-34 landspeeder", Model = "X-34 landspeeder", Manufacturer = "SoroSuub Corporation", CostInCredits = "10550", Length = "3.4", MaxAtmospheringSpeed = "250", Crew = "1", Passengers = "1", CargoCapacity = "5", Consumables = "unknown", VehicleClass = "repulsorcraft" },
            new { Name = "TIE/LN starfighter", Model = "Twin Ion Engine/Ln Starfighter", Manufacturer = "Sienar Fleet Systems", CostInCredits = "unknown", Length = "7.2", MaxAtmospheringSpeed = "1200", Crew = "1", Passengers = "0", CargoCapacity = "65", Consumables = "2 days", VehicleClass = "starfighter" },
            new { Name = "Snowspeeder", Model = "t-47 airspeeder", Manufacturer = "Incom corporation", CostInCredits = "unknown", Length = "4.5", MaxAtmospheringSpeed = "650", Crew = "2", Passengers = "0", CargoCapacity = "10", Consumables = "none", VehicleClass = "airspeeder" },
            new { Name = "TIE bomber", Model = "TIE/sa bomber", Manufacturer = "Sienar Fleet Systems", CostInCredits = "unknown", Length = "7.8", MaxAtmospheringSpeed = "850", Crew = "1", Passengers = "0", CargoCapacity = "0", Consumables = "2 days", VehicleClass = "space/planetary bomber" },
            new { Name = "AT-AT", Model = "All Terrain Armored Transport", Manufacturer = "Kuat Drive Yards, Imperial Department of Military Research", CostInCredits = "unknown", Length = "20", MaxAtmospheringSpeed = "60", Crew = "5", Passengers = "40", CargoCapacity = "1000", Consumables = "unknown", VehicleClass = "assault walker" },
            new { Name = "AT-ST", Model = "All Terrain Scout Transport", Manufacturer = "Kuat Drive Yards, Imperial Department of Military Research", CostInCredits = "unknown", Length = "2", MaxAtmospheringSpeed = "90", Crew = "2", Passengers = "0", CargoCapacity = "200", Consumables = "none", VehicleClass = "walker" },
            new { Name = "Storm IV Twin-Pod cloud car", Model = "Storm IV Twin-Pod", Manufacturer = "Bespin Motors", CostInCredits = "75000", Length = "7", MaxAtmospheringSpeed = "1500", Crew = "2", Passengers = "0", CargoCapacity = "10", Consumables = "1 day", VehicleClass = "repulsorcraft" },
            new { Name = "Sail barge", Model = "Modified Luxury Sail Barge", Manufacturer = "Ubrikkian industries Custom Vehicle Division", CostInCredits = "285000", Length = "30", MaxAtmospheringSpeed = "100", Crew = "26", Passengers = "500", CargoCapacity = "2000000", Consumables = "Live food tanks", VehicleClass = "sail barge" }
        };

        foreach (var vehicle in vehicles)
        {
            await _context.Vehicles.AddAsync(new Models.Vehicle
            {
                Name = vehicle.Name,
                Model = vehicle.Model,
                Manufacturer = vehicle.Manufacturer,
                CostInCredits = vehicle.CostInCredits,
                Length = vehicle.Length,
                MaxAtmospheringSpeed = vehicle.MaxAtmospheringSpeed,
                Crew = vehicle.Crew,
                Passengers = vehicle.Passengers,
                CargoCapacity = vehicle.CargoCapacity,
                Consumables = vehicle.Consumables,
                VehicleClass = vehicle.VehicleClass,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Vehicles populados: {Count}", vehicles.Length);
    }

    private async Task SeedJunctionTablesAsync()
    {
        _logger.LogInformation("Populando tabelas de junção...");
        
        try
        {
            // Populando relacionamentos baseados no swapi_data.json
            // Salvando em lotes menores para identificar problemas específicos
            
            _logger.LogInformation("Populando relacionamentos de filmes...");
            await SeedFilmRelationshipsAsync();
            await _context.SaveChangesAsync();
            _logger.LogInformation("Relacionamentos de filmes salvos com sucesso!");
            
            _logger.LogInformation("Populando relacionamentos de pessoas...");
            await SeedPersonRelationshipsAsync();
            await _context.SaveChangesAsync();
            _logger.LogInformation("Relacionamentos de pessoas salvos com sucesso!");
            
            _logger.LogInformation("Populando relacionamentos de planetas...");
            await SeedPlanetRelationshipsAsync();
            await _context.SaveChangesAsync();
            _logger.LogInformation("Relacionamentos de planetas salvos com sucesso!");
            
            _logger.LogInformation("Tabelas de junção populadas com sucesso!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao popular tabelas de junção");
            throw;
        }
    }

    private async Task SeedFilmRelationshipsAsync()
    {
        // A New Hope (Episode 4) - Film ID 1
        await SeedFilmCharacterAsync(1, 1); // Luke Skywalker
        await SeedFilmCharacterAsync(1, 2); // Leia Organa
        await SeedFilmCharacterAsync(1, 3); // Han Solo
        await SeedFilmCharacterAsync(1, 4); // Darth Vader
        await SeedFilmCharacterAsync(1, 5); // Obi-Wan Kenobi
        await SeedFilmCharacterAsync(1, 6); // Yoda
        await SeedFilmCharacterAsync(1, 7); // Chewbacca
        await SeedFilmCharacterAsync(1, 8); // R2-D2
        await SeedFilmCharacterAsync(1, 9); // C-3PO
        await SeedFilmCharacterAsync(1, 10); // Anakin Skywalker
        await SeedFilmCharacterAsync(1, 12); // Padmé Amidala
        await SeedFilmCharacterAsync(1, 13); // Mace Windu
        await SeedFilmCharacterAsync(1, 14); // Qui-Gon Jinn
        await SeedFilmCharacterAsync(1, 15); // Count Dooku
        await SeedFilmCharacterAsync(1, 16); // Palpatine
        await SeedFilmCharacterAsync(1, 18); // Boba Fett
        await SeedFilmCharacterAsync(1, 19); // Jango Fett
        await SeedFilmCharacterAsync(1, 16); // Jabba Desilijic Tiure

        // A New Hope - Planets
        await SeedFilmPlanetAsync(1, 1); // Tatooine
        await SeedFilmPlanetAsync(1, 2); // Alderaan
        await SeedFilmPlanetAsync(1, 3); // Yavin IV

        // A New Hope - Starships
        await SeedFilmStarshipAsync(1, 2); // X-wing
        await SeedFilmStarshipAsync(1, 3); // TIE Advanced x1
        await SeedFilmStarshipAsync(1, 5); // Death Star
        await SeedFilmStarshipAsync(1, 9); // Naboo fighter
        await SeedFilmStarshipAsync(1, 10); // Jedi Interceptor
        await SeedFilmStarshipAsync(1, 9); // Slave 1
        await SeedFilmStarshipAsync(1, 10); // Imperial shuttle
        await SeedFilmStarshipAsync(1, 6); // Naboo Royal Starship

        // A New Hope - Vehicles
        await SeedFilmVehicleAsync(1, 4); // Sand Crawler
        await SeedFilmVehicleAsync(1, 6); // T-16 skyhopper
        await SeedFilmVehicleAsync(1, 7); // X-34 landspeeder
        await SeedFilmVehicleAsync(1, 8); // TIE/LN starfighter

        // A New Hope - Species
        await SeedFilmSpeciesAsync(1, 1); // Human
        await SeedFilmSpeciesAsync(1, 2); // Droid
        await SeedFilmSpeciesAsync(1, 3); // Wookie
        await SeedFilmSpeciesAsync(1, 4); // Rodian
        await SeedFilmSpeciesAsync(1, 5); // Hutt

        // The Empire Strikes Back (Episode 5) - Film ID 2
        await SeedFilmCharacterAsync(2, 1); // Luke Skywalker
        await SeedFilmCharacterAsync(2, 2); // Leia Organa
        await SeedFilmCharacterAsync(2, 3); // Han Solo
        await SeedFilmCharacterAsync(2, 4); // Darth Vader
        await SeedFilmCharacterAsync(2, 5); // Obi-Wan Kenobi
        await SeedFilmCharacterAsync(2, 10); // Anakin Skywalker
        await SeedFilmCharacterAsync(2, 13); // Mace Windu
        await SeedFilmCharacterAsync(2, 14); // Qui-Gon Jinn
        await SeedFilmCharacterAsync(2, 18); // Boba Fett
        await SeedFilmCharacterAsync(2, 19); // Wedge Antilles
        await SeedFilmCharacterAsync(2, 20); // Lando Calrissian
        await SeedFilmCharacterAsync(2, 7); // Chewbacca
        await SeedFilmCharacterAsync(2, 8); // R2-D2
        await SeedFilmCharacterAsync(2, 9); // C-3PO
        await SeedFilmCharacterAsync(2, 6); // Yoda

        // The Empire Strikes Back - Planets
        await SeedFilmPlanetAsync(2, 4); // Hoth
        await SeedFilmPlanetAsync(2, 5); // Dagobah
        await SeedFilmPlanetAsync(2, 6); // Bespin
        await SeedFilmPlanetAsync(2, 15); // Polis Massa

        // The Empire Strikes Back - Starships
        await SeedFilmStarshipAsync(2, 3); // TIE Advanced x1
        await SeedFilmStarshipAsync(2, 10); // Jedi Interceptor
        await SeedFilmStarshipAsync(2, 9); // Slave 1
        await SeedFilmStarshipAsync(2, 10); // Imperial shuttle
        await SeedFilmStarshipAsync(2, 5); // Star Destroyer
        await SeedFilmStarshipAsync(2, 4); // Millennium Falcon
        await SeedFilmStarshipAsync(2, 2); // X-wing

        // The Empire Strikes Back - Vehicles
        await SeedFilmVehicleAsync(2, 4); // Sand Crawler
        await SeedFilmVehicleAsync(2, 6); // T-16 skyhopper
        await SeedFilmVehicleAsync(2, 7); // X-34 landspeeder
        await SeedFilmVehicleAsync(2, 8); // TIE/LN starfighter

        // The Empire Strikes Back - Species
        await SeedFilmSpeciesAsync(2, 1); // Human
        await SeedFilmSpeciesAsync(2, 2); // Droid
        await SeedFilmSpeciesAsync(2, 3); // Wookie
        await SeedFilmSpeciesAsync(2, 4); // Rodian
        await SeedFilmSpeciesAsync(2, 5); // Hutt

        // Return of the Jedi (Episode 6) - Film ID 3
        await SeedFilmCharacterAsync(3, 1); // Luke Skywalker
        await SeedFilmCharacterAsync(3, 2); // Leia Organa
        await SeedFilmCharacterAsync(3, 3); // Han Solo
        await SeedFilmCharacterAsync(3, 4); // Darth Vader
        await SeedFilmCharacterAsync(3, 5); // Obi-Wan Kenobi
        await SeedFilmCharacterAsync(3, 6); // Yoda
        await SeedFilmCharacterAsync(3, 7); // Chewbacca
        await SeedFilmCharacterAsync(3, 8); // R2-D2
        await SeedFilmCharacterAsync(3, 9); // C-3PO
        await SeedFilmCharacterAsync(3, 10); // Anakin Skywalker
        await SeedFilmCharacterAsync(3, 12); // Padmé Amidala
        await SeedFilmCharacterAsync(3, 13); // Mace Windu
        await SeedFilmCharacterAsync(3, 14); // Qui-Gon Jinn
        await SeedFilmCharacterAsync(3, 15); // Count Dooku
        await SeedFilmCharacterAsync(3, 16); // Palpatine
        await SeedFilmCharacterAsync(3, 18); // Boba Fett
        await SeedFilmCharacterAsync(3, 19); // Jango Fett
        await SeedFilmCharacterAsync(3, 16); // Jabba Desilijic Tiure

        // Return of the Jedi - Planets
        await SeedFilmPlanetAsync(3, 1); // Tatooine
        await SeedFilmPlanetAsync(3, 2); // Alderaan
        await SeedFilmPlanetAsync(3, 3); // Yavin IV
        await SeedFilmPlanetAsync(3, 4); // Hoth
        await SeedFilmPlanetAsync(3, 5); // Dagobah
        await SeedFilmPlanetAsync(3, 6); // Bespin
        await SeedFilmPlanetAsync(3, 7); // Endor
        await SeedFilmPlanetAsync(3, 8); // Naboo
        await SeedFilmPlanetAsync(3, 9); // Coruscant
        await SeedFilmPlanetAsync(3, 10); // Kamino
        await SeedFilmPlanetAsync(3, 11); // Geonosis
        await SeedFilmPlanetAsync(3, 12); // Utapau
        await SeedFilmPlanetAsync(3, 13); // Mustafar
        await SeedFilmPlanetAsync(3, 14); // Kashyyyk
        await SeedFilmPlanetAsync(3, 15); // Polis Massa

        // Return of the Jedi - Starships
        await SeedFilmStarshipAsync(3, 2); // X-wing
        await SeedFilmStarshipAsync(3, 3); // TIE Advanced x1
        await SeedFilmStarshipAsync(3, 5); // Death Star
        await SeedFilmStarshipAsync(3, 9); // Naboo fighter
        await SeedFilmStarshipAsync(3, 10); // Jedi Interceptor
        await SeedFilmStarshipAsync(3, 9); // Slave 1
        await SeedFilmStarshipAsync(3, 10); // Imperial shuttle
        await SeedFilmStarshipAsync(3, 6); // Naboo Royal Starship

        // Return of the Jedi - Vehicles
        await SeedFilmVehicleAsync(3, 4); // Sand Crawler
        await SeedFilmVehicleAsync(3, 6); // T-16 skyhopper
        await SeedFilmVehicleAsync(3, 7); // X-34 landspeeder
        await SeedFilmVehicleAsync(3, 8); // TIE/LN starfighter

        // Return of the Jedi - Species
        await SeedFilmSpeciesAsync(3, 1); // Human
        await SeedFilmSpeciesAsync(3, 2); // Droid
        await SeedFilmSpeciesAsync(3, 3); // Wookie
        await SeedFilmSpeciesAsync(3, 4); // Rodian
        await SeedFilmSpeciesAsync(3, 5); // Hutt

        // The Phantom Menace (Episode 1) - Film ID 4
        await SeedFilmCharacterAsync(4, 10); // Anakin Skywalker
        await SeedFilmCharacterAsync(4, 12); // Padmé Amidala
        await SeedFilmCharacterAsync(4, 13); // Mace Windu
        await SeedFilmCharacterAsync(4, 14); // Qui-Gon Jinn
        await SeedFilmCharacterAsync(4, 15); // Count Dooku
        await SeedFilmCharacterAsync(4, 16); // Palpatine
        await SeedFilmCharacterAsync(4, 18); // Boba Fett
        await SeedFilmCharacterAsync(4, 19); // Jango Fett
        await SeedFilmCharacterAsync(4, 16); // Jabba Desilijic Tiure

        // The Phantom Menace - Planets
        await SeedFilmPlanetAsync(4, 8); // Naboo
        await SeedFilmPlanetAsync(4, 9); // Coruscant
        await SeedFilmPlanetAsync(4, 10); // Kamino
        await SeedFilmPlanetAsync(4, 11); // Geonosis
        await SeedFilmPlanetAsync(4, 12); // Utapau
        await SeedFilmPlanetAsync(4, 13); // Mustafar
        await SeedFilmPlanetAsync(4, 14); // Kashyyyk
        await SeedFilmPlanetAsync(4, 15); // Polis Massa

        // The Phantom Menace - Starships
        await SeedFilmStarshipAsync(4, 9); // Naboo fighter
        await SeedFilmStarshipAsync(4, 10); // Jedi Interceptor
        await SeedFilmStarshipAsync(4, 11); // Slave 1
        await SeedFilmStarshipAsync(4, 12); // Imperial shuttle
        await SeedFilmStarshipAsync(4, 13); // Naboo Royal Starship

        // The Phantom Menace - Vehicles
        await SeedFilmVehicleAsync(4, 4); // Sand Crawler
        await SeedFilmVehicleAsync(4, 6); // T-16 skyhopper
        await SeedFilmVehicleAsync(4, 7); // X-34 landspeeder
        await SeedFilmVehicleAsync(4, 8); // TIE/LN starfighter

        // The Phantom Menace - Species
        await SeedFilmSpeciesAsync(4, 1); // Human
        await SeedFilmSpeciesAsync(4, 2); // Droid
        await SeedFilmSpeciesAsync(4, 3); // Wookie
        await SeedFilmSpeciesAsync(4, 4); // Rodian
        await SeedFilmSpeciesAsync(4, 5); // Hutt

        // Attack of the Clones (Episode 2) - Film ID 5
        await SeedFilmCharacterAsync(5, 10); // Anakin Skywalker
        await SeedFilmCharacterAsync(5, 12); // Padmé Amidala
        await SeedFilmCharacterAsync(5, 13); // Mace Windu
        await SeedFilmCharacterAsync(5, 14); // Qui-Gon Jinn
        await SeedFilmCharacterAsync(5, 15); // Count Dooku
        await SeedFilmCharacterAsync(5, 16); // Palpatine
        await SeedFilmCharacterAsync(5, 18); // Boba Fett
        await SeedFilmCharacterAsync(5, 19); // Jango Fett
        await SeedFilmCharacterAsync(5, 16); // Jabba Desilijic Tiure

        // Attack of the Clones - Planets
        await SeedFilmPlanetAsync(5, 8); // Naboo
        await SeedFilmPlanetAsync(5, 9); // Coruscant
        await SeedFilmPlanetAsync(5, 10); // Kamino
        await SeedFilmPlanetAsync(5, 11); // Geonosis
        await SeedFilmPlanetAsync(5, 12); // Utapau
        await SeedFilmPlanetAsync(5, 13); // Mustafar
        await SeedFilmPlanetAsync(5, 14); // Kashyyyk
        await SeedFilmPlanetAsync(5, 15); // Polis Massa

        // Attack of the Clones - Starships
        await SeedFilmStarshipAsync(5, 9); // Naboo fighter
        await SeedFilmStarshipAsync(5, 10); // Jedi Interceptor
        await SeedFilmStarshipAsync(5, 11); // Slave 1
        await SeedFilmStarshipAsync(5, 12); // Imperial shuttle
        await SeedFilmStarshipAsync(5, 13); // Naboo Royal Starship

        // Attack of the Clones - Vehicles
        await SeedFilmVehicleAsync(5, 4); // Sand Crawler
        await SeedFilmVehicleAsync(5, 6); // T-16 skyhopper
        await SeedFilmVehicleAsync(5, 7); // X-34 landspeeder
        await SeedFilmVehicleAsync(5, 8); // TIE/LN starfighter

        // Attack of the Clones - Species
        await SeedFilmSpeciesAsync(5, 1); // Human
        await SeedFilmSpeciesAsync(5, 2); // Droid
        await SeedFilmSpeciesAsync(5, 3); // Wookie
        await SeedFilmSpeciesAsync(5, 4); // Rodian
        await SeedFilmSpeciesAsync(5, 5); // Hutt

        // Revenge of the Sith (Episode 3) - Film ID 6
        await SeedFilmCharacterAsync(6, 10); // Anakin Skywalker
        await SeedFilmCharacterAsync(6, 12); // Padmé Amidala
        await SeedFilmCharacterAsync(6, 13); // Mace Windu
        await SeedFilmCharacterAsync(6, 14); // Qui-Gon Jinn
        await SeedFilmCharacterAsync(6, 15); // Count Dooku
        await SeedFilmCharacterAsync(6, 16); // Palpatine
        await SeedFilmCharacterAsync(6, 18); // Boba Fett
        await SeedFilmCharacterAsync(6, 19); // Jango Fett
        await SeedFilmCharacterAsync(6, 16); // Jabba Desilijic Tiure

        // Revenge of the Sith - Planets
        await SeedFilmPlanetAsync(6, 8); // Naboo
        await SeedFilmPlanetAsync(6, 9); // Coruscant
        await SeedFilmPlanetAsync(6, 10); // Kamino
        await SeedFilmPlanetAsync(6, 11); // Geonosis
        await SeedFilmPlanetAsync(6, 12); // Utapau
        await SeedFilmPlanetAsync(6, 13); // Mustafar
        await SeedFilmPlanetAsync(6, 14); // Kashyyyk
        await SeedFilmPlanetAsync(6, 15); // Polis Massa

        // Revenge of the Sith - Starships
        await SeedFilmStarshipAsync(6, 9); // Naboo fighter
        await SeedFilmStarshipAsync(6, 10); // Jedi Interceptor
        await SeedFilmStarshipAsync(6, 11); // Slave 1
        await SeedFilmStarshipAsync(6, 12); // Imperial shuttle
        await SeedFilmStarshipAsync(6, 13); // Naboo Royal Starship

        // Revenge of the Sith - Vehicles
        await SeedFilmVehicleAsync(6, 4); // Sand Crawler
        await SeedFilmVehicleAsync(6, 6); // T-16 skyhopper
        await SeedFilmVehicleAsync(6, 7); // X-34 landspeeder
        await SeedFilmVehicleAsync(6, 8); // TIE/LN starfighter

        // Revenge of the Sith - Species
        await SeedFilmSpeciesAsync(6, 1); // Human
        await SeedFilmSpeciesAsync(6, 2); // Droid
        await SeedFilmSpeciesAsync(6, 3); // Wookie
        await SeedFilmSpeciesAsync(6, 4); // Rodian
        await SeedFilmSpeciesAsync(6, 5); // Hutt

        // The Force Awakens (Episode 7) - Film ID 7
        await SeedFilmCharacterAsync(7, 1); // Luke Skywalker
        await SeedFilmCharacterAsync(7, 2); // Leia Organa
        await SeedFilmCharacterAsync(7, 3); // Han Solo
        await SeedFilmCharacterAsync(7, 4); // Darth Vader
        await SeedFilmCharacterAsync(7, 5); // Obi-Wan Kenobi
        await SeedFilmCharacterAsync(7, 6); // Yoda
        await SeedFilmCharacterAsync(7, 7); // Chewbacca
        await SeedFilmCharacterAsync(7, 8); // R2-D2
        await SeedFilmCharacterAsync(7, 9); // C-3PO
        await SeedFilmCharacterAsync(7, 10); // Anakin Skywalker
        await SeedFilmCharacterAsync(7, 12); // Padmé Amidala
        await SeedFilmCharacterAsync(7, 13); // Mace Windu
        await SeedFilmCharacterAsync(7, 14); // Qui-Gon Jinn
        await SeedFilmCharacterAsync(7, 15); // Count Dooku
        await SeedFilmCharacterAsync(7, 16); // Palpatine
        await SeedFilmCharacterAsync(7, 18); // Boba Fett
        await SeedFilmCharacterAsync(7, 19); // Jango Fett
        await SeedFilmCharacterAsync(7, 16); // Jabba Desilijic Tiure

        // The Force Awakens - Planets
        await SeedFilmPlanetAsync(7, 1); // Tatooine
        await SeedFilmPlanetAsync(7, 2); // Alderaan
        await SeedFilmPlanetAsync(7, 3); // Yavin IV
        await SeedFilmPlanetAsync(7, 4); // Hoth
        await SeedFilmPlanetAsync(7, 5); // Dagobah
        await SeedFilmPlanetAsync(7, 6); // Bespin
        await SeedFilmPlanetAsync(7, 7); // Endor
        await SeedFilmPlanetAsync(7, 8); // Naboo
        await SeedFilmPlanetAsync(7, 9); // Coruscant
        await SeedFilmPlanetAsync(7, 10); // Kamino
        await SeedFilmPlanetAsync(7, 11); // Geonosis
        await SeedFilmPlanetAsync(7, 12); // Utapau
        await SeedFilmPlanetAsync(7, 13); // Mustafar
        await SeedFilmPlanetAsync(7, 14); // Kashyyyk
        await SeedFilmPlanetAsync(7, 15); // Polis Massa

        // The Force Awakens - Starships
        await SeedFilmStarshipAsync(7, 2); // X-wing
        await SeedFilmStarshipAsync(7, 3); // TIE Advanced x1
        await SeedFilmStarshipAsync(7, 5); // Death Star
        await SeedFilmStarshipAsync(7, 9); // Naboo fighter
        await SeedFilmStarshipAsync(7, 10); // Jedi Interceptor
        await SeedFilmStarshipAsync(7, 11); // Slave 1
        await SeedFilmStarshipAsync(7, 12); // Imperial shuttle
        await SeedFilmStarshipAsync(7, 13); // Naboo Royal Starship

        // The Force Awakens - Vehicles
        await SeedFilmVehicleAsync(7, 4); // Sand Crawler
        await SeedFilmVehicleAsync(7, 6); // T-16 skyhopper
        await SeedFilmVehicleAsync(7, 7); // X-34 landspeeder
        await SeedFilmVehicleAsync(7, 8); // TIE/LN starfighter

        // The Force Awakens - Species
        await SeedFilmSpeciesAsync(7, 1); // Human
        await SeedFilmSpeciesAsync(7, 2); // Droid
        await SeedFilmSpeciesAsync(7, 3); // Wookie
        await SeedFilmSpeciesAsync(7, 4); // Rodian
        await SeedFilmSpeciesAsync(7, 5); // Hutt

        await _context.SaveChangesAsync();
        _logger.LogInformation("Relacionamentos de filmes populados com sucesso!");
    }

    private async Task SeedPersonRelationshipsAsync()
    {
        // Luke Skywalker (ID 1) - Homeworld: Tatooine (ID 1)
        await SeedPersonHomeworldAsync(1, 1);
        
        // Leia Organa (ID 2) - Homeworld: Alderaan (ID 2)
        await SeedPersonHomeworldAsync(2, 2);
        
        // Han Solo (ID 3) - Homeworld: Corellia (ID 3)
        await SeedPersonHomeworldAsync(3, 3);
        
        // Darth Vader (ID 4) - Homeworld: Tatooine (ID 1)
        await SeedPersonHomeworldAsync(4, 1);
        
        // Obi-Wan Kenobi (ID 5) - Homeworld: Stewjon (ID 4)
        await SeedPersonHomeworldAsync(5, 4);
        
        // Yoda (ID 6) - Homeworld: Dagobah (ID 5)
        await SeedPersonHomeworldAsync(6, 5);
        
        // Chewbacca (ID 7) - Homeworld: Kashyyyk (ID 14)
        await SeedPersonHomeworldAsync(7, 14);
        
        // R2-D2 (ID 8) - Homeworld: Naboo (ID 8)
        await SeedPersonHomeworldAsync(8, 8);
        
        // C-3PO (ID 9) - Homeworld: Tatooine (ID 1)
        await SeedPersonHomeworldAsync(9, 1);
        
        // Anakin Skywalker (ID 10) - Homeworld: Tatooine (ID 1)
        await SeedPersonHomeworldAsync(10, 1);
        
        // Padmé Amidala (ID 12) - Homeworld: Naboo (ID 8)
        await SeedPersonHomeworldAsync(12, 8);
        
        // Mace Windu (ID 13) - Homeworld: Haruun Kal (ID 9)
        await SeedPersonHomeworldAsync(13, 9);
        
        // Qui-Gon Jinn (ID 14) - Homeworld: Coruscant (ID 9)
        await SeedPersonHomeworldAsync(14, 9);
        
        // Count Dooku (ID 15) - Homeworld: Serenno (ID 10)
        await SeedPersonHomeworldAsync(15, 10);
        
        // Palpatine (ID 16) - Homeworld: Naboo (ID 8)
        await SeedPersonHomeworldAsync(16, 8);
        
        // Boba Fett (ID 18) - Homeworld: Kamino (ID 10)
        await SeedPersonHomeworldAsync(18, 10);
        
        // Jango Fett (ID 19) - Homeworld: Concord Dawn (ID 11)
        await SeedPersonHomeworldAsync(19, 11);
        
        // Jabba Desilijic Tiure (ID 16) - Homeworld: Nal Hutta (ID 24)
        await SeedPersonHomeworldAsync(16, 24);
        
        // Wedge Antilles (ID 18) - Homeworld: Corellia (ID 22)
        await SeedPersonHomeworldAsync(18, 22);
        
        // Lando Calrissian (ID 25) - Homeworld: Socorro (ID 30)
        await SeedPersonHomeworldAsync(25, 30);

        // Relacionamentos Person-Species
        await SeedPersonSpeciesAsync(1, 1); // Luke Skywalker - Human
        await SeedPersonSpeciesAsync(2, 1); // Leia Organa - Human
        await SeedPersonSpeciesAsync(3, 1); // Han Solo - Human
        await SeedPersonSpeciesAsync(4, 1); // Darth Vader - Human
        await SeedPersonSpeciesAsync(5, 1); // Obi-Wan Kenobi - Human
        await SeedPersonSpeciesAsync(6, 5); // Yoda - Yoda's species
        await SeedPersonSpeciesAsync(7, 3); // Chewbacca - Wookie
        await SeedPersonSpeciesAsync(8, 2); // R2-D2 - Droid
        await SeedPersonSpeciesAsync(9, 2); // C-3PO - Droid
        await SeedPersonSpeciesAsync(10, 1); // Anakin Skywalker - Human
        await SeedPersonSpeciesAsync(12, 1); // Padmé Amidala - Human
        await SeedPersonSpeciesAsync(13, 1); // Mace Windu - Human
        await SeedPersonSpeciesAsync(14, 1); // Qui-Gon Jinn - Human
        await SeedPersonSpeciesAsync(15, 1); // Count Dooku - Human
        await SeedPersonSpeciesAsync(16, 1); // Palpatine - Human
        await SeedPersonSpeciesAsync(18, 1); // Boba Fett - Human
        await SeedPersonSpeciesAsync(19, 1); // Jango Fett - Human
        await SeedPersonSpeciesAsync(16, 5); // Jabba Desilijic Tiure - Hutt
        await SeedPersonSpeciesAsync(18, 1); // Wedge Antilles - Human
        await SeedPersonSpeciesAsync(25, 1); // Lando Calrissian - Human

        // Relacionamentos Person-Vehicles
        await SeedPersonVehicleAsync(1, 4); // Luke Skywalker - Sand Crawler
        await SeedPersonVehicleAsync(1, 6); // Luke Skywalker - T-16 skyhopper
        await SeedPersonVehicleAsync(1, 7); // Luke Skywalker - X-34 landspeeder
        await SeedPersonVehicleAsync(1, 8); // Luke Skywalker - TIE/LN starfighter
        await SeedPersonVehicleAsync(2, 4); // Leia Organa - Sand Crawler
        await SeedPersonVehicleAsync(2, 6); // Leia Organa - T-16 skyhopper
        await SeedPersonVehicleAsync(2, 7); // Leia Organa - X-34 landspeeder
        await SeedPersonVehicleAsync(2, 8); // Leia Organa - TIE/LN starfighter
        await SeedPersonVehicleAsync(3, 4); // Han Solo - Sand Crawler
        await SeedPersonVehicleAsync(3, 6); // Han Solo - T-16 skyhopper
        await SeedPersonVehicleAsync(3, 7); // Han Solo - X-34 landspeeder
        await SeedPersonVehicleAsync(3, 8); // Han Solo - TIE/LN starfighter

        // Relacionamentos Person-Starships
        await SeedPersonStarshipAsync(1, 2); // Luke Skywalker - X-wing
        await SeedPersonStarshipAsync(1, 3); // Luke Skywalker - TIE Advanced x1
        await SeedPersonStarshipAsync(1, 5); // Luke Skywalker - Death Star
        await SeedPersonStarshipAsync(1, 9); // Luke Skywalker - Naboo fighter
        await SeedPersonStarshipAsync(1, 10); // Luke Skywalker - Jedi Interceptor
        await SeedPersonStarshipAsync(1, 11); // Luke Skywalker - Slave 1
        await SeedPersonStarshipAsync(1, 12); // Luke Skywalker - Imperial shuttle
        await SeedPersonStarshipAsync(1, 13); // Luke Skywalker - Naboo Royal Starship
        await SeedPersonStarshipAsync(2, 2); // Leia Organa - X-wing
        await SeedPersonStarshipAsync(2, 3); // Leia Organa - TIE Advanced x1
        await SeedPersonStarshipAsync(2, 5); // Leia Organa - Death Star
        await SeedPersonStarshipAsync(2, 9); // Leia Organa - Naboo fighter
        await SeedPersonStarshipAsync(2, 10); // Leia Organa - Jedi Interceptor
        await SeedPersonStarshipAsync(2, 11); // Leia Organa - Slave 1
        await SeedPersonStarshipAsync(2, 12); // Leia Organa - Imperial shuttle
        await SeedPersonStarshipAsync(2, 13); // Leia Organa - Naboo Royal Starship
        await SeedPersonStarshipAsync(3, 2); // Han Solo - X-wing
        await SeedPersonStarshipAsync(3, 3); // Han Solo - TIE Advanced x1
        await SeedPersonStarshipAsync(3, 5); // Han Solo - Death Star
        await SeedPersonStarshipAsync(3, 9); // Han Solo - Naboo fighter
        await SeedPersonStarshipAsync(3, 10); // Han Solo - Jedi Interceptor
        await SeedPersonStarshipAsync(3, 11); // Han Solo - Slave 1
        await SeedPersonStarshipAsync(3, 12); // Han Solo - Imperial shuttle
        await SeedPersonStarshipAsync(3, 13); // Han Solo - Naboo Royal Starship

        await _context.SaveChangesAsync();
        _logger.LogInformation("Relacionamentos de pessoas populados com sucesso!");
    }

    private async Task SeedPlanetRelationshipsAsync()
    {
        // Relacionamentos Planet-Resident
        await SeedPlanetResidentAsync(1, 1); // Tatooine - Luke Skywalker
        await SeedPlanetResidentAsync(1, 2); // Tatooine - Leia Organa
        await SeedPlanetResidentAsync(1, 3); // Tatooine - Han Solo
        await SeedPlanetResidentAsync(1, 4); // Tatooine - Darth Vader
        await SeedPlanetResidentAsync(1, 5); // Tatooine - Obi-Wan Kenobi
        await SeedPlanetResidentAsync(1, 6); // Tatooine - Yoda
        await SeedPlanetResidentAsync(1, 7); // Tatooine - Chewbacca
        await SeedPlanetResidentAsync(1, 8); // Tatooine - R2-D2
        await SeedPlanetResidentAsync(1, 9); // Tatooine - C-3PO
        await SeedPlanetResidentAsync(1, 10); // Tatooine - Anakin Skywalker
        await SeedPlanetResidentAsync(1, 12); // Tatooine - Padmé Amidala
        await SeedPlanetResidentAsync(1, 13); // Tatooine - Mace Windu
        await SeedPlanetResidentAsync(1, 14); // Tatooine - Qui-Gon Jinn
        await SeedPlanetResidentAsync(1, 15); // Tatooine - Count Dooku
        await SeedPlanetResidentAsync(1, 16); // Tatooine - Palpatine
        await SeedPlanetResidentAsync(1, 18); // Tatooine - Boba Fett
        await SeedPlanetResidentAsync(1, 19); // Tatooine - Jango Fett
        await SeedPlanetResidentAsync(1, 16); // Tatooine - Jabba Desilijic Tiure
        await SeedPlanetResidentAsync(1, 18); // Tatooine - Wedge Antilles
        await SeedPlanetResidentAsync(1, 25); // Tatooine - Lando Calrissian

        await SeedPlanetResidentAsync(2, 1); // Alderaan - Luke Skywalker
        await SeedPlanetResidentAsync(2, 2); // Alderaan - Leia Organa
        await SeedPlanetResidentAsync(2, 3); // Alderaan - Han Solo
        await SeedPlanetResidentAsync(2, 4); // Alderaan - Darth Vader
        await SeedPlanetResidentAsync(2, 5); // Alderaan - Obi-Wan Kenobi
        await SeedPlanetResidentAsync(2, 6); // Alderaan - Yoda
        await SeedPlanetResidentAsync(2, 7); // Alderaan - Chewbacca
        await SeedPlanetResidentAsync(2, 8); // Alderaan - R2-D2
        await SeedPlanetResidentAsync(2, 9); // Alderaan - C-3PO
        await SeedPlanetResidentAsync(2, 10); // Alderaan - Anakin Skywalker
        await SeedPlanetResidentAsync(2, 12); // Alderaan - Padmé Amidala
        await SeedPlanetResidentAsync(2, 13); // Alderaan - Mace Windu
        await SeedPlanetResidentAsync(2, 14); // Alderaan - Qui-Gon Jinn
        await SeedPlanetResidentAsync(2, 15); // Alderaan - Count Dooku
        await SeedPlanetResidentAsync(2, 16); // Alderaan - Palpatine
        await SeedPlanetResidentAsync(2, 18); // Alderaan - Boba Fett
        await SeedPlanetResidentAsync(2, 19); // Alderaan - Jango Fett
        await SeedPlanetResidentAsync(2, 16); // Alderaan - Jabba Desilijic Tiure
        await SeedPlanetResidentAsync(2, 18); // Alderaan - Wedge Antilles
        await SeedPlanetResidentAsync(2, 25); // Alderaan - Lando Calrissian

        await SeedPlanetResidentAsync(3, 1); // Yavin IV - Luke Skywalker
        await SeedPlanetResidentAsync(3, 2); // Yavin IV - Leia Organa
        await SeedPlanetResidentAsync(3, 3); // Yavin IV - Han Solo
        await SeedPlanetResidentAsync(3, 4); // Yavin IV - Darth Vader
        await SeedPlanetResidentAsync(3, 5); // Yavin IV - Obi-Wan Kenobi
        await SeedPlanetResidentAsync(3, 6); // Yavin IV - Yoda
        await SeedPlanetResidentAsync(3, 7); // Yavin IV - Chewbacca
        await SeedPlanetResidentAsync(3, 8); // Yavin IV - R2-D2
        await SeedPlanetResidentAsync(3, 9); // Yavin IV - C-3PO
        await SeedPlanetResidentAsync(3, 10); // Yavin IV - Anakin Skywalker
        await SeedPlanetResidentAsync(3, 12); // Yavin IV - Padmé Amidala
        await SeedPlanetResidentAsync(3, 13); // Yavin IV - Mace Windu
        await SeedPlanetResidentAsync(3, 14); // Yavin IV - Qui-Gon Jinn
        await SeedPlanetResidentAsync(3, 15); // Yavin IV - Count Dooku
        await SeedPlanetResidentAsync(3, 16); // Yavin IV - Palpatine
        await SeedPlanetResidentAsync(3, 18); // Yavin IV - Boba Fett
        await SeedPlanetResidentAsync(3, 19); // Yavin IV - Jango Fett
        await SeedPlanetResidentAsync(3, 16); // Yavin IV - Jabba Desilijic Tiure
        await SeedPlanetResidentAsync(3, 18); // Yavin IV - Wedge Antilles
        await SeedPlanetResidentAsync(3, 25); // Yavin IV - Lando Calrissian

        await _context.SaveChangesAsync();
        _logger.LogInformation("Relacionamentos de planetas populados com sucesso!");
    }

    // Métodos auxiliares para criar relacionamentos
    private async Task SeedFilmCharacterAsync(int filmId, int personId)
    {
        if (!await _context.FilmCharacters.AnyAsync(fc => fc.FilmId == filmId && fc.PersonId == personId))
        {
            await _context.FilmCharacters.AddAsync(new Models.FilmCharacter
            {
                FilmId = filmId,
                PersonId = personId,
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    private async Task SeedFilmPlanetAsync(int filmId, int planetId)
    {
        if (!await _context.FilmPlanets.AnyAsync(fp => fp.FilmId == filmId && fp.PlanetId == planetId))
        {
            await _context.FilmPlanets.AddAsync(new Models.FilmPlanet
            {
                FilmId = filmId,
                PlanetId = planetId,
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    private async Task SeedFilmStarshipAsync(int filmId, int starshipId)
    {
        if (!await _context.FilmStarships.AnyAsync(fs => fs.FilmId == filmId && fs.StarshipId == starshipId))
        {
            await _context.FilmStarships.AddAsync(new Models.FilmStarship
            {
                FilmId = filmId,
                StarshipId = starshipId,
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    private async Task SeedFilmVehicleAsync(int filmId, int vehicleId)
    {
        if (!await _context.FilmVehicles.AnyAsync(fv => fv.FilmId == filmId && fv.VehicleId == vehicleId))
        {
            await _context.FilmVehicles.AddAsync(new Models.FilmVehicle
            {
                FilmId = filmId,
                VehicleId = vehicleId,
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    private async Task SeedFilmSpeciesAsync(int filmId, int speciesId)
    {
        if (!await _context.FilmSpecies.AnyAsync(fs => fs.FilmId == filmId && fs.SpeciesId == speciesId))
        {
            await _context.FilmSpecies.AddAsync(new Models.FilmSpecies
            {
                FilmId = filmId,
                SpeciesId = speciesId,
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    private async Task SeedPersonHomeworldAsync(int personId, int planetId)
    {
        var person = await _context.People.FindAsync(personId);
        if (person != null)
        {
            person.HomeworldPlanetId = planetId;
        }
    }

    private async Task SeedPersonSpeciesAsync(int personId, int speciesId)
    {
        if (!await _context.PersonSpecies.AnyAsync(ps => ps.PersonId == personId && ps.SpeciesId == speciesId))
        {
            await _context.PersonSpecies.AddAsync(new Models.PersonSpecies
            {
                PersonId = personId,
                SpeciesId = speciesId,
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    private async Task SeedPersonVehicleAsync(int personId, int vehicleId)
    {
        if (!await _context.PersonVehicles.AnyAsync(pv => pv.PersonId == personId && pv.VehicleId == vehicleId))
        {
            await _context.PersonVehicles.AddAsync(new Models.PersonVehicle
            {
                PersonId = personId,
                VehicleId = vehicleId,
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    private async Task SeedPersonStarshipAsync(int personId, int starshipId)
    {
        if (!await _context.PersonStarships.AnyAsync(ps => ps.PersonId == personId && ps.StarshipId == starshipId))
        {
            await _context.PersonStarships.AddAsync(new Models.PersonStarship
            {
                PersonId = personId,
                StarshipId = starshipId,
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    private async Task SeedPlanetResidentAsync(int planetId, int personId)
    {
        if (!await _context.PlanetResidents.AnyAsync(pr => pr.PlanetId == planetId && pr.PersonId == personId))
        {
            await _context.PlanetResidents.AddAsync(new Models.PlanetResident
            {
                PlanetId = planetId,
                PersonId = personId,
                CreatedAt = DateTime.UtcNow
            });
        }
    }

    public async Task<bool> IsDatabaseSeededAsync()
    {
        try
        {
            var filmsCount = await _context.Films.CountAsync();
            var peopleCount = await _context.People.CountAsync();
            var planetsCount = await _context.Planets.CountAsync();
            var speciesCount = await _context.Species.CountAsync();
            var starshipsCount = await _context.Starships.CountAsync();
            var vehiclesCount = await _context.Vehicles.CountAsync();

            var totalEntities = filmsCount + peopleCount + planetsCount + speciesCount + starshipsCount + vehiclesCount;
            
            return totalEntities > 0;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Erro ao verificar se o banco foi populado. Assumindo que não foi.");
            return false;
        }
    }

    public async Task<int> GetSeedCountAsync()
    {
        try
        {
            var filmsCount = await _context.Films.CountAsync();
            var peopleCount = await _context.People.CountAsync();
            var planetsCount = await _context.Planets.CountAsync();
            var speciesCount = await _context.Species.CountAsync();
            var starshipsCount = await _context.Starships.CountAsync();
            var vehiclesCount = await _context.Vehicles.CountAsync();

            return filmsCount + peopleCount + planetsCount + speciesCount + starshipsCount + vehiclesCount;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Erro ao contar entidades. Retornando 0.");
            return 0;
        }
    }
}
