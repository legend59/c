using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Question4_Server
{
    class ReportHttpServer
    {
        static Mutex muSeq = new Mutex();
        static Mutex muEnd = new Mutex();
        public HttpListener listener;
        private Dictionary<int, Thread> dicThread = new Dictionary<int, Thread>();
        public void DoHttpServer(Object obj)
        {
            Console.WriteLine("Http Server Thread Start");

            listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8081/");
            listener.Start();

            try
            {
                while (true)
                {
                    var context = listener.GetContext();

                    Thread thContext = new Thread(DoContext);
                    thContext.Start(context);
                }
            }
            catch (HttpListenerException e)
            {
                //
                Console.WriteLine("Close HTTP Server");
            }
        }   

        private void DoContext(Object obj)
        {
            HttpListenerContext context = (HttpListenerContext)obj;
            JObject resJson = new JObject();

            Console.WriteLine(context.Request.Url);

            string[] words = context.Request.Url.LocalPath.Split('/');
            string command = words[1];

            if (context.Request.HttpMethod == "GET")
            {
                switch (command)
                {
                    case "REPORT":
                        
                        string managerId = words[2];
                        string strDate = words[3];

                        string report = ReportHandling.MakeReport(strDate);

                        muSeq.WaitOne();
                        string reportId = ReportHandling.IncreaseSeqNo().ToString();
                        muSeq.ReleaseMutex();

                        resJson["Result"] = "Ok";
                        resJson["ReportID"] = reportId;
                        resJson["Report"] = report;

                        ReportHandling.storeReport(reportId, report);

                        // start timeout thread
                        Thread th = new Thread(DoTimeout);
                        Console.WriteLine("REPORT ID : " + reportId); 
                        dicThread[int.Parse(reportId)] = th;
                        th.Start(new string[] {managerId, reportId});

                        // LOG
                        Logger.WriteLog(managerId, command, reportId);

                        // Console Print
                        Console.WriteLine("Report ID : " + reportId);
                        Console.WriteLine("------------- Report --------------");
                        Console.WriteLine(report);

                        break;
                    default:
                        break;
                }
            }
            else if (context.Request.HttpMethod == "POST")
            {
                JObject jsonBody = null;

                using (StreamReader sr = new StreamReader(context.Request.InputStream))
                {
                    string body = sr.ReadToEnd();
                    if (body != "null")
                        jsonBody = JObject.Parse(body);
                }

                switch (command)
                {
                    case "FINISH":
                        {
                            muEnd.WaitOne();

                            string reportId = jsonBody["ReportID"].ToString();
                            string managerId = jsonBody["ManagerID"].ToString();

                            resJson["Result"] = "Ok";

                            dicThread[int.Parse(reportId)].Interrupt();   // finish timeout thread
                            dicThread.Remove(int.Parse(reportId));
                            if (ReportHandling.SaveReportFile(reportId, command))
                            {
                                ReportHandling.removeReport(reportId);
                                Logger.WriteLog(managerId, command, reportId);
                            }

                            muEnd.ReleaseMutex();
                        }
                        break;
                    case "FAIL":
                        {
                            muEnd.WaitOne();

                            string reportId = jsonBody["ReportID"].ToString();
                            string managerId = jsonBody["ManagerID"].ToString();

                            resJson["Result"] = "Ok";

                            dicThread[int.Parse(reportId)].Interrupt();   // finish timeout thread
                            dicThread.Remove(int.Parse(reportId));
                            if (ReportHandling.SaveReportFile(reportId, command))
                            {
                                ReportHandling.removeReport(reportId);
                                Logger.WriteLog(managerId, command, reportId);
                            }

                            muEnd.ReleaseMutex();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                return;
            }

            SendData(context, resJson);
        }

        private void DoTimeout(Object obj)
        {
            string [] param = (string[])obj;
            string managerId = param[0];
            string reportId = param[1];

            const int timeoutMS = 5000;
            int startTick = Environment.TickCount;

            try
            {
                while ((Environment.TickCount - startTick) < timeoutMS)
                {

                    Thread.Sleep(1);
                }

                // timeout 처리
                muEnd.WaitOne();
                if (ReportHandling.SaveReportFile(reportId, "TIMEOUT"))
                {
                    ReportHandling.removeReport(reportId);
                    Logger.WriteLog(managerId, "TIMEOUT", reportId);
                }
                muEnd.ReleaseMutex();
            }
            catch (ThreadInterruptedException e)
            {
                // timeout cancel 
                Console.WriteLine("Timeout canceled " + reportId);
            }
        }

        private void SendData(HttpListenerContext context, JObject resJson)
        {
            byte[] data = Encoding.UTF8.GetBytes(resJson.ToString());
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.StatusCode = 200;
            context.Response.Close();
        }
    }
}
