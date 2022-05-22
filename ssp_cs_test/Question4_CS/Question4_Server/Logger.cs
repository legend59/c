using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Question4_Server
{
    public static class Logger
    {
        static Mutex mu = new Mutex();
        public static void WriteLog(params string[] paramArray)
        {
            string folderPath = "..\\SERVER\\LOG";
            Directory.CreateDirectory(folderPath);

            DateTime now = DateTime.Now;
            string filePath = string.Format("{0}\\LOG_{1:D2}{2:D2}{2:D2}.TXT", folderPath, now.Hour, now.Minute, now.Second);

            mu.WaitOne();
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                string strDT = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string strParam = string.Join(" | ", paramArray);
                string strLog = string.Format("[{0}] {1}",strDT, strParam);
                sw.WriteLine(strLog);
            }
            mu.ReleaseMutex();
        }
    }
}
