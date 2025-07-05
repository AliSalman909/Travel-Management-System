-- Create a new database named 'TravelEaseDB'
CREATE DATABASE TravelEaseDB;
GO  -- 'GO' is a batch separator used in SQL Server Management Studio (SSMS)

-- Switch the context to the newly created database
USE TravelEaseDB;
GO

-- USER Table
CREATE TABLE AppUsers 
(
    UserID INT IDENTITY(100,5) PRIMARY KEY,
	UserName VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(UserName))) > 0),
    UserPassword VARCHAR(255) NOT NULL,
    ContactNumber VARCHAR(11) NOT NULL UNIQUE  CHECK (ContactNumber LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' AND (LEN(ContactNumber) = 11) ),
    Email VARCHAR(255) NOT NULL UNIQUE CHECK (Email LIKE '%@%.%'),
    UserRole VARCHAR(50) NOT NULL CHECK (UserRole IN ('Traveler', 'Admin', 'TourOperator', 'ServiceProvider')),
    CreatedAt DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- TRAVELER Table
CREATE TABLE Traveler 
(
    UserID INT PRIMARY KEY,  
    CNIC CHAR(13) NOT NULL UNIQUE CHECK (CNIC LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' AND LEN(CNIC)=13),
    DateOfBirth DATE NOT NULL CHECK (DateOfBirth < GETDATE()),
    Nationality VARCHAR(100) NOT NULL CHECK (LEN(LTRIM(RTRIM(Nationality))) > 0),
    JoinDate DATE NOT NULL DEFAULT CURRENT_DATE CHECK (JoinDate <= CAST(GETDATE() AS DATE)),
    CONSTRAINT FK_Traveler_User FOREIGN KEY (UserID) REFERENCES AppUsers(UserID) ON DELETE CASCADE
);
GO


-- TRAVELER_PREFERENCE
CREATE TABLE TRAVELER_PREFERENCE
(
    UserID INT FOREIGN KEY REFERENCES TRAVELER(UserID),
    Preference VARCHAR(100) CHECK (Preference IN ('Adventure', 'Cultural', 'Luxury', 'Budget', 'Wildlife', 'Hiking', 'Beach','Historical', 'Religious', 'Culinary', 'Photography', 'Snow/Skiing','Wellness', 'Road Trips', 'Solo Travel', 'Family Friendly')),
    PRIMARY KEY(UserID, Preference),
	CONSTRAINT FK_TravelerPref_User FOREIGN KEY (UserID) REFERENCES TRAVELER(UserID) ON DELETE CASCADE
);
GO

-- ADMIN Table
CREATE TABLE ADMIN
(
    UserID INT PRIMARY KEY FOREIGN KEY REFERENCES AppUsers(UserID) ON DELETE CASCADE,
    JoinDate DATE NOT NULL DEFAULT CURRENT_DATE CHECK (JoinDate <= CAST(GETDATE() AS DATE)),
    LastLogin DATETIME DEFAULT NULL CHECK (LastLogin >= JoinDate),
);
GO

-- LOCATION Table
CREATE TABLE LOCATION (
    LocationID INT PRIMARY KEY,
    City VARCHAR(100),
    Country VARCHAR(100),
    Description TEXT
);
GO

-- SERVICE_PROVIDER Table
CREATE TABLE SERVICE_PROVIDER (
    LicenseNo VARCHAR(20) PRIMARY KEY,
    ProviderName VARCHAR(100),
    ProviderType VARCHAR(50),
    UserID INT FOREIGN KEY REFERENCES AppUsers(UserID),
    LocationID INT FOREIGN KEY REFERENCES LOCATION(LocationID),
    JoinDate DATE
);
GO

-- TOUR_OPERATOR
CREATE TABLE TOUR_OPERATOR (
    LicenseNo VARCHAR(20) PRIMARY KEY FOREIGN KEY REFERENCES SERVICE_PROVIDER(LicenseNo),
    CompanyName VARCHAR(100),
    JoinDate DATE
);
GO

-- HOTEL
CREATE TABLE HOTEL (
    H_Id INT PRIMARY KEY,
    TotalRooms INT,
    LicenseNo VARCHAR(20) FOREIGN KEY REFERENCES SERVICE_PROVIDER(LicenseNo)
);
GO

-- GUIDE
CREATE TABLE GUIDE (
    G_Id INT PRIMARY KEY,
    Specialization VARCHAR(100),
    LicenseNo VARCHAR(20) FOREIGN KEY REFERENCES SERVICE_PROVIDER(LicenseNo)
);
GO

-- GUIDE_LANGUAGE
CREATE TABLE GUIDE_LANGUAGE (
    G_Id INT FOREIGN KEY REFERENCES GUIDE(G_Id),
    Language VARCHAR(50),
    PRIMARY KEY (G_Id, Language)
);
GO

-- TRANSPORT
CREATE TABLE TRANSPORT (
    T_Id INT PRIMARY KEY,
    VehicleType VARCHAR(50),
    Capacity INT,
    LicenseNo VARCHAR(20) FOREIGN KEY REFERENCES SERVICE_PROVIDER(LicenseNo)
);
GO

-- TRIP Table
CREATE TABLE TRIP (
    TripID INT PRIMARY KEY,
    Title VARCHAR(100),
    Type VARCHAR(50),
    Status VARCHAR(50),
    Capacity INT,
    Price DECIMAL(10,2),
    DayOrder INT,
    StartDate DATE,
    EndDate DATE,
    AdminID INT FOREIGN KEY REFERENCES ADMIN(UserID)
);
GO

-- CATEGORY
CREATE TABLE CATEGORY (
    CategoryID INT PRIMARY KEY,
    Name VARCHAR(100),
    Description TEXT
);
GO

-- TRIP_CATEGORY
CREATE TABLE TRIP_CATEGORY (
    TripID INT FOREIGN KEY REFERENCES TRIP(TripID),
    CategoryID INT FOREIGN KEY REFERENCES CATEGORY(CategoryID),
    PRIMARY KEY (TripID, CategoryID)
);
GO

-- BOOKINGS
CREATE TABLE BOOKINGS (
    BookingID INT PRIMARY KEY,
    TripID INT FOREIGN KEY REFERENCES TRIP(TripID),
    UserID INT FOREIGN KEY REFERENCES TRAVELER(UserID),
    BookDate DATE,
    Status VARCHAR(50),
    GroupSize INT
);
GO

-- PAYMENT
CREATE TABLE PAYMENT (
    PaymentID INT PRIMARY KEY,
    BookingID INT FOREIGN KEY REFERENCES BOOKINGS(BookingID),
    TransactionDate DATETIME,
    PaymentMethod VARCHAR(50),
    Amount DECIMAL(10,2),
    Status VARCHAR(50)
);
GO

-- TICKET
CREATE TABLE TICKET (
    TicketID INT PRIMARY KEY,
    BookingID INT FOREIGN KEY REFERENCES BOOKINGS(BookingID),
    Type VARCHAR(50),
    IssueDate DATE,
    ExpiryDate DATE
);
GO

-- REVIEW
CREATE TABLE REVIEW (
    ReviewID INT PRIMARY KEY,
    TripID INT FOREIGN KEY REFERENCES TRIP(TripID),
    UserID INT FOREIGN KEY REFERENCES TRAVELER(UserID),
    Rating INT CHECK(Rating BETWEEN 1 AND 5),
    Comment TEXT,
    ReviewDate DATE,
    ApproveStatus VARCHAR(50),
    ModeratedBy INT FOREIGN KEY REFERENCES ADMIN(UserID),
    ModerationDate DATE
);
GO

-- TRIP_STOPS
CREATE TABLE TRIP_STOPS (
    StopID INT PRIMARY KEY,
    TripID INT FOREIGN KEY REFERENCES TRIP(TripID),
    LocationID INT FOREIGN KEY REFERENCES LOCATION(LocationID),
    PlaceName VARCHAR(100),
    ArrivalDate DATE,
    DepartDate DATE
);
GO

-- OFFERS
CREATE TABLE OFFERS (
    TripID INT FOREIGN KEY REFERENCES TRIP(TripID),
    LicenseNo VARCHAR(20) FOREIGN KEY REFERENCES SERVICE_PROVIDER(LicenseNo),
    Price DECIMAL(10,2),
    PRIMARY KEY (TripID, LicenseNo)
);
GO

-- CANCELLATION
CREATE TABLE CANCELLATION (
    CancellationID INT PRIMARY KEY,
    BookingID INT FOREIGN KEY REFERENCES BOOKINGS(BookingID),
    CancelDate DATE,
    Reason TEXT,
    Status VARCHAR(50)
);
GO

-- REFUND
CREATE TABLE REFUND (
    RefundID INT PRIMARY KEY,
    BookingID INT FOREIGN KEY REFERENCES BOOKINGS(BookingID),
    RefundDate DATE,
    Amount DECIMAL(10,2)
);
GO

-- TRIP_SERVICE_ASSIGNMENT
CREATE TABLE TRIP_SERVICE_ASSIGNMENT (
    AssignmentID INT PRIMARY KEY,
    TripID INT FOREIGN KEY REFERENCES TRIP(TripID),
    OperatorLicenseNo VARCHAR(20) FOREIGN KEY REFERENCES TOUR_OPERATOR(LicenseNo),
    ProviderLicenseNo VARCHAR(20) FOREIGN KEY REFERENCES SERVICE_PROVIDER(LicenseNo),
    AssignedDate DATE,
    AssignmentStatus VARCHAR(50),
    ResponseDate DATE
);
GO
