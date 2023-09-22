namespace hour;

using System;

public class hour(float t, float bt)
{
    using Microsoft.Data.SqlClient;
    var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hours;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    hoursUpdate();       

    void hoursUpdate()
    {
        DateTime now = DateTime.now;
        int h = now.Hour;
        var updateQuery = "UPDATE Test SET tph = t WHERE Id = h";
        var updateQuery = "UPDATE Test SET btph = bt WHERE Id = h";

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(updateQuery, connection))
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
