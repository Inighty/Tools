using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallDLL
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 反射对象
        /// </summary>
        private static Assembly assembly = null;

        /// <summary>
        /// dll名
        /// </summary>
        private static string dllName = string.Empty;

        /// <summary>
        /// 类名
        /// </summary>
        private static string className = string.Empty;

        /// <summary>
        /// dic(方法名,dic(入参,出参))
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 创建该对象的实例，object类型，参数（名称空间+类） 
        /// </summary>
        object instance = new object();  


        /// <summary>
        /// 构造函数
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;

            var Path = Application.StartupPath + @"\skin";
            DirectoryInfo dir = new DirectoryInfo(Path);
            if (dir.Exists)
            {
                FileInfo[] skinList = dir.GetFiles();
                foreach (var item in skinList)
                {
                    skinCollection.Items.Add(item.Name.Replace(".ssk",""));
                }
                skinEngine1.SkinFile = Application.StartupPath + @"\skin\" + skinList[0].Name;
                skinCollection.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 点击button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void button1_Click(object sender, EventArgs e)
        {
            ////清空 防止前一次数据没有删除
            classList.Items.Clear();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "DLL文件(*.dll)|*.dll";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //获得完整路径在textBox1中显示  
                dllDir.Text = openFileDialog.FileName.ToString();
                
            }
        }

        /// <summary>
        /// classList选择
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void classList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////清空 防止前一次数据没有删除
            methodList.Items.Clear();
            className = classList.SelectedItem.ToString();
            try
            {
                ////通过命名空间.类名 寻找类
                Type type = assembly.GetType(dllName + "." + className, true, true);
                if (type != null)
                {
                    dic.Clear();
                    foreach (var item in type.GetMethods())
                    {
                        Regex rex = new Regex(@"(?<Property>.*?)\s(?<Method>.*?)\((?<Param>.*?)\)");
                        //Regex rex = new Regex(@".*?\s(?<Method>.*?)\(");
                        
                        Match mat = rex.Match(item.ToString());
                        if (mat != null)
                        {
                            if (mat.Groups["Method"].ToString() != "ToString" && mat.Groups["Method"].ToString() != "Equals" && mat.Groups["Method"].ToString() != "GetHashCode" && mat.Groups["Method"].ToString() != "GetType")
                            {
                                Dictionary<string, string> param = new Dictionary<string, string>();
                                param.Add(mat.Groups["Param"].ToString(), mat.Groups["Property"].ToString());
                                //inputParam.Text = mat.Groups["Method"].ToString();
                                methodList.Items.Add(mat.Groups["Method"].ToString());
                                dic.Add(mat.Groups["Method"].ToString(), param);
                            }
                        }
                    }

                    //默认第一个
                    methodList.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void methodList_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputParam.Text = dic[methodList.SelectedItem.ToString()].ElementAt(0).Key;
            outputParam.Text = dic[methodList.SelectedItem.ToString()].ElementAt(0).Value;
        }

        private void excute_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dllName) && !string.IsNullOrEmpty(className))
            {
                Assembly.LoadFile(Application.StartupPath + "\\HttpHelper.dll");
                Assembly.LoadFile(Application.StartupPath + "\\Newtonsoft.Json.dll");
                Assembly.LoadFile(Application.StartupPath + "\\Newtonsoft.Json.dll");
                Type type = assembly.GetType(dllName + "." + className, true, true);
                MethodInfo method = type.GetMethod(methodList.SelectedItem.ToString());
                try
                {
                    //创建该对象的实例，object类型，参数（名称空间+类）   
                    instance = assembly.CreateInstance(dllName + "." + className);
                    if (!string.IsNullOrEmpty(inputParam.Text))
                    {
                        var paramStr = SplitStringWithComma(param.Text);
                        result.Text = method.Invoke(instance, paramStr).ToString();
                    }
                    else
                    {
                        result.Text = method.Invoke(instance, null).ToString();
                    }
                }
                catch (Exception ex) {
                    if (ex.ToString().Contains("未能加载文件或程序集")) {
                        Match mat = new Regex(@"(?<=未能加载文件或程序集“)(.*?)(?=, Version=\S+)").Match(ex.ToString());
                        if (mat != null) {
                            try
                            {
                                Assembly.LoadFile(Application.StartupPath + "\\DLL\\" + mat.Groups[0].ToString() + ".dll");
                            }
                            catch (Exception exc)
                            {
                                if (exc.ToString().Contains("系统找不到指定的文件。"))
                                {
                                    MessageBox.Show("请将" + mat.Groups[0].ToString() + ".dll放入应用根目录DLL文件夹下");
                                }
                            }
                            
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 以逗号拆分字符串
        /// 若字段中包含逗号(备注：包含逗号的字段必须有双引号引用)则对其进行拼接处理
        /// 最后在去除其字段的双引号
        /// </summary>
        /// <param name="splitStr"></param>
        /// <returns></returns>
        private static string[] SplitStringWithComma(string splitStr)
        {
            var newstr = string.Empty;
            List<string> sList = new List<string>();

            bool isSplice = false;
            string[] array = splitStr.Split(new char[] { ',' });
            foreach (var str in array)
            {
                if (!string.IsNullOrEmpty(str) && str.IndexOf('"') > -1)
                {
                    var firstchar = str.Substring(0, 1);
                    var lastchar = string.Empty;
                    if (str.Length > 0)
                    {
                        lastchar = str.Substring(str.Length - 1, 1);
                    }
                    if (firstchar.Equals("\"") && !lastchar.Equals("\""))
                    {
                        isSplice = true;
                    }
                    if (lastchar.Equals("\""))
                    {
                        if (!isSplice)
                            newstr += str;
                        else
                            newstr = newstr + "," + str;

                        isSplice = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(newstr))
                        newstr += str;
                }

                if (isSplice)
                {
                    //添加因拆分时丢失的逗号
                    if (string.IsNullOrEmpty(newstr))
                        newstr += str;
                    else
                        newstr = newstr + "," + str;
                }
                else
                {
                    sList.Add(newstr.Replace("\"", "").Trim());//去除字符中的双引号和首尾空格
                    newstr = string.Empty;
                }
            }
            return sList.ToArray();
        }

        private void skinCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = Application.StartupPath + @"\skin\" + skinCollection.SelectedItem.ToString() + ".ssk";
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string f in files)
                {
                    //listBox1.Items.Add(f);
                    dllDir.Text = f;
                }
            }
        }

        private void dllDir_TextChanged(object sender, EventArgs e)
        {
            dllName = Path.GetFileNameWithoutExtension(dllDir.Text);
            ////反射动态加载dll
            assembly = Assembly.LoadFile(dllDir.Text);
            classList.Items.Clear();
            //获取类  
            foreach (Type t in assembly.GetTypes())
            {
                classList.Items.Add(t.Name);
                //methodList.Items.Add(t.GetMethods());
            }
            //默认第一个
            classList.SelectedIndex = 0;
            //methodList.SelectedIndex = 0;
        }
    }
}
