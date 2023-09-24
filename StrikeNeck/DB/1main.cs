namespace DB;

using System;
using System.Globalization;
using Microsoft.Data.SqlClient;

static class Save_result(bool result)
{
    static Save_result(result)
    {
        static float tph = 0;   //tphは1時間内における検知を何回行ったかを数える変数
        static float btph = 0;  //btphは1時間内における検知の結果前傾姿勢だった回数を数える変数
        static DateTime lt = System.DateTime.Now.AddMinutes(-1); //ltは前回の検知を行った時間を記録しておく変数
        DayOfWeek today = date.DayOfWeek;   //今日の曜日を確認
        DateTime dt = DateTime.Now; //今回の検知を行った時間を補完する変数
        int difference = 0;

        tph++;

        if (lt.Hour == dt.Hour)
        {
            Minute(result, tph);
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
                Hour(tph, btph, difference);
                tph = 1;
                btph = 0;
                Minute(result, tph);
            }
            else
            {
                switch (today)
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
                Day(lt, difference);
            }
            lt = DateTime.Now;
            if (result)
            {
                btph++;
            }
        }
    }
}