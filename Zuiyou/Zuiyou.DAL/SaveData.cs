//-----------------------------------------------------------------------
// <copyright file="SaveData.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 4.0.30319.42000
// * author  : tianxun
// * FileName: SaveData.cs
// * history : created by tianxun 2016-8-2 9:40:08 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Zuiyou.Common;
using Zuiyou.Model;

namespace Zuiyou.DAL
{
    /// <summary>
    /// 存储数据类
    /// </summary>
    public class SaveData
    {
        /// <summary>
        /// List转为sql语句
        /// </summary>
        /// <param name="list">数据列表</param>
        /// <returns>sql语句</returns>
        public int ListToSave(List<PosterModel> list)
        {
            StringBuilder sqlStr = new StringBuilder();
            foreach (var item in list)
            {
                string names = string.Empty;
                string values = string.Empty;
                string updateStr = string.Empty;
                Type t = item.GetType();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    string name = pi.Name;
                    names += name + ",";
                    ////用pi.GetValue获得值
                    object value1 = pi.GetValue(item, null);
                    if (value1.ToString().Contains("'"))
                    {
                        value1 = value1.ToString().Replace("'", "''");
                    }
                    values += "'" + value1.ToString() + "'" + ",";
                    updateStr += name + "=" + "'" + value1 + "',";
                }
                //if (string.IsNullOrEmpty(values) || string.IsNullOrEmpty(names))
                //{
                //    continue;
                //}
                values = values.Substring(0, values.Length - 1);
                names = names.Substring(0, names.Length - 1);
                updateStr = updateStr.Substring(0, updateStr.Length - 1);
                sqlStr.AppendFormat("insert into {0}({1}) values ({2}) ON DUPLICATE KEY UPDATE {3};\n", ConfigReader.TableName, names, values, updateStr);
            }

            SqlHelper sqlHelper = new SqlHelper(sqlStr.ToString());
            return sqlHelper.Insert();
        }
    }
}
