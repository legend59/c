using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_FILE_CLIENT
{
    class FileClient
    {
        static void Main(string[] args)
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
