namespace hour;

using System;

public class hour(float t, float bt, int difference)
{
    using Microsoft.Data.SqlClient;
    var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hours;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    hoursUpdate(difference);       

    void hoursUpdate(int difference)
    {
        DateTime now = DateTime.now;
        int h = now.Hour;
        var updateQuery = "UPDATE Test SET detection_count = t WHERE Id = h+difference";
        var updateQuery = "UPDATE Test SET forward_lean_count = bt WHERE Id = h+difference";

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(updateQuery, connection))
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
