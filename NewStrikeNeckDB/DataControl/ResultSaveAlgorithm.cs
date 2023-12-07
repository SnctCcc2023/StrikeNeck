using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace StrikeNeckDB.DataControl
{
    internal class ResultSaveAlgorithm
    {
        private int FunctionChooser = 0;
        private DateTime LastTime = DateTime.Now;
        private DBControler DBcontrol = new DBControler();
        public void ResultSave()
        {
            DateTime now = DateTime.Now;
            //if ()
            //{
            //    FunctionChooser = 1;
            //}
            //else if ()
            //{
            //    FunctionChooser = 2;
            //}
            //else
            //{
            //    FunctionChooser = 3;
            //}

            if (FunctionChooser == 1)
            {
                DBcontrol.MinuteResultSave();
            }
            else if (FunctionChooser == 2)
            {
                DBcontrol.HourResultSave();
                DBcontrol.MinuteResultReset();
            }
            else if (FunctionChooser == 3)
            {
                DBcontrol.DayResultSave();
                DBcontrol.MinuteResultReset();
            }
        }
    }
}