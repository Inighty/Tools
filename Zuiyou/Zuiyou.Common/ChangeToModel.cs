//-----------------------------------------------------------------------
// <copyright file="ChangeToModel.cs" company="517Na Enterprises">
// * Copyright (C) 2016 517Na科技有限公司 版权所有。
// * version : 4.0.30319.42000
// * author  : tianxun
// * FileName: ChangeToModel.cs
// * history : created by tianxun 2016-8-2 13:47:55 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Better517Na.Json.Linq;
using Zuiyou.Model;

namespace Zuiyou.Common
{
    /// <summary>
    /// ChangeToModel类 
    /// </summary>
    public class ChangeToModel
    {
        /// <summary>
        /// 根据返回数据解析为model
        /// </summary>
        /// <param name="item">请求返回的数据</param>
        /// <returns>model</returns>
        public PosterModel GetModel(JToken item)
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

        /// <summary>
        /// unix时间戳转DateTime
        /// </summary>
        /// <param name="unixTimeStamp">时间戳</param>
        /// <returns>DateTime</returns>
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // 定义其实时间
            System.DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dt = dt.AddSeconds(unixTimeStamp).ToLocalTime();
            return dt;
        }
    }
}
