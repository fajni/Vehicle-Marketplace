
/* USER ACCOUNTS */

SELECT * FROM vehicle_marketplace.user_accounts;
DESCRIBE vehicle_marketplace.user_accounts;
INSERT INTO vehicle_marketplace.user_accounts VALUES 
	(5, 'Paige', 'Holt', 'paige@gmail.com', 'paige123', 'User'),
    (7, 'Madison', 'String', 'madison@gmail.com', 'madison123', 'User'),
    (8, 'Jerry', 'Fox', 'jerry@gmail.com', 'jerry123', 'Admin'),
    (9, 'Travis', 'Scott', 'travis@gmail.com', 'travis123', 'Admin'),
    (10, 'Veljko', 'Fajni', 'fajni@gmail.com', 'fajni123', 'User');


/* CARS */

SELECT * FROM vehicle_marketplace.cars;
DESCRIBE vehicle_marketplace.cars;
TRUNCATE TABLE vehicle_marketplace.cars;
INSERT INTO vehicle_marketplace.cars VALUES
	('1HGCM82633A000001', 1, '320d', 'Diesel sports limousine', 30000, 1995, 190, 9),
	('1HGCM82633A000002', 2, 'A4', 'Premium sedan', 25000, 2000, 195, 5),
    ('1HGCM82633A000003', 3, 'C200', 'C class', 35000, 1991, 204, 7),
    ('2ARCM82633A000011', 4, 'Giulia', 'Italian sports limousine', 42000, 1995, 280, 9),
    ('2NISM82633A000012', 5, 'Qashqai', 'Popular SUV', 28000, 1332, 158, 1),
    ('2DACM82633A000013', 6, 'Duster', 'SUV', 18000, 1498, 115, 2),
    ('2FODM82633A000014', 7, 'Mustang', 'American Legend', 55000, 4951, 450, 1),
    ('2CHVM82633A000017', 8, 'Challenger', 'American Legend', 40000, 3600, 312, 3),
    ('2FODM82633A000019', 9, 'Camaro', 'American Legend', 45000, 6162, 455, 6);


/* MOTORCYCLES */

SELECT * FROM vehicle_marketplace.motorcycles;
DESCRIBE vehicle_marketplace.motorcycles;
TRUNCATE vehicle_marketplace.motorcycles;
INSERT INTO vehicle_marketplace.motorcycles VALUES
	('3HDMC82633M000101', 11, 'Street Glide', 'V-Twin motor', 27000, 1868, 89, 9),
	('3BMWM82633M000106', 12, 'MT-09', '', 11000, 890, 119, 5),
    ('3BMWM82633M000107', 13, 'CBR600RR', 'Sports motorcycles', 12500, 600, 118, 7),
    ('3BMWM82633M000108', 14, 'Ninja ZX-10R', '', 165000, 998, 203, 9),
    ('3BMWM82633M000109', 15, 'Duke 790', 'Naked bike', 10500, 799, 105, 1),
    ('3BMWM82633M000110', 16, 'Hayabusa', 'Hyper', 18500, 1340, 190, 2),
    ('3BMWM82633M000111', 17, 'Street Triple RS', 'British naked bike', 13500, 765, 121, 1);

/* MAKES */

SELECT * FROM vehicle_marketplace.makes order by make_id;
DESCRIBE vehicle_marketplace.makes;
TRUNCATE vehicle_marketplace.makes;
DELETE FROM vehicle_marketplace.makes WHERE make_id BETWEEN 0 AND 100;

-- cars
INSERT INTO vehicle_marketplace.makes VALUES
	(1, 'BMW'),
    (2, 'Audi'),
    (3, 'Mercedes-Benz'),
    (4, 'Alfa Romeo'),
    (5, 'Nissan'),
    (6, 'Dacia'),
    (7, 'Fort'),
    (8, 'Dodge'),
    (9, 'Chevrolet');

-- motorcycles
INSERT INTO vehicle_marketplace.makes VALUES
	(11, 'Harley-Davidson'),
    (12, 'Yamaha'),
    (13, 'Honda'),
    (14, 'Kawasaki'),
    (15, 'KTM'),
	(16, 'Suzuki'),
    (17, 'Triumph');