//-----------------------------------------------------------------------
// <copyright file="dish.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 2.0.50727.5456
// * author  : tianxun
// * FileName: dish.cs
// * history : created by tianxun 2016-03-21 14:32:46 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Model
{
    /// <summary>
    /// 菜单类
    /// </summary>
    public class Dish
    {
        /// <summary>
        /// 类型(0：素菜/1：荤菜/2：汤)
        /// </summary>
        private int type = 0;

        /// <summary>
        /// 菜名
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// 价格
        /// </summary>
        private int price = 0;

        /// <summary>
        /// 类型(0：素菜/1：荤菜/2：汤)
        /// </summary>
        public int Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// 菜名
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public int Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
    }
}
