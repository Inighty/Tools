using Better517Na.Http.Helper;
using Better517Na.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Zuiyou.Business;
using Zuiyou.Common;
using Zuiyou.DAL;
using Zuiyou.Model;

namespace Zuiyou
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestZuiyou request = new RequestZuiyou();

            //Thread imgtxtThread = new Thread(new ParameterizedThreadStart(request.Zuiyou));
            ////imgtxtThread.Start("imgtxt");

            //Thread videoThread = new Thread(new ParameterizedThreadStart(request.Zuiyou));
            //videoThread.Start("video");

            Thread thread = new Thread(request.Zuiyou);

            Thread ramThread = new Thread(request.GenerateCheckCode);

            Thread enumThread = new Thread(request.FenPeiTask);

            string taskNo = ConfigReader.StartThread;
            string[] arr = taskNo.Split(',');

            foreach (var item in arr)
            {
                switch (item)
                {
                    case "1":
                        thread.Start();
                        break;
                    case "2":
                        ramThread.Start();
                        break;
                    case "3":
                        enumThread.Start();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
