-- =====================================================
-- SCRIPT DE SEED PARA SQLITE - STAR WARS API
-- =====================================================
-- Dados básicos para popular o banco SQLite
-- =====================================================

-- Inserir filmes
INSERT OR IGNORE INTO Films (Title, EpisodeId, OpeningCrawl, Director, Producer, ReleaseDate, CreatedAt) VALUES 
('A New Hope', 4, 'It is a period of civil war...', 'George Lucas', 'Gary Kurtz, Rick McCallum', '1977-05-25', datetime('now')),
('The Empire Strikes Back', 5, 'It is a dark time for the Rebellion...', 'Irvin Kershner', 'Gary Kurtz, Rick McCallum', '1980-05-17', datetime('now')),
('Return of the Jedi', 6, 'Luke Skywalker has returned to his home planet...', 'Richard Marquand', 'Howard G. Kazanjian, George Lucas, Rick McCallum', '1983-05-25', datetime('now')),
('The Phantom Menace', 1, 'Turmoil has engulfed the Galactic Republic...', 'George Lucas', 'Rick McCallum', '1999-05-19', datetime('now')),
('Attack of the Clones', 2, 'There is unrest in the Galactic Senate...', 'George Lucas', 'Rick McCallum', '2002-05-16', datetime('now')),
('Revenge of the Sith', 3, 'War! The Republic is crumbling...', 'George Lucas', 'Rick McCallum', '2005-05-19', datetime('now')),
('The Force Awakens', 7, 'Luke Skywalker has vanished...', 'J.J. Abrams', 'Kathleen Kennedy, J.J. Abrams, Bryan Burk', '2015-12-18', datetime('now'));

-- Inserir pessoas
INSERT OR IGNORE INTO People (Name, Height, Mass, HairColor, SkinColor, EyeColor, BirthYear, Gender, CreatedAt) VALUES 
('Luke Skywalker', '172', '77', 'blond', 'fair', 'blue', '19BBY', 'male', datetime('now')),
('Leia Organa', '150', '49', 'brown', 'light', 'brown', '19BBY', 'female', datetime('now')),
('Han Solo', '180', '80', 'brown', 'fair', 'brown', '29BBY', 'male', datetime('now')),
('Darth Vader', '202', '136', 'none', 'white', 'yellow', '41.9BBY', 'male', datetime('now')),
('Obi-Wan Kenobi', '182', '77', 'auburn, white', 'fair', 'blue-gray', '57BBY', 'male', datetime('now')),
('Yoda', '66', '17', 'white', 'green', 'brown', '896BBY', 'male', datetime('now')),
('Chewbacca', '228', '112', 'brown', 'unknown', 'blue', '200BBY', 'male', datetime('now')),
('R2-D2', '96', '32', 'n/a', 'white, blue', 'red', '33BBY', 'n/a', datetime('now')),
('C-3PO', '167', '75', 'n/a', 'gold', 'yellow', '112BBY', 'n/a', datetime('now')),
('Anakin Skywalker', '188', '84', 'blond', 'fair', 'blue', '41.9BBY', 'male', datetime('now'));

-- Inserir planetas
INSERT OR IGNORE INTO Planets (Name, RotationPeriod, OrbitalPeriod, Diameter, Climate, Gravity, Terrain, SurfaceWater, Population, CreatedAt) VALUES 
('Tatooine', '23', '304', '10465', 'arid', '1 standard', 'desert', '1', '200000', datetime('now')),
('Alderaan', '24', '364', '12500', 'temperate', '1 standard', 'grasslands, mountains', '40', '2000000000', datetime('now')),
('Yavin IV', '24', '4818', '10200', 'temperate, tropical', '1 standard', 'jungle, rainforests', '8', '1000', datetime('now')),
('Hoth', '23', '549', '7200', 'frozen', '1.1 standard', 'tundra, ice caves, mountain ranges', '100', 'unknown', datetime('now')),
('Dagobah', '23', '341', '8900', 'murky', 'N/A', 'swamp, jungles', '8', 'unknown', datetime('now'));

