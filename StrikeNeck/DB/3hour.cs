namespace hour;
public class hour(float t, float bt, int diff)
{
    using Microsoft.Data.SqlClient;
    var connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=minutes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hours;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    hoursUpdate(diff);

    float per = bt/t;
    per = per*100;

    void hoursUpdate(int diff)
    {
        DateTime now = DateTime.now;
        int h = now.Hour;
        var updateQuery = "UPDATE Test SET tph = t WHERE Id = h+diff";
        var updateQuery = "UPDATE Test SET btph = bt WHERE Id = h+diff";
        var updateQuery = "UPDATE Test SET persent = per WHERE Id = h+diff";

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(updateQuery, connection))
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    for(var i=1;i<t+2;i++)
    {
        hoursDelete(i);
    }

    void hoursDelete(i)
    {
        var deleteQuery = "DELETE FROM Test WHERE Id = i";

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(deleteQuery, connection))
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
