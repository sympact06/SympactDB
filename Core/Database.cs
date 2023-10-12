
using SympactDB.Interfaces;
using System.Data;

namespace SympactDB.Core
{
    public class Database
    {
        private readonly IDatabaseConnection _connection;

        public Database(IDatabaseConnection connection)
        {
            _connection = connection;
        }

        private void EnsureConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public int ExecuteNonQuery(string query)
        {
            try
            {
                EnsureConnection();

                using var command = _connection.CreateCommand();
                command.CommandText = query;
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // It's better to log the exception or handle it appropriately
                throw;
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            try
            {
                EnsureConnection();

                using var command = _connection.CreateCommand();
                command.CommandText = query;

                using var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                // Again, logging or better error handling is recommended
                throw;
            }
        }

        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}