SELECT * FROM vehicle_marketplace.user_accounts;
DESCRIBE vehicle_marketplace.user_accounts;

INSERT INTO vehicle_marketplace.user_accounts VALUES (
	4, 'Test', 'Test', 'test@gmail.com', 'test123', 'User'
);

SELECT * FROM vehicle_marketplace.cars;
describe vehicle_marketplace.cars;
TRUNCATE TABLE vehicle_marketplace.cars;

SELECT * FROM vehicle_marketplace.motorcycles;
describe vehicle_marketplace.motorcycles;

SELECT * FROM vehicle_marketplace.makes;
describe vehicle_marketplace.makes;

INSERT INTO vehicle_marketplace.makes VALUES(
	11, 'Yamaha'
);