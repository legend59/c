using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

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

        static void reqeustDate()
        {
            var context = listener.GetContext();
            Console.WriteLine("Request : " + context.Request.Url);

            string time = DateTime.Now.ToString();// ("h:mm:ss tt");
            Console.WriteLine("The current time is {0}", time);

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

           StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(s);
            sw.Close();
            body.Close();
            reader.Close();

            byte[] data = Encoding.UTF8.GetBytes(strRes);
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.StatusCode = 200;
            context.Response.Close();
        }
    }
}
