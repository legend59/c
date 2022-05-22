using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.Concurrent;

namespace Question4_Server
{
    public static class ReportHandling
    {
        private static int seqNo = 0;

        private static ConcurrentDictionary<int, string> dicReport = new ConcurrentDictionary<int, string>();
        public static int IncreaseSeqNo()
        {
            seqNo++;
            return seqNo;
        }

        public static void storeReport(string reportId, string report)
        {
            dicReport[int.Parse(reportId)] = report;
        }

        public static bool removeReport(string reportId)
        {
            Console.WriteLine("Remove : " + reportId);
            bool res = dicReport.TryRemove(int.Parse(reportId), out string val);
            return res;
        }

        public static string MakeReport(string strDate)
        {
            Dictionary<int, Report> m;

            m = new Dictionary<int, Report>();

            string strPath = "..\\SERVER";
            string[] fileEntries = Directory.GetFiles(strPath);

            foreach (string fileName in fileEntries)
            {
                string onlyFileName = fileName.Substring(strPath.Length + 1);
                if (onlyFileName.Length == 27 && onlyFileName.Substring(9, 8).Equals(strDate))
                {
                    AnalysisData(m, fileName);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, Report> items in m)
            {
                string s = items.Value.getStrID() + " " + items.Value.getCheckCard() + " " + items.Value.getFailCard() + "\n";
                sb.Append(s);
            }

            return sb.ToString(); 
        }

        private static void AnalysisData(Dictionary<int, Report> m, string path)
        {
            string line;
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                if (line == "")
                    break;
                Report userReport = new Report();

                int key = Convert.ToInt32(line.Substring(5, 3));
                if (!m.ContainsKey(key))
                {
                    userReport.setStrID(line.Substring(0, 8));
                    userReport.setCheckCard(1);
                    if (line.Substring(49, 1).Equals("1"))
                    {
                        userReport.setFailCard(0);
                    }
                    else
                    {
                        userReport.setFailCard(1);
                    }
                    m.Add(key, userReport);
                }
                else
                {
                    m[key].increaseCheckCard();
                    if (!line.Substring(49, 1).Equals("1"))
                    {
                        m[key].increaseFailCard();
                    }
                }
            }
            file.Close();
        }

        public static bool SaveReportFile(string reportId, string type)
        {
            if (!dicReport.ContainsKey(int.Parse(reportId)))
                return false;

            string path = "..\\SERVER\\REPORT";
            Directory.CreateDirectory(path);
            
            string filename = string.Format("{0}\\{1}_{2}.TXT", path, type, reportId);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(dicReport[int.Parse(reportId)]);
            }

            return true;
        }
    }
}
