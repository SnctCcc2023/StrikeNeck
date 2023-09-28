namespace Hour_Return;

using System.Data.SQLite;
using System;
using System.Collections.Generic;


public class Hour_Returnee
{
    public Dictionary<List<int>, List<float>> HourResult = new(); //これがDBの情報を持っている
    public void HourReturnee() //この関数の呼び出しによってメンバ変数を更新
    {
        DateTime dt = DateTime.Now;
        int difference = 0;
        DayOfWeek today = dt.DayOfWeek;

        var hour_checker = new List<int>() { };　//81,82行目に代入、説明あり
        var Result_Hour = new List<float>() { }; //83,84行目に代入、説明あり
        int rep_time_checker = 0;

        if (today == DayOfWeek.Sunday)
        {
            HourResult.Clear();
        }

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

        var connectionString = @"Data Source=hours.db;Version=3;";
        // hoursのDBの接続文字列
        SQLiteConnection connection = new(connectionString);
        connection.Open();
        for (int j = 0; j < 7; j++)
        {
            if (rep_time_checker == 0)
            {
                difference = 0;
            }
            for (int i = difference; i < 24 + difference; i++)
            {
                string sql = "SELECT day_detection_count,forward_lean_count FROM Test WHERE id = @i";
                using SQLiteCommand command = new(sql, connection);

                // パラメータの追加
                command.Parameters.AddWithValue("@i", i);

                using SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    float float1 = reader.GetFloat(0);
                    float float2 = reader.GetFloat(1);
                    Result_Hour[0] = float1; //起動時間
                    Result_Hour[1] = float2; //前傾姿勢だった時間
                    hour_checker[0] = j;　//0が日曜、1が月曜…
                    hour_checker[1] = i - difference; //これが12なら上の曜日の12時
                    if (HourResult.ContainsKey(hour_checker))
                    {
                        HourResult[hour_checker] = Result_Hour;
                    }
                    else
                    {
                        HourResult.Add(hour_checker, Result_Hour);
                    }
                }
            }
            rep_time_checker--;
            difference += 24;
        }
    }
}
