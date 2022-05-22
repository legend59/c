using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProcessExe
{
    class Program
    {
        static string getProcessOutput(string fileName, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = fileName;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;
            start.Arguments = args;  // argument 전달

            Process process = Process.Start(start);         // process를 실행시키고
            StreamReader reader = process.StandardOutput;   // 출력되는 값을 가져오기 위해 StreamReader에 연결 
            return reader.ReadLine();                       // 출력값의 한 라인을 읽는다
        }

        static void doWork(Object obj)
        {
            Tuple<int, int> nums = (Tuple<int, int>)obj;
            string args = nums.Item1.ToString() + " " + nums.Item2.ToString();
            string sum = getProcessOutput("add_2sec.exe", args);
            Console.WriteLine(nums.Item1 + " + " + nums.Item2 + " = " + sum);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Start - " + DateTime.Now);
            
            List<Thread> thList = new List<Thread>();
            StreamReader sr = new StreamReader("NUM.TXT");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] words = line.Split(' ');
                int num1 = int.Parse(words[0]);
                int num2 = int.Parse(words[1]);

                Thread th = new Thread(doWork);
                Object obj = (Object)new Tuple<int, int>(num1, num2);
                th.Start(obj);
                thList.Add(th);
            }

            sr.Close();

            foreach (var th in thList)
            {
                th.Join();
            }

            Console.WriteLine("End - " + DateTime.Now);
        }
    }
}
