
/* USER ACCOUNTS */

SELECT * FROM vehicle_marketplace.user_accounts;
DESCRIBE vehicle_marketplace.user_accounts;
TRUNCATE TABLE vehicle_marketplace.user_accounts;
DELETE FROM vehicle_marketplace.user_accounts WHERE user_id BETWEEN 0 AND 100;
INSERT INTO vehicle_marketplace.user_accounts VALUES 
	(5, 'Paige', 'Holt', 'paige@gmail.com', 'paige123', 'User'),
    (6, 'Walter', 'White', 'walter@gmail.com', 'walter123', 'User'),
    (7, 'Madison', 'String', 'madison@gmail.com', 'madison123', 'User'),
    (8, 'Jerry', 'Fox', 'jerry@gmail.com', 'jerry123', 'Admin'),
    (9, 'Travis', 'Scott', 'travis@gmail.com', 'travis123', 'Admin'),
    (10, 'Veljko', 'Fajni', 'fajni@gmail.com', 'fajni123', 'User');


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

/* CARS */

SELECT * FROM vehicle_marketplace.cars;
DESCRIBE vehicle_marketplace.cars;
TRUNCATE TABLE vehicle_marketplace.cars;
DELETE FROM vehicle_marketplace.cars WHERE user_account_id BETWEEN 0 AND 100;
INSERT INTO vehicle_marketplace.cars VALUES
	('1HGCM82633A000001', 1, '320d', 'Diesel sports limousine', 30000, 1995, 190, 5),
	('1HGCM82633A000002', 2, 'A4', 'Premium sedan', 25000, 2000, 195, 6),
    ('1HGCM82633A000003', 3, 'C200', 'C class', 35000, 1991, 204, 7),
    ('2ARCM82633A000011', 4, 'Giulia', 'Italian sports limousine', 42000, 1995, 280, 8),
    ('2NISM82633A000012', 5, 'Qashqai', 'Popular SUV', 28000, 1332, 158, 9),
    ('2DACM82633A000013', 6, 'Duster', 'SUV', 18000, 1498, 115, 10),
    ('2FODM82633A000014', 7, 'Mustang', 'American Legend', 55000, 4951, 450, 5),
    ('2CHVM82633A000017', 8, 'Challenger', 'American Legend', 40000, 3600, 312, 10),
    ('2FODM82633A000019', 9, 'Camaro', 'American Legend', 45000, 6162, 455, 9);


/* MOTORCYCLES */

SELECT * FROM vehicle_marketplace.motorcycles;
DESCRIBE vehicle_marketplace.motorcycles;
DELETE FROM vehicle_marketplace.motorcycles WHERE user_account_id BETWEEN 0 AND 100;
TRUNCATE vehicle_marketplace.motorcycles;
INSERT INTO vehicle_marketplace.motorcycles VALUES
	('3HDMC82633M000101', 11, 'Street Glide', 'V-Twin motor', 27000, 1868, 89, 9),
	('3BMWM82633M000106', 12, 'MT-09', '', 11000, 890, 119, 5),
    ('3BMWM82633M000107', 13, 'CBR600RR', 'Sports motorcycles', 12500, 600, 118, 7),
    ('3BMWM82633M000108', 14, 'Ninja ZX-10R', '', 165000, 998, 203, 9),
    ('3BMWM82633M000109', 15, 'Duke 790', 'Naked bike', 10500, 799, 105, 6),
    ('3BMWM82633M000110', 16, 'Hayabusa', 'Hyper', 18500, 1340, 190, 8),
    ('3BMWM82633M000111', 17, 'Street Triple RS', 'British naked bike', 13500, 765, 121, 10);

/* IMAGES */

SELECT * FROM vehicle_marketplace.images;
DESCRIBE vehicle_marketplace.images;
TRUNCATE vehicle_marketplace.images;
DELETE FROM vehicle_marketplace.images WHERE image_id BETWEEN 0 AND 100;

UPDATE vehicle_marketplace.images SET image_path='https://rynocars.com/wp-content/uploads/2023/10/2020-mercedes-benz-c-class-c200-1110x577.jpeg'
	WHERE image_id = 6;

