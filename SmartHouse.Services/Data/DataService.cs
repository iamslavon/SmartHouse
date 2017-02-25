using System;
using SmartHouse.Core.Dto;
using SmartHouse.Services.UnitOfWork;

namespace SmartHouse.Services.Data
{
    public class DataService
    {
        protected readonly AdoNetUnitOfWork unitOfWork;

        public DataService()
        {
            this.unitOfWork = UnitOfWorkFactory.Create();
        }

        public int SaveSensorData(SensorData data)
        {
            int newId;

            using (var command = unitOfWork.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO [SensorData] ([HouseId], [RoomId], [SensorId], [Value], [Time]) OUTPUT Inserted.ID VALUES (@houseId, @roomId, @sensorId, @data, @time);";
                command.Parameters.AddWithValue("@houseId", data.HouseId);
                command.Parameters.AddWithValue("@roomId", data.RoomId);
                command.Parameters.AddWithValue("@sensorId", data.SensorId);
                command.Parameters.AddWithValue("@data", data.Value);
                command.Parameters.AddWithValue("@time", DateTime.Now);

                newId = (int)command.ExecuteScalar();
            }

            unitOfWork.SaveChanges();

            return newId;
        }

        public SensorData GetLastSensorData(int houseId, int roomId, int sensorId)
        {
            using (var command = unitOfWork.CreateCommand())
            {
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
