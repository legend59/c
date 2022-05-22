using System;
using System.Threading;

namespace Question4_Server
{
    class ServerLauncher
    {
        static void Main(string[] args)
        {
            // Sokcet Server Start
            CardSocketServer cardServer = new CardSocketServer();
            Thread socketServerThread = new Thread(cardServer.DoSocketWork);
            socketServerThread.Start();

            // Http Server Start
            ReportHttpServer reportServer = new ReportHttpServer();
            Thread httpServerThread = new Thread(reportServer.DoHttpServer);
            httpServerThread.Start();

            string strLine;
            while (true)
            {
                strLine = Console.ReadLine();

                if (strLine.Equals("QUIT"))
                {
                    cardServer.listener.Close();
                    reportServer.listener.Close();
                    break;
                }
                else 
                {
                    Console.WriteLine("If you want to quit, input \"QUIT\"");
                }
            }

            socketServerThread.Join();
            httpServerThread.Join();
        }
    }
}