-- cars:
INSERT INTO vehicle_marketplace.images(image_id, image_path, image_car_vin) VALUES
	(1,'https://www.motoroids.com/wp-content/uploads/2016/05/2016-BMW-3-Series-320d-M-Sport-Head-On-view-13.jpg', '1HGCM82633A000001'),
    (2,'https://i.ytimg.com/vi/1lRHx89KDOw/sddefault.jpg', '1HGCM82633A000001'),
    (3,'https://www.greencarguide.co.uk/wp-content/uploads/2019/07/BMW-320d-005-low-res.jpeg', '1HGCM82633A000001'),
    
    (4,'https://images.cdn.autocar.co.uk/sites/autocar.co.uk/files/styles/gallery_slide/public/audi-a4-rt-2015-0024_0.jpg', '1HGCM82633A000002'),
    (5,'https://vuduperformance.com/cdn/shop/products/20220906191150_63178d668be7f_700x700.jpg', '1HGCM82633A000002'),
    
    (6,'https://rynocars.com/wp-content/uploads/2023/10/2020-mercedes-benz-c-class-c200-1110x577.jpeg', '1HGCM82633A000003'),
    (7,'https://flywheelcars.com/wp-content/uploads/2024/05/WhatsApp-Image-2024-05-17-at-4.25.22-PM.jpeg', '1HGCM82633A000003'),
    (8,'https://kai-and-karo.ams3.cdn.digitaloceanspaces.com/media/vehicles/images/Internet_20240430_143241_3.jpeg', '1HGCM82633A000003'),
    
    (9,'https://www.media.stellantis.com/cache/9/3/a/1/e/93a1e6de69461aceac5c301f9fefae8d98049a3f.jpeg', '2ARCM82633A000011'),
    (10,'https://cdn.motor1.com/images/mgl/8AYpzM/s3/l-alfa-romeo-giulia-2.0.jpg', '2ARCM82633A000011'),
    (11,'https://images.hgmsites.net/med/2023-alfa-romeo-giulia-rwd-rear-exterior-view_100857543_m.jpg', '2ARCM82633A000011'),
    
    (12,'https://www.topgear.com/sites/default/files/2024/07/Nissan%20Qashqai%20N-Design_012-source.jpg', '2NISM82633A000012'),
    (13,'https://bilogmotorbloggen.no/wp-content/uploads/2021/08/Nissan_Qashqai_005-e1629147065114.jpg', '2NISM82633A000012'),
    (14,'https://www.topgear.com/sites/default/files/images/news-article/carousel/2021/02/8a8074e92753d8943d368a07400dab87/all_new_nissan_qashqai_2.jpg', '2NISM82633A000012'),
    
    (15,'https://www.topgear.com/sites/default/files/2024/10/All-New%20Dacia%20Duster%20-%20Dynamic%20%287%29.jpg', '2DACM82633A000013'),
    (16,'https://www.topgear.com/sites/default/files/2024/10/All-New%20Dacia%20Duster%20-%20Dynamic%20%2813%29.jpg', '2DACM82633A000013'),
    (17,'https://www.topgear.com/sites/default/files/2024/10/All-New%20Dacia%20Duster%20-%20Dynamic%20%2820%29.jpg', '2DACM82633A000013'),
    
    (18,'https://d1zlgejnyf0gxt.cloudfront.net/production/car_pictures/large/ford-mustang-dark-horse-antony-8aae9e88d55d287658fcb70b6c64a6e8.jpg', '2FODM82633A000014'),
    (19,'https://img.autobytel.com/chrome/colormatched/white/640/cc_2018foc34_03_640/cc_2018foc340005_03_640_g1.jpg', '2FODM82633A000014'),
    (20,'https://www.rpidesigns.com/images/2024-mustang-carbon-fiber-gt-performance-wing-rear-gurney-flap-spoiler-3.jpg', '2FODM82633A000014'),
    
    (21,'https://di-uploads-pod17.dealerinspire.com/miamilakesautomall/uploads/2023/02/2021-Challenger-Miami-Lakes-Automall.png', '2CHVM82633A000017'),
    (22,'https://www.darcarschryslerjeeprockville.com/static/dealer-17139/DARCARS-CDJR-of-Rockville_Dodge-Challenger_ls1V.jpg', '2CHVM82633A000017'),
    (23,'https://www.motortrend.com/uploads/2023/08/003-2023-Dodge-Challenger-Scat-Pack-Shakedown-rear-three-quarters-in-action.jpg', '2CHVM82633A000017'),
    (24,'https://www.motortrend.com/uploads/2023/08/010-2023-Dodge-Challenger-Scat-Pack-Shakedown-side-profile-in-action.jpg', '2CHVM82633A000017'),
    
    (25,'https://cdn.prezentmarzen.com/img/p/2/2/8/5/4/22854.jpg', '2FODM82633A000019'),
    (26,'https://i.etsystatic.com/15554723/r/il/49ff8e/4450336229/il_fullxfull.4450336229_te2f.jpg', '2FODM82633A000019'),
    (27,'https://editorial.pxcrush.net/carsales/general/editorial/chevrolet-camaro-lw1_7171.jpg', '2FODM82633A000019');

