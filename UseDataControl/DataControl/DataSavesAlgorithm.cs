using System;
using System.Diagnostics;
using System.Xml.Linq;
using UseDataControl.DataControl;

namespace DataManagement
{
    public class DataSavesAlgorithm
    {
        private DateTime now;
        private int LastTime_yyyyMMddHH;
        private int LastTime_yyyyMMdd;
        private int now_yyyyMMddHH;
        private int now_yyyyMMdd;
        private int now_mm;
        private bool FirstTimeChecker = true;
        public void ConditionalDataSave(bool result) //データのセーブの際に呼び出す。結果をbool型で渡す
        {
            now = DateTime.Now;
            now_mm = int.Parse(now.ToString("mm")); 
            now_yyyyMMdd = int.Parse(now.ToString("yyyyMMdd"));
            now_yyyyMMddHH = int.Parse(now.ToString("yyyyMMddHH"));
            if (FirstTimeChecker)
            {
                TableCreater tableCreatorInstance = new TableCreater();
                tableCreatorInstance.CreateTable(); //初回のみテーブルを作成する
                FirstTimeChecker = false;
                LastTime_yyyyMMdd = int.Parse(now.ToString("yyyyMMdd"));
                LastTime_yyyyMMddHH = int.Parse(now.ToString("yyyyMMddHH"));
            }
            else if (now_yyyyMMdd > LastTime_yyyyMMdd)
            {

                LastTime_yyyyMMdd = int.Parse(now.ToString("yyyyMMdd"));
                LastTime_yyyyMMddHH = int.Parse(now.ToString("yyyyMMddHH"));
            }
            else if (now_yyyyMMddHH > LastTime_yyyyMMddHH)
            {

                LastTime_yyyyMMddHH = int.Parse(now.ToString("yyyyMMddHH"));
            }
            else
            {
                SaveMinuteResultToHourlyData(result);
            }
        }

        private void SaveMinuteResultToHourlyData(bool result)
        {
            // レコードの登録
            var query = "INSERT INTO HourlyData (DATETIME,RESULT) VALUES (" +
                $"{now_mm},{result})";

            // クエリー実行
            SQLiteCommandExecutor SaveMinuteResulter = new SQLiteCommandExecutor();
            SaveMinuteResulter.RunNonQueryCommand(query.ToString());
        }
    }
}