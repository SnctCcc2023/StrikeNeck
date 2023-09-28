namespace minute;

using System;
using Microsoft.Data.SqlClient;

internal class Minute
{
    private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=minutes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    public void MinuteInsert(bool result, float tp)
    {
        var insertQuery = @"INSERT INTO minutes(Id, results)
                              VALUES (@id, @results)";
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(insertQuery, connection);
        connection.Open();

        command.Parameters.AddWithValue("@id", tp);
        command.Parameters.AddWithValue("@results", result);

        command.ExecuteNonQuery();
    }
}
