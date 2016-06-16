//-----------------------------------------------------------------------
// <copyright file="GroupMenu.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5456
// * author  : tianxun
// * FileName: GroupMenu.cs
// * history : created by tianxun 2016-03-21 14:32:46 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menu.Common;
using Menu.Model;
using MySql.Data.MySqlClient;

namespace Menu.Business
{
    /// <summary>
    /// 数据类
    /// </summary>
    public class GroupMenu
    {
        // private static int sum = 1;
        // private string type = string.Empty;
        // private string name = string.Empty;
        // private int price = 0;
        // public data(string type, string name,  int price)
        // {
        //     this.type = type;
        //     this.name = name;
        //     this.price = price;
        // }

        /// <summary>
        /// 获取素菜个数
        /// </summary>
        private static int sucount = ConfigReader.SuCount;

        /// <summary>
        /// 获取荤菜个数
        /// </summary>
        private static int huncount = ConfigReader.HunCount;

        /// <summary>
        /// 汤(默认一个)
        /// </summary>
        private static int soupcount = 1;

        /// <summary>
        /// 主菜(默认一个)
        /// </summary>
        private static int maincount = 1;

        /// <summary>
        /// 组合菜单
        /// </summary>
        public string GroupDish(int sucount, int huncount,int minPrice,int maxPrice)
        {

            StringBuilder str = new StringBuilder();

            ////获取菜单
            DAL.MenuList getdata = new DAL.MenuList();
            List<Model.Dish> list = getdata.GetData();

            ////判断是否有数据
            if (list.Count == 0)
            {
                if (ConfigReader.Store == 0)
                {
                    return "李庄白肉没有菜单数据！";
                    //Environment.Exit(0);
                }

                if (ConfigReader.Store == 1)
                {
                     return "又一村没有菜单数据！";
                    //Environment.Exit(0);
                }
            }

            //// 排除不可能(设置的最高价格低于最低价格)
            if (maxPrice < minPrice)
            {
                 return "设置的最高价格低于最低价格！";
                //Environment.Exit(0);
            }

            //// 排除不可能(最低价格N个素菜/荤菜大于设置的最高价格或者最低的素菜荤菜汤都大于设置的最高价格)
            if (this.GetMin(list, 0, sucount) > maxPrice || this.GetMin(list, 1, huncount) > maxPrice || this.GetMin(list, 0, sucount) + this.GetMin(list, 1, huncount) + this.GetMin(list, 2, soupcount) + this.GetMin(list, 3, maincount) > maxPrice)
            {
                return "点太多了，钱明显不够！";
                //Environment.Exit(0);
            }

            //// 排除不可能(最高价格的素菜荤菜汤都低于设置的最低价格)
            if (this.GetMax(list, 0, sucount) + this.GetMax(list, 1, huncount) + this.GetMax(list, 2, soupcount) + this.GetMax(list, 3, maincount) < minPrice)
            {
                return "点太少了，明显不够吃！";
                //Environment.Exit(0);
            }

            while (true)
            {
                int sum = 0;

                List<Model.Dish> listsu = this.RandGet(list, 0, sucount);
                List<Model.Dish> listhun = this.RandGet(list, 1, huncount);
                List<Model.Dish> listsoup = this.RandGet(list, 2, soupcount);
                List<Model.Dish> listmain = this.RandGet(list, 3, maincount);
                for (int i = 0; i < sucount; i++)
                {
                    sum += listsu[i].Price;
                }

                for (int j = 0; j < huncount; j++)
                {
                    sum += listhun[j].Price;
                }

                for (int k = 0; k < soupcount; k++)
                {
                    sum += listsoup[k].Price;
                }

                for (int l = 0; l < maincount; l++)
                {
                    sum += listmain[l].Price;
                }

                //// DateTime d2 = System.DateTime.Now;
                //// TimeSpan ts = d2.Subtract(d1);
                //// Console.WriteLine("花了{0}ms",ts.Milliseconds);
                if (sum <= maxPrice && sum >= minPrice)
                {
                    for (int i = 0; i < sucount; i++)
                    {
                        str.AppendFormat("{0},", listsu[i].Name);
                    }

                    for (int i = 0; i < huncount; i++)
                    {
                        str.AppendFormat("{0},", listhun[i].Name);
                    }

                    for (int i = 0; i < soupcount; i++)
                    {
                        str.AppendFormat("{0},", listsoup[i].Name);
                    }

                    for (int i = 0; i < maincount; i++)
                    {
                        str.AppendFormat("{0},", listmain[i].Name);
                    }

                    str.AppendFormat("总价：{0}元", sum);

                    // str = str.Remove(str.Length - 1, 1);
                    break;
                }
            }

            return str.ToString();
        }

