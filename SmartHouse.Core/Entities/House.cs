using System.Collections.Generic;
using System.Data;

namespace SmartHouse.Core.Entities
{
    public class House : BaseEntity
    {
        public House() { }

        public House(IDataRecord reader) : base(reader) { }

        public IEnumerable<Room> Rooms { get; set; }
    }
}
