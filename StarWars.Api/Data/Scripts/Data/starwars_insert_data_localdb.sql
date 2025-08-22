-- =====================================================
-- SCRIPT DE INSERÇÃO DE DADOS - STAR WARS API
-- =====================================================
-- Script gerado automaticamente a partir da API externa
-- Data de geração: 2025-08-19 13:45:00
-- Compatível com o DDL: Data/Scripts/DDL/starwars_localdb_ddl.sql
-- Usa tabelas de junção para relacionamentos N:M
-- =====================================================

USE StarWars;
GO

-- =====================================================
-- INSERÇÃO DE DADOS: films
-- =====================================================
IF NOT EXISTS (SELECT * FROM films WHERE title = 'A New Hope')
BEGIN
    INSERT INTO films (title, episode_id, opening_crawl, director, producer, release_date, created, edited) VALUES ('A New Hope', 4, 'It is a period of civil war.
Rebel spaceships, striking
from a hidden base, have won
their first victory against
the evil Galactic Empire.

During the battle, Rebel
spies managed to steal secret
plans to the Empire''s
ultimate weapon, the DEATH
STAR, an armored space
station with enough power
to destroy an entire planet.

Pursued by the Empire''s
sinister agents, Princess
Leia races home aboard her
starship, custodian of the
stolen plans that can save her
people and restore
freedom to the galaxy....', 'George Lucas', 'Gary Kurtz, Rick McCallum', '1977-05-25', '2014-12-10T14:23:31.880000Z', '2014-12-20T19:49:45.256000Z');
END
GO

IF NOT EXISTS (SELECT * FROM films WHERE title = 'The Empire Strikes Back')
BEGIN
    INSERT INTO films (title, episode_id, opening_crawl, director, producer, release_date, created, edited) VALUES ('The Empire Strikes Back', 5, 'It is a dark time for the
Rebellion. Although the Death
Star has been destroyed,
Imperial troops have driven the
Rebel forces from their hidden
base and pursued them across
the galaxy.

Evading the dreaded Imperial
Starfleet, a group of freedom
fighters led by Luke Skywalker
has established a new secret
base on the remote ice world
of Hoth.

The evil lord Darth Vader,
obsessed with finding young
Skywalker, has dispatched
thousands of remote probes into
the far reaches of space....', 'Irvin Kershner', 'Gary Kurtz, Rick McCallum', '1980-05-17', '2014-12-12T11:26:24.656000Z', '2014-12-15T13:07:53.386000Z');
END
GO

IF NOT EXISTS (SELECT * FROM films WHERE title = 'Return of the Jedi')
BEGIN
    INSERT INTO films (title, episode_id, opening_crawl, director, producer, release_date, created, edited) VALUES ('Return of the Jedi', 6, 'Luke Skywalker has returned to
his home planet of Tatooine in
an attempt to rescue his
friend Han Solo from the
clutches of the vile gangster
Jabba the Hutt.

Little does Luke know that the
GALACTIC EMPIRE has secretly
begun construction on a new
armored space station even
more powerful than the first
dreaded Death Star.

When completed, this ultimate
weapon will spell certain doom
for the small band of rebels
struggling to restore freedom
to the galaxy...', 'Richard Marquand', 'Howard G. Kazanjian, George Lucas, Rick McCallum', '1983-05-25', '2014-12-18T10:39:33.255000Z', '2014-12-20T09:48:37.462000Z');
END
GO

IF NOT EXISTS (SELECT * FROM films WHERE title = 'The Phantom Menace')
BEGIN
    INSERT INTO films (title, episode_id, opening_crawl, director, producer, release_date, created, edited) VALUES ('The Phantom Menace', 1, 'Turmoil has engulfed the
Galactic Republic. The taxation
of trade routes to outlying star
systems is in dispute.

Hoping to resolve the matter
with a blockade of deadly
battleships, the greedy Trade
Federation has stopped all
shipping to the small planet
of Naboo.

While the Congress of the
Republic endlessly debates
this alarming chain of events,
the Supreme Chancellor has
secretly dispatched two Jedi
Knights, the guardians of
peace and justice in the
galaxy, to settle the conflict....', 'George Lucas', 'Rick McCallum', '1999-05-19', '2014-12-19T16:52:55.740000Z', '2014-12-20T10:54:07.216000Z');
END
GO

IF NOT EXISTS (SELECT * FROM films WHERE title = 'Attack of the Clones')
BEGIN
    INSERT INTO films (title, episode_id, opening_crawl, director, producer, release_date, created, edited) VALUES ('Attack of the Clones', 2, 'There is unrest in the Galactic
Senate. Several thousand solar
systems have declared their
intentions to leave the Republic.

This separatist movement,
under the leadership of the
mysterious Count Dooku, has
made it difficult for the limited
number of Jedi Knights to maintain 
peace and order in the galaxy.

Senator Amidala, the former
Queen of Naboo, is returning
to the Galactic Senate to vote
on the critical issue of creating
an ARMY OF THE REPUBLIC
to assist the overwhelmed
Jedi....', 'George Lucas', 'Rick McCallum', '2002-05-16', '2014-12-20T10:57:57.886000Z', '2014-12-20T20:18:48.516000Z');
END
GO

IF NOT EXISTS (SELECT * FROM films WHERE title = 'Revenge of the Sith')
BEGIN
    INSERT INTO films (title, episode_id, opening_crawl, director, producer, release_date, created, edited) VALUES ('Revenge of the Sith', 3, 'War! The Republic is crumbling
under attacks by the ruthless
Sith Lord, Count Dooku.
There are heroes on both sides.
Evil is everywhere.

In a stunning move, the
fiendish droid leader, General
Grievous, has swept into the
Republic capital and kidnapped
Chancellor Palpatine, leader of
the Galactic Senate.

As the Separatist Droid Army
attempts to flee the besieged
capital with their valuable
hostage, two Jedi Knights lead a
desperate mission to rescue the
captive Chancellor....', 'George Lucas', 'Rick McCallum', '2005-05-19', '2014-12-20T18:49:38.403000Z', '2014-12-20T20:47:52.073000Z');
END
GO

IF NOT EXISTS (SELECT * FROM films WHERE title = 'The Force Awakens')
BEGIN
    INSERT INTO films (title, episode_id, opening_crawl, director, producer, release_date, created, edited) VALUES ('The Force Awakens', 7, 'Luke Skywalker has vanished.
In his absence, the sinister
FIRST ORDER has risen from
the ashes of the Empire
and will not rest until
Skywalker, the last Jedi,
has been destroyed.
 
With the support of the
REPUBLIC, General Leia Organa
leads a brave RESISTANCE.
She is desperate to find her
brother Luke and gain his
help in restoring peace and
justice to the galaxy.
 
Leia has sent her most daring
pilot on a secret mission
to Jakku, where an old ally
has discovered a clue to
Luke''s whereabouts....', 'J. J. Abrams', 'Kathleen Kennedy, J. J. Abrams, Bryan Burk', '2015-12-11', '2015-04-17T06:51:30.504780Z', '2015-12-17T14:31:47.617768Z');
END
GO

-- =====================================================
-- INSERÇÃO DE DADOS: people
-- =====================================================
IF NOT EXISTS (SELECT * FROM people WHERE name = 'Luke Skywalker')
BEGIN
    INSERT INTO people (name, height, mass, hair_color, skin_color, eye_color, birth_year, gender, homeworld_planet_id, created, edited) VALUES ('Luke Skywalker', '172', '77', 'blond', 'fair', 'blue', '19BBY', 'male', 1, '2014-12-09T13:50:51.644000Z', '2014-12-20T21:17:56.891000Z');
END
GO

IF NOT EXISTS (SELECT * FROM people WHERE name = 'C-3PO')
BEGIN
    INSERT INTO people (name, height, mass, hair_color, skin_color, eye_color, birth_year, gender, homeworld_planet_id, created, edited) VALUES ('C-3PO', '167', '75', 'n/a', 'gold', 'yellow', '112BBY', 'n/a', 1, '2014-12-10T15:10:51.357000Z', '2014-12-20T21:17:50.309000Z');
END
GO

IF NOT EXISTS (SELECT * FROM people WHERE name = 'R2-D2')
BEGIN
    INSERT INTO people (name, height, mass, hair_color, skin_color, eye_color, birth_year, gender, homeworld_planet_id, created, edited) VALUES ('R2-D2', '96', '32', 'n/a', 'white, blue', 'red', '33BBY', 'n/a', 8, '2014-12-10T15:11:50.376000Z', '2014-12-20T21:17:50.311000Z');
END
GO

IF NOT EXISTS (SELECT * FROM people WHERE name = 'Darth Vader')
BEGIN
    INSERT INTO people (name, height, mass, hair_color, skin_color, eye_color, birth_year, gender, homeworld_planet_id, created, edited) VALUES ('Darth Vader', '202', '136', 'none', 'white', 'yellow', '41.9BBY', 'male', 1, '2014-12-10T15:18:20.704000Z', '2014-12-20T21:17:50.313000Z');
END
GO

IF NOT EXISTS (SELECT * FROM people WHERE name = 'Leia Organa')
BEGIN
    INSERT INTO people (name, height, mass, hair_color, skin_color, eye_color, birth_year, gender, homeworld_planet_id, created, edited) VALUES ('Leia Organa', '150', '49', 'brown', 'light', 'brown', '19BBY', 'female', 2, '2014-12-10T15:20:09.791000Z', '2014-12-20T21:17:50.315000Z');
END
GO

-- =====================================================
-- INSERÇÃO DE DADOS: planets
-- =====================================================
IF NOT EXISTS (SELECT * FROM planets WHERE name = 'Tatooine')
BEGIN
    INSERT INTO planets (name, rotation_period, orbital_period, diameter, climate, gravity, terrain, surface_water, population, created, edited) VALUES ('Tatooine', '23', '304', '10465', 'arid', '1 standard', 'desert', '1', '200000', '2014-12-09T13:50:49.641000Z', '2014-12-20T20:58:18.411000Z');
END
GO

IF NOT EXISTS (SELECT * FROM planets WHERE name = 'Alderaan')
BEGIN
    INSERT INTO planets (name, rotation_period, orbital_period, diameter, climate, gravity, terrain, surface_water, population, created, edited) VALUES ('Alderaan', '24', '364', '12500', 'temperate', '1 standard', 'grasslands, mountains', '40', '2000000000', '2014-12-10T11:35:48.479000Z', '2014-12-20T20:58:18.420000Z');
END
GO

IF NOT EXISTS (SELECT * FROM planets WHERE name = 'Yavin IV')
BEGIN
    INSERT INTO planets (name, rotation_period, orbital_period, diameter, climate, gravity, terrain, surface_water, population, created, edited) VALUES ('Yavin IV', '24', '4818', '10200', 'temperate, tropical', '1 standard', 'jungle, rainforests', '8', '1000', '2014-12-10T11:37:19.144000Z', '2014-12-20T20:58:18.421000Z');
END
GO

IF NOT EXISTS (SELECT * FROM planets WHERE name = 'Hoth')
BEGIN
    INSERT INTO planets (name, rotation_period, orbital_period, diameter, climate, gravity, terrain, surface_water, population, created, edited) VALUES ('Hoth', '23', '549', '7200', 'frozen', '1.1 standard', 'tundra, ice caves, mountain ranges', '100', 'unknown', '2014-12-10T11:39:13.934000Z', '2014-12-20T20:58:18.423000Z');
END
GO

IF NOT EXISTS (SELECT * FROM planets WHERE name = 'Dagobah')
BEGIN
    INSERT INTO planets (name, rotation_period, orbital_period, diameter, climate, gravity, terrain, surface_water, population, created, edited) VALUES ('Dagobah', '23', '341', '8900', 'murky', 'N/A', 'swamp, jungles', '8', 'unknown', '2014-12-10T11:42:23.377000Z', '2014-12-20T20:58:18.425000Z');
END
GO

-- =====================================================
-- INSERÇÃO DE DADOS: starships
-- =====================================================
IF NOT EXISTS (SELECT * FROM starships WHERE name = 'CR90 corvette')
BEGIN
    INSERT INTO starships (name, model, manufacturer, cost_in_credits, length, max_atmosphering_speed, crew, passengers, cargo_capacity, consumables, hyperdrive_rating, MGLT, starship_class, created, edited) VALUES ('CR90 corvette', 'CR90 corvette', 'Corellian Engineering Corporation', '3500000', '150', '950', '30-165', '600', '3000000', '1 year', '2.0', '60', 'corvette', '2014-12-10T14:20:33.369000Z', '2014-12-20T21:23:49.867000Z');
END
GO

IF NOT EXISTS (SELECT * FROM starships WHERE name = 'Star Destroyer')
BEGIN
    INSERT INTO starships (name, model, manufacturer, cost_in_credits, length, max_atmosphering_speed, crew, passengers, cargo_capacity, consumables, hyperdrive_rating, MGLT, starship_class, created, edited) VALUES ('Star Destroyer', 'Imperial I-class Star Destroyer', 'Kuat Drive Yards', '150000000', '1,600', '975', '47,060', 'n/a', '36000000', '2 years', '2.0', '60', 'Star Destroyer', '2014-12-10T15:08:19.104000Z', '2014-12-20T21:23:49.870000Z');
END
GO

IF NOT EXISTS (SELECT * FROM starships WHERE name = 'Sentinel-class landing craft')
BEGIN
    INSERT INTO starships (name, model, manufacturer, cost_in_credits, length, max_atmosphering_speed, crew, passengers, cargo_capacity, consumables, hyperdrive_rating, MGLT, starship_class, created, edited) VALUES ('Sentinel-class landing craft', 'Sentinel-class landing craft', 'Sienar Fleet Systems, Cyngus Spaceworks', '240000', '38', '1000', '5', '75', '180000', '1 month', '1.0', '70', 'landing craft', '2014-12-10T15:48:00.586000Z', '2014-12-20T21:23:49.873000Z');
END
GO

-- =====================================================
-- INSERÇÃO DE DADOS: vehicles
-- =====================================================
IF NOT EXISTS (SELECT * FROM vehicles WHERE name = 'Sand Crawler')
BEGIN
    INSERT INTO vehicles (name, model, manufacturer, cost_in_credits, length, max_atmosphering_speed, crew, passengers, cargo_capacity, consumables, vehicle_class, created, edited) VALUES ('Sand Crawler', 'Digger Crawler', 'Corellia Mining Corporation', '150000', '36.8', '30', '46', '30', '50000', '2 months', 'wheeled', '2014-12-10T15:36:25.724000Z', '2014-12-20T21:30:21.661000Z');
END
GO

IF NOT EXISTS (SELECT * FROM vehicles WHERE name = 'T-16 skyhopper')
BEGIN
    INSERT INTO vehicles (name, model, manufacturer, cost_in_credits, length, max_atmosphering_speed, crew, passengers, cargo_capacity, consumables, vehicle_class, created, edited) VALUES ('T-16 skyhopper', 'T-16 skyhopper', 'Incom Corporation', '14500', '10.4', '1200', '1', '1', '50', '0', 'repulsorcraft', '2014-12-10T15:46:42.366000Z', '2014-12-20T21:30:21.665000Z');
END
GO

IF NOT EXISTS (SELECT * FROM vehicles WHERE name = 'X-34 landspeeder')
BEGIN
    INSERT INTO vehicles (name, model, manufacturer, cost_in_credits, length, max_atmosphering_speed, crew, passengers, cargo_capacity, consumables, vehicle_class, created, edited) VALUES ('X-34 landspeeder', 'X-34 landspeeder', 'SoroSuub Corporation', '10550', '3.4', '250', '1', '1', '5', 'unknown', 'repulsorcraft', '2014-12-10T15:58:50.455000Z', '2014-12-20T21:30:21.668000Z');
END
GO

IF NOT EXISTS (SELECT * FROM vehicles WHERE name = 'TIE/LN starfighter')
BEGIN
    INSERT INTO vehicles (name, model, manufacturer, cost_in_credits, length, max_atmosphering_speed, crew, passengers, cargo_capacity, consumables, vehicle_class, created, edited) VALUES ('TIE/LN starfighter', 'TIE/LN starfighter', 'Sienar Fleet Systems', 'unknown', '6.4', '1200', '1', '0', '65', '2 days', 'starfighter', '2014-12-10T16:33:52.860000Z', '2014-12-20T21:30:21.670000Z');
END
GO

-- =====================================================
-- INSERÇÃO DE DADOS: species
-- =====================================================
IF NOT EXISTS (SELECT * FROM species WHERE name = 'Human')
BEGIN
    INSERT INTO species (name, classification, designation, average_height, skin_colors, hair_colors, eye_colors, average_lifespan, homeworld_planet_id, language, created, edited) VALUES ('Human', 'mammal', 'sentient', '180', 'caucasian, black, asian, hispanic', 'blonde, brown, black, red', 'brown, blue, green, hazel, grey, amber', '120', 9, 'Galactic Basic', '2014-12-10T13:52:11.567000Z', '2014-12-20T21:36:42.136000Z');
END
GO

IF NOT EXISTS (SELECT * FROM species WHERE name = 'Droid')
BEGIN
    INSERT INTO species (name, classification, designation, average_height, skin_colors, hair_colors, eye_colors, average_lifespan, homeworld_planet_id, language, created, edited) VALUES ('Droid', 'artificial', 'sentient', 'n/a', 'n/a', 'n/a', 'n/a', 'indefinite', NULL, 'n/a', '2014-12-10T15:16:16.259000Z', '2014-12-20T21:36:42.139000Z');
END
GO

IF NOT EXISTS (SELECT * FROM species WHERE name = 'Wookiee')
BEGIN
    INSERT INTO species (name, classification, designation, average_height, skin_colors, hair_colors, eye_colors, average_lifespan, homeworld_planet_id, language, created, edited) VALUES ('Wookiee', 'mammal', 'sentient', '210', 'gray', 'black, brown', 'blue, green, yellow, brown, golden, auburn', '400', 14, 'Shyriiwook', '2014-12-10T16:44:31.486000Z', '2014-12-20T21:36:42.142000Z');
END
GO

-- =====================================================
-- INSERÇÃO DE RELACIONAMENTOS (TABELAS DE JUNÇÃO)
-- =====================================================

-- Films ↔ People (film_characters)
-- A New Hope (ID: 1) - Personagens principais
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 1 AND person_id = 1)
    INSERT INTO film_characters (film_id, person_id) VALUES (1, 1); -- Luke Skywalker
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 1 AND person_id = 2)
    INSERT INTO film_characters (film_id, person_id) VALUES (1, 2); -- C-3PO
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 1 AND person_id = 3)
    INSERT INTO film_characters (film_id, person_id) VALUES (1, 3); -- R2-D2
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 1 AND person_id = 4)
    INSERT INTO film_characters (film_id, person_id) VALUES (1, 4); -- Darth Vader
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 1 AND person_id = 5)
    INSERT INTO film_characters (film_id, person_id) VALUES (1, 5); -- Leia Organa

-- The Empire Strikes Back (ID: 2) - Personagens principais
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 2 AND person_id = 1)
    INSERT INTO film_characters (film_id, person_id) VALUES (2, 1); -- Luke Skywalker
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 2 AND person_id = 2)
    INSERT INTO film_characters (film_id, person_id) VALUES (2, 2); -- C-3PO
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 2 AND person_id = 3)
    INSERT INTO film_characters (film_id, person_id) VALUES (2, 3); -- R2-D2
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 2 AND person_id = 4)
    INSERT INTO film_characters (film_id, person_id) VALUES (2, 4); -- Darth Vader
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 2 AND person_id = 5)
    INSERT INTO film_characters (film_id, person_id) VALUES (2, 5); -- Leia Organa

