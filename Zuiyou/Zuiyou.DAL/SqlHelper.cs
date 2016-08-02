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
        private MySqlConnection conn = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conn">数据库连接</param>
        public SqlHelper(MySqlConnection conn)
        {
            this.conn = conn;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <returns>影响行数</returns>
        public int Insert(string sqlStr)
        {
            using (this.conn)
            {
                int count = 0;
                if (this.conn.State == System.Data.ConnectionState.Closed)
                {
                    this.conn.Open();
                }

                MySqlCommand connCmd = new MySqlCommand(sqlStr, this.conn);
                try
                {
                    count = connCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }

                connCmd.Dispose();
                this.conn.Close();
                this.conn.Dispose();
                return count;
            }
        }
    }
}
