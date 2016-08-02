//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 4.0.30319.42000
// * author  : tianxun
// * FileName: Program.cs
// * history : created by tianxun 2016-8-2 9:40:08 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Better517Na.Http.Helper;
using Better517Na.Json.Linq;
using Zuiyou.Business;
using Zuiyou.Common;
using Zuiyou.DAL;
using Zuiyou.Model;

namespace Zuiyou
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="args">args</param>
        public static void Main(string[] args)
        {
            RefreshThread refreshThread = new RefreshThread();

            ////Thread imgtxtThread = new Thread(new ParameterizedThreadStart(request.Zuiyou));
            //////imgtxtThread.Start("imgtxt");

            ////Thread videoThread = new Thread(new ParameterizedThreadStart(request.Zuiyou));
            ////videoThread.Start("video");

            Thread thread = new Thread(refreshThread.Refresh);

            Thread ramThread = new Thread(refreshThread.GenerateCheckCode);
            
            Thread enumThread = new Thread(new EnumThread().FenPeiTask);

            string taskNo = ConfigReader.StartThread;
            string[] arr = taskNo.Split(',');

            foreach (var item in arr)
            {
                switch (item)
                {
                    case "1":
                        thread.Start();
                        break;
                    case "2":
                        ramThread.Start();
                        break;
                    case "3":
                        enumThread.Start();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
