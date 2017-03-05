using System;
using System.Data.SqlClient;
using SmartHouse.Core.Dto;

namespace SmartHouse.Services.Data
{
    public class DataService
    {
        private readonly string connectionString;

        public DataService()
        {
            this.connectionString = ConnectionStringProvider.ConnectionString;
        }

        public int SaveSensorData(SensorData data)
        {
            int newId;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText =
                    "INSERT INTO [SensorData] ([HouseId], [RoomId], [SensorId], [Value], [Time]) OUTPUT Inserted.ID VALUES (@houseId, @roomId, @sensorId, @data, @time);";
                command.Parameters.AddWithValue("@houseId", data.HouseId);
                command.Parameters.AddWithValue("@roomId", data.RoomId);
                command.Parameters.AddWithValue("@sensorId", data.SensorId);
                command.Parameters.AddWithValue("@data", data.Value);
                command.Parameters.AddWithValue("@time", DateTime.Now);

                newId = (int) command.ExecuteScalar();
            }

            return newId;
        }

        public SensorData GetLastSensorData(int houseId, int roomId, int sensorId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText =
                    "SELECT TOP 1 * FROM [SensorData] WHERE HouseId = @houseId and RoomId = @roomId and SensorId = @sensorId ORDER BY [Time] DESC";
                command.Parameters.AddWithValue("@houseId", houseId);
                command.Parameters.AddWithValue("@roomId", roomId);
                command.Parameters.AddWithValue("@sensorId", sensorId);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    return new SensorData(reader);
                }
            }
        }
    }
}
