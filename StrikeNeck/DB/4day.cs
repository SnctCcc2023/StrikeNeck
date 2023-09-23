namespace day;

using System;

public class day
{
    var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=days;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    // daysのDBの接続文字列

    dayInsert();

    void dayInsert()
    {
        static int times_update = 0;
        times_update++;

        var insertQuery = @"INSERT INTO Test(Id, date, detection_count, forward_lean_count) 
                                VALUES (@id, @date, @detection_count, @forward_lean_count)";
        DateTime today = DateTime.Today;
        var age = "28";
        var birthday = "1993/5/5";

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(insertQuery, connection))
        {
            connection.Open();

            // パラメーターの追加
            command.Parameters.AddWithValue("@id", times_update);
            command.Parameters.AddWithValue("@date", today);
            command.Parameters.AddWithValue("@detection_count", );
            command.Parameters.AddWithValue("@forward_lean_count", );

            command.ExecuteNonQuery();

        }


    DayOfWeek check = today.DayOfWeek;

    if(check == DayOfWeek.Monday)
    {
        for(int i=0;i<168;i++)
        {
            hoursReset(i);
}
    }

    void hoursReset(int i)
    {

        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hours;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
        // hoursのDBの接続文字列

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
