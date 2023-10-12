using System;
using System.Data;
using Moq;
using SympactDB.Core;
using SympactDB.Interfaces;
using Xunit;

namespace SympactDB.Tests
{
    public class DatabaseTests
    {
        [Fact]
        public void ExecuteNonQuery_ExecutesGivenQuery_ReturnsAffectedRows()
        {
            var mockConnection = new Mock<IDatabaseConnection>();
            var command = new Mock<IDbCommand>();

            mockConnection.Setup(conn => conn.CreateCommand()).Returns(command.Object);
            command.Setup(cmd => cmd.ExecuteNonQuery()).Returns(5); // Mocking that 5 rows are affected

            var database = new Database(mockConnection.Object);

            string sqlNonQuery = "UPDATE Users SET LastName = 'Smith' WHERE UserID = 1;";
            int affectedRows = database.ExecuteNonQuery(sqlNonQuery);

            Assert.Equal(5, affectedRows);
        }

        [Fact]
        public void ExecuteQuery_ExecutesGivenQuery_ReturnsDataTable()
        {
            var mockConnection = new Mock<IDatabaseConnection>();
            var command = new Mock<IDbCommand>();
            var reader = new Mock<IDataReader>();

            mockConnection.Setup(conn => conn.CreateCommand()).Returns(command.Object);
            command.Setup(cmd => cmd.ExecuteReader()).Returns(reader.Object);

            var database = new Database(mockConnection.Object);

            string sqlQuery = "SELECT FirstName, LastName FROM Users WHERE UserID = 1;";
            DataTable result = database.ExecuteQuery(sqlQuery);

            Assert.NotNull(result);
        }
    }
}