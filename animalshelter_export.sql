-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1:3306
-- Létrehozás ideje: 2026. Nov 25. 09:08
-- Kiszolgáló verziója: 8.4.7
-- PHP verzió: 8.3.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `animalshelter`
--
CREATE DATABASE IF NOT EXISTS `animalshelter` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `animalshelter`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `animals`
--

DROP TABLE IF EXISTS `animals`;
CREATE TABLE IF NOT EXISTS `animals` (
  `Id` char(36) NOT NULL,
  `Name` varchar(40) NOT NULL,
  `Species` varchar(30) NOT NULL,
  `Age` int NOT NULL,
  `Weight` double NOT NULL,
  `RegTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `ShelterId` char(36) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `ShelterId` (`ShelterId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `animals`
--

INSERT INTO `animals` (`Id`, `Name`, `Species`, `Age`, `Weight`, `RegTime`, `ShelterId`) VALUES
('a2691474-e139-594f-a782-443415ee7d11', 'AllatEgy', 'Kutya', 2, 12, '2026-04-10 08:46:25', 'dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64'),
('a2691474-e139-594f-a782-443415ee7d12', 'AllatKetto', 'Kutya', 5, 17, '2026-08-05 08:04:01', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('a2691474-e139-594f-a782-443415ee7d13', 'AllatHarom', 'Macska', 1, 8, '2026-07-18 11:51:48', 'dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64'),
('a2691474-e139-594f-a782-443415ee7d14', 'AllatNegy', 'Kutya', 2, 20, '2026-01-08 15:34:23', 'a2691474-e139-594f-a782-443415ee7d41'),
('a2691474-e139-594f-a782-443415ee7d15', 'AllatOt', 'Medve', 83, 638, '2026-05-25 09:52:06', 'c41845d3-62ac-5c31-9b16-80193b82aec0');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `shelters`
--

DROP TABLE IF EXISTS `shelters`;
CREATE TABLE IF NOT EXISTS `shelters` (
  `Id` char(36) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `City` varchar(40) NOT NULL,
  `Capacity` int NOT NULL,
  `RegTime` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `shelters`
--

INSERT INTO `shelters` (`Id`, `Name`, `City`, `Capacity`, `RegTime`) VALUES
('8d32af8e-bca2-5907-997f-05ff3b325fe7', 'Gazdira Vár', 'Debrecen', 25, '2026-08-05 08:04:01'),
('95f1187f-f2c2-54b7-afc1-3dba453580ab', 'Cirmos Menedék', 'Eger', 15, '2026-07-18 11:51:48'),
('a2691474-e139-594f-a782-443415ee7d41', 'Boldog Mancs', 'Szeged', 40, '2026-01-08 15:34:23'),
('c41845d3-62ac-5c31-9b16-80193b82aec0', 'Kis Menedék', 'Salgótarján', 3, '2026-05-25 09:52:06'),
('dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64', 'Négylábú Otthon', 'Miskolc', 30, '2026-04-10 08:46:25');

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `animals`
--
ALTER TABLE `animals`
  ADD CONSTRAINT `animals_ibfk_1` FOREIGN KEY (`ShelterId`) REFERENCES `shelters` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