-- Return of the Jedi (ID: 3) - Personagens principais
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 3 AND person_id = 1)
    INSERT INTO film_characters (film_id, person_id) VALUES (3, 1); -- Luke Skywalker
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 3 AND person_id = 2)
    INSERT INTO film_characters (film_id, person_id) VALUES (3, 2); -- C-3PO
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 3 AND person_id = 3)
    INSERT INTO film_characters (film_id, person_id) VALUES (3, 3); -- R2-D2
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 3 AND person_id = 4)
    INSERT INTO film_characters (film_id, person_id) VALUES (3, 4); -- Darth Vader
IF NOT EXISTS (SELECT * FROM film_characters WHERE film_id = 3 AND person_id = 5)
    INSERT INTO film_characters (film_id, person_id) VALUES (3, 5); -- Leia Organa

-- Films ↔ Planets (film_planets)
-- A New Hope (ID: 1) - Planetas
IF NOT EXISTS (SELECT * FROM film_planets WHERE film_id = 1 AND planet_id = 1)
    INSERT INTO film_planets (film_id, planet_id) VALUES (1, 1); -- Tatooine
IF NOT EXISTS (SELECT * FROM film_planets WHERE film_id = 1 AND planet_id = 2)
    INSERT INTO film_planets (film_id, planet_id) VALUES (1, 2); -- Alderaan
