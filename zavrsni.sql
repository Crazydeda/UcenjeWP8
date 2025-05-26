use master;

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
    zaliha INT
    -- slika VARCHAR(255)
);


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