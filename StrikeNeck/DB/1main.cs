namespace DB;

using System;
using Microsoft.Data.SqlClient;


void minute(bool result, float tp);
void hour(float t, float bt, int difference);
void day(DateTime lt, int difference);

public class save_result(bool result)
{
    static float tph = 0;   //tphは1時間内における検知を何回行ったかを数える変数
    static float btph = 0;  //btphは1時間内における検知の結果前傾姿勢だった回数を数える変数
    static DateTime lt = nowDateTime.AddMinutes(-1); //ltは前回の検知を行った時間を記録しておく変数
    DayOfWeek today = date.DayOfWeek;   //今日の曜日を確認
    DateTime dt = DateTime.now; //今回の検知を行った時間を補完する変数
    int difference = 0;

    tph++;
   
    if(lt.Hour == dt.Hour)
    {
        minute(result, tph);
    }
    else if (lt.Hour != dt.Hour)
    {
        if(lt.Date == dt.Date)
        {
            switch(today)
            case 0:
                difference=0;
                break;
            case 1:
                difference=24;
                break;
            case 2:
                difference=48;
                break;
            case 3:
                difference=72;
                break;
            case 4:
                difference=96;
                break;
            case 5:
                difference=120;
                break;
            case 6:
                difference=144;
                break;
            hour(tph, btph, difference);
            tph = 1;
            bt = 0;
            minute(result, t);
        }
        else
        {
        switch(today)
            case 0:
                difference = 0;
                break;
            case 1:
                difference = 24;
                break;
            case 2:
                difference = 48;
                break;
            case 3:
                difference = 72;
                break;
            case 4:
                difference = 96;
                break;
            case 5:
                difference = 120;
                break;
            case 6:
                difference = 144;
                break;
        day(lt, difference);
        }
    }
    lt = DateTime.now;
    if (result)
    {
        btph++;
    }
}