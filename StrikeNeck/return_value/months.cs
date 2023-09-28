namespace Month_Return;

using System.Data.SQLite;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;


public class Months_Returnee
{
    public Dictionary<List<int>, List<float>> MonthResult = new(); //これがDBの情報を持っている
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
        DateTime firstDayOfMonth = new(dt.Year, dt.Month, 1);
        DayOfWeek today = firstDayOfMonth.DayOfWeek;

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
                    List<int> key = new() { i, j };
                    List<float> value = new() { 0, 0, 0, 0 }; // 初期値を4つに変更
                    MonthResult[key] = value;
                }
            }
        }

        var connectionString = @"Data Source=days.db;Version=3;";
        // daysのDBの接続文字列

        switch (today)
        {
            case DayOfWeek.Sunday:
                day_coordinationner = 0;
                break;
            case DayOfWeek.Monday:
                day_coordinationner = 1;
                break;
            case DayOfWeek.Tuesday:
                day_coordinationner = 2;
                break;
            case DayOfWeek.Wednesday:
                day_coordinationner = 3;
                break;
            case DayOfWeek.Thursday:
                day_coordinationner = 4;
                break;
            case DayOfWeek.Friday:
                day_coordinationner = 5;
                break;
            case DayOfWeek.Saturday:
                day_coordinationner = 6;
                break;
        }

        SQLiteConnection connection = new(connectionString);
        connection.Open();

        float help3 = 0;
        float help4 = 0;

        for (int i = 0; i < 6; i++)
        {
            string sql = "SELECT day_detection_count,forward_lean_count FROM Table2 WHERE day = @date";
            using SQLiteCommand command = new(sql, connection);

            for (int j = 0; j < 7; j++)
            {
                if (j == 0)
                {
                    j = day_coordinationner;
                    date = firstDayOfMonth;
                    // パラメータの追加
                    command.Parameters.AddWithValue("@date", date);
                    using SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        help3 += reader.GetFloat(0);
                        help4 += reader.GetFloat(1);
                    }
                    else
                    {
                        help3 += 0;
                    }
                }
                else
                {

                    date = date.AddDays(1);

                    command.Parameters.AddWithValue("@date", date);
                    using SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        help3 += reader.GetFloat(0);
                        help4 += reader.GetFloat(1);
                    }
                    else
                    {
                        help3 += 0;
                    }
                }
            }

            month_checker[0] = dt.Month; //0が最新の週間
            month_checker[1] = i; //0が月初め、1がその次の週…
            Result_month[0] = help3;
            Result_month[1] = help4;

            DateTime sunday = firstDayOfMonth.AddDays(i * 7 - (int)firstDayOfMonth.DayOfWeek);
            DateTime saturday = sunday.AddDays(6);
            Result_month[2] = sunday.Day;
            Result_month[3] = saturday.Day;

            if (this.MonthResult.ContainsKey(month_checker))
            {
                this.MonthResult[month_checker] = Result_month;
            }
            else
            {
                this.MonthResult.Add(month_checker, Result_month);
            }
        }
        times = 1;
    }
}
