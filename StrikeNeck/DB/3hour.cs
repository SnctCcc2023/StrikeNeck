namespace hour;
public class hour(float t, float bt)
{
    using Microsoft.Data.SqlClient;
    var connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=minutes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
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

    for(int i=0;i<24;i++)
    {
        hoursReset(i);
    }

    void hoursReset(int i)
    {
        var updateQuery = "UPDATE Test SET tph = 0 WHERE Id = i";
        var updateQuery = "UPDATE Test SET btph = 0 WHERE Id = i";

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(deleteQuery, connection))
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
