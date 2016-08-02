//-----------------------------------------------------------------------
// <copyright file="EnumThread.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 4.0.30319.42000
// * author  : tianxun
// * FileName: EnumThread.cs
// * history : created by tianxun 2016-8-2 13:45:15 
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
    /// EnumThread类 
    /// </summary>
    public class EnumThread
    {
        /// <summary>
        /// 起始点
        /// </summary>
        private static int startpoint = ConfigReader.StartPoint;

        /// <summary>
        /// 线程数
        /// </summary>
        private static int threadNum = ConfigReader.ThreadNum;

        /// <summary>
        /// 终结点
        /// </summary>
        private static int endpoint = ConfigReader.EndPoint;

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object lk = new object();

        /// <summary>
        /// 分配任务
        /// </summary>
        public void FenPeiTask()
        {
            for (int i = 0; i < threadNum; i++)
            {
                int yu = (endpoint - startpoint) % threadNum;
                int shang = ((endpoint - startpoint) + 1) / threadNum;
                int start = startpoint + (shang * i);
                int end = yu != 0 && i == threadNum - 1 ? endpoint : startpoint + (shang * (i + 1)) - 1;
                ////为了能传递多个参数给线程，将数据封装为一个int[]传递
                int[] para = new int[3] { start, end, i };
                Thread enumThread = new Thread(new ParameterizedThreadStart(this.Method));
                enumThread.Start(para);
            }
        }

        /// <summary>
        /// 接收数据，执行方法
        /// </summary>
        /// <param name="o">数据对象</param>
        private void Method(object o)
        {
            ////此处对传进来的参数进行处理
            int[] p = (int[])o;

            ////调用GetPostEnum方法
            EnumThread enumThread = new EnumThread();
            enumThread.GetPostEnum(p[0], p[1], p[2]);
        }
        
        /// <summary>
        /// 根据起始结束点循环执行请求，解析返回数据存入数据库（根据帖子ID递增请求）
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        /// <param name="threadId">线程ID</param>
        private void GetPostEnum(int start, int end, int threadId)
        {
            try
            {
                SaveData saveData = new SaveData();
                ////每条线程 控制台打印必须加锁，以防光标位置混乱
                lock (lk)
                {
                    Console.SetCursorPosition(0, threadId == 0 ? 0 : threadId * 3);
                    Console.WriteLine("******线程{0}******", threadId);

                    Console.BackgroundColor = ConsoleColor.DarkCyan;

                    for (int i = 0; i < Console.WindowWidth - 3; i++)
                    {
                        Console.Write(" ");
                    }

                    Console.WriteLine(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("0%");
                }

                for (int i = start; i <= end; i++)
                {
                    ////for循环外不加锁，每一次循环中加锁
                    lock (lk)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;

                        ////计算到达进度设置光标位置
                        Console.SetCursorPosition(Convert.ToInt32((i - start) * 1.0 * 100 / (end - start) * (Console.WindowWidth - 2) / 100), (threadId * 3) + 1);

                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(0, (threadId * 3) + 2);
                        Console.Write("{0}%,id:{1}", Convert.ToDouble((i - start) * 1.0 / (end - start) * 100).ToString("0.00"), i);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    List<PosterModel> posterList = new List<PosterModel>();
                    HttpResult result = null;
                    HttpRequestParam param = new HttpRequestParam();
                    param.URL = "http://tbapi.ixiaochuan.cn/post/detail";
                    param.Method = "POST";

                    ////param.Postdata = "{\"h_ts\":" + ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000) + ",\"h_av\":\"2.6.0\",\"h_nt\":1,\"h_m\":0,\"h_did\":\""+GenerateCheckCode()+"_08:60:7E\"}";
                    param.Postdata = "{\"h_dt\":0,\"h_av\":\"2.6.0\",\"pid\":" + i + ",\"from\":\"index\",\"h_nt\":1}";
                    param.KeepAlive = true;
                    param.UserAgent = "tieba/20160715.184411(iPhone;IOS 10.0;Scale/2.00)";
                    ////proxyInfo = ProxyHelper.GetProxyInfo("Rightest_Grab");
                    try
                    {
                        result = HttpHelper.GetHttpRequestData(param);
                    }
                    catch
                    {
                    }

                    if (result != null)
                    {
                        JObject obj = JObject.Parse(result.Html);
                        if (obj["ret"].ToString() == "1" && obj["data"] != null)
                        {
                            ////Console.WriteLine(i);
                            JToken data = obj["data"]["post"];
                            ChangeToModel changeToModel = new ChangeToModel();
                            posterList.Add(changeToModel.GetModel(data));
                        }

                        if (posterList.Count != 0)
                        {
                            int count = saveData.ListToSave(posterList);
                        }
                    }
                    
                    Thread.Sleep(TimeSpan.FromMilliseconds(200));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
    }
}
