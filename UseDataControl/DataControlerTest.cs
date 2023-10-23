using System;
using System.Linq;
using strikeneck.DB_Result;

namespace UseDataControl
{
    internal class DataControlerTest
    {
        static void Main(string[] args)
        {
            SQLitePCL.Batteries.Init();
            DataControler Test = new DataControler();
            Test.DataSave(true);
            int result = Test.DataControl();
            Console.WriteLine(result);
        }
    }
}