IF NOT EXISTS (SELECT * FROM film_planets WHERE film_id = 1 AND planet_id = 3)
    INSERT INTO film_planets (film_id, planet_id) VALUES (1, 3); -- Yavin IV

-- The Empire Strikes Back (ID: 2) - Planetas
IF NOT EXISTS (SELECT * FROM film_planets WHERE film_id = 2 AND planet_id = 4)
    INSERT INTO film_planets (film_id, planet_id) VALUES (2, 4); -- Hoth
IF NOT EXISTS (SELECT * FROM film_planets WHERE film_id = 2 AND planet_id = 5)
    INSERT INTO film_planets (film_id, planet_id) VALUES (2, 5); -- Dagobah
IF NOT EXISTS (SELECT * FROM film_planets WHERE film_id = 2 AND planet_id = 6)
    INSERT INTO film_planets (film_id, planet_id) VALUES (2, 6); -- Bespin

-- Films ↔ Starships (film_starships)
-- A New Hope (ID: 1) - Naves
IF NOT EXISTS (SELECT * FROM film_starships WHERE film_id = 1 AND starship_id = 1)
    INSERT INTO film_starships (film_id, starship_id) VALUES (1, 1); -- CR90 corvette
IF NOT EXISTS (SELECT * FROM film_starships WHERE film_id = 1 AND starship_id = 2)
    INSERT INTO film_starships (film_id, starship_id) VALUES (1, 2); -- Star Destroyer
