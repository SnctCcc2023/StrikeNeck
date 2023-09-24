namespace day;

using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

float sum_detection_count(DateTime lt, int difference);
float sum_forward_lean_count(DateTime lt, int difference);
void dayInsert(float day_detection_count, float forward_lean_count);
void hoursReset();

public class day(DateTime lt, int difference)
{
    float day_result = sum_hoursResult(lt);
    dayInsert(day_result);
    hoursReset();

    float sum_detection_count(DateTime lt)
    {
        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                Initial Catalog=hours;
                                Integrated Security=True;
                                Connect Timeout=30;
                                Encrypt=False;
                                Trust Server Certificate=False;
                                Application Intent=ReadWrite;
                                Multi Subnet Failover=False"
                                // hoursのDBの接続文字列
        float day_detection_count;
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        for (int i = difference; i < 24; i++)
        {
            float day_detection_count_help = "SELECT detection_count FROM Test WHERE id i";
            day_detection_count += day_detection_count_help;
        }
        return day_detection_count;
    }

    float sum_forward_lean_count(DateTime lt, int difference)
    {
        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                Initial Catalog=hours;
                                Integrated Security=True;
                                Connect Timeout=30;
                                Encrypt=False;
                                Trust Server Certificate=False;
                                Application Intent=ReadWrite;
                                Multi Subnet Failover=False"
                                // hoursのDBの接続文字列
        float day__forward_lean_count;
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        for (int i = difference; i < 24; i++)
        {
            float day_detection_count_help = "SELECT forward_lean_count FROM Test WHERE id i";
            day__forward_lean_count += day__forward_lean_count_help;
        }
        return day__forward_lean_count;
    }

    void dayInsert(float day_detection_count, float forward_lean_count)
    {
        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                            Initial Catalog=days;
                            Integrated Security=True;
                            Connect Timeout=30;
                            Encrypt=False;
                            Trust Server Certificate=False;
                            Application Intent=ReadWrite;
                            Multi Subnet Failover=False"
                            // daysのDBの接続文字列
        static int times_update = 0;
        times_update++;

        var insertQuery = @"INSERT INTO Test(Id, date, detection_count, forward_lean_count) 
                                VALUES (@id, @date, @detection_count, @forward_lean_count)";
        DateTime today = DateTime.Today;

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(insertQuery, connection))
        {
            connection.Open();

            // パラメーターの追加
            command.Parameters.AddWithValue("@id", times_update);
            command.Parameters.AddWithValue("@date", today);
            command.Parameters.AddWithValue("@detection_count", day_detection_count);
            command.Parameters.AddWithValue("@forward_lean_count", forward_lean_count);

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

        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                Initial Catalog=hours;
                                Integrated Security=True;
                                Connect Timeout=30;
                                Encrypt=False;
                                Trust Server Certificate=False;
                                Application Intent=ReadWrite;
                                Multi Subnet Failover=False"
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