-- Inserir espécies
INSERT OR IGNORE INTO Species (Name, Classification, Designation, AverageHeight, SkinColors, HairColors, EyeColors, AverageLifespan, Language, CreatedAt) VALUES 
('Human', 'mammal', 'sentient', '180', 'caucasian, black, asian, hispanic', 'blonde, brown, black, red', 'brown, blue, green, hazel, grey, amber', '120', 'Galactic Basic', datetime('now')),
('Droid', 'artificial', 'sentient', '0', 'n/a', 'n/a', 'n/a', '0', 'n/a', datetime('now')),
('Wookie', 'mammal', 'sentient', '210', 'gray', 'black, brown', 'blue, green, yellow, brown, golden, red', '400', 'Shyriiwook', datetime('now'));

-- Inserir naves
INSERT OR IGNORE INTO Starships (Name, Model, Manufacturer, CostInCredits, Length, MaxAtmospheringSpeed, Crew, Passengers, CargoCapacity, Consumables, HyperdriveRating, MGLT, StarshipClass, CreatedAt) VALUES 
('Millennium Falcon', 'YT-1300 light freighter', 'Corellian Engineering Corporation', '100000', '34.37', '1050', '4', '6', '100000', '2 months', '0.5', '75', 'Light freighter', datetime('now')),
('X-wing', 'T-65 X-wing', 'Incom Corporation', '149999', '12.5', '1050', '1', '0', '110', '1 week', '1.0', '100', 'Starfighter', datetime('now')),
('Death Star', 'DS-1 Orbital Battle Station', 'Imperial Department of Military Research, Sienar Fleet Systems', '1000000000000', '120000', 'n/a', '342,953', '843,342', '1000000000000', '3 years', '4.0', '10', 'Deep Space Mobile Battlestation', datetime('now'));

-- Inserir veículos
INSERT OR IGNORE INTO Vehicles (Name, Model, Manufacturer, CostInCredits, Length, MaxAtmospheringSpeed, Crew, Passengers, CargoCapacity, Consumables, VehicleClass, CreatedAt) VALUES 
('Sand Crawler', 'Digger Crawler', 'Corellia Mining Corporation', '150000', '36.8', '30', '46', '30', '50000', '2 months', 'wheeled', datetime('now')),
('T-16 skyhopper', 'T-16 skyhopper', 'Incom Corporation', '14500', '10.4', '1200', '1', '1', '50', '0', 'repulsorcraft', datetime('now')),
('X-34 landspeeder', 'X-34 landspeeder', 'SoroSuub Corporation', '10550', '3.4', '250', '1', '1', '5', 'unknown', 'repulsorcraft', datetime('now'));

-- Inserir relacionamentos básicos
INSERT OR IGNORE INTO FilmCharacters (FilmId, PersonId, CreatedAt) VALUES 
(1, 1, datetime('now')), -- A New Hope - Luke Skywalker
(1, 2, datetime('now')), -- A New Hope - Leia Organa
(1, 3, datetime('now')), -- A New Hope - Han Solo
(1, 4, datetime('now')), -- A New Hope - Darth Vader
(2, 1, datetime('now')), -- Empire Strikes Back - Luke Skywalker
(2, 2, datetime('now')), -- Empire Strikes Back - Leia Organa
(2, 3, datetime('now')); -- Empire Strikes Back - Han Solo

-- Inserir relacionamentos de planetas
INSERT OR IGNORE INTO FilmPlanets (FilmId, PlanetId, CreatedAt) VALUES 
(1, 1, datetime('now')), -- A New Hope - Tatooine
(1, 2, datetime('now')), -- A New Hope - Alderaan
(2, 4, datetime('now')); -- Empire Strikes Back - Hoth

-- Inserir relacionamentos de espécies
INSERT OR IGNORE INTO PersonSpecies (PersonId, SpeciesId, CreatedAt) VALUES 
(1, 1, datetime('now')), -- Luke Skywalker - Human
(2, 1, datetime('now')), -- Leia Organa - Human
(3, 1, datetime('now')), -- Han Solo - Human
(4, 1, datetime('now')), -- Darth Vader - Human
(8, 2, datetime('now')), -- R2-D2 - Droid
(9, 2, datetime('now')); -- C-3PO - Droid
