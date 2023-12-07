using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace NewDB.DataControl
{
    internal class SQLCommandExecuter
    {
        public void NoReturnCommandExecuter(string command)
        {
            string tmp = ConfigurationManager.ConnectionStrings["DB1"].ConnectionString;
            SqlConnection myConn = new SqlConnection(tmp);
            SqlCommand myCommand = new SqlCommand(command, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
    }
}