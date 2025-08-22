-- =====================================================
-- BANCO DE DADOS STAR WARS - LOCALDB/SQL SERVER
-- =====================================================
-- Otimizado para LocalDB (SQL Server Express)
-- Com tabelas de junção para relacionamentos N:M
-- =====================================================

USE master;
GO

-- Criar banco se não existir
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'StarWars')
BEGIN
    CREATE DATABASE StarWars;
END
GO

USE StarWars;
GO

-- =====================================================
-- CRIAÇÃO DAS TABELAS PRINCIPAIS
-- =====================================================

-- Tabela: films
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'films')
BEGIN
    CREATE TABLE films (
        id INT IDENTITY(1,1) PRIMARY KEY,
        title NVARCHAR(255) NOT NULL,
        episode_id INT,
        opening_crawl NVARCHAR(MAX),
        director NVARCHAR(255),
        producer NVARCHAR(255),
        release_date NVARCHAR(50),
        created NVARCHAR(100),
        edited NVARCHAR(100),
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        updated_at DATETIME2 DEFAULT GETUTCDATE()
    );
END

-- Tabela: people
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'people')
BEGIN
    CREATE TABLE people (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(255) NOT NULL,
        height NVARCHAR(50),
        mass NVARCHAR(50),
        hair_color NVARCHAR(100),
        skin_color NVARCHAR(100),
        eye_color NVARCHAR(100),
        birth_year NVARCHAR(50),
        gender NVARCHAR(50),
        homeworld_planet_id INT,
        created NVARCHAR(100),
        edited NVARCHAR(100),
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        updated_at DATETIME2 DEFAULT GETUTCDATE()
    );
END

-- Tabela: planets
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'planets')
BEGIN
    CREATE TABLE planets (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(255) NOT NULL,
        rotation_period NVARCHAR(50),
        orbital_period NVARCHAR(50),
        diameter NVARCHAR(50),
        climate NVARCHAR(100),
        gravity NVARCHAR(100),
        terrain NVARCHAR(100),
        surface_water NVARCHAR(50),
        population NVARCHAR(100),
        created NVARCHAR(100),
        edited NVARCHAR(100),
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        updated_at DATETIME2 DEFAULT GETUTCDATE()
    );
END

-- Tabela: starships
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'starships')
BEGIN
    CREATE TABLE starships (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(255) NOT NULL,
        model NVARCHAR(255),
        manufacturer NVARCHAR(255),
        cost_in_credits NVARCHAR(100),
        length NVARCHAR(100),
        max_atmosphering_speed NVARCHAR(100),
        crew NVARCHAR(100),
        passengers NVARCHAR(100),
        cargo_capacity NVARCHAR(100),
        consumables NVARCHAR(100),
        hyperdrive_rating NVARCHAR(100),
        MGLT NVARCHAR(100),
        starship_class NVARCHAR(100),
        created NVARCHAR(100),
        edited NVARCHAR(100),
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        updated_at DATETIME2 DEFAULT GETUTCDATE()
    );
END

-- Tabela: vehicles
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'vehicles')
BEGIN
    CREATE TABLE vehicles (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(255) NOT NULL,
        model NVARCHAR(255),
        manufacturer NVARCHAR(255),
        cost_in_credits NVARCHAR(100),
        length NVARCHAR(100),
        max_atmosphering_speed NVARCHAR(100),
        crew NVARCHAR(100),
        passengers NVARCHAR(100),
        cargo_capacity NVARCHAR(100),
        consumables NVARCHAR(100),
        vehicle_class NVARCHAR(100),
        created NVARCHAR(100),
        edited NVARCHAR(100),
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        updated_at DATETIME2 DEFAULT GETUTCDATE()
    );
END

-- Tabela: species
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'species')
BEGIN
    CREATE TABLE species (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(255) NOT NULL,
        classification NVARCHAR(100),
        designation NVARCHAR(100),
        average_height NVARCHAR(100),
        skin_colors NVARCHAR(100),
        hair_colors NVARCHAR(100),
        eye_colors NVARCHAR(100),
        average_lifespan NVARCHAR(100),
        homeworld_planet_id INT,
        language NVARCHAR(100),
        created NVARCHAR(100),
        edited NVARCHAR(100),
        
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        updated_at DATETIME2 DEFAULT GETUTCDATE()
    );
END

-- =====================================================
-- CRIAÇÃO DAS TABELAS DE JUNÇÃO (RELACIONAMENTOS N:M)
-- =====================================================

-- Films ↔ People
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'film_characters')
BEGIN
    CREATE TABLE film_characters (
        id INT IDENTITY(1,1) PRIMARY KEY,
        film_id INT NOT NULL,
        person_id INT NOT NULL,
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (film_id) REFERENCES films(id),
        FOREIGN KEY (person_id) REFERENCES people(id),
        UNIQUE (film_id, person_id)
    );
END

-- Films ↔ Planets
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'film_planets')
BEGIN
    CREATE TABLE film_planets (
        id INT IDENTITY(1,1) PRIMARY KEY,
        film_id INT NOT NULL,
        planet_id INT NOT NULL,
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (film_id) REFERENCES films(id),
        FOREIGN KEY (planet_id) REFERENCES planets(id),
        UNIQUE (film_id, planet_id)
    );
