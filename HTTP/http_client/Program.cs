using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IO;
//using System.Net.Json;

namespace SP_TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            //helloworld();
            //requestDate();
            requestPost1();
            //requestPost2();
        }

        static void helloworld()
        {
            HttpClient client = new HttpClient();
            var res = client.GetAsync("http://127.0.0.1:8080/helloworld").Result;
            Console.WriteLine("Response : " + res.StatusCode + " - " + res.Content.ReadAsStringAsync().Result);
        }

        static void requestDate()
        {
            HttpClient client = new HttpClient();
            var res = client.GetAsync("http://127.0.0.1:8080/requestDate").Result;
            Console.WriteLine("Response : - " + res.Content.ReadAsStringAsync().Result);
        }

        static void requestPost1()
        {
            HttpClient client = new HttpClient();
            //var res = client.GetAsync("http://127.0.0.1:8080/requestDate").Result;
            //Console.WriteLine("Response : - " + res.Content.ReadAsStringAsync().Result);

            // method1
            /*
            JObject json = new JObject();
            json["name"] = "John Doe";
            json["salary"] = 300100;
            string jsonstr = json.ToString();
            */
            string strFileList = GetStrFileList();
            Console.WriteLine("Response :" + strFileList);

            var content = new StringContent(strFileList, Encoding.UTF8, "application/json");
            var result = client.PostAsync("http://127.0.0.1:8080/fileList", content).Result;
            Console.WriteLine("Response :" + " - " + result.Content.ReadAsStringAsync().Result);
        }

        static void requestPost2()
        {
            string strFileList = GetStrFileList();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://127.0.0.1:8080/fileList");


            httpRequest.Content = new StringContent(strFileList, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            var res = client.SendAsync(httpRequest).Result;
            Console.WriteLine("Response :" + " - " + res.Content.ReadAsStringAsync().Result);
        }

        static string GetStrFileList()
        {
            JObject jsonObj = new JObject();

            DirectoryInfo di = new DirectoryInfo("./Input");
            jsonObj.Add("Folder", "Input");
            JArray jsonArr = new JArray();

            FileInfo[] fiArr = di.GetFiles();
            foreach (FileInfo f in fiArr)
            {
                jsonArr.Add(f.Name);
            }
            jsonObj.Add("Files", jsonArr);
            return jsonObj.ToString();
        }
    }
}
