﻿use master;

GO

drop database if exists webshop;

CREATE DATABASE webshop collate Croatian_CI_AS;

GO

USE webshop;
GO


CREATE TABLE korisnici (
    ID_korisnika INT PRIMARY KEY IDENTITY(1,1),
    ime VARCHAR(50),
    prezime VARCHAR(50),
    god_rod DATETIME,
    email VARCHAR(100) UNIQUE,
    adresa VARCHAR(255),
    telefon VARCHAR(20)
);


CREATE TABLE proizvodi (
    ID_proizvoda INT PRIMARY KEY IDENTITY(1,1),
    naziv VARCHAR(100),
    vrsta VARCHAR(50), -- viski, rum, gin, konjak
    opis VARCHAR(500),
    cijena DECIMAL(10,2),
    zaliha INT,
    slika VARCHAR(355)
);

INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('The Macallan 25 #1', 'viski', 'Jedan od najpoznatijih škotskih viskija.', 3207.62, 10, 'https://example.com/images/viski/The_Macallan_25_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Yamazaki 18 #1', 'viski', 'Poznati japanski single malt.', 2256.16, 2, 'https://example.com/images/viski/Yamazaki_18_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Glenfiddich 30 #1', 'viski', 'Star i rijedak viski iz Speyside regije.', 1117.82, 2, 'https://example.com/images/viski/Glenfiddich_30_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Highland Park 21 #1', 'viski', 'Izuzetno izbalansiran viski s Orkneyskih otoka.', 4981.99, 7, 'https://example.com/images/viski/Highland_Park_21_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Balvenie 21 PortWood #1', 'viski', 'Sazrio u port vinskim bačvama.', 2904.32, 5, 'https://example.com/images/viski/Balvenie_21_PortWood_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Dalmore 25 #1', 'viski', 'Luksuzni škotski viski sa složenim notama.', 3335.48, 8, 'https://example.com/images/viski/Dalmore_25_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Lagavulin 16 #1', 'viski', 'Peaty viski sa Islay otoka.', 4257.36, 2, 'https://example.com/images/viski/Lagavulin_16_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Oban 21 #1', 'viski', 'Klasičan coastal viski s dubokim karakterom.', 4037.71, 5, 'https://example.com/images/viski/Oban_21_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Ardbeg Uigeadail #1', 'viski', 'Dimljeni viski s jakim aromama.', 971.89, 4, 'https://example.com/images/viski/Ardbeg_Uigeadail_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Aberlour A’Bunadh #1', 'viski', 'Viski buteljiran s punom snagom.', 1903.76, 8, 'https://example.com/images/viski/Aberlour_A’Bunadh_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('The Macallan 25 #2', 'viski', 'Jedan od najpoznatijih škotskih viskija.', 1966.1, 6, 'https://example.com/images/viski/The_Macallan_25_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Yamazaki 18 #2', 'viski', 'Poznati japanski single malt.', 4057.5, 6, 'https://example.com/images/viski/Yamazaki_18_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Glenfiddich 30 #2', 'viski', 'Star i rijedak viski iz Speyside regije.', 3142.32, 10, 'https://example.com/images/viski/Glenfiddich_30_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Highland Park 21 #2', 'viski', 'Izuzetno izbalansiran viski s Orkneyskih otoka.', 2063.07, 3, 'https://example.com/images/viski/Highland_Park_21_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Balvenie 21 PortWood #2', 'viski', 'Sazrio u port vinskim bačvama.', 2280.22, 3, 'https://example.com/images/viski/Balvenie_21_PortWood_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Dalmore 25 #2', 'viski', 'Luksuzni škotski viski sa složenim notama.', 1540.61, 7, 'https://example.com/images/viski/Dalmore_25_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Lagavulin 16 #2', 'viski', 'Peaty viski sa Islay otoka.', 3449.87, 7, 'https://example.com/images/viski/Lagavulin_16_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Oban 21 #2', 'viski', 'Klasičan coastal viski s dubokim karakterom.', 1291.28, 9, 'https://example.com/images/viski/Oban_21_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Ardbeg Uigeadail #2', 'viski', 'Dimljeni viski s jakim aromama.', 1685.75, 3, 'https://example.com/images/viski/Ardbeg_Uigeadail_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Aberlour A’Bunadh #2', 'viski', 'Viski buteljiran s punom snagom.', 4900.28, 9, 'https://example.com/images/viski/Aberlour_A’Bunadh_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Zacapa XO #1', 'rum', 'Guatemalski rum sa aromama karamele i vanilije.', 909.59, 1, 'https://example.com/images/rum/Zacapa_XO_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Diplomatico Ambassador #1', 'rum', 'Venecuelanski rum sa izraženim notama.', 4581.49, 9, 'https://example.com/images/rum/Diplomatico_Ambassador_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Plantation XO 20th Anniversary #1', 'rum', 'Karipski blend starijih rumova.', 3179.22, 6, 'https://example.com/images/rum/Plantation_XO_20th_Anniversary_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('El Dorado 21 #1', 'rum', 'Bogat i tamni rum iz Gvajane.', 4856.46, 3, 'https://example.com/images/rum/El_Dorado_21_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Mount Gay 1703 #1', 'rum', 'Jedan od najstarijih brendova ruma.', 4235.84, 10, 'https://example.com/images/rum/Mount_Gay_1703_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Facundo Paraiso #1', 'rum', 'Super-premium rum iz Bahama.', 1174.11, 5, 'https://example.com/images/rum/Facundo_Paraiso_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Ron Centenario 30 #1', 'rum', 'Costa Rica rum sa baršunastim okusom.', 3668.27, 6, 'https://example.com/images/rum/Ron_Centenario_30_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Pyrat Cask 1623 #1', 'rum', 'Aromatičan rum iz Angvile.', 3530.02, 1, 'https://example.com/images/rum/Pyrat_Cask_1623_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Dictador XO Insolent #1', 'rum', 'Kolumbijski rum punog tijela.', 4678.35, 2, 'https://example.com/images/rum/Dictador_XO_Insolent_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Brugal Papa Andres #1', 'rum', 'Limitirano izdanje iz Dominikanske Republike.', 1264.83, 8, 'https://example.com/images/rum/Brugal_Papa_Andres_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Zacapa XO #2', 'rum', 'Guatemalski rum sa aromama karamele i vanilije.', 2080.69, 1, 'https://example.com/images/rum/Zacapa_XO_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Diplomatico Ambassador #2', 'rum', 'Venecuelanski rum sa izraženim notama.', 1059.22, 8, 'https://example.com/images/rum/Diplomatico_Ambassador_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Plantation XO 20th Anniversary #2', 'rum', 'Karipski blend starijih rumova.', 2323.83, 6, 'https://example.com/images/rum/Plantation_XO_20th_Anniversary_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('El Dorado 21 #2', 'rum', 'Bogat i tamni rum iz Gvajane.', 3488.31, 9, 'https://example.com/images/rum/El_Dorado_21_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Mount Gay 1703 #2', 'rum', 'Jedan od najstarijih brendova ruma.', 4608.61, 3, 'https://example.com/images/rum/Mount_Gay_1703_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Facundo Paraiso #2', 'rum', 'Super-premium rum iz Bahama.', 3382.69, 7, 'https://example.com/images/rum/Facundo_Paraiso_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Ron Centenario 30 #2', 'rum', 'Costa Rica rum sa baršunastim okusom.', 3991.93, 8, 'https://example.com/images/rum/Ron_Centenario_30_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Pyrat Cask 1623 #2', 'rum', 'Aromatičan rum iz Angvile.', 2772.28, 2, 'https://example.com/images/rum/Pyrat_Cask_1623_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Dictador XO Insolent #2', 'rum', 'Kolumbijski rum punog tijela.', 1206.54, 1, 'https://example.com/images/rum/Dictador_XO_Insolent_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Brugal Papa Andres #2', 'rum', 'Limitirano izdanje iz Dominikanske Republike.', 4326.54, 8, 'https://example.com/images/rum/Brugal_Papa_Andres_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Monkey 47 #1', 'gin', 'Njemački gin sa 47 biljnih sastojaka.', 853.43, 6, 'https://example.com/images/gin/Monkey_47_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Hapusa Himalayan Dry Gin #1', 'gin', 'Indijski gin sa himalajskim borovnicama.', 3653.67, 1, 'https://example.com/images/gin/Hapusa_Himalayan_Dry_Gin_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('The Botanist #1', 'gin', 'Islay gin sa divljim biljem.', 1459.77, 8, 'https://example.com/images/gin/The_Botanist_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Sipsmith VJOP #1', 'gin', 'London dry gin s dodatnom snagom.', 884.18, 1, 'https://example.com/images/gin/Sipsmith_VJOP_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Tanqueray No. Ten #1', 'gin', 'Klasičan citrusni gin.', 3583.4, 8, 'https://example.com/images/gin/Tanqueray_No._Ten_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Four Pillars Rare Dry #1', 'gin', 'Australski gin bogatog profila.', 2547.92, 10, 'https://example.com/images/gin/Four_Pillars_Rare_Dry_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Beefeater Crown Jewel #1', 'gin', 'Ekskluzivno izdanje Beefeatera.', 4206.55, 5, 'https://example.com/images/gin/Beefeater_Crown_Jewel_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Citadelle Reserve #1', 'gin', 'Francuski gin odležan u bačvama.', 4110.19, 6, 'https://example.com/images/gin/Citadelle_Reserve_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Gin Mare #1', 'gin', 'Mediteranski gin s notama masline i ružmarina.', 1557.73, 4, 'https://example.com/images/gin/Gin_Mare_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('No. 3 London Dry Gin #1', 'gin', 'Tradicionalni britanski gin visokog kvaliteta.', 2075.43, 8, 'https://example.com/images/gin/No._3_London_Dry_Gin_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Monkey 47 #2', 'gin', 'Njemački gin sa 47 biljnih sastojaka.', 1542.86, 8, 'https://example.com/images/gin/Monkey_47_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Hapusa Himalayan Dry Gin #2', 'gin', 'Indijski gin sa himalajskim borovnicama.', 893.64, 1, 'https://example.com/images/gin/Hapusa_Himalayan_Dry_Gin_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('The Botanist #2', 'gin', 'Islay gin sa divljim biljem.', 3130.21, 5, 'https://example.com/images/gin/The_Botanist_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Sipsmith VJOP #2', 'gin', 'London dry gin s dodatnom snagom.', 1286.51, 9, 'https://example.com/images/gin/Sipsmith_VJOP_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Tanqueray No. Ten #2', 'gin', 'Klasičan citrusni gin.', 2014.57, 2, 'https://example.com/images/gin/Tanqueray_No._Ten_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Four Pillars Rare Dry #2', 'gin', 'Australski gin bogatog profila.', 4020.3, 3, 'https://example.com/images/gin/Four_Pillars_Rare_Dry_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Beefeater Crown Jewel #2', 'gin', 'Ekskluzivno izdanje Beefeatera.', 3535.28, 8, 'https://example.com/images/gin/Beefeater_Crown_Jewel_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Citadelle Reserve #2', 'gin', 'Francuski gin odležan u bačvama.', 1990.24, 9, 'https://example.com/images/gin/Citadelle_Reserve_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Gin Mare #2', 'gin', 'Mediteranski gin s notama masline i ružmarina.', 4108.1, 8, 'https://example.com/images/gin/Gin_Mare_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('No. 3 London Dry Gin #2', 'gin', 'Tradicionalni britanski gin visokog kvaliteta.', 4828.82, 1, 'https://example.com/images/gin/No._3_London_Dry_Gin_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Martell L’Or de Jean #1', 'konjak', 'Vrhunac majstorstva kuće Martell.', 4881.14, 3, 'https://example.com/images/konjak/Martell_L’Or_de_Jean_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Hennessy Paradis #1', 'konjak', 'Savršen spoj rijetkih eaux-de-vie.', 1512.53, 3, 'https://example.com/images/konjak/Hennessy_Paradis_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Rémy Martin XO #1', 'konjak', 'Ekskluzivni konjak punih aroma.', 3866.22, 10, 'https://example.com/images/konjak/Rémy_Martin_XO_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Courvoisier Initiale Extra #1', 'konjak', 'Kompleksan konjak s notama karamele.', 4750.93, 9, 'https://example.com/images/konjak/Courvoisier_Initiale_Extra_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Delamain Le Voyage #1', 'konjak', 'Izuzetno rijedak i luksuzan konjak.', 2084.6, 10, 'https://example.com/images/konjak/Delamain_Le_Voyage_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Camus Cuvée 5.150 #1', 'konjak', 'Jedinstven blend u čast obljetnice.', 1533.75, 8, 'https://example.com/images/konjak/Camus_Cuvée_5.150_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Hardy Perfection #1', 'konjak', 'Jedan od najstarijih dostupnih konjaka.', 2011.77, 6, 'https://example.com/images/konjak/Hardy_Perfection_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Frapin Cuvée 1888 #1', 'konjak', 'Premium konjak limitiranog izdanja.', 3513.82, 7, 'https://example.com/images/konjak/Frapin_Cuvée_1888_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Pierre Ferrand Selection Des Anges #1', 'konjak', 'Stil 19. stoljeća u čaši.', 4018.67, 10, 'https://example.com/images/konjak/Pierre_Ferrand_Selection_Des_Anges_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Louis Royer Extra #1', 'konjak', 'Bogate teksture i dubine okusa.', 1214.22, 3, 'https://example.com/images/konjak/Louis_Royer_Extra_1.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Martell L’Or de Jean #2', 'konjak', 'Vrhunac majstorstva kuće Martell.', 2467.23, 9, 'https://example.com/images/konjak/Martell_L’Or_de_Jean_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Hennessy Paradis #2', 'konjak', 'Savršen spoj rijetkih eaux-de-vie.', 1199.68, 9, 'https://example.com/images/konjak/Hennessy_Paradis_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Rémy Martin XO #2', 'konjak', 'Ekskluzivni konjak punih aroma.', 1953.19, 9, 'https://example.com/images/konjak/Rémy_Martin_XO_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Courvoisier Initiale Extra #2', 'konjak', 'Kompleksan konjak s notama karamele.', 1172.1, 3, 'https://example.com/images/konjak/Courvoisier_Initiale_Extra_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Delamain Le Voyage #2', 'konjak', 'Izuzetno rijedak i luksuzan konjak.', 1091.93, 4, 'https://example.com/images/konjak/Delamain_Le_Voyage_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Camus Cuvée 5.150 #2', 'konjak', 'Jedinstven blend u čast obljetnice.', 1736.86, 10, 'https://example.com/images/konjak/Camus_Cuvée_5.150_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Hardy Perfection #2', 'konjak', 'Jedan od najstarijih dostupnih konjaka.', 3701.94, 5, 'https://example.com/images/konjak/Hardy_Perfection_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Frapin Cuvée 1888 #2', 'konjak', 'Premium konjak limitiranog izdanja.', 974.4, 2, 'https://example.com/images/konjak/Frapin_Cuvée_1888_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Pierre Ferrand Selection Des Anges #2', 'konjak', 'Stil 19. stoljeća u čaši.', 4986.16, 6, 'https://example.com/images/konjak/Pierre_Ferrand_Selection_Des_Anges_2.jpg');
INSERT INTO proizvodi (naziv, vrsta, opis, cijena, zaliha, slika) VALUES ('Louis Royer Extra #2', 'konjak', 'Bogate teksture i dubine okusa.', 4103.16, 4, 'https://example.com/images/konjak/Louis_Royer_Extra_2.jpg');

CREATE TABLE kosarice (
    ID_kosarice INT PRIMARY KEY IDENTITY(1,1),
    ID_korisnika INT,
    datum_kreiranja DATETIME DEFAULT GETDATE(),
    status VARCHAR(20), -- aktivna, naručena
    FOREIGN KEY (ID_korisnika) REFERENCES korisnici(ID_korisnika)
);


CREATE TABLE stavke_kosarice (
    ID_stavke INT PRIMARY KEY IDENTITY(1,1),
    ID_kosarice INT,
    ID_proizvoda INT,
    kolicina INT,
    cijena_kom DECIMAL(10,2),
    FOREIGN KEY (ID_kosarice) REFERENCES kosarice(ID_kosarice),
    FOREIGN KEY (ID_proizvoda) REFERENCES proizvodi(ID_proizvoda)
);

CREATE TABLE narudzbe (
    ID_narudzbe INT PRIMARY KEY IDENTITY(1,1),
    ID_korisnika INT,
    datum_narudzbe DATETIME DEFAULT GETDATE(),
    ukupna_cijena DECIMAL(10,2),
    status VARCHAR(20), -- u obradi, isporučena, otkazana
    FOREIGN KEY (ID_korisnika) REFERENCES korisnici(ID_korisnika)
);


CREATE TABLE stavke_narudzbe (
    ID_stavke_narudzbe INT PRIMARY KEY IDENTITY(1,1),
    ID_narudzbe INT,
    ID_proizvoda INT,
    kolicina INT,
    cijena_kom DECIMAL(10,2),
    FOREIGN KEY (ID_narudzbe) REFERENCES narudzbe(ID_narudzbe),
    FOREIGN KEY (ID_proizvoda) REFERENCES proizvodi(ID_proizvoda)
);