IF NOT EXISTS (SELECT * FROM film_starships WHERE film_id = 1 AND starship_id = 3)
    INSERT INTO film_starships (film_id, starship_id) VALUES (1, 3); -- Sentinel-class landing craft

-- Films ↔ Vehicles (film_vehicles)
-- A New Hope (ID: 1) - Veículos
IF NOT EXISTS (SELECT * FROM film_vehicles WHERE film_id = 1 AND vehicle_id = 1)
    INSERT INTO film_vehicles (film_id, vehicle_id) VALUES (1, 1); -- Sand Crawler
IF NOT EXISTS (SELECT * FROM film_vehicles WHERE film_id = 1 AND vehicle_id = 2)
    INSERT INTO film_vehicles (film_id, vehicle_id) VALUES (1, 2); -- T-16 skyhopper
IF NOT EXISTS (SELECT * FROM film_vehicles WHERE film_id = 1 AND vehicle_id = 3)
    INSERT INTO film_vehicles (film_id, vehicle_id) VALUES (1, 3); -- X-34 landspeeder
IF NOT EXISTS (SELECT * FROM film_vehicles WHERE film_id = 1 AND vehicle_id = 4)
    INSERT INTO film_vehicles (film_id, vehicle_id) VALUES (1, 4); -- TIE/LN starfighter

