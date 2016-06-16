//-----------------------------------------------------------------------
// <copyright file="ConfigReader.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5456
// * author  : tianxun
// * FileName: ConfigReader.cs
// * history : created by tianxun 2016-03-21 14:32:46 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Common
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class ConfigReader
    {
        /// <summary>
        /// 配置参数字典
        /// </summary>
        private static Dictionary<string, string> config = new Dictionary<string, string>();

        /// <summary>
        /// 素菜个数
        /// </summary>
        public static int SuCount
        {
            get { return GetConfigValue("SuCount", 2); }
        }

        /// <summary>
        /// 荤菜个数
        /// </summary>
        public static int HunCount
        {
            get { return GetConfigValue("HunCount", 2); }
        }

        /// <summary>
        /// 饭店
        /// </summary>
        public static int Store
        {
            get { return GetConfigValue("Store", 0); }
        }

        /// <summary>
        /// 最大总价
        /// </summary>
        public static int SumMaxPrice
        {
            get { return GetConfigValue("SumMaxPrice", 90); }
        }

        /// <summary>
        /// 最小总价
        /// </summary>
        public static int SumMinPrice
        {
            get { return GetConfigValue("SumMinPrice", 85); }
        }

        /// <summary>
        /// 数据库地址
        /// </summary>
        public static string MysqlAddress
        {
            get { return GetConfigValue("MysqlAddress", "Data Source=192.168.1.70;Port=3306;Database=test;User ID=writeuser;Password=writeuser;Charset=utf8"); }
        }

        /// <summary>
        /// 获取配置文件参数
        /// </summary>
        /// <typeparam name="T">参数值类型</typeparam>
        /// <param name="name">参数名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        private static T GetConfigValue<T>(string name, T defaultValue)
        {
            try
            {
                if (string.IsNullOrEmpty(config.ContainsKey(name) ? config[name] : string.Empty))
                {
                    config[name] = ConfigurationManager.AppSettings[name];
                }

                T value = (T)Convert.ChangeType(config[name], typeof(T));
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return defaultValue;
                }
                else
                {
                    return value;
                }
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}