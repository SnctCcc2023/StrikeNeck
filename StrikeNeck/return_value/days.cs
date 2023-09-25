using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;


internal class Day_Returnee
{
    Dictionary<List<int>, List<float>> DayResult = new();
    private void DayReturnee()
    {
        DateTime dt = DateTime.Now;
        DayOfWeek today = dt.DayOfWeek;

        var day_checker = new List<int>() { };
        var Result_day = new List<float>() { };
        var date = DateTime.Now;

        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                 Initial Catalog=days;
                                 Integrated Security=True;
                                 Connect Timeout=30;Encrypt=False;
                                 Trust Server Certificate=False;
                                 Application Intent=ReadWrite;
                                 Multi Subnet Failover=False";
        // hoursのDBの接続文字列

        switch (today)
        {
            case DayOfWeek.Sunday:
                date = date.AddDays(0);
                break;
            case DayOfWeek.Monday:
                date = date.AddDays(-1);
                break;
            case DayOfWeek.Tuesday:
                date = date.AddDays(-2);
                break;
            case DayOfWeek.Wednesday:
                date = date.AddDays(-3);
                break;
            case DayOfWeek.Thursday:
                date = date.AddDays(-4);
                break;
            case DayOfWeek.Friday:
                date = date.AddDays(-5);
                break;
            case DayOfWeek.Saturday:
                date = date.AddDays(-6);
                break;
        }

        SqlConnection connection = new(connectionString);
        connection.Open();
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 7; i++)
            {
                string sql = "SELECT day_detection_count,forward_lean_count FROM Test WHERE day = @date";
                using SqlCommand command = new(sql, connection);

                // パラメータの追加
                command.Parameters.AddWithValue("@date", date);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.Read())
                    {
                        float float1 = reader.GetFloat(0);
                        float float2 = reader.GetFloat(1);
                        Result_day[0] = float1;
                        Result_day[1] = float2;
                        day_checker[0] = j;
                        day_checker[1] = i;
                        DayResult.Add(day_checker, Result_day);
                    }
                    else
                    {
                        float float1 = 0;
                        float float2 = 0;
                        Result_day[0] = float1;
                        Result_day[1] = float2;
                        day_checker[0] = j;
                        day_checker[1] = i;
                        DayResult.Add(day_checker, Result_day);
                    }
                }
                date = date.AddDays(1);
            }
            date = date.AddDays(-14);
        }
    }
}
    

