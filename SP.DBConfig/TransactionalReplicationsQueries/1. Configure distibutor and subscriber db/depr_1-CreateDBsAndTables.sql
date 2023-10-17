CREATE DATABASE [SP.Customer.DB];
GO

USE [SP.Customer.DB];
GO
  
CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY,
    UserId NVARCHAR(MAX),
    FirstName NVARCHAR(MAX),
    LastName NVARCHAR(MAX)
);



CREATE DATABASE [SP.Provider.DB];
GO

USE [SP.Customer.DB];
GO
  
CREATE TABLE Providers (
    ProviderId INT PRIMARY KEY,
    UserId NVARCHAR(MAX),
    FirstName NVARCHAR(MAX),
    LastName NVARCHAR(MAX),
    EnterpriseName NVARCHAR(MAX),
    WorkHoursBegin INT NOT NULL CHECK (WorkHoursBegin >= 0 AND WorkHoursBegin <= 23),
    WorkHoursEnd INT NOT NULL CHECK (WorkHoursEnd >= 0 AND WorkHoursEnd <= 23)
);

CREATE DATABASE [SP.Service.DB];
GO

USE [SP.Service.DB];
GO
  
CREATE TABLE Services (
    ServiceId INT PRIMARY KEY,
    Name NVARCHAR(MAX),
    Price INT,
    ProviderUserId NVARCHAR(MAX),
);

CREATE TABLE Events (
    EventId INT PRIMARY KEY,
    ServiceId INT,
    CustomerUserId NVARCHAR(MAX),
    DateOfStart NVARCHAR(MAX),
    DateOfEnd NVARCHAR(MAX),
    FOREIGN KEY (ServiceId) REFERENCES Services(ServiceId),
);



