-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema elektroniczny_indeks
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema elektroniczny_indeks
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `elektroniczny_indeks` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `elektroniczny_indeks` ;

-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`dane_adresowe`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`dane_adresowe` (
  `id_dane_adresowe` SMALLINT NOT NULL AUTO_INCREMENT,
  `kraj_zamieszkania` VARCHAR(30) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NOT NULL,
  `miasto` VARCHAR(30) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  `ulica` VARCHAR(60) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NOT NULL,
  `numer_domu` VARCHAR(10) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NOT NULL,
  `numer_lokalu` VARCHAR(10) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NOT NULL,
  `kod_pocztowy` VARCHAR(6) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NOT NULL,
  PRIMARY KEY (`id_dane_adresowe`),
  INDEX `kraj_zamieszkania_idx` (`kraj_zamieszkania` ASC) ,
  INDEX `miasto_idx` (`miasto` ASC) ,
  INDEX `ulica` (`ulica` ASC) ,
  INDEX `numer_domu` (`numer_domu` ASC) ,
  INDEX `numer_lokalu` (`numer_lokalu` ASC) ,
  INDEX `kod_pocztowy` (`kod_pocztowy` ASC) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`uzytkownicy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`uzytkownicy` (
  `id_uzytkownika` SMALLINT NOT NULL AUTO_INCREMENT,
  `login` VARCHAR(15) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  `haslo` VARCHAR(15) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NOT NULL,
  `rola` CHAR(1) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  PRIMARY KEY (`id_uzytkownika`),
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) ,
  INDEX `login_idx` (`login` ASC) ,
  INDEX `haslo_idx` (`haslo` ASC) ,
  INDEX `rola_idx` (`rola` ASC) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` (
  `id_dane` SMALLINT NOT NULL AUTO_INCREMENT,
  `id_uzytkownika` SMALLINT NOT NULL,
  `id_dane_adresowe` SMALLINT NOT NULL,
  `imie` VARCHAR(20) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  `nazwisko` VARCHAR(45) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  `pesel` VARCHAR(11) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NOT NULL,
  `data_urodzenia` DATE NOT NULL,
  `plec` CHAR(1) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NOT NULL,
  `numer_kontaktowy` VARCHAR(9) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NOT NULL,
  PRIMARY KEY (`id_dane`),
  UNIQUE INDEX `pesel_UNIQUE` (`pesel` ASC) ,
  INDEX `id_uzytkownika_idx` (`id_uzytkownika` ASC) ,
  INDEX `id_dane_adresowe_idx` (`id_dane_adresowe` ASC) ,
  INDEX `imie_idx` (`imie` ASC) ,
  INDEX `nazwisko_idx` (`nazwisko` ASC) ,
  INDEX `pesel_idx` (`pesel` ASC) ,
  INDEX `data_urodzenia_idx` (`data_urodzenia` ASC) ,
  INDEX `plec_idx` (`plec` ASC) ,
  INDEX `numer_kontaktowy__idx` (`numer_kontaktowy` ASC) ,
  CONSTRAINT `id_dane_adresowe`
    FOREIGN KEY (`id_dane_adresowe`)
    REFERENCES `elektroniczny_indeks`.`dane_adresowe` (`id_dane_adresowe`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `id_uzytkownika`
    FOREIGN KEY (`id_uzytkownika`)
    REFERENCES `elektroniczny_indeks`.`uzytkownicy` (`id_uzytkownika`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`administrator`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`administrator` (
  `id_administratora` SMALLINT NOT NULL AUTO_INCREMENT,
  `id_dane` SMALLINT NOT NULL,
  `email` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  PRIMARY KEY (`id_administratora`),
  INDEX `id_dane_idx` (`id_dane` ASC) ,
  INDEX `email_idx` (`email` ASC) ,
  CONSTRAINT `fk_uzytkownik_administrator`
    FOREIGN KEY (`id_dane`)
    REFERENCES `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` (`id_uzytkownika`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`prowadzacy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`prowadzacy` (
  `id_prowadzacego` TINYINT NOT NULL AUTO_INCREMENT,
  `id_dane` SMALLINT NOT NULL,
  `email` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  PRIMARY KEY (`id_prowadzacego`),
  INDEX `id_dane_idx` (`id_dane` ASC) ,
  INDEX `email_idx` (`email` ASC) ,
  CONSTRAINT `fk_uzytkownik_prowadzacy`
    FOREIGN KEY (`id_dane`)
    REFERENCES `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` (`id_uzytkownika`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`kurs`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`kurs` (
  `id_kursu` SMALLINT NOT NULL AUTO_INCREMENT,
  `id_prowadzacego` TINYINT NOT NULL,
  `nazwa_kursu` VARCHAR(30) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  `ects` TINYINT NOT NULL,
  PRIMARY KEY (`id_kursu`),
  UNIQUE INDEX `nazwa_kursu_UNIQUE` (`nazwa_kursu` ASC) ,
  INDEX `id_prowadzacego_idx` (`id_prowadzacego` ASC) ,
  INDEX `nazwa_kursu_idx` (`nazwa_kursu` ASC) ,
  INDEX `ects_idx` (`ects` ASC) ,
  CONSTRAINT `id_prowadzacego`
    FOREIGN KEY (`id_prowadzacego`)
    REFERENCES `elektroniczny_indeks`.`prowadzacy` (`id_prowadzacego`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`grupa_zajeciowa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`grupa_zajeciowa` (
  `id_grupy` SMALLINT NOT NULL AUTO_INCREMENT,
  `id_kursu` SMALLINT NOT NULL,
  `budynek` VARCHAR(4) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  `sala` SMALLINT NOT NULL,
  `dzien_tygodnia` TINYINT NOT NULL,
  `typ_zajec` CHAR(1) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  `godzina_rozpoczecia` TIME NOT NULL,
  `godzina_zakonczenia` TIME NOT NULL,
  PRIMARY KEY (`id_grupy`),
  INDEX `id_kursu_idx` (`id_kursu` ASC) ,
  INDEX `budynek_idx` (`budynek` ASC) ,
  INDEX `sala_idx` (`sala` ASC) ,
  INDEX `dzien_tygodnia_idx` (`dzien_tygodnia` ASC) ,
  INDEX `typ_zajec_idx` (`typ_zajec` ASC) ,
  INDEX `godzina_rozpoczecia__idx` (`godzina_rozpoczecia` ASC) ,
  INDEX `godzina_zakonczenia_idx` (`godzina_zakonczenia` ASC) ,
  CONSTRAINT `id_kursu`
    FOREIGN KEY (`id_kursu`)
    REFERENCES `elektroniczny_indeks`.`kurs` (`id_kursu`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`kierunek`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`kierunek` (
  `id_kierunku` TINYINT NOT NULL AUTO_INCREMENT,
  `nazwa_kierunku` VARCHAR(30) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  `semestr` TINYINT NOT NULL,
  `stopien` TINYINT NOT NULL,
  PRIMARY KEY (`id_kierunku`),
  INDEX `nazwa_kierunku_idx` (`nazwa_kierunku` ASC) ,
  INDEX `semestr_idx` (`semestr` ASC) ,
  INDEX `stopien_idx` (`stopien` ASC) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`pracownik_dziekanatu`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`pracownik_dziekanatu` (
  `id_pracownika` TINYINT NOT NULL AUTO_INCREMENT,
  `id_dane` SMALLINT NOT NULL,
  `email` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  PRIMARY KEY (`id_pracownika`),
  INDEX `id_dane_idx` (`id_dane` ASC) ,
  INDEX `email_idx` (`email` ASC) ,
  CONSTRAINT `fk_uzytkownik_dziekanat`
    FOREIGN KEY (`id_dane`)
    REFERENCES `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` (`id_uzytkownika`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`student`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`student` (
  `numer_indeksu` MEDIUMINT NOT NULL AUTO_INCREMENT,
  `id_kierunku` TINYINT NOT NULL,
  `id_dane` SMALLINT NOT NULL,
  `email` VARCHAR(25) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' COLLATE 'utf8_general_ci' NOT NULL,
  PRIMARY KEY (`numer_indeksu`),
  INDEX `id_kierunku_idx` (`id_kierunku` ASC) ,
  INDEX `email_idx` (`email` ASC) ,
  INDEX `fk_uzytkownik_student_idx` (`id_dane` ASC) ,
  CONSTRAINT `fk_uzytkownik_student`
    FOREIGN KEY (`id_dane`)
    REFERENCES `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` (`id_dane`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `id_kierunku`
    FOREIGN KEY (`id_kierunku`)
    REFERENCES `elektroniczny_indeks`.`kierunek` (`id_kierunku`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 240000
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;


-- -----------------------------------------------------
-- Table `elektroniczny_indeks`.`zapis`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`zapis` (
  `id_zapisu` SMALLINT NOT NULL AUTO_INCREMENT,
  `numer_indeksu` MEDIUMINT NOT NULL,
  `id_grupy` SMALLINT NOT NULL,
  `ocena` FLOAT NULL DEFAULT NULL,
  `status_oceny` CHAR(1) CHARACTER SET 'utf8' COLLATE 'utf8_general_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`id_zapisu`),
  INDEX `numer_indeksu_idx` (`numer_indeksu` ASC) ,
  INDEX `id_grupy_idx` (`id_grupy` ASC) ,
  INDEX `ocena_idx` (`ocena` ASC) ,
  INDEX `status_oceny_idx` (`status_oceny` ASC) ,
  CONSTRAINT `id_grupy`
    FOREIGN KEY (`id_grupy`)
    REFERENCES `elektroniczny_indeks`.`grupa_zajeciowa` (`id_grupy`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `numer_indeksu`
    FOREIGN KEY (`numer_indeksu`)
    REFERENCES `elektroniczny_indeks`.`student` (`numer_indeksu`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_general_ci;

USE `elektroniczny_indeks` ;

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`dane_studenta_podglad_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`dane_studenta_podglad_view` (`numer_indeksu` INT, `pesel` INT, `imie` INT, `nazwisko` INT, `data_urodzenia` INT, `plec` INT, `numer_kontaktowy` INT, `kraj_zamieszkania` INT, `miasto` INT, `ulica` INT, `numer_domu` INT, `numer_lokalu` INT, `kod_pocztowy` INT, `nazwa_kierunku` INT, `semestr` INT, `stopien` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`dane_studenta_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`dane_studenta_view` (`id_uzytkownika` INT, `nazwa_kierunku` INT, `stopien` INT, `semestr` INT, `numer_indeksu` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`dane_studentow_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`dane_studentow_view` (`id_uzytkownika` INT, `numer_indeksu` INT, `imie` INT, `nazwisko` INT, `pesel` INT, `nazwa_kierunku` INT, `stopien` INT, `semestr` INT, `numer_kontaktowy` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`dane_uzytkownika_podglad_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`dane_uzytkownika_podglad_view` (`id_uzytkownika` INT, `pesel` INT, `imie` INT, `nazwisko` INT, `data_urodzenia` INT, `plec` INT, `numer_kontaktowy` INT, `kraj_zamieszkania` INT, `miasto` INT, `ulica` INT, `numer_domu` INT, `numer_lokalu` INT, `kod_pocztowy` INT, `login` INT, `haslo` INT, `rola` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`dane_uzytkownika_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`dane_uzytkownika_view` (`id_uzytkownika` INT, `imie` INT, `nazwisko` INT, `pesel` INT, `data_urodzenia` INT, `plec` INT, `numer_kontaktowy` INT, `kraj_zamieszkania` INT, `miasto` INT, `ulica` INT, `numer_domu` INT, `numer_lokalu` INT, `kod_pocztowy` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`grupy_kursu_prowadzacego_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`grupy_kursu_prowadzacego_view` (`id_uzytkownika` INT, `id_kursu` INT, `id_grupy` INT, `budynek` INT, `sala` INT, `dzien_tygodnia` INT, `typ_zajec` INT, `godzina_rozpoczecia` INT, `godzina_zakonczenia` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`lista_uzytkownikow_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`lista_uzytkownikow_view` (`id_uzytkownika` INT, `imie` INT, `nazwisko` INT, `rola` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`oceny_studenta_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`oceny_studenta_view` (`id_zapisu` INT, `id_dane` INT, `imie` INT, `nazwisko` INT, `nazwa_kursu` INT, `ects` INT, `grupa_zajeciowa` INT, `ocena` INT, `status_oceny` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`plan_zajec_studenta_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`plan_zajec_studenta_view` (`id_dane` INT, `imie` INT, `nazwisko` INT, `nazwa_kursu` INT, `ects` INT, `id_grupy` INT, `budynek` INT, `sala` INT, `dzien_tygodnia` INT, `typ_zajec` INT, `godzina_rozpoczecia` INT, `godzina_zakonczenia` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`prowadzone_kursy_prowadzacego_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`prowadzone_kursy_prowadzacego_view` (`id_uzytkownika` INT, `id_kursu` INT, `nazwa_kursu` INT, `ects` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`przegladanie_grup_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`przegladanie_grup_view` (`imie` INT, `nazwisko` INT, `nazwa_kursu` INT, `ects` INT, `id_grupy` INT, `budynek` INT, `sala` INT, `dzien_tygodnia` INT, `typ_zajec` INT, `godzina_rozpoczecia` INT, `godzina_zakonczenia` INT);

-- -----------------------------------------------------
-- Placeholder table for view `elektroniczny_indeks`.`studenci_prowadzacego_view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `elektroniczny_indeks`.`studenci_prowadzacego_view` (`id_uzytkownika` INT, `numer_indeksu` INT, `id_dane` INT, `nazwa_kursu` INT, `id_grupy` INT, `typ_zajec` INT, `ocena` INT, `status_oceny` INT);

-- -----------------------------------------------------
-- procedure czy_kierunek_istnieje
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `czy_kierunek_istnieje`(nazwa_kierunku_n varchar(30), semestr_n tinyint, stopien_n tinyint)
BEGIN
	SELECT id_kierunku from kierunek where nazwa_kierunku=nazwa_kierunku_n and semestr=semestr_n and stopien=stopien_n;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure dodaj_grupe_zajeciowa
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `dodaj_grupe_zajeciowa`(kurs smallint, budynek_n varchar(4), sala_n smallint,
																	dzien_tygodnia_n tinyint, typ_zajec_n char(1), godzina_rozpoczecia_n time, godzina_zakonczenia_n time, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if (rola_uzytkownika='a') then
		insert into `grupa_zajeciowa`(`id_kursu`, `budynek`, `sala`, `dzien_tygodnia`, `typ_zajec`, `godzina_rozpoczecia`, `godzina_zakonczenia`) values
		(kurs, budynek_n, sala_n, dzien_tygodnia_n, typ_zajec_n, godzina_rozpoczecia_n, godzina_zakonczenia_n);
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure dodaj_kierunek
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `dodaj_kierunek`(nazwa varchar(30), semestr_n tinyint, stopien_n tinyint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if (rola_uzytkownika='a') then
		insert into `kierunek` (`nazwa_kierunku`, `semestr`, `stopien`) values (nazwa, semestr_n, stopien_n);
    end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure dodaj_kurs
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `dodaj_kurs`(prowadzacy tinyint, nazwa varchar(30), punkty_ects tinyint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if (rola_uzytkownika='a') then
		insert into `kurs`(`id_prowadzacego`,`nazwa_kursu`,`ects`) values (prowadzacy, nazwa, punkty_ects);
    end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure dodaj_uzytkownika
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `dodaj_uzytkownika`(pesel_n varchar(11), imie_n varchar(20), nazwisko_n varchar(45), data_urodzenia_n date, plec_n char(1), numer_kontaktowy_n varchar(9), kraj_zamieszkania_n varchar(30), miasto_n varchar(30), ulica_n varchar(30), numer_domu_n varchar(10), numer_lokalu_n varchar(10), kod_pocztowy_n varchar(6), login_n varchar(15), haslo_n varchar(15), rola_n char(1), kierunek varchar(30), ktory_semestr tinyint, ktory_stopien tinyint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    declare czy_istnieje bool;
    declare identyfikator mediumint;
    declare identyfikator_danych mediumint;
    declare imie_email varchar(20);
    declare nazwisko_email varchar(45);

	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    set czy_istnieje = true;
    
    if (((rola_uzytkownika='a') or (rola_uzytkownika='d' and rola_n='s')) and ((select not exists(select id_dane from  `dane_osobowe_i_kontaktowe` where pesel=pesel_n)) and (select not exists(select id_uzytkownika from  `uzytkownicy` where login=login_n)))) then
		insert into `uzytkownicy`(`login`,`haslo`,`rola`) values (login_n, haslo_n, rola_n);
        if (select not exists(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n)) then
			insert into `dane_adresowe`(`kraj_zamieszkania`,`miasto`,`ulica`,`numer_domu`,`numer_lokalu`,`kod_pocztowy`) values (kraj_zamieszkania_n, miasto_n, ulica_n, numer_domu_n, numer_lokalu_n, kod_pocztowy_n);
		end if;
        insert into `dane_osobowe_i_kontaktowe`(`id_uzytkownika`, `id_dane_adresowe`, `imie`, `pesel`,`nazwisko`,`data_urodzenia`,`plec`,`numer_kontaktowy`) values 
		((select id_uzytkownika from `uzytkownicy` where (login=login_n and haslo=haslo_n and rola=rola_n)),
		(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n),
		imie_n, pesel_n, nazwisko_n, data_urodzenia_n, plec_n, numer_kontaktowy_n);
		if rola_n='s' then 
			insert into `student`(`id_kierunku`,`id_dane`,`email`) values 
			((select id_kierunku from `kierunek` where (nazwa_kierunku=kierunek and semestr=ktory_semestr and stopien=ktory_stopien)),
			(select id_dane from `dane_osobowe_i_kontaktowe` where (id_uzytkownika=(select id_uzytkownika from `uzytkownicy` where (login=login_n and haslo=haslo_n and rola=rola_n)) and 
			id_dane_adresowe=(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n) and 
			imie=imie_n and pesel=pesel_n and nazwisko=nazwisko_n and data_urodzenia=data_urodzenia_n and 
			plec=plec_n and numer_kontaktowy=numer_kontaktowy_n)),
			'@student.po.edu.pl');
            set identyfikator=(select numer_indeksu from student where
            (id_kierunku=(select id_kierunku from `kierunek` where (nazwa_kierunku=kierunek and semestr=ktory_semestr and stopien=ktory_stopien))and
            id_dane=(select id_dane from `dane_osobowe_i_kontaktowe` where (id_uzytkownika=(select id_uzytkownika from `uzytkownicy` where (login=login_n and haslo=haslo_n and rola=rola_n)) and 
			id_dane_adresowe=(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n) and 
			imie=imie_n and pesel=pesel_n and nazwisko=nazwisko_n and data_urodzenia=data_urodzenia_n and 
			plec=plec_n and numer_kontaktowy=numer_kontaktowy_n))));
            update `student`
			set email = concat((select cast(identyfikator as char(6))),'@student.po.edu.pl')
			where numer_indeksu = identyfikator;
		elseif rola_n='p' then 
			insert into `prowadzacy`(`id_dane`,`email`) values 
			((select id_dane from `dane_osobowe_i_kontaktowe` where (id_uzytkownika=(select id_uzytkownika from `uzytkownicy` where (login=login_n and haslo=haslo_n and rola=rola_n)) and 
			id_dane_adresowe=(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n) and 
			imie=imie_n and pesel=pesel_n and nazwisko=nazwisko_n and data_urodzenia=data_urodzenia_n and 
			plec=plec_n and numer_kontaktowy=numer_kontaktowy_n)),
			'@po.edu.pl');
            set identyfikator_danych = (select id_dane from `dane_osobowe_i_kontaktowe` where (id_uzytkownika=(select id_uzytkownika from `uzytkownicy` where (login=login_n and haslo=haslo_n and rola=rola_n)) and 
			id_dane_adresowe=(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n) and 
			imie=imie_n and pesel=pesel_n and nazwisko=nazwisko_n and data_urodzenia=data_urodzenia_n and 
			plec=plec_n and numer_kontaktowy=numer_kontaktowy_n));
            set identyfikator = (select id_prowadzacego from `prowadzacy` where id_dane=identyfikator_danych);
            set imie_email = (select imie from `dane_osobowe_i_kontaktowe` where id_dane=identyfikator_danych);
            set nazwisko_email = (select nazwisko from `dane_osobowe_i_kontaktowe` where id_dane=identyfikator_danych);
            update `prowadzacy`
			set email = concat(imie_email, '.', nazwisko_email, '@po.edu.pl')
			where id_prowadzacego = identyfikator;
		elseif rola_n='d' then
			insert into `pracownik_dziekanatu`(`id_dane`,`email`) values 
			((select id_dane from `dane_osobowe_i_kontaktowe` where (id_uzytkownika=(select id_uzytkownika from `uzytkownicy` where (login=login_n and haslo=haslo_n and rola=rola_n)) and 
			id_dane_adresowe=(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n) and 
			imie=imie_n and pesel=pesel_n and nazwisko=nazwisko_n and data_urodzenia=data_urodzenia_n and 
			plec=plec_n and numer_kontaktowy=numer_kontaktowy_n)),
			'@po.edu.pl');
            set identyfikator_danych = (select id_dane from `dane_osobowe_i_kontaktowe` where (id_uzytkownika=(select id_uzytkownika from `uzytkownicy` where (login=login_n and haslo=haslo_n and rola=rola_n)) and 
			id_dane_adresowe=(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n) and 
			imie=imie_n and pesel=pesel_n and nazwisko=nazwisko_n and data_urodzenia=data_urodzenia_n and 
			plec=plec_n and numer_kontaktowy=numer_kontaktowy_n));
            set identyfikator = (select id_pracownika from `pracownik_dziekanatu` where id_dane=identyfikator_danych);
            set imie_email = (select imie from `dane_osobowe_i_kontaktowe` where id_dane=identyfikator_danych);
            set nazwisko_email = (select nazwisko from `dane_osobowe_i_kontaktowe` where id_dane=identyfikator_danych);
            update `pracownik_dziekanatu`
			set email = concat(imie_email, '.', nazwisko_email, '@po.edu.pl')
			where id_pracownika = identyfikator;
		elseif rola_n='a' then
			insert into `administrator`(`id_dane`,`email`) values 
			((select id_dane from `dane_osobowe_i_kontaktowe` where (id_uzytkownika=(select id_uzytkownika from `uzytkownicy` where (login=login_n and haslo=haslo_n and rola=rola_n)) and 
			id_dane_adresowe=(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n) and 
			imie=imie_n and pesel=pesel_n and nazwisko=nazwisko_n and data_urodzenia=data_urodzenia_n and 
			plec=plec_n and numer_kontaktowy=numer_kontaktowy_n)),
			'@po.edu.pl');
            set identyfikator_danych = (select id_dane from `dane_osobowe_i_kontaktowe` where (id_uzytkownika=(select id_uzytkownika from `uzytkownicy` where (login=login_n and haslo=haslo_n and rola=rola_n)) and 
			id_dane_adresowe=(select id_dane_adresowe from `dane_adresowe` where kraj_zamieszkania=kraj_zamieszkania_n and miasto=miasto_n and ulica=ulica_n and numer_domu=numer_domu_n and numer_lokalu=numer_lokalu_n and kod_pocztowy=kod_pocztowy_n) and 
			imie=imie_n and pesel=pesel_n and nazwisko=nazwisko_n and data_urodzenia=data_urodzenia_n and 
			plec=plec_n and numer_kontaktowy=numer_kontaktowy_n));
            set identyfikator = (select id_administratora from `administrator` where id_dane=identyfikator_danych);
            set imie_email = (select imie from `dane_osobowe_i_kontaktowe` where id_dane=identyfikator_danych);
            set nazwisko_email = (select nazwisko from `dane_osobowe_i_kontaktowe` where id_dane=identyfikator_danych);
            update `administrator`
			set email = concat(imie_email, '.', nazwisko_email, '@po.edu.pl')
			where id_administratora = identyfikator;
		end if;
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure generuj_dane_testowe
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `generuj_dane_testowe`(ilosc int)
BEGIN
	DECLARE counter INT DEFAULT 1;
	DECLARE pesel varchar(11);
	DECLARE imie varchar(20);
	DECLARE nazwisko varchar(45);
	DECLARE numer_kontaktowy varchar(9);
	DECLARE kraj_zamieszkania varchar(30);
	DECLARE miasto varchar(30);
	DECLARE ulica varchar(30);
	DECLARE numer_domu varchar(10);
	DECLARE numer_lokalu varchar(10);
	DECLARE kod_pocztowy varchar(5);
	DECLARE login varchar(15);
	DECLARE haslo varchar(15);
		
	insert into `uzytkownicy`(`login`,`haslo`,`rola`) values ('admin', 'admin', 'a');

	call dodaj_kierunek('Automatyka', 1, 2, 1);
		
	WHILE counter <= ilosc DO
			SET pesel = concat('P', (select cast(counter as char(11))));
			SET imie = concat('I', (select cast(counter as char(20))));
			SET nazwisko = concat('N', (select cast(counter as char(45))));
			SET numer_kontaktowy = concat('NK', (select cast(counter as char(9))));
			SET kraj_zamieszkania = concat('KZ', (select cast(counter as char(30))));
			SET miasto = concat('M', (select cast(counter as char(30))));
			SET ulica = concat('U', (select cast(counter as char(30))));
			SET numer_domu = concat('ND', (select cast(counter as char(10))));
			SET numer_lokalu = concat('NL', (select cast(counter as char(10))));
			SET kod_pocztowy = concat('K', (select cast(counter as char(5))));
			SET login = concat('L', (select cast(counter as char(15))));
			SET haslo = concat('H', (select cast(counter as char(15))));
            
			call dodaj_uzytkownika(pesel, imie, nazwisko, '2000-01-01', 'm', 
							numer_kontaktowy, kraj_zamieszkania, miasto, ulica, numer_domu, numer_lokalu, kod_pocztowy, login, 
                            haslo, 's', 'Automatyka', 1, 2, 1);
			SET counter = counter + 1;
	END WHILE;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure odrzuc_reklamacje
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `odrzuc_reklamacje`(indeks_studenta mediumint, grupa smallint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if ((rola_uzytkownika='a')or(rola_uzytkownika='p')) then
		update `zapis`
		set status_oceny = 'o'
		where numer_indeksu = indeks_studenta and id_grupy = grupa;
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure popraw_ocene
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `popraw_ocene`(nowa_ocena float, indeks_studenta mediumint, grupa smallint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if ((rola_uzytkownika='a')or(rola_uzytkownika='p')) then
		update `zapis`
		set status_oceny = 'p', 
		ocena = nowa_ocena
		where numer_indeksu = indeks_studenta and id_grupy = grupa;
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure reklamuj_ocene
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `reklamuj_ocene`(zapis smallint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if ((rola_uzytkownika='a')or(rola_uzytkownika='s')) then
		update zapis
		set status_oceny = 'n'
		where id_zapisu = zapis;
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure sprawdz_uzytkownika
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sprawdz_uzytkownika`(login_n varchar(15), haslo_n varchar(15))
BEGIN
	SELECT * FROM uzytkownicy WHERE login = login_n  AND haslo = haslo_n;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure usun_uzytkownika
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usun_uzytkownika`(id_uzytkownika_usun smallint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    declare rola_uzytkownika_usun char(1);
    declare mynumber smallint;
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    set rola_uzytkownika_usun = (select rola from `uzytkownicy` where id_uzytkownika = id_uzytkownika_usun);
    
    set mynumber = (select count(id_dane_adresowe) from `dane_osobowe_i_kontaktowe` where id_dane_adresowe=(select id_dane_adresowe from `dane_osobowe_i_kontaktowe` where id_uzytkownika=id_uzytkownika_usun));
    
    if ((rola_uzytkownika='a') or (rola_uzytkownika='d' and rola_uzytkownika_usun='s')) then
		if mynumber=1 then
			delete from `dane_adresowe` where id_dane_adresowe=(select id_dane_adresowe from `dane_osobowe_i_kontaktowe` where id_uzytkownika = id_uzytkownika_usun);
		end if;
        delete from `uzytkownicy` where id_uzytkownika = id_uzytkownika_usun;
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wprowadz_ocene
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wprowadz_ocene`(nowa_ocena float, indeks_studenta mediumint, grupa smallint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if ((rola_uzytkownika='a')or(rola_uzytkownika='p')) then
		update `zapis`
		set ocena = nowa_ocena,
        status_oceny = 'w'
		where numer_indeksu = indeks_studenta and id_grupy = grupa;
        end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wyswietl_dane_studenta
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wyswietl_dane_studenta`(id_uzytkownika_n SMALLINT)
BEGIN
	SELECT nazwa_kierunku, stopien, semestr, numer_indeksu FROM dane_studenta_view WHERE id_uzytkownika = id_uzytkownika_n;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wyswietl_dane_studenta_podglad
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wyswietl_dane_studenta_podglad`(numer_indeksu_n MEDIUMINT)
BEGIN
	SELECT numer_indeksu, pesel, imie, nazwisko, data_urodzenia, plec, numer_kontaktowy, kraj_zamieszkania, miasto, ulica, numer_domu, numer_lokalu, kod_pocztowy, nazwa_kierunku, semestr, stopien FROM dane_studenta_podglad_view WHERE numer_indeksu = numer_indeksu_n;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wyswietl_dane_uzytkownika
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wyswietl_dane_uzytkownika`(id_uzytkownika_n SMALLINT)
BEGIN
	SELECT imie, nazwisko, pesel, data_urodzenia, plec, numer_kontaktowy, kraj_zamieszkania, miasto, ulica, numer_domu, numer_lokalu, kod_pocztowy FROM dane_uzytkownika_view WHERE id_uzytkownika = id_uzytkownika_n;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wyswietl_dane_uzytkownika_podglad
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wyswietl_dane_uzytkownika_podglad`(id_uzytkownika_n SMALLINT)
BEGIN
	SELECT pesel, imie, nazwisko, data_urodzenia, plec, numer_kontaktowy, kraj_zamieszkania, miasto, ulica, numer_domu, numer_lokalu, kod_pocztowy, login, haslo, rola FROM dane_uzytkownika_podglad_view WHERE id_uzytkownika = id_uzytkownika_n;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wyswietl_grupy_kursu_prowadzacego
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wyswietl_grupy_kursu_prowadzacego`(id_uzytkownika_n SMALLINT, id_kursu_n SMALLINT)
BEGIN
	SELECT id_grupy, budynek, sala, dzien_tygodnia, typ_zajec, godzina_rozpoczecia, godzina_zakonczenia FROM grupy_kursu_prowadzacego_view WHERE id_uzytkownika = id_uzytkownika_n AND id_kursu = id_kursu_n;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wyswietl_oceny_studenta
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wyswietl_oceny_studenta`(id_uzytkownika_n SMALLINT)
BEGIN
	SELECT id_zapisu, imie, nazwisko, nazwa_kursu, ects, grupa_zajeciowa, ocena, status_oceny FROM oceny_studenta_view WHERE id_dane = (SELECT id_dane FROM dane_osobowe_i_kontaktowe WHERE id_uzytkownika = id_uzytkownika_n);
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wyswietl_plan_zajec_studenta
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wyswietl_plan_zajec_studenta`(id_uzytkownika_n SMALLINT)
BEGIN
	SELECT imie, nazwisko, nazwa_kursu, id_grupy, ects, budynek, sala, dzien_tygodnia, typ_zajec, godzina_rozpoczecia, godzina_zakonczenia FROM plan_zajec_studenta_view WHERE id_dane = (SELECT id_dane FROM dane_osobowe_i_kontaktowe WHERE id_uzytkownika = id_uzytkownika_n);
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wyswietl_prowadzone_kursy_prowadzacego
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wyswietl_prowadzone_kursy_prowadzacego`(id_uzytkownika_n SMALLINT)
BEGIN
	SELECT id_kursu, nazwa_kursu, ects FROM prowadzone_kursy_prowadzacego_view WHERE id_uzytkownika = id_uzytkownika_n;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure wyswietl_studentow_prowadzacego
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `wyswietl_studentow_prowadzacego`(id_uzytkownika_n SMALLINT)
BEGIN
	SELECT numer_indeksu, nazwa_kursu, id_grupy, typ_zajec, ocena, status_oceny FROM studenci_prowadzacego_view WHERE id_uzytkownika = id_uzytkownika_n;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zapisz_studenta
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zapisz_studenta`(student mediumint, grupa smallint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if ((rola_uzytkownika='a') or (rola_uzytkownika='d')) then
		insert into `zapis`(`numer_indeksu`, `id_grupy`) values (student, grupa);
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zatwierdz_ocene
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zatwierdz_ocene`(zapis smallint, biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if ((rola_uzytkownika='a')or(rola_uzytkownika='s')) then
		update zapis
		set status_oceny = 't'
		where id_zapisu = zapis;
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_imie
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_imie`(imie_n varchar(20), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych smallint;
    set id_danych = (select id_dane from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_osobowe_i_kontaktowe`
		set imie = imie_n
		where id_dane = id_danych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_kierunek_semestr_stopien
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_kierunek_semestr_stopien`(indeks mediumint, kierunek_n varchar(30), semestr_n tinyint, stopien_n tinyint, biezacy_uzytkownik smallint)
BEGIN
	declare id_nowego_kierunku tinyint;
    declare rola_uzytkownika char(1);
    
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
	
	if (rola_uzytkownika='d') then
		set id_nowego_kierunku = (select id_kierunku from `kierunek` where nazwa_kierunku=kierunek_n and semestr=semestr_n and stopien=stopien_n);
		update `student`
		set id_kierunku=id_nowego_kierunku
		where numer_indeksu = indeks;
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_kod_pocztowy
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_kod_pocztowy`(kod_pocztowy_n varchar(10), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych_adresowych smallint;
    set id_danych_adresowych = (select id_dane_adresowe from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_adresowe`
		set kod_pocztowy = kod_pocztowy_n
		where id_dane_adresowe = id_danych_adresowych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_kraj_zamieszkania
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_kraj_zamieszkania`(kraj_zamieszkania_n varchar(30), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych_adresowych smallint;
    set id_danych_adresowych = (select id_dane_adresowe from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_adresowe`
		set kraj_zamieszkania = kraj_zamieszkania_n
		where id_dane_adresowe = id_danych_adresowych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_login_haslo
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_login_haslo`(uzytkownik_n smallint, login_n varchar(15), haslo_n varchar(15), biezacy_uzytkownik smallint)
BEGIN
	declare rola_uzytkownika char(1);
	set rola_uzytkownika = (select rola from `uzytkownicy` where id_uzytkownika = biezacy_uzytkownik);
    
    if (rola_uzytkownika='a') then
		update `uzytkownicy`
			set login =login_n,
                haslo=haslo_n
			where id_uzytkownika = uzytkownik_n;
	end if;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_miasto
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_miasto`(miasto_n varchar(30), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych_adresowych smallint;
    set id_danych_adresowych = (select id_dane_adresowe from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_adresowe`
		set miasto = miasto_n
		where id_dane_adresowe = id_danych_adresowych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_nazwisko
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_nazwisko`(nazwisko_n varchar(45), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych smallint;
    set id_danych = (select id_dane from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_osobowe_i_kontaktowe`
		set nazwisko = nazwisko_n
		where id_dane = id_danych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_numer_domu
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_numer_domu`(numer_domu_n varchar(10), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych_adresowych smallint;
    set id_danych_adresowych = (select id_dane_adresowe from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_adresowe`
		set numer_domu = numer_domu_n
		where id_dane_adresowe = id_danych_adresowych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_numer_kontaktowy
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_numer_kontaktowy`(numer_kontaktowy_n varchar(9), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych smallint;
    set id_danych = (select id_dane from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_osobowe_i_kontaktowe`
		set numer_kontaktowy = numer_kontaktowy_n
		where id_dane = id_danych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_numer_lokalu
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_numer_lokalu`(numer_lokalu_n varchar(10), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych_adresowych smallint;
    set id_danych_adresowych = (select id_dane_adresowe from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_adresowe`
		set numer_lokalu = numer_lokalu_n
		where id_dane_adresowe = id_danych_adresowych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_plec
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_plec`(plec_n char(1), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych smallint;
    set id_danych = (select id_dane from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_osobowe_i_kontaktowe`
		set plec = plec_n
		where id_dane = id_danych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure zmien_ulice
-- -----------------------------------------------------

DELIMITER $$
USE `elektroniczny_indeks`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `zmien_ulice`(ulica_n varchar(30), biezacy_uzytkownik smallint)
BEGIN
	declare id_danych_adresowych smallint;
    set id_danych_adresowych = (select id_dane_adresowe from `dane_osobowe_i_kontaktowe` where id_uzytkownika=biezacy_uzytkownik);
	update `dane_adresowe`
		set ulica = ulica_n
		where id_dane_adresowe = id_danych_adresowych;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`dane_studenta_podglad_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`dane_studenta_podglad_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`dane_studenta_podglad_view` AS select `elektroniczny_indeks`.`student`.`numer_indeksu` AS `numer_indeksu`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`pesel` AS `pesel`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`imie` AS `imie`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`nazwisko` AS `nazwisko`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`data_urodzenia` AS `data_urodzenia`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`plec` AS `plec`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`numer_kontaktowy` AS `numer_kontaktowy`,`elektroniczny_indeks`.`dane_adresowe`.`kraj_zamieszkania` AS `kraj_zamieszkania`,`elektroniczny_indeks`.`dane_adresowe`.`miasto` AS `miasto`,`elektroniczny_indeks`.`dane_adresowe`.`ulica` AS `ulica`,`elektroniczny_indeks`.`dane_adresowe`.`numer_domu` AS `numer_domu`,`elektroniczny_indeks`.`dane_adresowe`.`numer_lokalu` AS `numer_lokalu`,`elektroniczny_indeks`.`dane_adresowe`.`kod_pocztowy` AS `kod_pocztowy`,`elektroniczny_indeks`.`kierunek`.`nazwa_kierunku` AS `nazwa_kierunku`,`elektroniczny_indeks`.`kierunek`.`semestr` AS `semestr`,`elektroniczny_indeks`.`kierunek`.`stopien` AS `stopien` from (((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` join `elektroniczny_indeks`.`dane_adresowe`) join `elektroniczny_indeks`.`student`) join `elektroniczny_indeks`.`kierunek`) where ((`elektroniczny_indeks`.`dane_adresowe`.`id_dane_adresowe` = `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane_adresowe`) and (`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane` = `elektroniczny_indeks`.`student`.`id_dane`) and (`elektroniczny_indeks`.`student`.`id_kierunku` = `elektroniczny_indeks`.`kierunek`.`id_kierunku`));

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`dane_studenta_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`dane_studenta_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`dane_studenta_view` AS select `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_uzytkownika` AS `id_uzytkownika`,`elektroniczny_indeks`.`kierunek`.`nazwa_kierunku` AS `nazwa_kierunku`,`elektroniczny_indeks`.`kierunek`.`stopien` AS `stopien`,`elektroniczny_indeks`.`kierunek`.`semestr` AS `semestr`,`elektroniczny_indeks`.`student`.`numer_indeksu` AS `numer_indeksu` from ((`elektroniczny_indeks`.`kierunek` join `elektroniczny_indeks`.`student`) join `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`) where ((`elektroniczny_indeks`.`kierunek`.`id_kierunku` = `elektroniczny_indeks`.`student`.`id_kierunku`) and (`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane` = `elektroniczny_indeks`.`student`.`id_dane`));

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`dane_studentow_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`dane_studentow_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`dane_studentow_view` AS select `elektroniczny_indeks`.`uzytkownicy`.`id_uzytkownika` AS `id_uzytkownika`,`elektroniczny_indeks`.`student`.`numer_indeksu` AS `numer_indeksu`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`imie` AS `imie`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`nazwisko` AS `nazwisko`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`pesel` AS `pesel`,`elektroniczny_indeks`.`kierunek`.`nazwa_kierunku` AS `nazwa_kierunku`,`elektroniczny_indeks`.`kierunek`.`stopien` AS `stopien`,`elektroniczny_indeks`.`kierunek`.`semestr` AS `semestr`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`numer_kontaktowy` AS `numer_kontaktowy` from (((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` join `elektroniczny_indeks`.`student`) join `elektroniczny_indeks`.`kierunek`) join `elektroniczny_indeks`.`uzytkownicy`) where ((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane` = `elektroniczny_indeks`.`student`.`id_dane`) and (`elektroniczny_indeks`.`student`.`id_kierunku` = `elektroniczny_indeks`.`kierunek`.`id_kierunku`) and (`elektroniczny_indeks`.`uzytkownicy`.`id_uzytkownika` = `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_uzytkownika`) and (`elektroniczny_indeks`.`uzytkownicy`.`rola` = 's'));

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`dane_uzytkownika_podglad_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`dane_uzytkownika_podglad_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`dane_uzytkownika_podglad_view` AS select `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_uzytkownika` AS `id_uzytkownika`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`pesel` AS `pesel`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`imie` AS `imie`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`nazwisko` AS `nazwisko`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`data_urodzenia` AS `data_urodzenia`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`plec` AS `plec`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`numer_kontaktowy` AS `numer_kontaktowy`,`elektroniczny_indeks`.`dane_adresowe`.`kraj_zamieszkania` AS `kraj_zamieszkania`,`elektroniczny_indeks`.`dane_adresowe`.`miasto` AS `miasto`,`elektroniczny_indeks`.`dane_adresowe`.`ulica` AS `ulica`,`elektroniczny_indeks`.`dane_adresowe`.`numer_domu` AS `numer_domu`,`elektroniczny_indeks`.`dane_adresowe`.`numer_lokalu` AS `numer_lokalu`,`elektroniczny_indeks`.`dane_adresowe`.`kod_pocztowy` AS `kod_pocztowy`,`elektroniczny_indeks`.`uzytkownicy`.`login` AS `login`,`elektroniczny_indeks`.`uzytkownicy`.`haslo` AS `haslo`,`elektroniczny_indeks`.`uzytkownicy`.`rola` AS `rola` from ((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` join `elektroniczny_indeks`.`dane_adresowe`) join `elektroniczny_indeks`.`uzytkownicy`) where ((`elektroniczny_indeks`.`dane_adresowe`.`id_dane_adresowe` = `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane_adresowe`) and (`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_uzytkownika` = `elektroniczny_indeks`.`uzytkownicy`.`id_uzytkownika`));

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`dane_uzytkownika_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`dane_uzytkownika_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`dane_uzytkownika_view` AS select `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_uzytkownika` AS `id_uzytkownika`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`imie` AS `imie`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`nazwisko` AS `nazwisko`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`pesel` AS `pesel`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`data_urodzenia` AS `data_urodzenia`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`plec` AS `plec`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`numer_kontaktowy` AS `numer_kontaktowy`,`elektroniczny_indeks`.`dane_adresowe`.`kraj_zamieszkania` AS `kraj_zamieszkania`,`elektroniczny_indeks`.`dane_adresowe`.`miasto` AS `miasto`,`elektroniczny_indeks`.`dane_adresowe`.`ulica` AS `ulica`,`elektroniczny_indeks`.`dane_adresowe`.`numer_domu` AS `numer_domu`,`elektroniczny_indeks`.`dane_adresowe`.`numer_lokalu` AS `numer_lokalu`,`elektroniczny_indeks`.`dane_adresowe`.`kod_pocztowy` AS `kod_pocztowy` from (`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` join `elektroniczny_indeks`.`dane_adresowe`) where (`elektroniczny_indeks`.`dane_adresowe`.`id_dane_adresowe` = `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane_adresowe`);

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`grupy_kursu_prowadzacego_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`grupy_kursu_prowadzacego_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`grupy_kursu_prowadzacego_view` AS select `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_uzytkownika` AS `id_uzytkownika`,`elektroniczny_indeks`.`grupa_zajeciowa`.`id_kursu` AS `id_kursu`,`elektroniczny_indeks`.`grupa_zajeciowa`.`id_grupy` AS `id_grupy`,`elektroniczny_indeks`.`grupa_zajeciowa`.`budynek` AS `budynek`,`elektroniczny_indeks`.`grupa_zajeciowa`.`sala` AS `sala`,`elektroniczny_indeks`.`grupa_zajeciowa`.`dzien_tygodnia` AS `dzien_tygodnia`,`elektroniczny_indeks`.`grupa_zajeciowa`.`typ_zajec` AS `typ_zajec`,`elektroniczny_indeks`.`grupa_zajeciowa`.`godzina_rozpoczecia` AS `godzina_rozpoczecia`,`elektroniczny_indeks`.`grupa_zajeciowa`.`godzina_zakonczenia` AS `godzina_zakonczenia` from (((`elektroniczny_indeks`.`grupa_zajeciowa` join `elektroniczny_indeks`.`kurs`) join `elektroniczny_indeks`.`prowadzacy`) join `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`) where ((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane` = `elektroniczny_indeks`.`prowadzacy`.`id_dane`) and (`elektroniczny_indeks`.`prowadzacy`.`id_prowadzacego` = `elektroniczny_indeks`.`kurs`.`id_prowadzacego`) and (`elektroniczny_indeks`.`kurs`.`id_kursu` = `elektroniczny_indeks`.`grupa_zajeciowa`.`id_kursu`));

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`lista_uzytkownikow_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`lista_uzytkownikow_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`lista_uzytkownikow_view` AS select `elektroniczny_indeks`.`uzytkownicy`.`id_uzytkownika` AS `id_uzytkownika`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`imie` AS `imie`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`nazwisko` AS `nazwisko`,`elektroniczny_indeks`.`uzytkownicy`.`rola` AS `rola` from (`elektroniczny_indeks`.`uzytkownicy` join `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`) where (`elektroniczny_indeks`.`uzytkownicy`.`id_uzytkownika` = `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_uzytkownika`);

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`oceny_studenta_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`oceny_studenta_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`oceny_studenta_view` AS select `elektroniczny_indeks`.`zapis`.`id_zapisu` AS `id_zapisu`,`elektroniczny_indeks`.`student`.`id_dane` AS `id_dane`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`imie` AS `imie`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`nazwisko` AS `nazwisko`,`elektroniczny_indeks`.`kurs`.`nazwa_kursu` AS `nazwa_kursu`,`elektroniczny_indeks`.`kurs`.`ects` AS `ects`,`elektroniczny_indeks`.`grupa_zajeciowa`.`id_grupy` AS `grupa_zajeciowa`,`elektroniczny_indeks`.`zapis`.`ocena` AS `ocena`,`elektroniczny_indeks`.`zapis`.`status_oceny` AS `status_oceny` from (((((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` join `elektroniczny_indeks`.`prowadzacy`) join `elektroniczny_indeks`.`kurs`) join `elektroniczny_indeks`.`grupa_zajeciowa`) join `elektroniczny_indeks`.`zapis`) join `elektroniczny_indeks`.`student`) where ((`elektroniczny_indeks`.`student`.`numer_indeksu` = `elektroniczny_indeks`.`zapis`.`numer_indeksu`) and (`elektroniczny_indeks`.`grupa_zajeciowa`.`id_grupy` = `elektroniczny_indeks`.`zapis`.`id_grupy`) and (`elektroniczny_indeks`.`kurs`.`id_kursu` = `elektroniczny_indeks`.`grupa_zajeciowa`.`id_kursu`) and (`elektroniczny_indeks`.`prowadzacy`.`id_prowadzacego` = `elektroniczny_indeks`.`kurs`.`id_prowadzacego`) and (`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane` = `elektroniczny_indeks`.`prowadzacy`.`id_dane`));

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`plan_zajec_studenta_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`plan_zajec_studenta_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`plan_zajec_studenta_view` AS select `elektroniczny_indeks`.`student`.`id_dane` AS `id_dane`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`imie` AS `imie`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`nazwisko` AS `nazwisko`,`elektroniczny_indeks`.`kurs`.`nazwa_kursu` AS `nazwa_kursu`,`elektroniczny_indeks`.`kurs`.`ects` AS `ects`,`elektroniczny_indeks`.`grupa_zajeciowa`.`id_grupy` AS `id_grupy`,`elektroniczny_indeks`.`grupa_zajeciowa`.`budynek` AS `budynek`,`elektroniczny_indeks`.`grupa_zajeciowa`.`sala` AS `sala`,`elektroniczny_indeks`.`grupa_zajeciowa`.`dzien_tygodnia` AS `dzien_tygodnia`,`elektroniczny_indeks`.`grupa_zajeciowa`.`typ_zajec` AS `typ_zajec`,`elektroniczny_indeks`.`grupa_zajeciowa`.`godzina_rozpoczecia` AS `godzina_rozpoczecia`,`elektroniczny_indeks`.`grupa_zajeciowa`.`godzina_zakonczenia` AS `godzina_zakonczenia` from (((((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` join `elektroniczny_indeks`.`prowadzacy`) join `elektroniczny_indeks`.`kurs`) join `elektroniczny_indeks`.`grupa_zajeciowa`) join `elektroniczny_indeks`.`zapis`) join `elektroniczny_indeks`.`student`) where ((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane` = `elektroniczny_indeks`.`prowadzacy`.`id_dane`) and (`elektroniczny_indeks`.`prowadzacy`.`id_prowadzacego` = `elektroniczny_indeks`.`kurs`.`id_prowadzacego`) and (`elektroniczny_indeks`.`kurs`.`id_kursu` = `elektroniczny_indeks`.`grupa_zajeciowa`.`id_kursu`) and (`elektroniczny_indeks`.`zapis`.`id_grupy` = `elektroniczny_indeks`.`grupa_zajeciowa`.`id_grupy`) and (`elektroniczny_indeks`.`student`.`numer_indeksu` = `elektroniczny_indeks`.`zapis`.`numer_indeksu`));

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`prowadzone_kursy_prowadzacego_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`prowadzone_kursy_prowadzacego_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`prowadzone_kursy_prowadzacego_view` AS select `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_uzytkownika` AS `id_uzytkownika`,`elektroniczny_indeks`.`kurs`.`id_kursu` AS `id_kursu`,`elektroniczny_indeks`.`kurs`.`nazwa_kursu` AS `nazwa_kursu`,`elektroniczny_indeks`.`kurs`.`ects` AS `ects` from ((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` join `elektroniczny_indeks`.`prowadzacy`) join `elektroniczny_indeks`.`kurs`) where ((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane` = `elektroniczny_indeks`.`prowadzacy`.`id_dane`) and (`elektroniczny_indeks`.`prowadzacy`.`id_prowadzacego` = `elektroniczny_indeks`.`kurs`.`id_prowadzacego`));

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`przegladanie_grup_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`przegladanie_grup_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`przegladanie_grup_view` AS select `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`imie` AS `imie`,`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`nazwisko` AS `nazwisko`,`elektroniczny_indeks`.`kurs`.`nazwa_kursu` AS `nazwa_kursu`,`elektroniczny_indeks`.`kurs`.`ects` AS `ects`,`elektroniczny_indeks`.`grupa_zajeciowa`.`id_grupy` AS `id_grupy`,`elektroniczny_indeks`.`grupa_zajeciowa`.`budynek` AS `budynek`,`elektroniczny_indeks`.`grupa_zajeciowa`.`sala` AS `sala`,`elektroniczny_indeks`.`grupa_zajeciowa`.`dzien_tygodnia` AS `dzien_tygodnia`,`elektroniczny_indeks`.`grupa_zajeciowa`.`typ_zajec` AS `typ_zajec`,`elektroniczny_indeks`.`grupa_zajeciowa`.`godzina_rozpoczecia` AS `godzina_rozpoczecia`,`elektroniczny_indeks`.`grupa_zajeciowa`.`godzina_zakonczenia` AS `godzina_zakonczenia` from ((((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` join `elektroniczny_indeks`.`prowadzacy`) join `elektroniczny_indeks`.`kurs`) join `elektroniczny_indeks`.`grupa_zajeciowa`) join `elektroniczny_indeks`.`zapis`) where ((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane` = `elektroniczny_indeks`.`prowadzacy`.`id_dane`) and (`elektroniczny_indeks`.`prowadzacy`.`id_prowadzacego` = `elektroniczny_indeks`.`kurs`.`id_prowadzacego`) and (`elektroniczny_indeks`.`kurs`.`id_kursu` = `elektroniczny_indeks`.`grupa_zajeciowa`.`id_kursu`) and (`elektroniczny_indeks`.`zapis`.`id_grupy` = `elektroniczny_indeks`.`grupa_zajeciowa`.`id_grupy`));

-- -----------------------------------------------------
-- View `elektroniczny_indeks`.`studenci_prowadzacego_view`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `elektroniczny_indeks`.`studenci_prowadzacego_view`;
USE `elektroniczny_indeks`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `elektroniczny_indeks`.`studenci_prowadzacego_view` AS select `elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_uzytkownika` AS `id_uzytkownika`,`elektroniczny_indeks`.`student`.`numer_indeksu` AS `numer_indeksu`,`elektroniczny_indeks`.`student`.`id_dane` AS `id_dane`,`elektroniczny_indeks`.`kurs`.`nazwa_kursu` AS `nazwa_kursu`,`elektroniczny_indeks`.`grupa_zajeciowa`.`id_grupy` AS `id_grupy`,`elektroniczny_indeks`.`grupa_zajeciowa`.`typ_zajec` AS `typ_zajec`,`elektroniczny_indeks`.`zapis`.`ocena` AS `ocena`,`elektroniczny_indeks`.`zapis`.`status_oceny` AS `status_oceny` from (((((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe` join `elektroniczny_indeks`.`prowadzacy`) join `elektroniczny_indeks`.`kurs`) join `elektroniczny_indeks`.`grupa_zajeciowa`) join `elektroniczny_indeks`.`zapis`) join `elektroniczny_indeks`.`student`) where ((`elektroniczny_indeks`.`dane_osobowe_i_kontaktowe`.`id_dane` = `elektroniczny_indeks`.`prowadzacy`.`id_dane`) and (`elektroniczny_indeks`.`prowadzacy`.`id_prowadzacego` = `elektroniczny_indeks`.`kurs`.`id_prowadzacego`) and (`elektroniczny_indeks`.`kurs`.`id_kursu` = `elektroniczny_indeks`.`grupa_zajeciowa`.`id_kursu`) and (`elektroniczny_indeks`.`zapis`.`id_grupy` = `elektroniczny_indeks`.`grupa_zajeciowa`.`id_grupy`) and (zapis.`numer_indeksu` = student.`numer_indeksu`));

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
