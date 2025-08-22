using Microsoft.EntityFrameworkCore;
using StarWars.Api.Models;

namespace StarWars.Api.Data.Context;

public class StarWarsDbContext : DbContext
{
    // Entidades principais
    public DbSet<Film> Films => Set<Film>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<Planet> Planets => Set<Planet>();
    public DbSet<Starship> Starships => Set<Starship>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Species> Species => Set<Species>();

    // Tabelas de junção
    public DbSet<FilmCharacter> FilmCharacters => Set<FilmCharacter>();
    public DbSet<FilmPlanet> FilmPlanets => Set<FilmPlanet>();
    public DbSet<FilmStarship> FilmStarships => Set<FilmStarship>();
    public DbSet<FilmVehicle> FilmVehicles => Set<FilmVehicle>();
    public DbSet<FilmSpecies> FilmSpecies => Set<FilmSpecies>();
    public DbSet<PersonSpecies> PersonSpecies => Set<PersonSpecies>();
    public DbSet<PersonVehicle> PersonVehicles => Set<PersonVehicle>();
    public DbSet<PersonStarship> PersonStarships => Set<PersonStarship>();
    public DbSet<PlanetResident> PlanetResidents => Set<PlanetResident>();

    public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StarWarsDbContext).Assembly);
        
        // Configurações das entidades principais
        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("films");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
            entity.HasIndex(e => e.Title);
            entity.HasIndex(e => e.EpisodeId);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("people");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name);
            
            // Relacionamento com Planet (homeworld)
            entity.HasOne(e => e.HomeworldPlanet)
                  .WithMany(e => e.HomeworldPeople)
                  .HasForeignKey(e => e.HomeworldPlanetId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Planet>(entity =>
        {
            entity.ToTable("planets");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name);
        });

        modelBuilder.Entity<Starship>(entity =>
        {
            entity.ToTable("starships");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("vehicles");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name);
        });

        modelBuilder.Entity<Species>(entity =>
        {
            entity.ToTable("species");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name);
            
            // Relacionamento com Planet (homeworld)
            entity.HasOne(e => e.HomeworldPlanet)
                  .WithMany(e => e.HomeworldSpecies)
                  .HasForeignKey(e => e.HomeworldPlanetId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configurações das tabelas de junção
        modelBuilder.Entity<FilmCharacter>(entity =>
        {
            entity.ToTable("film_characters");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.FilmId, e.PersonId }).IsUnique();
            
            entity.HasOne(e => e.Film)
                  .WithMany(e => e.FilmCharacters)
                  .HasForeignKey(e => e.FilmId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Person)
                  .WithMany(e => e.FilmCharacters)
                  .HasForeignKey(e => e.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FilmPlanet>(entity =>
        {
            entity.ToTable("film_planets");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.FilmId, e.PlanetId }).IsUnique();
            
            entity.HasOne(e => e.Film)
                  .WithMany(e => e.FilmPlanets)
                  .HasForeignKey(e => e.FilmId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Planet)
                  .WithMany(e => e.FilmPlanets)
                  .HasForeignKey(e => e.PlanetId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FilmStarship>(entity =>
        {
            entity.ToTable("film_starships");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.FilmId, e.StarshipId }).IsUnique();
            
            entity.HasOne(e => e.Film)
                  .WithMany(e => e.FilmStarships)
                  .HasForeignKey(e => e.FilmId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Starship)
                  .WithMany(e => e.FilmStarships)
                  .HasForeignKey(e => e.StarshipId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FilmVehicle>(entity =>
        {
            entity.ToTable("film_vehicles");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.FilmId, e.VehicleId }).IsUnique();
            
            entity.HasOne(e => e.Film)
                  .WithMany(e => e.FilmVehicles)
                  .HasForeignKey(e => e.FilmId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Vehicle)
                  .WithMany(e => e.FilmVehicles)
                  .HasForeignKey(e => e.VehicleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FilmSpecies>(entity =>
        {
            entity.ToTable("film_species");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.FilmId, e.SpeciesId }).IsUnique();
            
            entity.HasOne(e => e.Film)
                  .WithMany(e => e.FilmSpecies)
                  .HasForeignKey(e => e.FilmId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Species)
                  .WithMany(e => e.FilmSpecies)
                  .HasForeignKey(e => e.SpeciesId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PersonSpecies>(entity =>
        {
            entity.ToTable("person_species");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.PersonId, e.SpeciesId }).IsUnique();
            
            entity.HasOne(e => e.Person)
                  .WithMany(e => e.PersonSpecies)
                  .HasForeignKey(e => e.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Species)
                  .WithMany(e => e.PersonSpecies)
                  .HasForeignKey(e => e.SpeciesId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PersonVehicle>(entity =>
        {
            entity.ToTable("person_vehicles");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.PersonId, e.VehicleId }).IsUnique();
            
            entity.HasOne(e => e.Person)
                  .WithMany(e => e.PersonVehicles)
                  .HasForeignKey(e => e.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Vehicle)
                  .WithMany(e => e.PersonVehicles)
                  .HasForeignKey(e => e.VehicleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PersonStarship>(entity =>
        {
            entity.ToTable("person_starships");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.PersonId, e.StarshipId }).IsUnique();
            
            entity.HasOne(e => e.Person)
                  .WithMany(e => e.PersonStarships)
                  .HasForeignKey(e => e.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Starship)
                  .WithMany(e => e.PersonStarships)
                  .HasForeignKey(e => e.StarshipId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PlanetResident>(entity =>
        {
            entity.ToTable("planet_residents");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.PlanetId, e.PersonId }).IsUnique();
            
            entity.HasOne(e => e.Planet)
                  .WithMany(e => e.PlanetResidents)
                  .HasForeignKey(e => e.PlanetId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Person)
                  .WithMany(e => e.PlanetResidents)
                  .HasForeignKey(e => e.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

