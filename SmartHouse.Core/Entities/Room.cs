using System.Collections.Generic;
using System.Data;

namespace SmartHouse.Core.Entities
{
    public class Room : BaseEntity
    {
        public Room() { }

        public Room(IDataRecord reader) : base(reader) { }

        public IEnumerable<Sensor> Sensors { get; set; }
    }
}
