using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    class SocketClient
    {
        static void Main(string[] args)
        {
            const string strIP = "127.0.0.1";
            const int BUF_SIZE = 4096;
            const int PORT = 27015;

            // Data buffer for incoming data.
            byte[] bytes = new byte[BUF_SIZE];

            // Establish the remote endpoint for the socket.
            IPAddress ipAddress = IPAddress.Parse(strIP);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                sender.Connect(remoteEP);

                NetworkStream ns = new NetworkStream(sender);
                BinaryWriter bw = new BinaryWriter(ns);

                DirectoryInfo di = new DirectoryInfo("./ClientFiles");
                FileInfo[] fiArr = di.GetFiles();
                foreach (FileInfo infoFile in fiArr)
                {
                    // 파일이름 전송
                    bw.Write(infoFile.Name);
                    long lSize = infoFile.Length;

                    // 파일크기 전송
                    bw.Write(lSize);

                    // 파일내용 전송
                    FileStream fs = new FileStream(infoFile.FullName, FileMode.Open);
                    while (lSize > 0)
                    {
                        int nReadLen = fs.Read(bytes, 0, Math.Min(BUF_SIZE, (int)lSize));
                        bw.Write(bytes, 0, nReadLen);
                        lSize -= nReadLen;
                    }
                    fs.Close();
                }

                bw.Close();
                ns.Close();

                // Release the socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

                Console.WriteLine("Finished.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
    }
}
