using System.Data;

namespace SmartHouse.Core.Entities
{
    public class BaseEntity
    {
        public BaseEntity() { }

        public BaseEntity(IDataRecord reader)
        {
            this.Id = (int)reader["Id"];
            this.Name = reader["Name"].ToString();
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