END

-- Films ↔ Starships
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'film_starships')
BEGIN
    CREATE TABLE film_starships (
        id INT IDENTITY(1,1) PRIMARY KEY,
        film_id INT NOT NULL,
        starship_id INT NOT NULL,
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (film_id) REFERENCES films(id),
        FOREIGN KEY (starship_id) REFERENCES starships(id),
        UNIQUE (film_id, starship_id)
    );
END

-- Films ↔ Vehicles
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'film_vehicles')
BEGIN
    CREATE TABLE film_vehicles (
        id INT IDENTITY(1,1) PRIMARY KEY,
        film_id INT NOT NULL,
        vehicle_id INT NOT NULL,
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (film_id) REFERENCES films(id),
        FOREIGN KEY (vehicle_id) REFERENCES vehicles(id),
        UNIQUE (film_id, vehicle_id)
    );
END

-- Films ↔ Species
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'film_species')
BEGIN
    CREATE TABLE film_species (
        id INT IDENTITY(1,1) PRIMARY KEY,
        film_id INT NOT NULL,
        species_id INT NOT NULL,
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (film_id) REFERENCES films(id),
        FOREIGN KEY (species_id) REFERENCES species(id),
        UNIQUE (film_id, species_id)
    );
END

-- People ↔ Species
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'person_species')
BEGIN
    CREATE TABLE person_species (
        id INT IDENTITY(1,1) PRIMARY KEY,
        person_id INT NOT NULL,
        species_id INT NOT NULL,
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (person_id) REFERENCES people(id),
        FOREIGN KEY (species_id) REFERENCES species(id),
        UNIQUE (person_id, species_id)
    );
END

-- People ↔ Vehicles
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'person_vehicles')
BEGIN
    CREATE TABLE person_vehicles (
        id INT IDENTITY(1,1) PRIMARY KEY,
        person_id INT NOT NULL,
        vehicle_id INT NOT NULL,
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (person_id) REFERENCES people(id),
        FOREIGN KEY (vehicle_id) REFERENCES vehicles(id),
        UNIQUE (person_id, vehicle_id)
    );
END

-- People ↔ Starships
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'person_starships')
BEGIN
    CREATE TABLE person_starships (
        id INT IDENTITY(1,1) PRIMARY KEY,
        person_id INT NOT NULL,
        starship_id INT NOT NULL,
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (person_id) REFERENCES people(id),
        FOREIGN KEY (starship_id) REFERENCES starships(id),
        UNIQUE (person_id, starship_id)
    );
END

-- Planets ↔ People (Residents)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'planet_residents')
BEGIN
    CREATE TABLE planet_residents (
        id INT IDENTITY(1,1) PRIMARY KEY,
        planet_id INT NOT NULL,
        person_id INT NOT NULL,
        created_at DATETIME2 DEFAULT GETUTCDATE(),
        FOREIGN KEY (planet_id) REFERENCES planets(id),
        FOREIGN KEY (person_id) REFERENCES people(id),
        UNIQUE (planet_id, person_id)
    );
END

-- =====================================================
-- CRIAÇÃO DOS ÍNDICES
-- =====================================================

-- Índices para films
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_films_title')
    CREATE INDEX idx_films_title ON films(title);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_films_episode_id')
    CREATE INDEX idx_films_episode_id ON films(episode_id);

-- Índices para people
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_people_name')
    CREATE INDEX idx_people_name ON people(name);

-- Índices para planets
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_planets_name')
    CREATE INDEX idx_planets_name ON planets(name);

-- Índices para starships
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_starships_name')
    CREATE INDEX idx_starships_name ON starships(name);

-- Índices para vehicles
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_vehicles_name')
    CREATE INDEX idx_vehicles_name ON vehicles(name);

-- Índices para species
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_species_name')
    CREATE INDEX idx_species_name ON species(name);

-- Índices para tabelas de junção
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_film_characters_film_id')
    CREATE INDEX idx_film_characters_film_id ON film_characters(film_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_film_characters_person_id')
    CREATE INDEX idx_film_characters_person_id ON film_characters(person_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_film_planets_film_id')
    CREATE INDEX idx_film_planets_film_id ON film_planets(film_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_film_planets_planet_id')
    CREATE INDEX idx_film_planets_planet_id ON film_planets(planet_id);

-- =====================================================
-- COMENTÁRIOS SOBRE A ESTRUTURA
-- =====================================================

/*
ESTRUTURA DE RELACIONAMENTOS:

1. RELACIONAMENTOS 1:N (Foreign Keys diretas):
   - people.homeworld_planet_id -> planets.id
   - species.homeworld_planet_id -> planets.id

2. RELACIONAMENTOS N:M (Tabelas de Junção):
   - Films ↔ People: film_characters
   - Films ↔ Planets: film_planets  
   - Films ↔ Starships: film_starships
   - Films ↔ Vehicles: film_vehicles
   - Films ↔ Species: film_species
   - People ↔ Species: person_species
   - People ↔ Vehicles: person_vehicles
   - People ↔ Starships: person_starships
   - Planets ↔ People: planet_residents

BENEFÍCIOS:
- Consultas JOIN eficientes
- Integridade referencial
- Flexibilidade para consultas complexas
- Performance otimizada com índices
*/
