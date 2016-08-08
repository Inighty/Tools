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
            skinEngine1.SkinFile = Application.StartupPath + @"\MacOS.ssk";
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
                dllName = Path.GetFileNameWithoutExtension(dllDir.Text);
                ////反射动态加载dll
                assembly = Assembly.LoadFile(dllDir.Text);
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
                //创建该对象的实例，object类型，参数（名称空间+类）   
                instance = assembly.CreateInstance(dllName + "." + className);
                if (!string.IsNullOrEmpty(inputParam.Text))
                {
                    var paramStr = param.Text.Split('@');
                    result.Text = method.Invoke(instance, paramStr).ToString();
                }
                else
                {
                    result.Text = method.Invoke(instance, null).ToString();
                }

            }
        }


    }
}
