// Sympact06 -- Olivier Flentge

using System.Data;

namespace SympactDB.Interfaces
{
    public interface IDatabaseConnection
    {
        ConnectionState State { get; }
        void Open();
        void Close();
        IDbCommand CreateCommand();
    }
}