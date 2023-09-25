namespace DB;

using System;
using System.Globalization;
using hour;
using Microsoft.Data.SqlClient;
using minute;

internal class Save_result
{
    float tph = 0;   //tphは1時間内における検知を何回行ったかを数える変数
    float btph = 0;  //btphは1時間内における検知の結果前傾姿勢だった回数を数える変数
    DateTime lt = System.DateTime.Now.AddMinutes(-1); //ltは前回の検知を行った時間を記録しておく変数
    Minute minute = new();

    private void ResultSaver(bool result)
    {
        DateTime dt = DateTime.Now; //今回の検知を行った時間を補完する変数
        DayOfWeek today = dt.DayOfWeek;   //今日の曜日を確認
        int difference = 0;

        tph++;

        if (lt.Hour == dt.Hour)
        {
            minute.MinuteInsert(result, tph);
        }
        else if (lt.Hour != dt.Hour)
        {
            if (lt.Date == dt.Date)
            {
                switch (today)
                {
                    case DayOfWeek.Sunday:
                        difference = 0;
                        break;
                    case DayOfWeek.Monday:
                        difference = 24;
                        break;
                    case DayOfWeek.Tuesday:
                        difference = 48;
                        break;
                    case DayOfWeek.Wednesday:
                        difference = 72;
                        break;
                    case DayOfWeek.Thursday:
                        difference = 96;
                        break;
                    case DayOfWeek.Friday:
                        difference = 120;
                        break;
                    case DayOfWeek.Saturday:
                        difference = 144;
                        break;
                }
                Hour.HoursUpdate(tph, btph, difference);
                Hour.MinutesDelete(tph);

                tph = 1;
                btph = 0;
                minute.MinuteInsert(result, tph);
            }
            else
            {
                switch (today)
                {
                case DayOfWeek.Sunday:
                    difference = 0;
                    break;
                case DayOfWeek.Monday:
                    difference = 24;
                    break;
                case DayOfWeek.Tuesday:
                    difference = 48;
                    break;
                case DayOfWeek.Wednesday:
                    difference = 72;
                    break;
                case DayOfWeek.Thursday:
                    difference = 96;
                    break;
                case DayOfWeek.Friday:
                    difference = 120;
                    break;
                case DayOfWeek.Saturday:
                    difference = 144;
                    break;
                }
                float Sum_detection = day.Day.Sum_detection_count(lt, difference);
                float day_forward_lean = day.Day.Sum_forward_lean_count(lt, difference);
                day.Day.DayInsert(Sum_detection, day_forward_lean);
            }
            lt = DateTime.Now;
            if (result)
            {
                btph++;
            }
        }
    }
}