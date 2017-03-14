using System;
using System.Data;

namespace SmartHouse.Core.Entities
{
    public class Sensor : BaseEntity
    {
        public Sensor() { }

        public Sensor(IDataRecord reader) : base(reader) { }

        public int Value { get; set; }

        public DateTime Time { get; set; }
    }
}
