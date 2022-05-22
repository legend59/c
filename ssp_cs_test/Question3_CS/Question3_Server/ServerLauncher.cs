using System;
using System.Threading;

namespace Question3_Server
{
    class ServerLauncher
    {
        static void Main(string[] args)
        {
            // Sokcet Server Start
            CardSocketServer cardServer = new CardSocketServer();
            Thread socketServerThread = new Thread(cardServer.DoSocketWork);
            socketServerThread.Start();

            string strLine;
            while (true)
            {
                strLine = Console.ReadLine();

                if (strLine.Equals("QUIT"))
                {
                    cardServer.listener.Close();
                    break;
                }
                else 
                {
                    Console.WriteLine("If you want to quit, input \"QUIT\"");
                }
            }

            socketServerThread.Join();
        }
    }
}