        /// <summary>
        /// 取出N个价格最高的菜，计算总价
        /// </summary>
        /// <param name="list">菜单list</param>
        /// <param name="type">筛选菜单类型</param>
        /// <param name="count">取出的个数</param>
        /// <returns>价格</returns>
        public int GetMax(List<Dish> list, int type, int count)
        {
            int max = 0;
            List<Model.Dish> newlist = new List<Model.Dish>();
            List<Model.Dish> listtype = list.FindAll(arg => arg.Type == type);
            int numAll = listtype.Count();
            while (numAll < count)
            {
                if (type == 0)
                {
                    MessageBox.Show("全部素菜个数都没这么多好么？");
                    Environment.Exit(0);
                }
                else if (type == 1)
                {
                    MessageBox.Show("全部荤菜个数都没这么多好么？");
                    Environment.Exit(0);
                }
            }

            listtype.Sort(delegate(Dish a, Dish b)
            {
                return a.Price.CompareTo(b.Price);
            });

            for (int i = listtype.Count - 1; i > listtype.Count - 1 - count; i--)
            {
                max += listtype[i].Price;
            }

            return max;
        }

        /// <summary>
        /// 从list中取出N个价格最低的菜，计算总价
        /// </summary>
        /// <param name="list">菜单list</param>
        /// <param name="type">筛选菜单类型</param>
        /// <param name="count">取出的个数</param>
        /// <returns>价格</returns>
        public int GetMin(List<Dish> list, int type, int count)
        {
            int min = 0;
            List<Model.Dish> newlist = new List<Model.Dish>();
            List<Model.Dish> listtype = list.FindAll(arg => arg.Type == type);
            int numAll = listtype.Count();
            while (numAll < count)
            {
                if (type == 0)
                {
                    MessageBox.Show("全部素菜个数都没这么多好么？");
                    Environment.Exit(0);
                }
                else if (type == 1)
                {
                    MessageBox.Show("全部荤菜个数都没这么多好么？");
                    Environment.Exit(0);
                }
            }

            listtype.Sort(delegate(Dish a, Dish b)
            {
                return a.Price.CompareTo(b.Price);
            });

            for (int i = 0; i < count; i++)
            {
                min += listtype[i].Price;
            }

            return min;
        }

        /// <summary>
        /// 从list中选出类型进行随机选择出菜
        /// </summary>
        /// <param name="list">菜单list</param>
        /// <param name="type">菜的种类</param>
        /// <param name="count">个数</param>
        /// <returns>筛选结果list</returns>
        public List<Dish> RandGet(List<Dish> list, int type, int count)
        {
            List<Model.Dish> newlist = new List<Model.Dish>();
            List<Model.Dish> listtype = list.FindAll(arg => arg.Type == type);
            int numAll = listtype.Count();

            int num = 0;
            int temp = 0;
            try
            {
                for (int i = 0; i < count; i++)
                {
                    if (numAll != 1)
                    {
                        Random rand = new Random();
                        num = rand.Next(0, numAll);

                        while (temp == num)
                        {
                            num = rand.Next(0, numAll);
                            if (temp == num)
                            {
                                temp = num;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    newlist.Add(listtype[num]);
                    temp = num;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("设置的个数有点离谱啊");
            }
            return newlist;
        }
    }
}