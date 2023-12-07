using StrikeNeckDB.DataControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStrikeNeckDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBCreator tmp = new DBCreator();
            tmp.MinuteResultSaveDBCreator();
            tmp.HourResultSaveDBCreator();
            tmp.DayResultSaveDBCreator();
            
        }
    }
}
