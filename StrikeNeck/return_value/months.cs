using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;


internal class Months_Returnee
{
    Dictionary<List<int>, List<float>> MonthResult = new();
    private static bool isFirstCall = true;
    private int times = 1;
    public void MonthsReturnee()
    {
        if (isFirstCall)
        {
            times = 0;
            isFirstCall = false;
        }

        var help1 = new List<int>() { };
        var help2 = new List<float>() { };

        if (times==0) 
        {
            help1[0] = 1;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 2;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 3;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 4;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 5;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 6;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 7;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 8;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 9;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 10;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 11;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
            help1[0] = 12;
            help2[0] = 0;
            help2[1] = 0;
            MonthResult.Add(help1, help2);
        }

        DateTime dt = DateTime.Now;
        DayOfWeek today = dt.DayOfWeek;

        var month_checker = new List<int>() { }; //下の方に説明あるよ
        var Result_month = new List<float>() { }; //下の方に説明あるよ
        var date = DateTime.Now;

        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                 Initial Catalog=days;
                                 Integrated Security=True;
                                 Connect Timeout=30;
                                 Encrypt=False;
                                 Trust Server Certificate=False;
                                 Application Intent=ReadWrite;
                                 Multi Subnet Failover=False";
                                 // daysのDBの接続文字列

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
                string sql = "SELECT day_detection_count,forward_lean_count FROM Table2 WHERE day = @date";
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
                        Result_month[0] = float1/60; //アプリが何時間起動していたか
                        Result_month[1] = float2/60; //前傾姿勢が何時間だったか 
                        month_checker[0] = j; //0が最新の週間
                        month_checker[1] = i; //0が日曜、1が月曜…
                        MonthResult.Add(month_checker, Result_month);
                    }
                    else
                    {
                        Result_month[0] = 0; //アプリが何時間起動していたか
                        Result_month[1] = 0; //前傾姿勢が何時間だったか
                        month_checker[0] = j; //0が最新の週間
                        month_checker[1] = i; //0が日曜、1が月曜…
                        MonthResult.Add(month_checker, Result_month);
                    }
                }
                date = date.AddDays(1);
            }
            date = date.AddDays(-14);
        }
        times = 1;
    }
}
    

