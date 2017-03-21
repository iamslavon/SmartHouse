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

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetHouses]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT		[Houses].[Id],
				[Houses].[Name]
	FROM		[Houses]
END
GO

CREATE PROCEDURE [dbo].[GetLastSensorValue]
	@HouseId int, 
	@RoomId int,
	@SensorId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1	[sd].[Value], [sd].[Time]
	FROM			[SensorData] [sd]
	WHERE			[sd].[HouseId] = @HouseId AND [sd].[RoomId] = @RoomId AND [sd].[SensorId] = @SensorId
	ORDER BY		[sd].[Time] DESC
END
GO

CREATE PROCEDURE [dbo].[GetRoomsForHouse]
	@HouseId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	[Rooms].[Id],
			[Rooms].[Name]
	FROM	[Rooms]
	WHERE	[Rooms].[HouseId] = @HouseId
END
GO

CREATE PROCEDURE [dbo].[GetSensorsForRoom]
	@HouseId int, 
	@RoomId int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [s].[Id], [s].[Name], [s].[Measurement]
	FROM
		(SELECT [sd].[SensorId]
		FROM [SensorData] [sd]
		WHERE	[sd].[HouseId] = @HouseId 
			AND [sd].[RoomId] = @RoomId
		GROUP BY [sd].[SensorId]) tt
	INNER JOIN [Sensors] [s]
	ON [s].[Id] = [tt].[SensorId]
END
GO