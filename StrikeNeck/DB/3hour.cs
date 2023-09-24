namespace hour;

using System;
using Microsoft.Data.SqlClient;

void hoursUpdate(int difference);
void minutesDelete(float t);

public class hour(float t, float bt, int difference)
{
    using Microsoft.Data.SqlClient;
    var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hours;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    hoursUpdate(difference);
    minutesDelete(t);
   
}
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

void minutesDelete(float t);
{
    for (int i = 0; i < t; i++)
    {
        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=minutes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"

        var deleteQuery = "DELETE FROM Test WHERE Id = t";

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(deleteQuery, connection))
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}