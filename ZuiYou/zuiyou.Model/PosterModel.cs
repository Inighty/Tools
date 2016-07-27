using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zuiyou.Model
{
    public class PosterModel
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int mid;

        public int Mid
        {
            get { return mid; }
            set { mid = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int gender;

        public int Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private string img = string.Empty;

        public string Img
        {
            get { return img; }
            set { img = value; }
        }
        private string video = string.Empty;

        public string Video
        {
            get { return video; }
            set { video = value; }
        }

        private int reviews;

        public int Reviews
        {
            get { return reviews; }
            set { reviews = value; }
        }
        private int likes;

        public int Likes
        {
            get { return likes; }
            set { likes = value; }
        }
        private int up;

        public int Up
        {
            get { return up; }
            set { up = value; }
        }
        private int down;

        public int Down
        {
            get { return down; }
            set { down = value; }
        }
        private DateTime ctime;

        public DateTime Ctime
        {
            get { return ctime; }
            set { ctime = value; }
        }
        private string topic = string.Empty;

        public string Topic
        {
            get { return topic; }
            set { topic = value; }
        }
        private int god_review_id;

        public int God_review_id
        {
            get { return god_review_id; }
            set { god_review_id = value; }
        }
        private int god_review_mid;

        public int God_review_mid
        {
            get { return god_review_mid; }
            set { god_review_mid = value; }
        }
        private string god_review_name = string.Empty;

        public string God_review_name
        {
            get { return god_review_name; }
            set { god_review_name = value; }
        }

        private int god_review_gender;

        public int God_review_gender
        {
            get { return god_review_gender; }
            set { god_review_gender = value; }
        }
        private string god_review_content = string.Empty;

        public string God_review_content
        {
            get { return god_review_content; }
            set { god_review_content = value; }
        }

        private string god_review_img = string.Empty;

        public string God_review_img
        {
            get { return god_review_img; }
            set { god_review_img = value; }
        }


        private string god_review_video = string.Empty;

        public string God_review_video
        {
            get { return god_review_video; }
            set { god_review_video = value; }
        }


        private int god_review_likes;

        public int God_review_likes
        {
            get { return god_review_likes; }
            set { god_review_likes = value; }
        }
        private int god_review_up;

        public int God_review_up
        {
            get { return god_review_up; }
            set { god_review_up = value; }
        }
        private int god_review_down;

        public int God_review_down
        {
            get { return god_review_down; }
            set { god_review_down = value; }
        }
        private DateTime god_review_ctime;

        public DateTime God_review_ctime
        {
            get { return god_review_ctime; }
            set { god_review_ctime = value; }
        }


    }
}
