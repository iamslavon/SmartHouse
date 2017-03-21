USE [master] 
GO 
CREATE DATABASE [SmartHouse];
GO 
USE [SmartHouse]
GO 

CREATE TABLE [Houses]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[Name] [nvarchar](50))
GO

CREATE TABLE [Sensors]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[Name] [nvarchar](50),
[Measurement] [nvarchar](10))
GO

CREATE TABLE [Rooms]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[HouseId] [int] FOREIGN KEY REFERENCES [Houses]([Id]),
[Name] [nvarchar](50))
GO

CREATE TABLE [SensorData]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[HouseId] [int] FOREIGN KEY REFERENCES [Houses]([Id]), 
[RoomId] [int] FOREIGN KEY REFERENCES [Rooms]([Id]), 
[SensorId] [int] FOREIGN KEY REFERENCES [Sensors]([Id]),
[Value] [int],
[Time] [datetime2] NOT NULL)
GO

--INSERT INTO [SensorData] ([HouseId], [RoomId], [SensorId], [Value]) OUTPUT Inserted.ID VALUES (1, 1, 1, 100);