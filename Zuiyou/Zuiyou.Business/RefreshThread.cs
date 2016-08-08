//-----------------------------------------------------------------------
// <copyright file="RefreshThread.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 4.0.30319.42000
// * author  : tianxun
// * FileName: RefreshThread.cs
// * history : created by tianxun 2016-8-2 9:40:08 
// </copyright>
//-----------------------------------------------------------------------找你去
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Better517Na.Http.Helper;
using Better517Na.Json.Linq;
using Better517Na.Proxy.Helper;
using Zuiyou.Common;
using Zuiyou.DAL;
using Zuiyou.Model;

namespace Zuiyou.Business
{
    /// <summary>
    /// 业务请求类
    /// </summary>
    public class RefreshThread
    {
        ////string token = string.Empty;

        ////public string Token
        ////{
        ////    get { return token; }
        ////    set { token = value; }
        ////}
        ////string mid = string.Empty;

        ////public string Mid
        ////{
        ////    get { return mid; }
        ////    set { mid = value; }
        ////}

        /////// <summary>
        /////// 构造函数
        /////// </summary>
        ////public RequestZuiyou()
        ////{
        ////    HttpRequestParam paramToken = new HttpRequestParam();
        ////    paramToken.URL = "http://tbapi.ixiaochuan.cn/account/register_guest";
        ////    paramToken.Method = "POST";
        ////    paramToken.Postdata = string.Format("{{\"h_ts\":{0},\"h_did\":\"864394100860115_08:60:6E\"}}", (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        ////    paramToken.UserAgent = "tieba/20160715.184411(iPhone;IOS 10.0;Scale/2.00)";
        ////    HttpResult resultToken = HttpHelper.GetHttpRequestData(paramToken);
        ////    JObject tokenObj = JObject.Parse(resultToken.Html);
        ////    if (tokenObj["data"] != null)
        ////    {
        ////        this.token = tokenObj["data"]["token"].ToString();
        ////        this.mid = tokenObj["data"]["mid"].ToString();
        ////    }
        ////}

        /////// <summary>
        /////// 锁对象
        /////// </summary>
        ////private static object lk = new object();

        /// <summary>
        /// 初始化随机数
        /// </summary>
        private static string checkCode = "0001";

        /// <summary>
        /// 请求数据，解析返回数据存入数据库（每次返回12条数据左右）
        /// </summary>
        public void Refresh()
        {
            int count = 0;
            string sql = string.Empty;
            List<PosterModel> list = new List<PosterModel>();
            SaveData saveData = new SaveData();
            while (true)
            {
                try
                {
                    list = this.GetPost();
                    if (list.Count == 0)
                    {
                        continue;
                    }

                    count = saveData.ListToSave(list);
                    ////Console.WriteLine("更新或添加成功" + count + "条数据");
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 请求返回list
        /// </summary>
        /// <returns>返回list</returns>
        public List<PosterModel> GetPost()
        {
            List<PosterModel> posterList = new List<PosterModel>();
            HttpResult result = null;
            try
            {
                HttpRequestParam param = new HttpRequestParam();
                ////MProxyInfo proxyInfo = null;
                param.URL = "http://tbapi.ixiaochuan.cn/index/recommend";
                param.Method = "POST";

                ////param.Postdata = "{\"h_ts\":" + ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000) + ",\"h_av\":\"2.6.0\",\"h_nt\":1,\"h_m\":0,\"h_did\":\""+GenerateCheckCode()+"_08:60:7E\"}";
                param.Postdata = "{\"h_ts\":" + ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000) + ",\"h_av\":\"2.6.0\",\"h_nt\":1,\"h_m\":0,\"h_did\":\"86439410086" + checkCode + "_08:60:7E\"}";
                param.KeepAlive = true;
                param.UserAgent = "tieba/20160715.184411(iPhone;IOS 10.0;Scale/2.00)";
                ////proxyInfo = ProxyHelper.GetProxyInfo("Rightest_Grab");
                ////if (proxyInfo != null)
                ////{
                ////    param.ProxyIP = proxyInfo.IP + ":" + proxyInfo.Port;
                ////    param.ProxyUserName = proxyInfo.UserName;
                ////    param.ProxyPwd = proxyInfo.Password;
                ////    param.ProxyBusinessName = "Rightest_Grab";
                ////}
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
                        JArray datalist = obj["data"]["list"] as JArray;
                        foreach (JToken item in datalist)
                        {
                            PosterModel model = new PosterModel();
                            model = new Common.Tools().GetModel(item);
                            posterList.Add(model);
                        }
                        ////lock (lk)
                        ////{
                        Console.BackgroundColor = Console.BackgroundColor;
                        Console.SetCursorPosition(0, 3 * ConfigReader.ThreadNum);
                        Console.WriteLine(DateTime.Now + " 持续刷新线程获取到" + datalist.Count + "条数据");
                        ////}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return posterList;
        }

        /// <summary>
        /// 产生4位的随机字符串
        /// </summary>
        public void GenerateCheckCode()
        {
            while (true)
            {
                int number;
                char code;

                System.Random random = new Random();

                for (int i = 0; i < 4; i++)
                {
                    number = random.Next();
                    code = (char)('0' + (char)(number % 10));
                    checkCode += code.ToString();
                }

                Thread.Sleep(TimeSpan.FromMinutes(2));
            }
        }
    }
}
