USE [master] 
GO 
CREATE DATABASE [SmartHouse];
GO 
USE [SmartHouse]
GO 

CREATE TABLE [SensorData]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[HouseId] [int] NOT NULL, 
[RoomId] [int] NOT NULL, 
[SensorId] [int] NOT NULL, 
[Value] [int],
[Time] [datetime2] NOT NULL)
GO

CREATE TABLE [Rooms]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[HouseId] [int] NOT NULL, 
[Name] [nvarchar](50))
GO

CREATE TABLE [Houses]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[Name] [nvarchar](50))
GO

CREATE TABLE [Sensors]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[Name] [nvarchar](50))
GO

--INSERT INTO [SensorData] ([HouseId], [RoomId], [SensorId], [Value]) OUTPUT Inserted.ID VALUES (1, 1, 1, 100);