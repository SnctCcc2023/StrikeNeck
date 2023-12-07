using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using StrikeNeckDB.DataControl;

namespace StrikeNeckDB
{
    internal class Class1
    {
        static void main(string[] args)
        {
            DBCreator tmp = new DBCreator();
            tmp.MinuteResultSaveDBCreator();
            tmp.HourResultSaveDBCreator();
            tmp.DayResultSaveDBCreator();
        }
    }
}
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
