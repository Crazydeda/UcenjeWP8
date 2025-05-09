--create database trgovina;

use trgovina;

create table racuni(
sifra int,
broj varchar(10),
kupac varchar(50)
);


create table proizvodi(
sifra int,
naziv varchar(20),
cijena decimal(10,2)
);

create table stavke(
racun int,
proizvod int,
kolicina decimal(10,2)
);
