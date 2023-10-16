using System;
using UseDataControl.DataControl;

namespace DataManagement
{
    public class DataSaver
    {
        private DateTime now;
        private int dt_yyyyMMdd;
        private int now_yyyyMMdd;
        private int dt_yyyyMMddHH;
        private int now_yyyyMMddHH;
        private bool FirstTimeChecker = true;
        public void DataSave(bool result) //データのセーブの際に呼び出す。結果をbool型で渡す
        {
            now = DateTime.Now;
            now_yyyyMMdd = int.Parse(now.ToString("yyyyMMdd"));
            now_yyyyMMddHH = int.Parse(now.ToString("yyyyMMddHH"));
            if (FirstTimeChecker)
            {
                TableCreater tableCreatorInstance = new TableCreater();
                tableCreatorInstance.CreateTable(); //初回のみテーブルを作成する
                FirstTimeChecker = false;
                dt_yyyyMMdd = int.Parse(now.ToString("yyyyMMdd"));
                dt_yyyyMMddHH = int.Parse(now.ToString("yyyyMMddHH"));
            }
            else if (now_yyyyMMdd > dt_yyyyMMdd)
            {

                dt_yyyyMMddHH = int.Parse(now.ToString("yyyyMMddHH"));
            }
            else if(now_yyyyMMddHH > dt_yyyyMMddHH)
            {

            }
        }
    }
}