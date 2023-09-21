namespace DB;

public static void minute(bool result, float tp);
public static void hour(float t, float bt, int diff);
public static void day(bool result);
public static void weekend(bool result);
public static void month(bool result);
public static void year(bool result);

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
            switch(today){
                case0:
                    difference = 0;
                    break;
                case1:
                    difference = 24;
                    break;
                case2:
                    difference = 48;
                    break;
                case3:
                    difference = 72;
                    break;
                case4:
                    difference = 96;
                    break;
                case5:
                    difference = 120;
                    break;
                case6:
                    difference = 144;
                    break;
            }
            hour(tph, btph, difference);
            t = 1;
            bt = 0;
            minute(result, t);
        }
        else
        {
            day();
            if(today == DayOfWeek.Monday)
            {
                weekend();
            }
        }
    }
    lt = DateTime.now;
    if (result)
    {
        btph++;
    }
}