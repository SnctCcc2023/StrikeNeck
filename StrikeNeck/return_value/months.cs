using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;


internal class Months_Returnee 
{
    Dictionary<List<int>, List<float>> MonthResult = new(); //これがDBの情報を持っている
    private static bool isFirstCall = true;
    private int times = 1;
    public void MonthsReturnee() //この関数の呼び出しによってメンバ変数を更新
    {
        if (isFirstCall)
        {
            times = 0;
            isFirstCall = false;
        }

        var help1 = new List<int>() { };
        var help2 = new List<float>() { };
        var day_coordinationner = 0;

        DateTime dt = DateTime.Now;
        DayOfWeek today = dt.DayOfWeek;

        var month_checker = new List<int>() { }; //下の方に説明あるよ
        var Result_month = new List<float>() { }; //下の方に説明あるよ
        var date = DateTime.Now;

        if (times == 0)
        {
            Dictionary<List<int>, List<float>> MonthResult = new();

            for (int i = 1; i <= 12; i++)
            {
                for (int j = 0; j <= 5; j++)
                {
                    List<int> key = new List<int> { i, j };
                    List<float> value = new List<float> { 0, 0 };
                    MonthResult[key] = value;
                }
            }

        }

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
                day_coordinationner=0;
                break;
            case DayOfWeek.Monday:
                day_coordinationner=1;
                break;
            case DayOfWeek.Tuesday:
                day_coordinationner=2;
                break;
            case DayOfWeek.Wednesday:
                day_coordinationner=3;
                break;
            case DayOfWeek.Thursday:
                day_coordinationner=4;
                break;
            case DayOfWeek.Friday:
                day_coordinationner=5;
                break;
            case DayOfWeek.Saturday:
                day_coordinationner=6;
                break;
        }

        SqlConnection connection = new(connectionString);
        connection.Open();
        
        for (int i = 0; i < 6; i++)
        {
            string sql = "SELECT day_detection_count,forward_lean_count FROM Table2 WHERE day = @date";
            using SqlCommand command = new(sql, connection);

            // パラメータの追加
            command.Parameters.AddWithValue("@date", date);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                month_checker[0] = dt.Month; //0が最新の週間
                month_checker[1] = i; //0が月初め、1がその次の週…
                MonthResult.Add(month_checker, Result_month);
            }
        }
        times = 1;
    }
}
    

