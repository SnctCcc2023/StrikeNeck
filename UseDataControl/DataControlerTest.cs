using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using strikeneck.DB_Result;

namespace UseDataControl
{
    internal class DataControlerTest
    {
        static void Main(string[] args)
        {
            DataControler Test = new DataControler();
            int result = Test.DataControl();
            Console.WriteLine(result);
        }
    }
}
