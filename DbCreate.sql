USE [master] 
GO 
CREATE DATABASE [SmartHouse];
GO 
USE [SmartHouse]
GO 

CREATE TABLE [Sensors]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[Name] [nvarchar](50),
[Measurement] [nvarchar](10))
GO

CREATE TABLE [Rooms]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[Name] [nvarchar](50))
GO

CREATE TABLE [SensorData]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[RoomId] [int] FOREIGN KEY REFERENCES [Rooms]([Id]), 
[SensorId] [int] FOREIGN KEY REFERENCES [Sensors]([Id]),
[Value] [int],
[Time] [datetime2] NOT NULL)
GO

CREATE TABLE [Modules]( 
[Id] [int] PRIMARY KEY IDENTITY NOT NULL, 
[RoomId] [int] FOREIGN KEY REFERENCES [Rooms]([Id]), 
[Ip] [nvarchar](50))
GO

-- Stored procedures --

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLastSensorValue]
	@RoomId int,
	@SensorId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1	[sd].[Value], [sd].[Time]
	FROM			[SensorData] [sd]
	WHERE			[sd].[RoomId] = @RoomId AND [sd].[SensorId] = @SensorId
	ORDER BY		[sd].[Time] DESC
END
GO

CREATE PROCEDURE [dbo].[GetRooms]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	*
	FROM	[Rooms]
END
GO

CREATE PROCEDURE [dbo].[GetSensorsForRoom]
	@RoomId int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [s].[Id], [s].[Name], [s].[Measurement]
	FROM (
		SELECT	 [sd].[SensorId]
		FROM	 [SensorData] [sd]
		WHERE	 [sd].[RoomId] = @RoomId 
		GROUP BY [sd].[SensorId]) tt
	INNER JOIN [Sensors] [s]
	ON [s].[Id] = [tt].[SensorId]
END
GO

CREATE PROCEDURE [dbo].[GetModules]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT		*
	FROM		[Modules]
END

GO