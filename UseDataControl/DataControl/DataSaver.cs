using UseDataControl.DataControl;

namespace DataManagement
{
    public class DataSaver
    {
        private bool FirstTimeChecker = true;
        public void DataSave(bool result) //データのセーブの際に呼び出す。結果をbool型で渡す
        {
            if (FirstTimeChecker)
            {
                TableCreater tableCreatorInstance = new TableCreater();
                tableCreatorInstance.CreateTable(); //初回のみテーブルを作成する
                FirstTimeChecker = false;
            }
        }
    }
}