using System;
using System.Data.SQLite;

namespace hour2
{
    internal class Hour
    {
        private string connectionString = @"Data Source=hours.db;Version=3;";
        public void InitializeDatabase()
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            var createTableQuery = @"CREATE TABLE IF NOT EXISTS hours(Id INTEGER, Column1 REAL, Column2 REAL)";
            using var command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();

            for (int i = 0; i <= 167; i++)
            {
                var insertQuery = @"INSERT INTO hours(Id, Column1, Column2) VALUES (@id, 0, 0)";
                using var insertCommand = new SQLiteCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@id", i);
                insertCommand.ExecuteNonQuery();
            }
        }
        public static void HoursUpdate(float t, float bt, int difference)
        {
            string connectionString = @"Data Source=hours.db;Version=3;";
            DateTime now = DateTime.Now;
            int h = now.Hour;
            var updateQuery = "UPDATE Test SET detection_count = @t WHERE Id = @h+@difference;" +
                              "UPDATE Test SET forward_lean_count = @bt WHERE Id = @h+@difference";

            using var connection = new SQLiteConnection(connectionString);
            using var command = new SQLiteCommand(updateQuery, connection);

            // パラメータの追加
            command.Parameters.AddWithValue("@t", t);
            command.Parameters.AddWithValue("@h", h);
            command.Parameters.AddWithValue("@bt", bt);
            command.Parameters.AddWithValue("@difference", difference);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public static void MinutesDelete(float t)
        {
            for (int i = 0; i < t; i++)
            {
                string connectionString = @"Data Source=minutes.db;Version=3;";

                string deleteQuery = "DELETE FROM Test WHERE Id = @t";

                using var connection = new SQLiteConnection(connectionString);
                using var command = new SQLiteCommand(deleteQuery, connection);

                // パラメータの追加
                command.Parameters.AddWithValue("@t", t);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
