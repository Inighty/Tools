//-----------------------------------------------------------------------
// <copyright file="MenuList.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5456
// * author  : tianxun
// * FileName: MenuList.cs
// * history : created by tianxun 2016-03-21 14:32:46 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Common;
using Menu.Model;
using MySql.Data.MySqlClient;

namespace Menu.DAL
{
    /// <summary>
    /// 菜单列表类
    /// </summary>
    public class MenuList
    {
        /// <summary>
        /// 获取数据库地址
        /// </summary>
        /// private static string mysqlAddress = "Data Source=192.168.1.70;Port=3306;Database=test;User ID=writeuser;Password=writeuser;Charset=utf8";
        private static string mysqlAddress = ConfigReader.MysqlAddress;

        /// <summary>
        /// 初始化list
        /// </summary>
        private static List<Dish> list = null;
        private static List<string> listStore = null;

        /// <summary>
        /// 获取饭店
        /// </summary>
        /// <returns>菜单list</returns>
        public List<string> GetStore() {
            MySqlConnection conn = new MySqlConnection(mysqlAddress);
            string cmdstr = "select DISTINCT storeDes from dish where isdelete = 0";
            MySqlCommand cmd = new MySqlCommand(cmdstr, conn);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // 执行sql
                if (listStore != null)
                {
                    listStore.Clear();
                }
                else
                {
                    listStore = new List<string>();
                }

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    listStore.Add(row.ItemArray[0].ToString());
                }

                return listStore;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns>菜单list</returns>
        public List<Dish> GetData()
        {
            MySqlConnection conn = new MySqlConnection(mysqlAddress);
            string cmdstr = "select type,name,price from dish where isdelete = 0 and store = " + ConfigReader.Store;
            MySqlCommand cmd = new MySqlCommand(cmdstr, conn);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // 执行sql
                if (list != null)
                {
                    list.Clear();
                }
                else
                {
                    list = new List<Dish>();
                }

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Model.Dish dish = new Dish();
                    dish.Type = Convert.ToInt32(row.ItemArray[0]);
                    dish.Name = row.ItemArray[1].ToString();
                    dish.Price = Convert.ToInt32(row.ItemArray[2]);
                    list.Add(dish);
                }

                return list;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
    }
}
