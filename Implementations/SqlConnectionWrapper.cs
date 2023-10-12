
using System.Data;
using System.Data.SqlClient;
using SympactDB.Interfaces;

namespace SympactDB.Implementations
{
    public class SqlConnectionWrapper : IDatabaseConnection
    {
        private readonly SqlConnection _connection;

        public SqlConnectionWrapper(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public ConnectionState State => _connection.State;

        public void Open() => _connection.Open();

        public void Close() => _connection.Close();

        public IDbCommand CreateCommand() => _connection.CreateCommand();
    }
}