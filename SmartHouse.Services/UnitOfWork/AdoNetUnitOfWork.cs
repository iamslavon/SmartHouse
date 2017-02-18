using System;
using System.Data.SqlClient;

namespace SmartHouse.Services.UnitOfWork
{
    public class AdoNetUnitOfWork : IDisposable
    {
        private SqlConnection connection;
        private readonly bool ownsConnection;
        private SqlTransaction transaction;

        public AdoNetUnitOfWork(SqlConnection connection, bool ownsConnection)
        {
            this.connection = connection;
            this.ownsConnection = ownsConnection;
            this.transaction = connection.BeginTransaction();
        }

        public SqlCommand CreateCommand()
        {
            var command = this.connection.CreateCommand();
            command.Transaction = this.transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (this.transaction == null)
                throw new InvalidOperationException("Transaction have already been commited. Check your transaction handling.");

            this.transaction.Commit();
            this.transaction = null;
        }

        public void Dispose()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
                this.transaction = null;
            }

            if (this.connection != null && this.ownsConnection)
            {
                this.connection.Close();
                this.connection = null;
            }
        }
    }
}
