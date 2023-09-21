namespace minute;
public class minute(bool result, float tp)
{
    using Microsoft.Data.SqlClient;
    var connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=minutes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    minuteInsert(result);
    
    void minuteInsert(result)
    {
        var insertQuery = @"INSERT INTO minutes(Id, results)
                              VALUES (@id, @results)";
        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(insertQuery, connection))
        {
        connection.Open();

        command.Parameters.AddWithValue("@id", tp);
        command.Parameters.AddWithValue("@results", result);

        command.ExecuteNonQuery();
        }
    }
}
