using System.Collections.Generic;

namespace SmartHouse.Core.Entities
{
    public class House
    {
        public string Name { get; set; }

        public IEnumerable<Room> Rooms { get; set; }
    }
}
