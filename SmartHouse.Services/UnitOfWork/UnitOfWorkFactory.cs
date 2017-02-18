using System.Data.SqlClient;

namespace SmartHouse.Services.UnitOfWork
{
    public static class UnitOfWorkFactory
    {
        public static string ConnectionString;

        public static AdoNetUnitOfWork Create()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            return new AdoNetUnitOfWork(connection, true);
        }
    }
}
