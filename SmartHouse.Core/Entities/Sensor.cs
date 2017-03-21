using System;
using System.Data;

namespace SmartHouse.Core.Entities
{
    public class Sensor : BaseEntity
    {
        public Sensor() { }

        public Sensor(IDataRecord reader) : base(reader)
        {
            this.Measurement = reader["Measurement"].ToString();
        }

        public string Measurement { get; set; }

        public int Value { get; set; }

        public DateTime Time { get; set; }
    }
}
