using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataManagement;

namespace strikeneck.DB_Result
{
    public class DataControler
    {
        DataSavesAlgorithm test1 = new DataSavesAlgorithm();
        DataGetter test2 = new DataGetter();

        public void DataSave(bool result)
        {
            test1.ConditionalDataSave(result);
        }

        public int DataControl()
        {
            int result = test2.HourDataGet(2);
            return result;
        }
    }
}
