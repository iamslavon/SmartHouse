using System.Data;

namespace SmartHouse.Core.Entities
{
    public class Module
    {
        public Module(IDataRecord reader)
        {
            this.Id = (int)reader["Id"];
            this.Ip = reader["Ip"].ToString();
        }

        public int Id { get; set; }

        public string Ip { get; set; }
    }
}
