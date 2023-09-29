using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.Data.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace dbtest
{
    public class DBAccessor
    {
        public static void SaveResult(bool isFowardLeanPosture, DateTime dateTime) 
        {
            var list = SelectPerDay(dateTime);
            if(list.Count > 0)
            {
                var fldData = list[dateTime.Hour];
                if(isFowardLeanPosture) fldData.FLDTime += 1;
                
                fldData.ActivationTime += 1;
                Insert(fldData.Date, (byte)fldData.ActivationTime, (byte)fldData.FLDTime);
            }
            else
            {
                if(isFowardLeanPosture) Insert(dateTime, 1, 1);
                else Insert(dateTime, 1, 0);
            }
        }

        public static List<AnalyticsUnit> GetAnalyticsPerDay(DateTime dateTime)
        {
            var result = new List<AnalyticsUnit>();
            var fldDataList = SelectPerDay(dateTime);

            foreach(var fldData in fldDataList)
            {
                var analyticsUnit = new AnalyticsUnit();
                analyticsUnit.Date = fldData.Date;
                analyticsUnit.ActiveTime = fldData.ActivationTime;
                analyticsUnit.FowardLeanTime = fldData.FLDTime;
                result.Add(analyticsUnit);
            }
            return result;
        }

        public static List<AnalyticsUnit> GetAnalyticsPerWeek(DateTime dateTime)
        {
            var result = new List<AnalyticsUnit>();
            var fldDataList = SelectPerWeek(dateTime);

            foreach (var fldData in fldDataList)
            {
                var analyticsUnit = new AnalyticsUnit();
                analyticsUnit.Date = fldData.Date;
                analyticsUnit.ActiveTime = fldData.ActivationTime / 60.0f;
                analyticsUnit.FowardLeanTime = fldData.FLDTime / 60.0f;
                result.Add(analyticsUnit);
            }
            return result;
        }

        public static List<AnalyticsUnit> GetAnalyticsPerMonth(DateTime dateTime)
        {
            var result = new List<AnalyticsUnit>();
            var fldDataList = SelectPerMonth(dateTime);

            foreach (var fldData in fldDataList)
            {
                var analyticsUnit = new AnalyticsUnit();
                analyticsUnit.Date = fldData.Date;
                analyticsUnit.ActiveTime = fldData.ActivationTime / 60.0f;
                analyticsUnit.FowardLeanTime = fldData.FLDTime / 60.0f;
                result.Add(analyticsUnit);
            }
            return result;
        }



        public static void Create()
        {
            using (var connection = new SqliteConnection("Data Source=test/database.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS FLDDB ( Date datetime NOT NULL PRIMARY KEY, ActivationTime INTEGER(1) NOT NULL, FLDTime INTEGER(1) NOT NULL);";
                command.ExecuteNonQuery();
            }
        }

        private static void Insert(DateTime date, byte activationTime, byte fldTime)
        {
            using (var connection = new SqliteConnection("Data Source=test/database.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT OR REPLACE INTO FLDDB (Date, ActivationTime, FLDTime) VALUES (@Date, @ActivationTime, @FLDTime);";
                command.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd HH:mm"));
                command.Parameters.AddWithValue("@ActivationTime", activationTime);
                command.Parameters.AddWithValue("@FLDTime", fldTime);
                command.ExecuteNonQuery();
            }
        }

        private static List<FLDData> SelectPerDay(DateTime datetime)
        {
            var result = new List<FLDData>(24);
            using var connection = new SqliteConnection("Data Source=test/database.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT strftime('%Y-%m-%d %H', Date) AS YearMonth, SUM(ActivationTime) AS TotalActivationTime, SUM(FLDTime) AS TotalFLDTime FROM FLDDB WHERE Date >= @Beginning AND Date < @Ending GROUP BY YearMonth ORDER BY Date ASC;";

            var begin = DateTime.ParseExact(datetime.ToString("yyyy-MM-dd"), "yyyy-MM-dd", null).AddSeconds(-1);
            var end = DateTime.ParseExact(datetime.AddDays(1).ToString("yyyy-MM-dd"), "yyyy-MM-dd", null).AddSeconds(-1);


            command.Parameters.AddWithValue("@Beginning", begin);
            command.Parameters.AddWithValue("@Ending", end);
            using var reader = command.ExecuteReader();

            int max = 0;

            while(reader.Read())
            {
                var fldData = new FLDData();
                var dateStr = reader.GetString(0);
                var date = DateTime.ParseExact(dateStr, "yyyy-MM-dd HH", null);
                fldData.ActivationTime = reader.GetInt16(1);
                fldData.FLDTime = reader.GetInt16(2);
                result.Add(fldData);
            }
         
            return result;
        }

        private static List<FLDData> SelectPerWeek(DateTime datetime)
        {
            var result = new List<FLDData>();
            using var connection = new SqliteConnection("Data Source=test/database.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT strftime('%Y-%m-%d', Date) AS YearMonth, SUM(ActivationTime) AS TotalActivationTime, SUM(FLDTime) AS TotalFLDTime FROM FLDDB WHERE Date >= @Beginning AND Date <= @Ending GROUP BY YearMonth ORDER BY Date ASC;";

            var previousSunday = datetime.AddDays(-(int)datetime.DayOfWeek-1); // 直前の日曜日を取得
            var beginningOfWeek = previousSunday.AddDays(1); // 直前の日曜日の翌日を開始日とする
            command.Parameters.AddWithValue("@Beginning", beginningOfWeek);


            var nextSaturday = datetime.AddDays(6 - (int)datetime.DayOfWeek-1); // 直後の土曜日を取得
            var endOfWeek = nextSaturday.AddDays(1); // 直後の土曜日の翌日を終了日とする
            command.Parameters.AddWithValue("@Ending", endOfWeek);


            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var fldData = new FLDData();
                var dateStr = reader.GetString(0);
                fldData.Date = DateTime.ParseExact(dateStr, "yyyy-MM-dd", null);
                fldData.ActivationTime = reader.GetInt16(1);
                fldData.FLDTime = reader.GetInt16(2);
                result.Add(fldData);
            }

            return result;
        }


        private static List<FLDData> SelectPerMonth(DateTime datetime)
        {
            var result = new List<FLDData>();
            using var connection = new SqliteConnection("Data Source=test/database.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT strftime('%W', Date) AS WeekNumber, SUM(ActivationTime) AS TotalActivationTime, SUM(FLDTime) AS TotalFLDTime FROM FLDDB WHERE @begin <= Date AND Date < @end GROUP BY WeekNumber ORDER BY Date ASC;";
            var begin = DateTime.ParseExact(datetime.ToString("yyyy-MM"), "yyyy-MM", null);
            var end = begin.AddMonths(1);
            command.Parameters.AddWithValue("@begin", begin);
            command.Parameters.AddWithValue("@end", end);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var fldData = new FLDData();
                var dateW = reader.GetInt16(0);
                fldData.Date = GetDateTimeFromWeekOfYear(datetime.Year, dateW);
                fldData.ActivationTime = reader.GetInt16(1);
                fldData.FLDTime = reader.GetInt16(2);
                result.Add(fldData);
            }
            return result;
        }

        private static DateTime GetDateTimeFromWeekOfYear(int year, int weekOfYear)
        {
            var jan1 = new DateTime(year, 1, 1);
            var date = jan1.AddDays(7 * weekOfYear);
            return date.AddDays(-(int)date.DayOfWeek);
        }
    }

    enum DayOfWeek
    {
        SUN, MON, TUE, WED, THU, FRI, SAT
    }

    internal struct FLDData
    {
        public DateTime Date;
        public int ActivationTime;
        public int FLDTime;
    }

    public class AnalyticsUnit
    {
        public float ActiveTime;
        public float FowardLeanTime;
        public DateTime Date;
    }
}