using System;
using Microsoft.Data.Sqlite;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using NewDB.DataControl;

namespace StrikeNeckDB.DataControl
{
    public class DBCreator
    {
        SQLCommandExecuter tmp = new SQLCommandExecuter();
        public void MinuteResultSaveDBCreator()
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            query.Append("CREATE TABLE IF NOT EXISTS HourlyData(");
            query.Append("DATETIME TEXT NOT NULL");
            query.Append(",RESULT INTEGER NOT NULL");
            query.Append(",primary key(DATETIME)");
            query.Append(");");

            tmp.ExecuteNonQuery(query.ToString());
        }

        public void HourResultSaveDBCreator()
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            query.Append("CREATE TABLE IF NOT EXISTS DailyData(");
            query.Append("DATETIME TEXT NOT NULL");
            query.Append(",UptimeMinute INTEGER NOT NULL");
            query.Append(",ForwardLeanMinute INTEGER NOT NULL");
            query.Append(",primary key(DATETIME)");
            query.Append(");");

            tmp.ExecuteNonQuery(query.ToString());
        }

        public void DayResultSaveDBCreator()
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            query.Append("CREATE TABLE IF NOT EXISTS YearlyData(");
            query.Append("DATETIME TEXT NOT NULL");
            query.Append(",UptimeHour INTEGER NOT NULL");
            query.Append(",ForwardLeanHour NOT NULL");
            query.Append(",primary key(DATETIME)");
            query.Append(");");
        }
    }
}