//-----------------------------------------------------------------------
// <copyright file="TimerObtainThread.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 4.0.30319.42000
// * author  : tianxun
// * FileName: TimerObtainThread.cs
// * history : created by tianxun 2016-8-2 16:06:46 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Better517Na.Http.Helper;
using Better517Na.Json.Linq;
using Zuiyou.Common;
using Zuiyou.DAL;
using Zuiyou.Model;

namespace Zuiyou.Business
{
    /// <summary>
    /// TimerObtainThread类 
    /// </summary>
    public class TimerObtainThread
    {
        /// <summary>
        /// 获取表名
        /// </summary>
        private string tableName = ConfigReader.TableName;

        /// <summary>
        /// 临时变量 存储最新ID
        /// </summary>
        private int temp = 0;

        /// <summary>
        /// 定时获取
        /// </summary>
        public void TimerObtain()
        {
            List<PosterModel> posterList = new List<PosterModel>();
            SaveData saveData = new SaveData();
            string sqlStr = string.Format("select id from {0} order by ctime desc limit 1", tableName);
            SqlHelper sqlHelper = new SqlHelper(sqlStr);
            while (true)
            {
                temp = sqlHelper.GetNewestData();
                for (int i = temp + 1; i < temp + 20; i++)
                {
                    RequestMethod requestMethod = new RequestMethod();
                    HttpResult result = requestMethod.RequstMethod(i);

                    if (result != null)
                    {
                        JObject obj = JObject.Parse(result.Html);
                        if (obj["ret"].ToString() == "1" && obj["data"] != null)
                        {
                            ////Console.WriteLine(i);
                            JToken data = obj["data"]["post"];
                            Tools changeToModel = new Tools();
                            posterList.Add(changeToModel.GetModel(data));
                        }
                    }
                }
                if (posterList.Count != 0)
                {
                    int count = saveData.ListToSave(posterList);
                    Console.BackgroundColor = Console.BackgroundColor;
                    Console.SetCursorPosition(0, 3 * ConfigReader.ThreadNum + 1);
                    Console.WriteLine(DateTime.Now + " 定时扫描线程获取到" + posterList.Count + "条数据");
                }

                Thread.Sleep(60000);
            }
        }


    }
}
