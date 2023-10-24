using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseDataControl.DataControl
{
    class TableCreater
    {
        public void CreateHourlyTable()
        {
            StringBuilder queryHours = new StringBuilder();
            queryHours.Clear();
            queryHours.Append("CREATE TABLE IF NOT EXISTS HourlyData(");
            queryHours.Append("DATETIME TEXT NOT NULL");
            queryHours.Append(", RESULT INTEGER NOT NULL");
            queryHours.Append(", primary key(DATETIME)");
            queryHours.Append(")");

            SQLiteCommandExecutor HourlyDataTableCreator = new SQLiteCommandExecutor();
            HourlyDataTableCreator.RunNonQueryCommand(queryHours.ToString());
        }


        public void CreateDailyTable()
        {
            StringBuilder queryDaily = new StringBuilder();
            queryDaily.Clear();
            queryDaily.Append("CREATE TABLE IF NOT EXISTS DailyData(");
            queryDaily.Append(" DATETIME TEXT NOT NULL");
            queryDaily.Append(", UptimeMinute INTEGER NOT NULL");
            queryDaily.Append(", ForwardLeanMinute INTEGER NOT NULL");
            queryDaily.Append(", primary key(DATETIME)");
            queryDaily.Append(")");

            SQLiteCommandExecutor DailyDataTableCreator = new SQLiteCommandExecutor();
            DailyDataTableCreator.RunNonQueryCommand(queryDaily.ToString());
        }


        public void CreateYearlyTable()
        {
            StringBuilder queryYearly = new StringBuilder();
            queryYearly.Clear();
            queryYearly.Append("CREATE TABLE IF NOT EXISTS HourlyData(");
            queryYearly.Append("DATETIME TEXT NOT NULL");
            queryYearly.Append(", UptimeHour INTEGER NOT NULL");
            queryYearly.Append(", ForwardLeanHour");
            queryYearly.Append(", primary key(DATETIME)");
            queryYearly.Append(")");

            SQLiteCommandExecutor YearlyDataTableCreator = new SQLiteCommandExecutor();
            YearlyDataTableCreator.RunNonQueryCommand(queryYearly.ToString());
        }
    }
}
