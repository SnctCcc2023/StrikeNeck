namespace DB;

public static void minute(bool result, float tp);
public static void hour(float t, float bt);
public static void day(bool result);

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
            hour(tph, btph);
            t = 1;
            bt = 0;
            minute(result, t);
        }
        else
        {
            day();
        }
    }
    lt = DateTime.now;
    if (result)
    {
        btph++;
    }
}