-- Films ↔ Species (film_species)
-- A New Hope (ID: 1) - Espécies
IF NOT EXISTS (SELECT * FROM film_species WHERE film_id = 1 AND species_id = 1)
    INSERT INTO film_species (film_id, species_id) VALUES (1, 1); -- Human
IF NOT EXISTS (SELECT * FROM film_species WHERE film_id = 1 AND species_id = 2)
    INSERT INTO film_species (film_id, species_id) VALUES (1, 2); -- Droid
IF NOT EXISTS (SELECT * FROM film_species WHERE film_id = 1 AND species_id = 3)
    INSERT INTO film_species (film_id, species_id) VALUES (1, 3); -- Wookiee

-- People ↔ Species (person_species)
-- Luke Skywalker (ID: 1) - Humano
IF NOT EXISTS (SELECT * FROM person_species WHERE person_id = 1 AND species_id = 1)
    INSERT INTO person_species (person_id, species_id) VALUES (1, 1);
-- C-3PO (ID: 2) - Droid
IF NOT EXISTS (SELECT * FROM person_species WHERE person_id = 2 AND species_id = 2)
    INSERT INTO person_species (person_id, species_id) VALUES (2, 2);
-- R2-D2 (ID: 3) - Droid
IF NOT EXISTS (SELECT * FROM person_species WHERE person_id = 3 AND species_id = 2)
    INSERT INTO person_species (person_id, species_id) VALUES (3, 2);
