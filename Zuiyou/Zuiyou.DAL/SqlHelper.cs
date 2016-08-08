//-----------------------------------------------------------------------
// <copyright file="SqlHelper.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 4.0.30319.42000
// * author  : tianxun
// * FileName: SqlHelper.cs
// * history : created by tianxun 2016-8-2 9:40:08 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Zuiyou.DAL
{
    /// <summary>
    /// SqlHelper类 
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private static MySqlConnection conn = null;

        /// <summary>
        /// 数据库
        /// </summary>
        private static string connStr = "server=localhost;user id=root;password=mc0321..;database=test"; 

        /// <summary>
        /// sql语句
        /// </summary>
        private string sqlStr = string.Empty;

        /// <summary>
        /// sql命令
        /// </summary>
        private MySqlCommand connCmd = null;

        private object obj = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        public SqlHelper(string sqlStr)
        {
            try
            {
                this.sqlStr = sqlStr;
            }
            catch (Exception e) {
                Console.Write(sqlStr);
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns>影响行数</returns>
        public int Insert()
        {
            lock (obj)
            {
                using (conn =new MySqlConnection(connStr))
                {
                    int count = 0;
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    this.connCmd = new MySqlCommand(this.sqlStr, conn);
                    try
                    {
                        count = this.connCmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                    }
                    return count;
                }
            }
        }

        /// <summary>
        /// 获取最新数据
        /// </summary>
        /// <returns>id</returns>
        public int GetNewestData()
        {
            lock (obj)
            {
                int id = 0;
                using (conn = new MySqlConnection(connStr))
                {
                    
                    try
                    {
                        if (conn.State == System.Data.ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        this.connCmd = new MySqlCommand(this.sqlStr, conn);
                        MySqlDataReader reader = this.connCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader[0]);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }

                return id;
            }
        }
    }
}
