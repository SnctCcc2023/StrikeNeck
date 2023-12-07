using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using StrikeNeckDB.DataControl;

// クラスライブラリプロジェクトのコード
namespace StrikeNeckDB
{
    public class main
    {
        public int Add(int x, int y)
        {
            DBCreator tmp = new DBCreator();
            tmp.MinuteResultSaveDBCreator();
            tmp.HourResultSaveDBCreator();
            tmp.DayResultSaveDBCreator();
            return x + y;
        }
    }
}