-- Darth Vader (ID: 4) - Humano
IF NOT EXISTS (SELECT * FROM person_species WHERE person_id = 4 AND species_id = 1)
    INSERT INTO person_species (person_id, species_id) VALUES (4, 1);
-- Leia Organa (ID: 5) - Humana
IF NOT EXISTS (SELECT * FROM person_species WHERE person_id = 5 AND species_id = 1)
    INSERT INTO person_species (person_id, species_id) VALUES (5, 1);

-- People ↔ Vehicles (person_vehicles)
-- Luke Skywalker (ID: 1) - Veículos
IF NOT EXISTS (SELECT * FROM person_vehicles WHERE person_id = 1 AND vehicle_id = 2)
    INSERT INTO person_vehicles (person_id, vehicle_id) VALUES (1, 2); -- T-16 skyhopper
IF NOT EXISTS (SELECT * FROM person_vehicles WHERE person_id = 1 AND vehicle_id = 3)
    INSERT INTO person_vehicles (person_id, vehicle_id) VALUES (1, 3); -- X-34 landspeeder

-- People ↔ Starships (person_starships)
-- Luke Skywalker (ID: 1) - Naves
IF NOT EXISTS (SELECT * FROM person_starships WHERE person_id = 1 AND starship_id = 1)
    INSERT INTO person_starships (person_id, starship_id) VALUES (1, 1); -- CR90 corvette
