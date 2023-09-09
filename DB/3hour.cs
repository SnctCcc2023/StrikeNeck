namespace hour;
public class hour(bool result,float t, float bt)
{
    using Microsoft.Data.SqlClient;
    var connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=minutes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hours;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    hoursUpdate();

    float per = bt/t;
    per = per*100;

    void hoursUpdate()
    {
        var updateQuery = "UPDATE Test SET tph = t WHERE Id = ";
        var updateQuery = "UPDATE Test SET btph = bt WHERE Id = ";
        var updateQuery = "UPDATE Test SET persent = per WHERE Id = ";

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(updateQuery, connection))
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    for(var i=1;i<t+1;i++)
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

    t=1;
    bt=0;

    minute(result, t);

}
