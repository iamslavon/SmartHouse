using System.Collections.Generic;

namespace SmartHouse.Core.Entities
{
    public class Room
    {
        public string Name { get; set; }

        public IEnumerable<Sensor> Sensors { get; set; }
    }
}
