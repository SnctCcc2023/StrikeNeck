namespace hour;

using System;
using Microsoft.Data.SqlClient;

public class Hour
{
    static void HoursUpdate(float t, float bt, int difference)
    {
    string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hours;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    DateTime now = DateTime.Now;
    int h = now.Hour;
    var updateQuery = "UPDATE Test SET detection_count = t WHERE Id = h+difference" +
                      "UPDATE Test SET forward_lean_count = bt WHERE Id = h+difference";

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(updateQuery, connection);
        connection.Open();
        command.ExecuteNonQuery();
    }

    static void MinutesDelete(float t)
    {
        for (int i = 0; i < t; i++)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=minutes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            string deleteQuery = "DELETE FROM Test WHERE Id = t";

            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(deleteQuery, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}