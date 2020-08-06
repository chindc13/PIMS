-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 10, 2017 at 09:30 AM
-- Server version: 10.1.21-MariaDB
-- PHP Version: 5.6.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `final_pims`
--

-- --------------------------------------------------------

--
-- Table structure for table `accounts`
--

CREATE TABLE `accounts` (
  `auser` varchar(50) NOT NULL,
  `apass` varchar(50) NOT NULL,
  `designation` varchar(50) NOT NULL,
  `email` varchar(70) NOT NULL,
  `firstname` varchar(70) NOT NULL,
  `lastname` varchar(70) NOT NULL,
  `user_id` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `accounts`
--

INSERT INTO `accounts` (`auser`, `apass`, `designation`, `email`, `firstname`, `lastname`, `user_id`) VALUES
('admin', 'admin', '', '', '', '', 1000123332),
('Cashier', 'cashier', 'Cashier', 'dc.chin@yahoo.com', 'chin', 'chin', 1000123334),
('inpatient', 'inpatient', 'In Patient', 'dagohoythird@yahoo.com', 'Ernesto', 'Dagohoy', 1000123335),
('outpatient', 'outpatient', 'Out Patient', 'timothy@yahoo.com', 'Timothy', 'Cabasal', 1000123336);

-- --------------------------------------------------------

--
-- Table structure for table `doctor_appointments`
--

CREATE TABLE `doctor_appointments` (
  `contact` varchar(30) NOT NULL,
  `day` varchar(30) NOT NULL,
  `doctorid` int(30) NOT NULL,
  `doctorname` varchar(30) NOT NULL,
  `ends` varchar(30) NOT NULL,
  `firstname` varchar(30) NOT NULL,
  `lastname` varchar(30) NOT NULL,
  `middlename` varchar(30) NOT NULL,
  `starts` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `doctor_appointments`
--

INSERT INTO `doctor_appointments` (`contact`, `day`, `doctorid`, `doctorname`, `ends`, `firstname`, `lastname`, `middlename`, `starts`) VALUES
('Male', 'Tuesday, February 7, 2017', 2147483647, '5000', '10:17:00 PM', 'Jerryson', 'Derraco', 'DeGuzman', '10:17:00 PM');

-- --------------------------------------------------------

--
-- Table structure for table `doctor_maintenance`
--

CREATE TABLE `doctor_maintenance` (
  `contact` varchar(30) NOT NULL,
  `doctorcharges` varchar(30) NOT NULL,
  `doctorid` int(30) NOT NULL,
  `licencenumber` varchar(30) NOT NULL,
  `name` varchar(30) NOT NULL,
  `sex` varchar(30) NOT NULL,
  `specialization` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `doctor_maintenance`
--

INSERT INTO `doctor_maintenance` (`contact`, `doctorcharges`, `doctorid`, `licencenumber`, `name`, `sex`, `specialization`) VALUES
('', '', 2017003, '', '', '', ''),
('09319284712', '5000', 2017004, '12873982131', 'Jigs V Dizon', 'Male', 'Xray');

-- --------------------------------------------------------

--
-- Table structure for table `hospital_appointment`
--

CREATE TABLE `hospital_appointment` (
  `day` varchar(30) NOT NULL,
  `ends` varchar(30) NOT NULL,
  `firstname` varchar(30) NOT NULL,
  `lastname` varchar(30) NOT NULL,
  `middlename` varchar(30) NOT NULL,
  `note` varchar(100) NOT NULL,
  `PatientNumber` int(30) NOT NULL,
  `starts` varchar(30) NOT NULL,
  `topic` varchar(70) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `inpatients_admissions`
--

CREATE TABLE `inpatients_admissions` (
  `additionalnote` varchar(100) NOT NULL,
  `admitnumber` int(30) NOT NULL,
  `date` varchar(30) NOT NULL,
  `doctor` varchar(30) NOT NULL,
  `firstname` varchar(30) NOT NULL,
  `lastname` varchar(30) NOT NULL,
  `middlename` varchar(30) NOT NULL,
  `PatientNumber` int(30) NOT NULL,
  `room` varchar(30) NOT NULL,
  `roomid` int(30) NOT NULL,
  `time` varchar(30) NOT NULL,
  `doctorid` int(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `inpatients_admissions`
--

INSERT INTO `inpatients_admissions` (`additionalnote`, `admitnumber`, `date`, `doctor`, `firstname`, `lastname`, `middlename`, `PatientNumber`, `room`, `roomid`, `time`, `doctorid`) VALUES
('', 3600010, '', '', '', '', '', 0, '', 0, '', 0),
('None', 3600012, 'Monday, March 13, 2017', 'Jigs V Dizon', 'Jerryson', 'Derraco', 'DeGuzman', 820170001, 'Surgery', 1, '11:03:31 AM', 2017004);

-- --------------------------------------------------------

--
-- Table structure for table `inpatient_billing`
--

CREATE TABLE `inpatient_billing` (
  `admitnumber` varchar(30) NOT NULL,
  `balance` varchar(30) NOT NULL,
  `date` varchar(30) NOT NULL,
  `discount` varchar(30) NOT NULL,
  `firstname` varchar(30) NOT NULL,
  `lastname` varchar(30) NOT NULL,
  `middlename` varchar(30) NOT NULL,
  `PatientNumber` int(30) NOT NULL,
  `time` varchar(30) NOT NULL,
  `totalcost` int(30) NOT NULL,
  `totalpaidsofar` int(30) NOT NULL,
  `totalpayable` int(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `inpatient_discharge`
--

CREATE TABLE `inpatient_discharge` (
  `admitnumber` int(30) NOT NULL,
  `amountpaid` int(30) NOT NULL,
  `balance` int(30) NOT NULL,
  `date` varchar(30) NOT NULL,
  `firstname` varchar(30) NOT NULL,
  `lastname` varchar(30) NOT NULL,
  `middlename` varchar(30) NOT NULL,
  `nettotal` int(30) NOT NULL,
  `PatientNumber` int(30) NOT NULL,
  `time` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `inpatient_payment`
--

CREATE TABLE `inpatient_payment` (
  `admitnumber` int(30) NOT NULL,
  `amouttopay` int(30) NOT NULL,
  `cash` int(30) NOT NULL,
  `change` int(30) NOT NULL,
  `date` varchar(30) NOT NULL,
  `firstname` varchar(30) NOT NULL,
  `lastname` varchar(30) NOT NULL,
  `middlename` varchar(30) NOT NULL,
  `PatientNumber` int(30) NOT NULL,
  `payby` int(30) NOT NULL,
  `paymentnumber` int(30) NOT NULL,
  `time` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `medical_maintenance`
--

CREATE TABLE `medical_maintenance` (
  `additionalnotes` varchar(100) NOT NULL,
  `dosageform` varchar(30) NOT NULL,
  `medicineid` int(30) NOT NULL,
  `medicine_name` varchar(30) NOT NULL,
  `unitprice` int(30) NOT NULL,
  `unitstock` int(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `medical_maintenance`
--

INSERT INTO `medical_maintenance` (`additionalnotes`, `dosageform`, `medicineid`, `medicine_name`, `unitprice`, `unitstock`) VALUES
('', '', 102, '', 0, 0),
('Nothing', 'Tablet', 10011, 'Bio Flue', 50, 50);

-- --------------------------------------------------------

--
-- Table structure for table `medical_treatment`
--

CREATE TABLE `medical_treatment` (
  `date` varchar(40) NOT NULL,
  `dateofissue` varchar(40) NOT NULL,
  `firstname` varchar(40) NOT NULL,
  `lastname` varchar(40) NOT NULL,
  `medicineid` int(40) NOT NULL,
  `medicinename` varchar(40) NOT NULL,
  `middlename` varchar(40) NOT NULL,
  `nettotal` int(40) NOT NULL,
  `PatientNumber` int(40) NOT NULL,
  `qty` int(40) NOT NULL,
  `time` varchar(40) NOT NULL,
  `total` int(40) NOT NULL,
  `treatmentid` int(40) NOT NULL,
  `unitprice` int(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `medical_treatment`
--

INSERT INTO `medical_treatment` (`date`, `dateofissue`, `firstname`, `lastname`, `medicineid`, `medicinename`, `middlename`, `nettotal`, `PatientNumber`, `qty`, `time`, `total`, `treatmentid`, `unitprice`) VALUES
('Monday, March 13, 2017', 'Monday, March 13, 2017', 'Jerryson', 'Derraco', 10011, 'Bio Flue', 'DeGuzman', 250, 820170001, 5, '11:04:17 AM', 250, 601, 50);

-- --------------------------------------------------------

--
-- Table structure for table `new_patient`
--

CREATE TABLE `new_patient` (
  `address` varchar(40) NOT NULL,
  `age` int(40) NOT NULL,
  `allergie` varchar(40) NOT NULL,
  `birth` varchar(40) NOT NULL,
  `city` varchar(40) NOT NULL,
  `contact` varchar(40) NOT NULL,
  `date` varchar(40) NOT NULL,
  `diagnosed` varchar(40) NOT NULL,
  `email` varchar(40) NOT NULL,
  `firstname` varchar(40) NOT NULL,
  `gname` varchar(40) NOT NULL,
  `gnumber` int(40) NOT NULL,
  `grelationship` varchar(40) NOT NULL,
  `lastname` varchar(40) NOT NULL,
  `middlename` varchar(40) NOT NULL,
  `PatientNumber` int(40) NOT NULL,
  `sex` varchar(40) NOT NULL,
  `status` varchar(40) NOT NULL,
  `telephone` int(40) NOT NULL,
  `time` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `new_patient`
--

INSERT INTO `new_patient` (`address`, `age`, `allergie`, `birth`, `city`, `contact`, `date`, `diagnosed`, `email`, `firstname`, `gname`, `gnumber`, `grelationship`, `lastname`, `middlename`, `PatientNumber`, `sex`, `status`, `telephone`, `time`) VALUES
('154 3rd st 11th ave', 30, 'cancer', 'Monday, September 11, 1995', 'caloocan', '09098906633', 'Sunday, March 12, 2017', 'asd', 'dc.chin@gmail.com', 'Jerryson', 'Jen', 2147483647, 'Mother', 'Derraco', 'DeGuzman', 820170001, 'Male', 'Unmarried', 36901120, '7:28:57 PM');

-- --------------------------------------------------------

--
-- Table structure for table `outpatient_billing`
--

CREATE TABLE `outpatient_billing` (
  `amountpaid` int(40) NOT NULL,
  `balance` int(40) NOT NULL,
  `discount` int(40) NOT NULL,
  `firstname` varchar(40) NOT NULL,
  `lastname` varchar(40) NOT NULL,
  `middlename` varchar(40) NOT NULL,
  `nettotal` int(40) NOT NULL,
  `outpatientid` int(40) NOT NULL,
  `total` int(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `outpatient_payment`
--

CREATE TABLE `outpatient_payment` (
  `amountpaid` int(40) NOT NULL,
  `cash` int(40) NOT NULL,
  `change` int(40) NOT NULL,
  `date` varchar(40) NOT NULL,
  `firstname` varchar(40) NOT NULL,
  `lastname` varchar(40) NOT NULL,
  `middlename` varchar(40) NOT NULL,
  `outpatientid` int(40) NOT NULL,
  `payby` varchar(40) NOT NULL,
  `paymentnumber` int(40) NOT NULL,
  `time` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `outpatient_treatment`
--

CREATE TABLE `outpatient_treatment` (
  `age` int(40) NOT NULL,
  `birth` varchar(40) NOT NULL,
  `careid` int(40) NOT NULL,
  `contact` varchar(40) NOT NULL,
  `date` varchar(40) NOT NULL,
  `doctorid` int(40) NOT NULL,
  `doctorname` varchar(40) NOT NULL,
  `firstname` varchar(40) NOT NULL,
  `lastname` varchar(40) NOT NULL,
  `medicineid` int(40) NOT NULL,
  `medicinename` varchar(40) NOT NULL,
  `middlename` varchar(40) NOT NULL,
  `outpatientid` int(40) NOT NULL,
  `sex` varchar(40) NOT NULL,
  `status` varchar(40) NOT NULL,
  `time` varchar(40) NOT NULL,
  `typeofcare` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `outpatient_treatment`
--

INSERT INTO `outpatient_treatment` (`age`, `birth`, `careid`, `contact`, `date`, `doctorid`, `doctorname`, `firstname`, `lastname`, `medicineid`, `medicinename`, `middlename`, `outpatientid`, `sex`, `status`, `time`, `typeofcare`) VALUES
(0, '', 0, '', '', 0, '', '', '', 0, '', '', 200011, '', '', '', ''),
(21, 'Monday, March 13, 2017', 405, '09351029381', 'Monday, March 13, 2017', 2017004, 'Jigs V Dizon', 'Jerryson', 'Derraco', 10011, 'Bio Flue', 'DeGuzman', 200013, 'Male', 'Unmarried', '11:19:53 AM', 'Resting'),
(20, '', 20, '', '', 20, '', '', '', 20, '', '', 2000001, '', '', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `room_maintenance`
--

CREATE TABLE `room_maintenance` (
  `booked` varchar(40) NOT NULL,
  `department` varchar(40) NOT NULL,
  `roomcost` int(40) NOT NULL,
  `roomid` int(40) NOT NULL,
  `roomnumber` int(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `room_maintenance`
--

INSERT INTO `room_maintenance` (`booked`, `department`, `roomcost`, `roomid`, `roomnumber`) VALUES
('', '', 0, 0, 1000001),
('', 'Surgery', 5000, 1, 101);

-- --------------------------------------------------------

--
-- Table structure for table `service_maintenance`
--

CREATE TABLE `service_maintenance` (
  `additionalnotes` varchar(100) NOT NULL,
  `amount` int(40) NOT NULL,
  `averageduration` varchar(40) NOT NULL,
  `service_id` int(40) NOT NULL,
  `service_name` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `service_maintenance`
--

INSERT INTO `service_maintenance` (`additionalnotes`, `amount`, `averageduration`, `service_id`, `service_name`) VALUES
('', 0, '', 300001, ''),
('Surgery', 50000, '2', 300002, 'Surgery'),
('', 50, '', 3000001, '');

-- --------------------------------------------------------

--
-- Table structure for table `service_treatment`
--

CREATE TABLE `service_treatment` (
  `date` varchar(40) NOT NULL,
  `firstname` varchar(40) NOT NULL,
  `lastname` varchar(40) NOT NULL,
  `middlename` varchar(40) NOT NULL,
  `PatientNumber` int(40) NOT NULL,
  `servicecharge` int(40) NOT NULL,
  `serviceid` int(40) NOT NULL,
  `servicename` varchar(40) NOT NULL,
  `time` varchar(40) NOT NULL,
  `treatmentdate` varchar(40) NOT NULL,
  `treatmentid` int(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `service_treatment`
--

INSERT INTO `service_treatment` (`date`, `firstname`, `lastname`, `middlename`, `PatientNumber`, `servicecharge`, `serviceid`, `servicename`, `time`, `treatmentdate`, `treatmentid`) VALUES
('Monday, March 13, 2017', 'Jerryson', 'Derraco', 'DeGuzman', 820170001, 50000, 300002, 'Surgery', '11:05:50 AM', 'Monday, March 13, 2017', 701);

-- --------------------------------------------------------

--
-- Table structure for table `typeofcare`
--

CREATE TABLE `typeofcare` (
  `careid` int(30) NOT NULL,
  `name` varchar(50) NOT NULL,
  `carecost` int(11) NOT NULL,
  `additionalnotes` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `typeofcare`
--

INSERT INTO `typeofcare` (`careid`, `name`, `carecost`, `additionalnotes`) VALUES
(404, '', 100, ''),
(405, 'Resting', 500, 'Sleep, Rest');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`user_id`);

--
-- Indexes for table `doctor_appointments`
--
ALTER TABLE `doctor_appointments`
  ADD PRIMARY KEY (`doctorid`);

--
-- Indexes for table `doctor_maintenance`
--
ALTER TABLE `doctor_maintenance`
  ADD PRIMARY KEY (`doctorid`);

--
-- Indexes for table `hospital_appointment`
--
ALTER TABLE `hospital_appointment`
  ADD PRIMARY KEY (`PatientNumber`);

--
-- Indexes for table `inpatients_admissions`
--
ALTER TABLE `inpatients_admissions`
  ADD PRIMARY KEY (`admitnumber`);

--
-- Indexes for table `inpatient_billing`
--
ALTER TABLE `inpatient_billing`
  ADD PRIMARY KEY (`admitnumber`);

--
-- Indexes for table `inpatient_discharge`
--
ALTER TABLE `inpatient_discharge`
  ADD PRIMARY KEY (`admitnumber`);

--
-- Indexes for table `inpatient_payment`
--
ALTER TABLE `inpatient_payment`
  ADD PRIMARY KEY (`admitnumber`);

--
-- Indexes for table `medical_maintenance`
--
ALTER TABLE `medical_maintenance`
  ADD PRIMARY KEY (`medicineid`);

--
-- Indexes for table `medical_treatment`
--
ALTER TABLE `medical_treatment`
  ADD PRIMARY KEY (`treatmentid`);

--
-- Indexes for table `new_patient`
--
ALTER TABLE `new_patient`
  ADD PRIMARY KEY (`PatientNumber`);

--
-- Indexes for table `outpatient_billing`
--
ALTER TABLE `outpatient_billing`
  ADD PRIMARY KEY (`outpatientid`);

--
-- Indexes for table `outpatient_payment`
--
ALTER TABLE `outpatient_payment`
  ADD PRIMARY KEY (`paymentnumber`);

--
-- Indexes for table `outpatient_treatment`
--
ALTER TABLE `outpatient_treatment`
  ADD PRIMARY KEY (`outpatientid`);

--
-- Indexes for table `room_maintenance`
--
ALTER TABLE `room_maintenance`
  ADD PRIMARY KEY (`roomid`);

--
-- Indexes for table `service_maintenance`
--
ALTER TABLE `service_maintenance`
  ADD PRIMARY KEY (`service_id`);

--
-- Indexes for table `service_treatment`
--
ALTER TABLE `service_treatment`
  ADD PRIMARY KEY (`treatmentid`);

--
-- Indexes for table `typeofcare`
--
ALTER TABLE `typeofcare`
  ADD PRIMARY KEY (`careid`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
