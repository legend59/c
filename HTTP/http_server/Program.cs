﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SP_TEST
{
    class Program
    {
        static HttpListener listener;
        static void Main(string[] args)
        {
            //HttpListener listener = new HttpListener();
            listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8080/");
            listener.Start();

            while (true)
            {
                //sample();
                //reqeustDate();
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                Console.WriteLine("request  : " + request.ToString());
                Console.WriteLine("request.HttpMethod  : " + request.HttpMethod);
                if (request.HttpMethod == "POST")
                {
                    //Console.WriteLine("Request : " + context.Request.Url);
                    processPost1(context);
                }
                else if (request.HttpMethod == "GET")
                {
                    reqeustDate();
                }
            }
        }

        static void sample()
        {
            var context = listener.GetContext();
            Console.WriteLine("Request : " + context.Request.Url);
            byte[] data = Encoding.UTF8.GetBytes("HelloWorld");
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        /*
        Request : http://127.0.0.1:8080/search?hl=en&q=parsing+a+url+in+c%23&aq=f&aqi=g1g-j9&aql=&oq=
        The current time is 2022-06-12 오후 12:54:33
        Protocol: http
        Host: 127.0.0.1
        Path: /search
        Query: ?hl=en&q=parsing+a+url+in+c%23&aq=f&aqi=g1g-j9&aql=&oq=
        Parms: 6
        Parm: hl = en
        Parm: q = parsing a url in c#
        Parm: aq = f
        Parm: aqi = g1g-j9
        Parm: aql =
        Parm: oq =
            */
        static void reqeustDate()
        {
            var context = listener.GetContext();
            Console.WriteLine("Request : " + context.Request.Url);
            Uri tmp = context.Request.Url;

            string time = DateTime.Now.ToString();// ("h:mm:ss tt");
            Console.WriteLine("The current time is {0}", time);

            Console.WriteLine("Protocol: {0}", tmp.Scheme);
            Console.WriteLine("Host: {0}", tmp.Host);
            Console.WriteLine("Path: {0}", HttpUtility.UrlDecode(tmp.AbsolutePath));
            Console.WriteLine("Query: {0}", tmp.Query);

            // case1. 파라미터를 모를 때
            //
            NameValueCollection Parms = HttpUtility.ParseQueryString(tmp.Query);
            Console.WriteLine("Parms: {0}", Parms.Count);
            foreach (string x in Parms.AllKeys)
                Console.WriteLine("\tParm: {0} = {1}", x, Parms[x]);
            //

            // case2. 파라미터를 알고 있을 때
            /*
            var Parms = HttpUtility.ParseQueryString(tmp.Query);
            string aql = Parms["hl"];
            Console.WriteLine("Parms for hl: {0}", aql);
            */

            byte[] data = Encoding.UTF8.GetBytes(time);
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        static void processPost1(HttpListenerContext context)
        {
            Console.WriteLine("in processPost1");

            String strRes = "";
            DateTime dt = DateTime.Now;
            strRes = "(POST) " + dt.ToString();
            //var context = listener.GetContext();
            Console.WriteLine("in processPost1-1");
            System.IO.Stream body = context.Request.InputStream;
            System.Text.Encoding encoding = context.Request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            string s = reader.ReadToEnd();
            Console.WriteLine("in processPost1-2");
            System.IO.Directory.CreateDirectory(".\\Output");
            Console.WriteLine("in processPost1-3");
            string fileName = string.Format(".\\Output\\{0:D2}{1:D2}{2:D2}.json", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            Console.WriteLine("fileName : " + fileName);

            Console.WriteLine("s : " + s);
            JObject json_p1 = JObject.Parse(s);
            Console.WriteLine("get type of json_p1 : {0}", json_p1.Type);
            Console.WriteLine("profile_image : " + json_p1["profile_image"]);


            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(s);
            sw.Close();
            body.Close();
            reader.Close();

            strRes = MakeResposneString();

            byte[] data = Encoding.UTF8.GetBytes(strRes);
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.StatusCode = 200;
            context.Response.Close();
        }

        static string MakeResposneString ()
        {
            string str = "";

            JObject data1 = new JObject();

            data1.Add("employee_name", "ObjectTest");
            data1.Add("profile_image", "test1.png");
            data1.Add("employee_age", "30");
            data1.Add("employee_salary", "11111");

            // JSON Object for second employee
            JObject data2 = new JObject();

            data2.Add("employee_name", "MapTest");
            data2.Add("profile_image", "test2.png");
            data2.Add("employee_age", "20");
            data2.Add("employee_salary", "99999");

            // Creating JSON array to add both JSON objects
            JArray array = new JArray();
            array.Add(data1);
            array.Add(data2);

            str = array.ToString();

            Console.WriteLine("str : " + str);

            return str;
        }
    }
}