-- motorcycles:
INSERT INTO vehicle_marketplace.images(image_id, image_path, image_motorcycle_vin) VALUES
	(28, 'https://www.thunderbike.com/wp-content/uploads/2024/01/2025-street-glide-m48b-motorcycle-01.jpg', '3HDMC82633M000101'),
    (29, 'https://ricks-motorcycles.com/wp-content/uploads/2021/02/Harley-Davidson_Street-Glide-26-Custom_Ricks-001.jpg', '3HDMC82633M000101'),
    (30, 'https://cdn.room58.com/2024/07/26/125c5518de56a4ff4a941e2ab6f2d8f5_08463bbe8fd86249.jpg', '3HDMC82633M000101'),
    
    (31, 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/ed/MT_09_RN43_2017.jpg/960px-MT_09_RN43_2017.jpg', '3BMWM82633M000106'),
    (32, 'https://5.imimg.com/data5/BQ/CX/MY-45927837/mt09-500x500.jpg', '3BMWM82633M000106'),
    (33, 'https://nerostickers.com/cdn/shop/files/IMG_0751.jpg', '3BMWM82633M000106'),
    
    (34, 'https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/2006HondaCBR600RR-001.jpg/960px-2006HondaCBR600RR-001.jpg', '3BMWM82633M000107'),
    (35, 'https://cdpcdn.dx1app.com/products/USA/HO/2023/MC/SUPERSPORT/CBR600RR/50/GRAND_PRIX_RED/2000000008.jpg', '3BMWM82633M000107'),
    (36, 'https://www.slashgear.com/img/gallery/2024-honda-cbr600rr-review-so-outdated-yet-still-so-good/intro-1732552005.jpg', '3BMWM82633M000107'),
    
    (37, 'https://www.bossrides.in/wp-content/uploads/2023/03/kawasaki-ninja-zx10r-1-min-scaled-1-1024x800.jpg', '3BMWM82633M000108'),
    (38, 'https://vuongkhangmotor.com/upload/product/1-7453.jpg', '3BMWM82633M000108'),
    (39, 'https://vuongkhangmotor.com/upload/product/2-7341.jpg', '3BMWM82633M000108'),
    
    (40, 'https://www.mortonsdirect.co.uk/wp-content/uploads/sites/20/2024/03/KTMDuke790-1037.jpg', '3BMWM82633M000109'),
    (41, 'https://m.atcdn.co.uk/vms/media/32ab175da974469e8d7a4c07cf5951a3.jpg', '3BMWM82633M000109'),
    (42, 'https://www.datocms-assets.com/119921/1714074304-ktm_790duke_04.jpg', '3BMWM82633M000109'),
    
    (43, 'https://media.umbraco.io/suzuki-gb/imkfxrw5/hayabusa_m5_homepagebanner_2.jpg', '3BMWM82633M000110'),
    (44, 'https://images.classic.com/vehicles/a0599201bbbd5bb0791e728eb1020f4906010f10.jpg', '3BMWM82633M000110'),
    (45, 'https://www.centralfloridapowersports.com/cdn/shop/files/IMG_3615.jpg', '3BMWM82633M000110'),
    
    (46, 'https://mcn-images.bauersecure.com/wp-images/194543/600x400/triumph-street-triple-765-r-01.jpg', '3BMWM82633M000111'),
    (47, 'https://images.otf3.pixelmotiondemo.com/640x451/CGWRq-20230610213729.jpeg', '3BMWM82633M000111'),
    (48, 'https://cdn.dealerspike.com/imglib/v1/800x600/imglib/Assets/Inventory/1B/DD/1BDD43C5-5A20-45AC-AC89-1CC8CB751BB6.jpg', '3BMWM82633M000111');
    

