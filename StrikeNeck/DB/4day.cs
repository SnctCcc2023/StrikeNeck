namespace day;

using System;

public class day
{
    var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hours;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    // hoursのDBの接続文字列


    DayOfWeek check = today.DayOfWeek;

    if(checked == DayOfWeek.Monday)
    {
        for(int i=0;i<168;i++)
        {
            hoursReset(i);
}
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
