-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 28, 2019 at 10:22 AM
-- Server version: 10.1.38-MariaDB
-- PHP Version: 7.1.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `classattendance`
--

-- --------------------------------------------------------

--
-- Table structure for table `class`
--

CREATE TABLE `class` (
  `ID` int(11) NOT NULL,
  `CLASS_CODE` varchar(255) NOT NULL,
  `ROOM` int(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `class`
--

INSERT INTO `class` (`ID`, `CLASS_CODE`, `ROOM`) VALUES
(1, 'C322-1', 322),
(3, 'C201-2', 201),
(4, 'c333', 333);

-- --------------------------------------------------------

--
-- Table structure for table `device`
--

CREATE TABLE `device` (
  `ID` int(11) NOT NULL,
  `S_ID` int(11) NOT NULL,
  `MAC_ADDR` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `device`
--

INSERT INTO `device` (`ID`, `S_ID`, `MAC_ADDR`) VALUES
(7, 1, '68:F7:28:BA:A4:26'),
(8, 1, 'B4:69:21:79:7F:AC'),
(9, 2, '24:fd:52:3a:21:da'),
(10, 2, 'b0:55:08:39:03:79');

-- --------------------------------------------------------

--
-- Table structure for table `logincredential`
--

CREATE TABLE `logincredential` (
  `ID` int(11) NOT NULL,
  `USERNAME` varchar(255) NOT NULL,
  `PASSWORD` char(64) NOT NULL,
  `NAME` varchar(255) NOT NULL,
  `ROLE` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `logincredential`
--

INSERT INTO `logincredential` (`ID`, `USERNAME`, `PASSWORD`, `NAME`, `ROLE`) VALUES
(1, 'ahpoh1222', '1111', 'POH', 3),
(2, 'student1', '11', 'S1', 2),
(3, 'student2', '11', 'S2', 2),
(4, 'teacher1', '11', 't1', 1),
(5, 'student3', '11', 'ahpoh', 2);

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE `student` (
  `ID` int(11) NOT NULL,
  `L_ID` int(11) NOT NULL,
  `C_ID` int(11) NOT NULL,
  `NAME` varchar(255) NOT NULL,
  `STUDENT_ID` varchar(255) NOT NULL,
  `STATUS` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `student`
--

INSERT INTO `student` (`ID`, `L_ID`, `C_ID`, `NAME`, `STUDENT_ID`, `STATUS`) VALUES
(1, 2, 1, 's1', '111222', 1),
(2, 3, 1, 's2', '111223', 1),
(3, 5, 1, 'ahpoh', '112345', 1);

-- --------------------------------------------------------

--
-- Table structure for table `teacher`
--

CREATE TABLE `teacher` (
  `ID` int(11) NOT NULL,
  `L_ID` int(11) NOT NULL,
  `NAME` varchar(255) NOT NULL,
  `STATUS` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `teacher`
--

INSERT INTO `teacher` (`ID`, `L_ID`, `NAME`, `STATUS`) VALUES
(1, 4, 't1', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `class`
--
ALTER TABLE `class`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `device`
--
ALTER TABLE `device`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `logincredential`
--
ALTER TABLE `logincredential`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `teacher`
--
ALTER TABLE `teacher`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `class`
--
ALTER TABLE `class`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `device`
--
ALTER TABLE `device`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `logincredential`
--
ALTER TABLE `logincredential`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `student`
--
ALTER TABLE `student`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `teacher`
--
ALTER TABLE `teacher`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
