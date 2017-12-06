Create Database Hotel;

Create Table TIP_SOBA (
	ID bigInt identity primary key,
	NAZIV VarChar(25) not null,
	BR_KREVETA int not null,
	POSTOJI bit not null
);

Create table SOBA (
	ID bigInt identity primary key,
	BROJ int not null,
	TIP_ID bigInt foreign key references TIP_SOBA(ID),
	TV bit not null,
	MINI_BAR bit not null,
	POSTOJI bit not null
);

Create table KORISNIK (
	ID bigInt identity primary key,
	IME VarChar(25),
	PREZIME VarChar(25),
	JMBG VarChar(15),
	KORISNICKO_IME VarChar(15),
	LOZINKA VarChar(15),
	TIP_KORISNIKA Int,
	POSTOJI bit not null
);

Create Table TIP_IZNAJMLJIVANJA (
	ID bigInt primary key,
	NAZIV VarChar(25) not null
);

Create Table CENOVNIK (
	TIP_IZNAJMLJIVANJA_ID bigInt foreign key references TIP_IZNAJMLJIVANJA(ID),
	TIP_SOBA_ID bigInt foreign key references TIP_SOBA(ID),
	CENA Money not null,
	POSTOJI bit not null,
	Primary key(TIP_IZNAJMLJIVANJA_ID,TIP_SOBA_ID)
);

Create table IZNAJMLJIVANJE (
	ID bigInt identity primary key,
	SOBA_ID bigInt foreign key references SOBA(ID),
	POCETNI_DATUM DateTime,
	ZAVRSNI_DATUM DateTime,
	TIP_IZNAJMLJIVANJA_ID bigInt foreign key references TIP_IZNAJMLJIVANJA(ID),
	UKUPNA_CENA Money,
	POSTOJI bit not null
);

Create table GOST (
	ID bigInt identity primary key,
	IME VarChar(25),
	PREZIME VarChar(25),
	JMBG VarChar(15),
	BR_LICNE_KARTE VarChar(15),
	IZNAJMLJIVANJE_ID bigInt foreign key references IZNAJMLJIVANJE(ID),
	POSTOJI bit not null
);