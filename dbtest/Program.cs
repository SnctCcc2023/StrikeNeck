using Microsoft.Data.Sqlite;
using Csv;
namespace dbtest
{
    public class DbTest
    {
        public static void Main()
        {
            DBAccessor.SaveResult(true, new DateTime(2020, 3, 5, 10, 0, 0));
            var res = DBAccessor.GetAnalyticsPerDay(new DateTime(2020, 3, 5, 10, 0, 0));
            foreach(var r in res)
            {
                Console.WriteLine(r.Date);
                Console.WriteLine(r.ActiveTime);
                Console.WriteLine(r.FowardLeanTime);
            }
        }
    }
}
