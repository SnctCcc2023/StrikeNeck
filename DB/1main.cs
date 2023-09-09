namespace DB;

public static void minute(bool result, float tp);
public static void hour(bool result, float t, float bt);
public static void day(bool result);
public static void weekend(bool result);
public static void month(bool result);
public static void year(bool result);

public class Class1(bool result)
{
    static float tph = 0;
    static float btph = 0;
    static DateTime? lt = null;
    DayOfWeek today = date.DayOfWeek;
    DateTime dt = DateTime.now;

    tph++;
    
   
    if(lt.Hour == dt.Hour)
    {
        minute(result, tph);
    }
    if(lt.Hour == null)
    {
        minute(result, tph); 
    }
    else if (lt.Hour != dt.Hour)
    {
        if(lt.Date == dt.Date)
        {
            hour(result, tph, btph);
        }
        else
        {
            day();
            if(today == DayOfWeek.Monday && lt.Month != dt)
            {
                weekwnd();
                month();
                if(lt.year == dt.year)
                {

                }
                else
                {
                    year();
                }
            }
            else if(today == DayOfWeek.Monday)
            {
                weekend();
            }
            else if(lt.Month != dt)
            {
                month();
                if (lt.year == dt.year)
                {

                }
                else
                {
                    year();
                }
            }
        }
    }
    lt = DateTime.now;
    if (result)
    {
        btph++;
    }
    else
    {

    }
}

