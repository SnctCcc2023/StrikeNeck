using System;
using System.Data.SQLite;
using System.IO;

namespace minute2
{
    public class Minute
    {
        private string connectionString = @"Data Source=minutes.db;Version=3;";
        public void InitializeDatabase()
        {
            if (!File.Exists("minutes.db"))
            {
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();
                var createTableQuery = @"CREATE TABLE IF NOT EXISTS minutes(Id INTEGER, results REAL)";
                using var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }
        public void MinuteInsert(bool result, float tp)
        {
            var insertQuery = @"INSERT INTO minutes(Id, results) VALUES (@id, @results)";
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            using var command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@id", tp);
            command.Parameters.AddWithValue("@results", result);
            command.ExecuteNonQuery();
        }
    }
}