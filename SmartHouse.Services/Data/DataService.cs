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
    }
}
