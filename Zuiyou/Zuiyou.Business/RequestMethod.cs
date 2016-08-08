using Better517Na.Http.Helper;
using Better517Na.Json.Linq;
//-----------------------------------------------------------------------
// <copyright file="RequestMethod.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 4.0.30319.42000
// * author  : tianxun
// * FileName: RequestMethod.cs
// * history : created by tianxun 2016-8-2 16:57:35 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Zuiyou.Common;
using Zuiyou.Model;

namespace Zuiyou.Business
{
    /// <summary>
    /// RequestMethod类 
    /// </summary>
    public class RequestMethod
    {
        public HttpResult RequstMethod(int i)
        {
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
            return result;
        }
    }
}
