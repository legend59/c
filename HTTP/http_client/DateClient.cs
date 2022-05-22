using System;
using System.Collections.Generic;
using System.Net.Http;

namespace HTTP_DATE_CLIENT
{
    class DateClient
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            var res = client.GetAsync("http://127.0.0.1:8080/requestDate").Result;
            Console.WriteLine("Response :" + " - " + res.Content.ReadAsStringAsync().Result);
        }
    }
}
