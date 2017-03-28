using System;
using System.Data.SqlClient;
using SmartHouse.Core.Dto;
using System.Data;
using SmartHouse.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SmartHouse.Services.Data
{
    public class DataService
    {
        private readonly string connectionString;

        public DataService()
        {
            this.connectionString = Settings.ConnectionString;
        }

        public int SaveSensorData(SensorData data)
        {
            int newId;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText =
                    "INSERT INTO [SensorData] ([RoomId], [SensorId], [Value], [Time]) OUTPUT Inserted.ID VALUES (@roomId, @sensorId, @data, @time);";
                command.Parameters.AddWithValue("@roomId", data.RoomId);
                command.Parameters.AddWithValue("@sensorId", data.SensorId);
                command.Parameters.AddWithValue("@data", data.Value);
                command.Parameters.AddWithValue("@time", DateTime.Now);

                newId = (int) command.ExecuteScalar();
            }

            return newId;
        }

        public IEnumerable<Room> GetInfrastructure()
        {
            var rooms = GetRooms().ToList();

            foreach (var room in rooms)
            {
                room.Sensors = GetSensors(room.Id);

                foreach (var sensor in room.Sensors)
                {
                    var sensorData = GetLastSensorData(room.Id, sensor.Id);
                    sensor.Value = sensorData.Value;
                    sensor.Time = sensorData.Time;
                }
            }

            return rooms;
        }

        public IEnumerable<Room> GetRooms()
        {
            var rooms = new List<Room>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.GetRooms";
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rooms.Add(new Room(reader));
                    }
                }
            }

            return rooms;
        }

        public IEnumerable<Sensor> GetSensors(int roomId)
        {
            var sensors = new List<Sensor>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.GetSensorsForRoom";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoomId", roomId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sensors.Add(new Sensor(reader));
                    }
                }
            }

            return sensors;
        }

        public Sensor GetLastSensorData(int roomId, int sensorId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.GetLastSensorValue";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoomId", roomId);
                command.Parameters.AddWithValue("@SensorId", sensorId);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    return new Sensor
                    {
                        Value = (int)reader["Value"],
                        Time = (DateTime)reader["Time"]
                    };
                }
            }
        }

        public IEnumerable<Module> GetModules()
        {
            var modules = new List<Module>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "dbo.GetModules";
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        modules.Add(new Module(reader));
                    }
                }
            }

            return modules;
        }
    }
}