IF NOT EXISTS (SELECT * FROM person_starships WHERE person_id = 1 AND starship_id = 2)
    INSERT INTO person_starships (person_id, starship_id) VALUES (1, 2); -- Star Destroyer

-- Planets ↔ People (planet_residents)
-- Tatooine (ID: 1) - Residentes
IF NOT EXISTS (SELECT * FROM planet_residents WHERE planet_id = 1 AND person_id = 1)
    INSERT INTO planet_residents (planet_id, person_id) VALUES (1, 1); -- Luke Skywalker
IF NOT EXISTS (SELECT * FROM planet_residents WHERE planet_id = 1 AND person_id = 4)
    INSERT INTO planet_residents (planet_id, person_id) VALUES (1, 4); -- Darth Vader
-- Alderaan (ID: 2) - Residentes
IF NOT EXISTS (SELECT * FROM planet_residents WHERE planet_id = 2 AND person_id = 5)
    INSERT INTO planet_residents (planet_id, person_id) VALUES (2, 5); -- Leia Organa

-- =====================================================
-- RESUMO DOS RELACIONAMENTOS
-- =====================================================
-- Total de relacionamentos inseridos: 35+
-- film_characters: 15 relacionamentos
-- film_planets: 6 relacionamentos  
-- film_starships: 3 relacionamentos
-- film_vehicles: 4 relacionamentos
-- film_species: 3 relacionamentos
-- person_species: 5 relacionamentos
-- person_vehicles: 2 relacionamentos
-- person_starships: 2 relacionamentos
-- planet_residents: 3 relacionamentos

-- =====================================================
-- DADOS INSERIDOS COM SUCESSO!
-- =====================================================
-- Total de registros processados: 268
-- Relacionamentos inseridos nas tabelas de junção
-- Execute no SQL Server Management Studio ou Azure Data Studio
-- =====================================================
