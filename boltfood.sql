-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 19, 2026 at 09:02 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `boltfood`
--

-- --------------------------------------------------------

--
-- Table structure for table `krepselioirasas`
--

DROP TABLE IF EXISTS `krepselioirasas`;
CREATE TABLE `krepselioirasas` (
  `Kaina` float NOT NULL,
  `Kiekis` int(11) NOT NULL,
  `id` int(11) NOT NULL,
  `fk_Patiekalasid` int(11) DEFAULT NULL,
  `fk_Patiekalasfk_Restoranasid` int(11) DEFAULT NULL,
  `fk_Krepselisid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `krepselis`
--

DROP TABLE IF EXISTS `krepselis`;
CREATE TABLE `krepselis` (
  `KoordinateX` float NOT NULL,
  `KoordinateY` float NOT NULL,
  `PristatymoAdresas` varchar(255) NOT NULL,
  `Kaina` float NOT NULL,
  `id` int(11) NOT NULL,
  `fk_Vartotojasid` int(11) NOT NULL,
  `fk_Restoranasid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `kurjeris`
--

DROP TABLE IF EXISTS `kurjeris`;
CREATE TABLE `kurjeris` (
  `KoordinateX` float NOT NULL,
  `KoordinateY` float NOT NULL,
  `MaxAtstumas` float NOT NULL,
  `CentroAdresas` varchar(255) NOT NULL,
  `SavaitesPradziosVal` varchar(255) NOT NULL,
  `SavaitesPabaigosVal` varchar(255) NOT NULL,
  `TransportoTipas` int(11) NOT NULL,
  `id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `kurjeris`
--

INSERT INTO `kurjeris` (`KoordinateX`, `KoordinateY`, `MaxAtstumas`, `CentroAdresas`, `SavaitesPradziosVal`, `SavaitesPabaigosVal`, `TransportoTipas`, `id`) VALUES
(54.9145, 23.957, 35, 'Savanorių pr. 321, Kaunas', '0808080808080808----', '2000200020002000----', 1, 2),
(54.9001, 23.9402, 28, 'Taikos pr. 45, Kaunas', '0909090909090909----', '2100210021002100----', 2, 3);

-- --------------------------------------------------------

--
-- Table structure for table `pasirenka`
--

DROP TABLE IF EXISTS `pasirenka`;
CREATE TABLE `pasirenka` (
  `fk_KrepselioIrasasid` int(11) NOT NULL,
  `fk_PasirinkimoVariantasid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `pasirinkimokategorija`
--

DROP TABLE IF EXISTS `pasirinkimokategorija`;
CREATE TABLE `pasirinkimokategorija` (
  `Pavadinimas` varchar(255) NOT NULL,
  `ArKeliPasirinkimai` tinyint(1) NOT NULL DEFAULT 0,
  `ArPrivalomas` tinyint(1) NOT NULL DEFAULT 0,
  `id` int(11) NOT NULL,
  `fk_Patiekalasid` int(11) NOT NULL,
  `fk_Patiekalasfk_Restoranasid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `pasirinkimokategorija`
--

INSERT INTO `pasirinkimokategorija` (`Pavadinimas`, `ArKeliPasirinkimai`, `ArPrivalomas`, `id`, `fk_Patiekalasid`, `fk_Patiekalasfk_Restoranasid`) VALUES
('Dydis', 0, 1, 1, 2, 1),
('Padažas', 0, 1, 2, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `pasirinkimovariantas`
--

DROP TABLE IF EXISTS `pasirinkimovariantas`;
CREATE TABLE `pasirinkimovariantas` (
  `Pavadinimas` varchar(255) NOT NULL,
  `KainosPokytis` float NOT NULL,
  `id` int(11) NOT NULL,
  `fk_ChoiceCatid` int(11) NOT NULL,
  `fk_ChoiceCatfk_Patiekalasid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `pasirinkimovariantas`
--

INSERT INTO `pasirinkimovariantas` (`Pavadinimas`, `KainosPokytis`, `id`, `fk_ChoiceCatid`, `fk_ChoiceCatfk_Patiekalasid`) VALUES
('Mažas', 0, 1, 1, 2),
('Vidutinis', 1, 2, 1, 2),
('Didelis', 2, 3, 1, 2),
('BBQ', 0, 4, 2, 2),
('Aštrus', 0.5, 5, 2, 2),
('Česnakinis', 0.5, 6, 2, 2);

-- --------------------------------------------------------

--
-- Table structure for table `patiekalas`
--

DROP TABLE IF EXISTS `patiekalas`;
CREATE TABLE `patiekalas` (
  `Aprasas` varchar(255) DEFAULT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Kaina` float NOT NULL DEFAULT 0,
  `ArParduodamas` tinyint(1) NOT NULL DEFAULT 0,
  `ArIstrintas` tinyint(1) NOT NULL DEFAULT 0,
  `id` int(11) NOT NULL,
  `fk_Restoranasid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `patiekalas`
--

INSERT INTO `patiekalas` (`Aprasas`, `Pavadinimas`, `Kaina`, `ArParduodamas`, `ArIstrintas`, `id`, `fk_Restoranasid`) VALUES
('Bulvių košė su krapais, 300 gramų', 'Bulvių košė su krapais', 6.7, 1, 0, 1, 1),
('Sultingas jautienos burgeris su BBQ padažu', 'BBQ Burger', 9.5, 1, 0, 2, 1),
('Klasikinis burgeris su dvigubu sūriu', 'Cheese Burger', 8.9, 1, 0, 3, 1),
('Ant grotelių kepta vištiena', 'Vištienos kepsnys', 10.2, 1, 0, 4, 1),
('Keptos daržovės', 'Grill daržovės', 6, 1, 0, 5, 1),
('Brandintos jautienos kepsnys', 'Steikas', 18.5, 1, 0, 6, 1),
('Tortilija su vištiena', 'Tortilija su vištiena', 7.8, 1, 0, 7, 1),
('Aštri jautienos tortilija', 'Tortilija su jautiena', 8.2, 1, 0, 8, 1),
('Traškios bulvytės fri', 'Bulvytės', 3, 1, 0, 9, 1),
('Klasikinės salotos su vištiena', 'Cezario salotos', 7.5, 1, 0, 10, 1),
('Tradiciniai lietuviški šaltibarščiai', 'Šaltibarščiai', 4.2, 1, 0, 11, 1),
('Bulvių košė su krapais, 300 gramų', 'Bulvių košė su krapais', 6.7, 1, 0, 12, 2),
('Bulvių košė su krapais, 300 gramų', 'Bulvių košė su krapais', 6.7, 1, 0, 13, 3),
('Brandintos jautienos kepsnys', 'Steikas', 18.5, 1, 0, 14, 3),
('Traškus tempura suktinis su krevetėmis', 'Shrimp Tempura Roll', 11, 1, 0, 15, 5),
('Skanūs, sultingi suktinukai pagal slaptą Pijaus receptą', 'Pijaus firminiai suktinukai', 5.13, 1, 0, 16, 4),
('Skanios, sultingos bandelės pagal slaptą Pijaus receptą', 'Pijaus firminės bandelės', 2.5, 1, 0, 17, 4),
('Karolio firminiai koldūnai', 'Karolio koldūnai', 3.5, 1, 0, 18, 2),
('Skani, didelė pica pagal firminį Pijaus receptą', 'Pijaus pica', 8.8, 1, 0, 19, 6),
('Sultingas jautienos burgeris su BBQ padažu', 'BBQ Burger', 9.5, 1, 0, 20, 2),
('Klasikinis burgeris su dvigubu sūriu', 'Cheese Burger', 8.9, 1, 0, 21, 2),
('Ant grotelių kepta vištiena', 'Vištienos kepsnys', 10.2, 1, 0, 22, 2),
('Keptos daržovės', 'Grill daržovės', 6, 1, 0, 23, 2),
('Brandintos jautienos kepsnys', 'Steikas', 18.5, 1, 0, 24, 2),
('Tortilija su vištiena', 'Tortilija su vištiena', 7.8, 1, 0, 25, 2),
('Aštri jautienos tortilija', 'Tortilija su jautiena', 8.2, 1, 0, 26, 2),
('Traškios bulvytės fri', 'Bulvytės', 3, 1, 0, 27, 2),
('Klasikinės salotos su vištiena', 'Cezario salotos', 7.5, 1, 0, 28, 2),
('Tradiciniai lietuviški šaltibarščiai', 'Šaltibarščiai', 4.2, 1, 0, 29, 2),
('Aštrus BBQ burgeris su jalapeno', 'Spicy BBQ Burger', 10.2, 1, 0, 30, 2),
('Dvigubas jautienos burgeris', 'Double Beef Burger', 11.5, 1, 0, 31, 2),
('Grill dešrelės su garstyčiomis', 'Grill dešrelės', 7.2, 1, 0, 32, 2),
('Keptos bulvės su lupena', 'Bulvės su lupena', 4, 1, 0, 33, 2),
('Grill kukurūzai', 'Kukurūzai ant grotelių', 3.5, 1, 0, 34, 2),
('Vištienos sparneliai BBQ', 'BBQ sparneliai', 8.9, 1, 0, 35, 2),
('Aštrūs vištienos sparneliai', 'Aštrūs sparneliai', 9.2, 1, 0, 36, 2),
('Grill sumuštinis su sūriu', 'Grill sūrio sumuštinis', 5.5, 1, 0, 37, 2),
('Grill sumuštinis su kumpiu', 'Grill kumpio sumuštinis', 6, 1, 0, 38, 2),
('Grill salotos su daržovėmis', 'Grill salotos', 7, 1, 0, 39, 2),
('Sultingas jautienos burgeris su BBQ padažu', 'BBQ Burger', 9.5, 1, 0, 40, 3),
('Klasikinis burgeris su dvigubu sūriu', 'Cheese Burger', 8.9, 1, 0, 41, 3),
('Ant grotelių kepta vištiena', 'Vištienos kepsnys', 10.2, 1, 0, 42, 3),
('Keptos daržovės', 'Grill daržovės', 6, 1, 0, 43, 3),
('Tortilija su vištiena', 'Tortilija su vištiena', 7.8, 1, 0, 45, 3),
('Aštri jautienos tortilija', 'Tortilija su jautiena', 8.2, 1, 0, 46, 3),
('Traškios bulvytės fri', 'Bulvytės', 3, 1, 0, 47, 3),
('Klasikinės salotos su vištiena', 'Cezario salotos', 7.5, 1, 0, 48, 3),
('Tradiciniai lietuviški šaltibarščiai', 'Šaltibarščiai', 4.2, 1, 0, 49, 3),
('Aštrus BBQ burgeris su jalapeno', 'Spicy BBQ Burger', 10.2, 1, 0, 50, 3),
('Dvigubas jautienos burgeris', 'Double Beef Burger', 11.5, 1, 0, 51, 3),
('Grill dešrelės su garstyčiomis', 'Grill dešrelės', 7.2, 1, 0, 52, 3),
('Keptos bulvės su lupena', 'Bulvės su lupena', 4, 1, 0, 53, 3),
('Grill kukurūzai', 'Kukurūzai ant grotelių', 3.5, 1, 0, 54, 3),
('Vištienos sparneliai BBQ', 'BBQ sparneliai', 8.9, 1, 0, 55, 3),
('Aštrūs vištienos sparneliai', 'Aštrūs sparneliai', 9.2, 1, 0, 56, 3),
('Grill sumuštinis su sūriu', 'Grill sūrio sumuštinis', 5.5, 1, 0, 57, 3),
('Grill sumuštinis su kumpiu', 'Grill kumpio sumuštinis', 6, 1, 0, 58, 3),
('Grill salotos su daržovėmis', 'Grill salotos', 7, 1, 0, 59, 3),
('Kepta duona su sūriu', 'Kepta duona', 4.5, 1, 0, 60, 4),
('Traškios sūrio lazdelės', 'Sūrio lazdelės', 5.2, 1, 0, 61, 4),
('Vištienos piršteliai', 'Vištienos piršteliai', 6.8, 1, 0, 62, 4),
('Traškios bulvytės', 'Bulvytės', 3, 1, 0, 63, 4),
('Tortilija su sūriu', 'Tortilija su sūriu', 6.2, 1, 0, 64, 4),
('Tortilija su vištiena', 'Tortilija su vištiena', 7.5, 1, 0, 65, 4),
('Tortilija su jautiena', 'Tortilija su jautiena', 8, 1, 0, 66, 4),
('Cezario salotos', 'Cezario salotos', 7.5, 1, 0, 67, 4),
('Šaltibarščiai', 'Šaltibarščiai', 4.2, 1, 0, 68, 4),
('Kibinai', 'Kibinai', 3.5, 1, 0, 69, 4),
('Aštrūs čili gruzdinti kukurūzai', 'Čili kukurūzai', 4.2, 1, 0, 70, 4),
('Sūrio kamuoliukai su jalapeno', 'Sūrio kamuoliukai', 5.8, 1, 0, 71, 4),
('Vištienos kepsneliai su BBQ', 'BBQ kepsneliai', 7.2, 1, 0, 72, 4),
('Bulvių traškučiai su paprika', 'Bulvių traškučiai', 3.2, 1, 0, 73, 4),
('Sūrio užkepėlė', 'Sūrio užkepėlė', 6.5, 1, 0, 74, 4),
('Tortilija su daržovėmis', 'Daržovių tortilija', 6.8, 1, 0, 75, 4),
('Tortilija su kiauliena', 'Kiaulienos tortilija', 8.5, 1, 0, 76, 4),
('Šviežių daržovių salotos', 'Daržovių salotos', 6, 1, 0, 77, 4),
('Kepta duona su česnaku', 'Česnakinė duona', 4, 1, 0, 78, 4),
('Kibinai su vištiena', 'Kibinai su vištiena', 3.8, 1, 0, 79, 4),
('Philadelphia su lašiša', 'Philadelphia Roll', 9.9, 1, 0, 80, 5),
('California su krabų lazdelėmis', 'California Roll', 8.5, 1, 0, 81, 5),
('Traškus tempura suktinis', 'Tempura Roll', 10.5, 1, 0, 82, 5),
('Lašišos nigiri', 'Sake Nigiri', 4, 1, 0, 83, 5),
('Tunos nigiri', 'Maguro Nigiri', 4.5, 1, 0, 84, 5),
('Tradicinė miso sriuba', 'Miso sriuba', 3, 1, 0, 85, 5),
('Ramen makaronų sriuba', 'Ramen', 8.9, 1, 0, 86, 5),
('Udon makaronai', 'Udon makaronai', 7.8, 1, 0, 87, 5),
('Sushi rinkinys mažas', 'Sushi rinkinys mažas', 12, 1, 0, 88, 5),
('Sushi rinkinys didelis', 'Sushi rinkinys didelis', 22, 1, 0, 89, 5),
('Suktinis su lašiša ir avokadu', 'Salmon Avocado Roll', 9.2, 1, 0, 90, 5),
('Suktinis su tunu ir agurku', 'Tuna Cucumber Roll', 8.8, 1, 0, 91, 5),
('Traškus tempura suktinis su krevetėmis', 'Shrimp Tempura Roll', 11, 1, 0, 92, 5),
('Nigiri su unguriu', 'Unagi Nigiri', 5.2, 1, 0, 93, 5),
('Nigiri su krevetėmis', 'Ebi Nigiri', 4.5, 1, 0, 94, 5),
('Aštri miso sriuba', 'Spicy Miso', 3.5, 1, 0, 95, 5),
('Ramen su jautiena', 'Beef Ramen', 9.8, 1, 0, 96, 5),
('Ramen su vištiena', 'Chicken Ramen', 9.2, 1, 0, 97, 5),
('Sushi rinkinys premium', 'Premium Sushi Set', 28, 1, 0, 98, 5),
('Sushi rinkinys šeimai', 'Family Sushi Set', 35, 1, 0, 99, 5),
('Klasikinė margarita', 'Margarita', 7.5, 1, 0, 100, 6),
('Aštri pepperoni pica', 'Pepperoni', 8.5, 1, 0, 101, 6),
('Pica su kumpiu ir grybais', 'Capricciosa', 9, 1, 0, 102, 6),
('Pica su ananasais', 'Havajų', 8.8, 1, 0, 103, 6),
('Keturių sūrių pica', 'Keturi sūriai', 9.5, 1, 0, 104, 6),
('Vegetariška pica', 'Vegetariška', 8.2, 1, 0, 105, 6),
('BBQ vištienos pica', 'BBQ vištiena', 9.8, 1, 0, 106, 6),
('Aštri pica su jalapeno', 'Aštri pica', 9.2, 1, 0, 107, 6),
('Traškios bulvytės', 'Bulvytės', 3, 1, 0, 108, 6),
('Cezario salotos', 'Cezario salotos', 7.5, 1, 0, 109, 6),
('Aštrus BBQ burgeris su jalapeno', 'Spicy BBQ Burger', 10.2, 1, 0, 110, 1),
('Dvigubas jautienos burgeris', 'Double Beef Burger', 11.5, 1, 0, 111, 1),
('Grill dešrelės su garstyčiomis', 'Grill dešrelės', 7.2, 1, 0, 112, 1),
('Keptos bulvės su lupena', 'Bulvės su lupena', 4, 1, 0, 113, 1),
('Grill kukurūzai', 'Kukurūzai ant grotelių', 3.5, 1, 0, 114, 1),
('Vištienos sparneliai BBQ', 'BBQ sparneliai', 8.9, 1, 0, 115, 1),
('Aštrūs vištienos sparneliai', 'Aštrūs sparneliai', 9.2, 1, 0, 116, 1),
('Grill sumuštinis su sūriu', 'Grill sūrio sumuštinis', 5.5, 1, 0, 117, 1),
('Grill sumuštinis su kumpiu', 'Grill kumpio sumuštinis', 6, 1, 0, 118, 1),
('Grill salotos su daržovėmis', 'Grill salotos', 7, 1, 0, 119, 1);

-- --------------------------------------------------------

--
-- Table structure for table `priklauso`
--

DROP TABLE IF EXISTS `priklauso`;
CREATE TABLE `priklauso` (
  `fk_Vartotojasid` int(11) NOT NULL,
  `fk_Restoranasid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `priklauso`
--

INSERT INTO `priklauso` (`fk_Vartotojasid`, `fk_Restoranasid`) VALUES
(4, 1),
(4, 2),
(4, 3),
(5, 4),
(6, 5),
(7, 6);

-- --------------------------------------------------------

--
-- Table structure for table `restoranas`
--

DROP TABLE IF EXISTS `restoranas`;
CREATE TABLE `restoranas` (
  `KoordinateX` float NOT NULL,
  `KoordinateY` float NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Aprasas` varchar(255) NOT NULL,
  `Adresas` varchar(255) NOT NULL,
  `SavaitesPradziosVal` varchar(255) NOT NULL,
  `SavaitesPabaigosVal` varchar(255) NOT NULL,
  `ArPatvirtintas` tinyint(1) NOT NULL DEFAULT 0,
  `id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `restoranas`
--

INSERT INTO `restoranas` (`KoordinateX`, `KoordinateY`, `Pavadinimas`, `Aprasas`, `Adresas`, `SavaitesPradziosVal`, `SavaitesPabaigosVal`, `ArPatvirtintas`, `id`) VALUES
(54.9141, 23.9512, 'Aistės grilis', 'Grilio patiekalai ir burgeriai', 'Karaliaus Mindaugo pr. 12, Kaunas', '0808080808080808----', '2000200020002000----', 1, 1),
(54.8988, 23.9355, 'Aistės grilis', 'Grilio patiekalai ir burgeriai', 'Kęstučio g. 88, Kaunas', '0808080808080808----', '2000200020002000----', 1, 2),
(54.9277, 23.9781, 'Aistės grilis', 'Grilio patiekalai ir burgeriai', 'Pramonės pr. 4, Kaunas', '0808080808080808----', '2000200020002000----', 1, 3),
(54.9155, 23.9901, 'Skanumynų Namai', 'Užkandžiai ir greitas maistas', 'Šilainių pl. 15, Kaunas', '0909090909090909----', '2100210021002100----', 1, 4),
(54.8866, 23.9122, 'Kauno Sushi Spot', 'Sushi ir Azijos virtuvė', 'Veiverių g. 67, Kaunas', '1000100010001000----', '2200220022002200----', 1, 5),
(54.9033, 23.9655, 'Pica Express', 'Picos ir salotos', 'V. Krėvės pr. 120, Kaunas', '0808080808----0808', '20002000----2000', 1, 6);

-- --------------------------------------------------------

--
-- Table structure for table `statusas`
--

DROP TABLE IF EXISTS `statusas`;
CREATE TABLE `statusas` (
  `id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `statusas`
--

INSERT INTO `statusas` (`id`, `name`) VALUES
(1, 'Nepatvirtintas'),
(2, 'Neapmokėtas'),
(3, 'Pagamintas'),
(4, 'Paimtas'),
(5, 'Užbaigtas'),
(6, 'Atšauktas');

-- --------------------------------------------------------

--
-- Table structure for table `transportas`
--

DROP TABLE IF EXISTS `transportas`;
CREATE TABLE `transportas` (
  `id` int(11) NOT NULL,
  `name` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `transportas`
--

INSERT INTO `transportas` (`id`, `name`) VALUES
(1, 'Automobilis'),
(2, 'Dviratis'),
(3, 'Paspirtukas');

-- --------------------------------------------------------

--
-- Table structure for table `uzsakymas`
--

DROP TABLE IF EXISTS `uzsakymas`;
CREATE TABLE `uzsakymas` (
  `KoordinateX` float NOT NULL,
  `KoordinateY` float NOT NULL,
  `PristatymoAdresas` varchar(255) NOT NULL,
  `PristatymoInstrukcijos` varchar(255) DEFAULT NULL,
  `Data` date NOT NULL,
  `Kaina` float NOT NULL DEFAULT 0,
  `id` int(11) NOT NULL,
  `Statusas` int(11) NOT NULL,
  `fk_Vartotojasid` int(11) NOT NULL,
  `fk_Kurjerisid` int(11) NOT NULL,
  `fk_Restoranasid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `uzsakymoirasas`
--

DROP TABLE IF EXISTS `uzsakymoirasas`;
CREATE TABLE `uzsakymoirasas` (
  `PatiekaloPavadinimas` varchar(255) NOT NULL,
  `PasirinkimuAprasymas` varchar(255) NOT NULL,
  `Kaina` float NOT NULL,
  `Kiekis` int(11) NOT NULL,
  `id` int(11) NOT NULL,
  `fk_Uzsakymasid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

-- --------------------------------------------------------

--
-- Table structure for table `vartotojas`
--

DROP TABLE IF EXISTS `vartotojas`;
CREATE TABLE `vartotojas` (
  `Pastas` varchar(255) NOT NULL,
  `Slaptazodis` varchar(255) NOT NULL,
  `Vardas` varchar(255) NOT NULL,
  `KeiciamasSlaptazodis` tinyint(1) NOT NULL DEFAULT 0,
  `id` int(11) NOT NULL,
  `VartotojoTipas` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `vartotojas`
--

INSERT INTO `vartotojas` (`Pastas`, `Slaptazodis`, `Vardas`, `KeiciamasSlaptazodis`, `id`, `VartotojoTipas`) VALUES
('klientas@pastas.lt', 'klientas', 'klientas', 0, 1, 1),
('kurjeris1@pastas.lt', 'kurjeris', 'kurjeris1', 0, 2, 2),
('kurjeris2@pastas.lt', 'kurjeris', 'kurjeris2', 0, 3, 2),
('rest1@pastas.lt', 'restoranas', 'restoranas1', 0, 4, 3),
('rest2@pastas.lt', 'restoranas', 'restoranas2', 0, 5, 3),
('rest3@pastas.lt', 'restoranas', 'restoranas3', 0, 6, 3),
('rest4@pastas.lt', 'restoranas', 'restoranas4', 0, 7, 3),
('jonas@pastas.lt', 'klientas', 'jonas', 0, 8, 1),
('vardenis@pastas.lt', 'klientas', 'vardenis', 0, 9, 1);

-- --------------------------------------------------------

--
-- Table structure for table `vartotojotipas`
--

DROP TABLE IF EXISTS `vartotojotipas`;
CREATE TABLE `vartotojotipas` (
  `id` int(11) NOT NULL,
  `name` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Dumping data for table `vartotojotipas`
--

INSERT INTO `vartotojotipas` (`id`, `name`) VALUES
(1, 'Klientas'),
(2, 'Kurjeris'),
(3, 'Restoranas'),
(4, 'Restorano darbuotojas'),
(5, 'Administratorius'),
(6, 'Pagalbos centro darbuotojas');

-- --------------------------------------------------------

--
-- Table structure for table `zinute`
--

DROP TABLE IF EXISTS `zinute`;
CREATE TABLE `zinute` (
  `Tekstas` varchar(255) NOT NULL,
  `SiuntimoData` date NOT NULL,
  `id` int(11) NOT NULL,
  `fk_Vartotojasid` int(11) NOT NULL,
  `fk_Vartotojasid1` int(11) NOT NULL,
  `fk_Uzsakymasid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_lithuanian_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `krepselioirasas`
--
ALTER TABLE `krepselioirasas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `priklauso2` (`fk_Patiekalasid`),
  ADD KEY `Yra_Krepselyje` (`fk_Krepselisid`);

--
-- Indexes for table `krepselis`
--
ALTER TABLE `krepselis`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `fk_Vartotojasid` (`fk_Vartotojasid`),
  ADD KEY `renkasi_is` (`fk_Restoranasid`);

--
-- Indexes for table `kurjeris`
--
ALTER TABLE `kurjeris`
  ADD PRIMARY KEY (`id`),
  ADD KEY `TransportoTipas` (`TransportoTipas`);

--
-- Indexes for table `pasirenka`
--
ALTER TABLE `pasirenka`
  ADD PRIMARY KEY (`fk_KrepselioIrasasid`,`fk_PasirinkimoVariantasid`);

--
-- Indexes for table `pasirinkimokategorija`
--
ALTER TABLE `pasirinkimokategorija`
  ADD PRIMARY KEY (`id`),
  ADD KEY `turi3` (`fk_Patiekalasid`);

--
-- Indexes for table `pasirinkimovariantas`
--
ALTER TABLE `pasirinkimovariantas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `turi4` (`fk_ChoiceCatid`);

--
-- Indexes for table `patiekalas`
--
ALTER TABLE `patiekalas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `gamina` (`fk_Restoranasid`);

--
-- Indexes for table `priklauso`
--
ALTER TABLE `priklauso`
  ADD PRIMARY KEY (`fk_Vartotojasid`,`fk_Restoranasid`);

--
-- Indexes for table `restoranas`
--
ALTER TABLE `restoranas`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `statusas`
--
ALTER TABLE `statusas`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `transportas`
--
ALTER TABLE `transportas`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `uzsakymas`
--
ALTER TABLE `uzsakymas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Statusas` (`Statusas`),
  ADD KEY `turi` (`fk_Vartotojasid`),
  ADD KEY `veza` (`fk_Kurjerisid`),
  ADD KEY `uzsako_is` (`fk_Restoranasid`);

--
-- Indexes for table `uzsakymoirasas`
--
ALTER TABLE `uzsakymoirasas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_Uzsakymasid` (`fk_Uzsakymasid`);

--
-- Indexes for table `vartotojas`
--
ALTER TABLE `vartotojas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `VartotojoTipas` (`VartotojoTipas`);

--
-- Indexes for table `vartotojotipas`
--
ALTER TABLE `vartotojotipas`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `zinute`
--
ALTER TABLE `zinute`
  ADD PRIMARY KEY (`id`),
  ADD KEY `siuncia` (`fk_Vartotojasid`),
  ADD KEY `gauna` (`fk_Vartotojasid1`),
  ADD KEY `turi2` (`fk_Uzsakymasid`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `krepselioirasas`
--
ALTER TABLE `krepselioirasas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `krepselis`
--
ALTER TABLE `krepselis`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `kurjeris`
--
ALTER TABLE `kurjeris`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `pasirinkimokategorija`
--
ALTER TABLE `pasirinkimokategorija`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `pasirinkimovariantas`
--
ALTER TABLE `pasirinkimovariantas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `patiekalas`
--
ALTER TABLE `patiekalas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=120;

--
-- AUTO_INCREMENT for table `restoranas`
--
ALTER TABLE `restoranas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `uzsakymas`
--
ALTER TABLE `uzsakymas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `uzsakymoirasas`
--
ALTER TABLE `uzsakymoirasas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `vartotojas`
--
ALTER TABLE `vartotojas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `zinute`
--
ALTER TABLE `zinute`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `krepselioirasas`
--
ALTER TABLE `krepselioirasas`
  ADD CONSTRAINT `Yra_Krepselyje` FOREIGN KEY (`fk_Krepselisid`) REFERENCES `krepselis` (`id`),
  ADD CONSTRAINT `priklauso2` FOREIGN KEY (`fk_Patiekalasid`) REFERENCES `patiekalas` (`id`);

--
-- Constraints for table `krepselis`
--
ALTER TABLE `krepselis`
  ADD CONSTRAINT `Turi_krepseli` FOREIGN KEY (`fk_Vartotojasid`) REFERENCES `vartotojas` (`id`),
  ADD CONSTRAINT `renkasi_is` FOREIGN KEY (`fk_Restoranasid`) REFERENCES `restoranas` (`id`);

--
-- Constraints for table `kurjeris`
--
ALTER TABLE `kurjeris`
  ADD CONSTRAINT `kurjeris_ibfk_1` FOREIGN KEY (`TransportoTipas`) REFERENCES `transportas` (`id`),
  ADD CONSTRAINT `kurjeris_ibfk_2` FOREIGN KEY (`id`) REFERENCES `vartotojas` (`id`);

--
-- Constraints for table `pasirenka`
--
ALTER TABLE `pasirenka`
  ADD CONSTRAINT `pasirenka` FOREIGN KEY (`fk_KrepselioIrasasid`) REFERENCES `krepselioirasas` (`id`);

--
-- Constraints for table `pasirinkimokategorija`
--
ALTER TABLE `pasirinkimokategorija`
  ADD CONSTRAINT `turi3` FOREIGN KEY (`fk_Patiekalasid`) REFERENCES `patiekalas` (`id`);

--
-- Constraints for table `pasirinkimovariantas`
--
ALTER TABLE `pasirinkimovariantas`
  ADD CONSTRAINT `turi4` FOREIGN KEY (`fk_ChoiceCatid`) REFERENCES `pasirinkimokategorija` (`id`);

--
-- Constraints for table `patiekalas`
--
ALTER TABLE `patiekalas`
  ADD CONSTRAINT `gamina` FOREIGN KEY (`fk_Restoranasid`) REFERENCES `restoranas` (`id`);

--
-- Constraints for table `priklauso`
--
ALTER TABLE `priklauso`
  ADD CONSTRAINT `priklauso` FOREIGN KEY (`fk_Vartotojasid`) REFERENCES `vartotojas` (`id`);

--
-- Constraints for table `uzsakymas`
--
ALTER TABLE `uzsakymas`
  ADD CONSTRAINT `turi` FOREIGN KEY (`fk_Vartotojasid`) REFERENCES `vartotojas` (`id`),
  ADD CONSTRAINT `uzsako_is` FOREIGN KEY (`fk_Restoranasid`) REFERENCES `restoranas` (`id`),
  ADD CONSTRAINT `uzsakymas_ibfk_1` FOREIGN KEY (`Statusas`) REFERENCES `statusas` (`id`),
  ADD CONSTRAINT `veza` FOREIGN KEY (`fk_Kurjerisid`) REFERENCES `kurjeris` (`id`);

--
-- Constraints for table `uzsakymoirasas`
--
ALTER TABLE `uzsakymoirasas`
  ADD CONSTRAINT `uzsakymoirasas_ibfk_1` FOREIGN KEY (`fk_Uzsakymasid`) REFERENCES `uzsakymas` (`id`);

--
-- Constraints for table `vartotojas`
--
ALTER TABLE `vartotojas`
  ADD CONSTRAINT `vartotojas_ibfk_1` FOREIGN KEY (`VartotojoTipas`) REFERENCES `vartotojotipas` (`id`);

--
-- Constraints for table `zinute`
--
ALTER TABLE `zinute`
  ADD CONSTRAINT `gauna` FOREIGN KEY (`fk_Vartotojasid1`) REFERENCES `vartotojas` (`id`),
  ADD CONSTRAINT `siuncia` FOREIGN KEY (`fk_Vartotojasid`) REFERENCES `vartotojas` (`id`),
  ADD CONSTRAINT `turi2` FOREIGN KEY (`fk_Uzsakymasid`) REFERENCES `uzsakymas` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
