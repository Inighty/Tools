using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using zuiyou.Model;
using Better517Na.Http.Helper;
using Better517Na.Json.Linq;
using System.Text.RegularExpressions;
using zuiyou.DAL;
using System.Threading;
using Better517Na.Proxy.Helper;
using zuiyou.Common;

namespace zuiyou.Business
{
    public class RequestZuiyou
    {
        //string token = string.Empty;

        //public string Token
        //{
        //    get { return token; }
        //    set { token = value; }
        //}
        //string mid = string.Empty;

        //public string Mid
        //{
        //    get { return mid; }
        //    set { mid = value; }
        //}


        ///// <summary>
        ///// 构造函数
        ///// </summary>
        //public RequestZuiyou()
        //{
        //    HttpRequestParam paramToken = new HttpRequestParam();
        //    paramToken.URL = "http://tbapi.ixiaochuan.cn/account/register_guest";
        //    paramToken.Method = "POST";
        //    paramToken.Postdata = string.Format("{{\"h_ts\":{0},\"h_did\":\"864394100860115_08:60:6E\"}}", (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        //    paramToken.UserAgent = "tieba/20160715.184411(iPhone;IOS 10.0;Scale/2.00)";
        //    HttpResult resultToken = HttpHelper.GetHttpRequestData(paramToken);
        //    JObject tokenObj = JObject.Parse(resultToken.Html);
        //    if (tokenObj["data"] != null)
        //    {
        //        this.token = tokenObj["data"]["token"].ToString();
        //        this.mid = tokenObj["data"]["mid"].ToString();
        //    }
        //}
        private static int startpoint = ConfigReader.StartPoint;
        private static int threadNum = ConfigReader.ThreadNum;
        private static int endpoint = ConfigReader.EndPoint;
        private static string table = "zuiyoulist";
        private static string checkCode = "0001";
        SaveData saveData = new SaveData();
        public void Zuiyou()
        {
            int count = 0;
            string sql = string.Empty;
            List<PosterModel> list = new List<PosterModel>();
            while (true)
            {
                try
                {
                    list = GetPost();
                    if (list.Count == 0)
                    {
                        continue;
                    }
                    sql = ListToSql(list, table);
                    if (string.IsNullOrEmpty(sql)) {
                        continue;
                    }
                    count = saveData.getmysqlcom(sql);
                    //Console.WriteLine("更新或添加成功" + count + "条数据");
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
        }

        public void GetPostEnum(int start, int end)
        {
            try
            {
                for (int i = start; i <= end; i++)
                {

                    List<PosterModel> posterList = new List<PosterModel>();
                    HttpResult result = null;
                    HttpRequestParam param = new HttpRequestParam();
                    param.URL = "http://tbapi.ixiaochuan.cn/post/detail";
                    param.Method = "POST";

                    //param.Postdata = "{\"h_ts\":" + ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000) + ",\"h_av\":\"2.6.0\",\"h_nt\":1,\"h_m\":0,\"h_did\":\""+GenerateCheckCode()+"_08:60:7E\"}";
                    param.Postdata = "{\"h_dt\":0,\"h_av\":\"2.6.0\",\"pid\":" + i + ",\"from\":\"index\",\"h_nt\":1}";
                    param.KeepAlive = true;
                    param.UserAgent = "tieba/20160715.184411(iPhone;IOS 10.0;Scale/2.00)";
                    //proxyInfo = ProxyHelper.GetProxyInfo("Rightest_Grab");
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
                            Console.WriteLine(i);
                            JToken data = obj["data"]["post"];
                            posterList.Add(getModel(data));
                        }

                        if (posterList.Count == 0)
                        {
                            continue;
                        }
                        string sql = ListToSql(posterList, table);
                        int count = saveData.getmysqlcom(sql);
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        public List<PosterModel> GetPost()
        {
            List<PosterModel> posterList = new List<PosterModel>();
            HttpResult result = null;
            try
            {
                HttpRequestParam param = new HttpRequestParam();
                //MProxyInfo proxyInfo = null;
                param.URL = "http://tbapi.ixiaochuan.cn/index/recommend";
                param.Method = "POST";

                //param.Postdata = "{\"h_ts\":" + ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000) + ",\"h_av\":\"2.6.0\",\"h_nt\":1,\"h_m\":0,\"h_did\":\""+GenerateCheckCode()+"_08:60:7E\"}";
                param.Postdata = "{\"h_ts\":" + ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000) + ",\"h_av\":\"2.6.0\",\"h_nt\":1,\"h_m\":0,\"h_did\":\"86439410086" + checkCode + "_08:60:7E\"}";
                param.KeepAlive = true;
                param.UserAgent = "tieba/20160715.184411(iPhone;IOS 10.0;Scale/2.00)";
                //proxyInfo = ProxyHelper.GetProxyInfo("Rightest_Grab");
                //if (proxyInfo != null)
                //{
                //    param.ProxyIP = proxyInfo.IP + ":" + proxyInfo.Port;
                //    param.ProxyUserName = proxyInfo.UserName;
                //    param.ProxyPwd = proxyInfo.Password;
                //    param.ProxyBusinessName = "Rightest_Grab";
                //}
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
                            model = getModel(item);
                            posterList.Add(model);
                        }
                        Console.WriteLine(DateTime.Now + " 获取到" + datalist.Count + "条数据");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return posterList;
        }

        public PosterModel getModel(JToken item)
        {
                PosterModel model = new PosterModel();
                model.Id = Convert.ToInt32(item["id"]);
                model.Mid = Convert.ToInt32(item["member"]["id"]);
                model.Name = item["member"]["name"].ToString();
                model.Gender = Convert.ToInt32(item["member"]["gender"]);
                model.Content = item["content"].ToString();
                if (item["imgs"] != null && item["imgs"].ToArray().Length != 0)
                {
                    JArray imglist = item["imgs"] as JArray;
                    foreach (var img in imglist)
                    {
                        model.Img += string.Format("http://tbfile.ixiaochuan.cn/img/view/id/{0}/sz/src,", img["id"].ToString());
                        JObject imgDetail = JObject.Parse(img.ToString());
                        try
                        {
                            if (imgDetail["video"] != null)
                            {
                                if (item["videos"] != null && item["videos"].ToArray().Length != 0 && imgDetail["video"].ToString() == "1")
                                {
                                    model.Video += item["videos"][img["id"].ToString()]["url"].ToString() + ",";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Write(item);
                        }
                    }
                    model.Img = model.Img.Remove(model.Img.Length - 1);
                    model.Video = string.IsNullOrEmpty(model.Video) ? string.Empty : model.Video.Remove(model.Video.Length - 1);
                }

                model.Reviews = Convert.ToInt32(item["reviews"]);
                model.Likes = Convert.ToInt32(item["likes"]);
                model.Up = Convert.ToInt32(item["up"]);
                model.Down = Convert.ToInt32(item["down"]);
                model.Ctime = UnixTimeStampToDateTime(double.Parse(item["ct"].ToString()));
                model.Topic = item["topic"]["topic"].ToString();
                if (item["god_review"] != null && item["god_review"].ToArray().Length != 0)
                {
                    model.God_review_id = Convert.ToInt32(item["god_review"]["id"]);
                    model.God_review_mid = Convert.ToInt32(item["god_review"]["mid"]);
                    model.God_review_name = item["god_review"]["mname"].ToString();
                    model.God_review_gender = Convert.ToInt32(item["god_review"]["gender"]);
                    model.God_review_content = item["god_review"]["review"].ToString();
                    if (item["god_review"]["imgs"] != null && item["god_review"]["imgs"].ToArray().Length != 0)
                    {
                        JArray imglist = item["god_review"]["imgs"] as JArray;
                        foreach (var img in imglist)
                        {
                            model.God_review_img += string.Format("http://tbfile.ixiaochuan.cn/img/view/id/{0}/sz/src,", img["id"].ToString());
                            JObject imgDetail = JObject.Parse(img.ToString());
                            if (imgDetail["video"] != null)
                            {
                                if (item["god_review"]["videos"] != null && item["god_review"]["videos"].ToArray().Length != 0 && imgDetail["video"].ToString() == "1")
                                {
                                    model.God_review_video += item["god_review"]["videos"][img["id"].ToString()]["url"].ToString() + ",";
                                }
                            }
                        }
                        model.God_review_img = model.God_review_img.Remove(model.God_review_img.Length - 1);
                        model.God_review_video = string.IsNullOrEmpty(model.God_review_video) ? string.Empty : model.God_review_video.Remove(model.God_review_video.Length - 1);
                    }
                    model.God_review_likes = Convert.ToInt32(item["god_review"]["likes"]);
                    model.God_review_up = Convert.ToInt32(item["god_review"]["up"]);
                    model.God_review_down = Convert.ToInt32(item["god_review"]["down"]);
                    model.God_review_ctime = UnixTimeStampToDateTime(double.Parse(item["god_review"]["ct"].ToString()));
                }
            return model;
        }

        public void FenPeiTask()
        {
            for (int i = 0; i < threadNum; i++)
            {
                int yu = (endpoint - startpoint) % threadNum;
                int shang = (endpoint - startpoint) / threadNum;
                int start = startpoint + (endpoint - startpoint) / threadNum * i;
                int end = yu != 0 && i == threadNum - 1 ? shang * (i + 1) + yu : shang * (i + 1);
                int[] para = new int[2] { start, end };
                Thread enumThread = new Thread(new ParameterizedThreadStart(method));
                enumThread.Start(para);
            }
        }

        public static void method(object o)
        {
            //此处对传进来的参数进行处理
            int[] p = (int[])o;

            //调用GetPostEnum方法
            RequestZuiyou request = new RequestZuiyou();
            request.GetPostEnum(p[0], p[1]);
        }

        //产生15位的随机字符串
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

        public string ListToSql(List<PosterModel> list, string table)
        {
            StringBuilder sqlStr = new StringBuilder();
            foreach (var item in list)
            {
                string names = string.Empty;
                string values = string.Empty;
                string updateStr = string.Empty;
                Type t = item.GetType();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    string name = pi.Name;
                    names += name + ",";
                    object value1 = pi.GetValue(item, null);//用pi.GetValue获得值
                    if (value1.ToString().Contains("'"))
                    {
                        value1 = value1.ToString().Replace("'", "''");
                    }
                    values += "'" + value1.ToString() + "'" + ",";
                    updateStr += name + "=" + "'" + value1 + "',";
                }
                values = values.Substring(0, values.Length - 1);
                names = names.Substring(0, names.Length - 1);
                updateStr = updateStr.Substring(0, updateStr.Length - 1);
                sqlStr.AppendFormat("insert into {0}({1}) values ({2}) ON DUPLICATE KEY UPDATE {3};\n", table, names, values, updateStr);
            }
            return sqlStr.ToString();
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // 定义其实时间
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
