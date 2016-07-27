using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuiyou.DAL
{
    public class SaveData
    {
        private static MySqlConnection mysqlcon = null;

        public int getmysqlcom(string M_str_sqlstr)
        {
            string M_str_sqlcon = "server=localhost;user id=root;password=mc0321..;database=test"; //根据自己的设置
            using (mysqlcon = new MySqlConnection(M_str_sqlcon))
            {
                int count = 0;
                if (mysqlcon.State == System.Data.ConnectionState.Closed)
                {
                    mysqlcon.Open();
                }
                MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
                try
                {
                    count = mysqlcom.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
                mysqlcom.Dispose();
                mysqlcon.Close();
                mysqlcon.Dispose();
                return count;
            }
        }
    }
}
