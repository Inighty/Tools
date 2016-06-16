//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5456
// * author  : tianxun
// * FileName: Program.cs
// * history : created by tianxun 2016-03-21 14:32:46 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Menu
{
    /// <summary>
    /// Class Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 主程序入口
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            Business.GroupMenu datas = new Business.GroupMenu();
            Console.WriteLine("开始随机组合...");
            while (true)
            {
                //datas.GroupDish();
                Thread.Sleep(200);
            }
        }
    }
}
