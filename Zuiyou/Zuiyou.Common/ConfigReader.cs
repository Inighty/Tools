//-----------------------------------------------------------------------
// <copyright file="ConfigReader.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 4.0.30319.42000
// * author  : tianxun
// * FileName: ConfigReader.cs
// * history : created by tianxun 2016-8-2 13:45:15 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuiyou.Common
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
        /// 开启线程
        /// </summary>
        public static string StartThread
        {
            get { return GetConfigValue("StartThread", "1,2,3"); }
        }

        /// <summary>
        /// 起始ID
        /// </summary>
        public static int StartPoint
        {
            get { return GetConfigValue("StartPoint", 100000); }
        }

        /// <summary>
        /// 终点ID
        /// </summary>
        public static int EndPoint
        {
            get { return GetConfigValue("EndPoint", 99999999); }
        }

        /// <summary>
        /// 终点ID
        /// </summary>
        public static int ThreadNum
        {
            get { return GetConfigValue("ThreadNum", 4); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public static string TableName
        {
            get { return GetConfigValue("TableName", "zuiyoulist"); }
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
