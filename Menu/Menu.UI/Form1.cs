using Menu.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ////初始化
            ////获取饭店并显示
            MenuList menuList = new MenuList();
            List<string> listStore = new List<string>();
            listStore = menuList.GetStore();
            foreach (var item in listStore)
            {
                storeSelect.Items.Add(item);
            }
            storeSelect.SelectedIndex = 0;

            ////初始化
            sucaiCount.Value = 1;
            huncaiCount.Value = 2;

            
        }

        private void calc_Click(object sender, EventArgs e)
        {
            Business.GroupMenu groupMenu = new Business.GroupMenu();
            string str = groupMenu.GroupDish(Int32.Parse(sucaiCount.Value.ToString()), Int32.Parse(huncaiCount.Value.ToString()), Int32.Parse(minPrice.Text == string.Empty ? "0" : minPrice.Text), Int32.Parse(maxPrice.Text == string.Empty ? "0" : maxPrice.Text));
            calcResult.Text += str + "\r\n";
        }

        private void maxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void minPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void Clscr_Click(object sender, EventArgs e)
        {
            calcResult.Text = string.Empty;
        }

    }
}
