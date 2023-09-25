using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;


internal class Hour_Returnee
{
    Dictionary<List<int>, List<float>> HourResult = new();
    private void HourReturnee()
    {
        DateTime dt = DateTime.Now;
        int difference=0;
        DayOfWeek today = dt.DayOfWeek;

        var hour_checker = new List<int>() { };
        var Result_Hour = new List<float>() { };
        int rep_time_checker=0;

        switch (today)
        {
            case DayOfWeek.Sunday:
                difference = 0;
                rep_time_checker = 7;
                break;
            case DayOfWeek.Monday:
                difference = 24;
                rep_time_checker = 6;
                break;
            case DayOfWeek.Tuesday:
                difference = 48;
                rep_time_checker = 5;
                break;
            case DayOfWeek.Wednesday:
                difference = 72;
                rep_time_checker = 4;
                break;
            case DayOfWeek.Thursday:
                difference = 96;
                rep_time_checker = 3;
                break;
            case DayOfWeek.Friday:
                difference = 120;
                rep_time_checker = 2;
                break;
            case DayOfWeek.Saturday:
                difference = 144;
                rep_time_checker = 1;
                break;
        }

        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                        Initial Catalog=hours;
                        Integrated Security=True;
                        Connect Timeout=30;
                        Encrypt=False;
                        Trust Server Certificate=False;
                        Application Intent=ReadWrite;
                        Multi Subnet Failover=False";
        // hoursのDBの接続文字列
        SqlConnection connection = new(connectionString);
        connection.Open();
        for (int j = 0; j < 7; j++)
        {
            if(rep_time_checker == 0)
            {
                difference = 0;
            }
            for (int i = difference; i < 24 + difference; i++)
            {
                string sql = "SELECT day_detection_count,forward_lean_count FROM Test WHERE id = @i";
                using SqlCommand command = new(sql, connection);

                // パラメータの追加
                command.Parameters.AddWithValue("@i", i);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    float float1 = reader.GetFloat(0);
                    float float2 = reader.GetFloat(1);
                    Result_Hour[0] = float1;
                    Result_Hour[1] = float2;
                    hour_checker[0] = j;
                    hour_checker[1] = i-difference;
                    HourResult.Add(hour_checker, Result_Hour);
                }
            }
            rep_time_checker--;
            difference += 24;
        }
    }
}
