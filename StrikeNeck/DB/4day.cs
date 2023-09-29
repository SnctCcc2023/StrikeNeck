using System;
using System.Data.SQLite;

namespace day2
{
    public class Day
    {
        static int times_update = 0;
        private string connectionString = @"Data Source=days.db;Version=3;";
        public void InitializeDatabase()
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            var createTableQuery = @"CREATE TABLE IF NOT EXISTS Test(Id INTEGER, date DATE, detection_count REAL, forward_lean_count REAL)";
            using var command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        public static float Sum_detection_count(DateTime lt, int difference)
        {
            var connectionString = @"Data Source=hours.db;Version=3;";
            float day_detection_count = 0;
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            for (int i = difference; i < 24 + difference; i++)
            {
                string sql = "SELECT forward_lean_count FROM Test WHERE id = @i";
                using var command = new SQLiteCommand(sql, connection);

                // パラメータの追加
                command.Parameters.AddWithValue("@i", i);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    float day_detection_count_help = reader.GetFloat(0);
                    day_detection_count += day_detection_count_help;
                }
            }

            return day_detection_count;
        }

        public static float Sum_forward_lean_count(DateTime lt, int difference)
        {
            var connectionString = @"Data Source=hours.db;Version=3;";
            float day_forward_lean_count = 0;
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            for (int i = difference; i < 24 + difference; i++)
            {
                string sql = "SELECT forward_lean_count FROM Test WHERE id = @i";
                using var command = new SQLiteCommand(sql, connection);

                // パラメータの追加
                command.Parameters.AddWithValue("@i", i);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    float day_forward_lean_count_help = reader.GetFloat(0);
                    day_forward_lean_count += day_forward_lean_count_help;
                }
            }
            return day_forward_lean_count;

        }

        public static void DayInsert(float day_detection_count, float forward_lean_count)
        {
            var connectionString = @"Data Source=days.db;Version=3;";
            times_update++;

            var insertQuery = @"INSERT INTO Test(Id, date, detection_count, forward_lean_count) 
                                VALUES (@id, @date, @detection_count, @forward_lean_count)";
            DateTime today = DateTime.Today;

            using var connection = new SQLiteConnection(connectionString);
            using var command = new SQLiteCommand(insertQuery, connection);
            connection.Open();

            // パラメーターの追加
            command.Parameters.AddWithValue("@id", times_update);
            command.Parameters.AddWithValue("@date", today);
            command.Parameters.AddWithValue("@detection_count", day_detection_count);
            command.Parameters.AddWithValue("@forward_lean_count", forward_lean_count);

            command.ExecuteNonQuery();
        }
    }
}
