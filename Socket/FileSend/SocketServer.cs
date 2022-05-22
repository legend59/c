using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServer
{
    class SocketServer
    {
        static Socket listener;
        static string ReceiveFolder;
        static void Main(string[] args)
        {
            ReceiveFolder = "./ServerFiles";
            Directory.CreateDirectory(ReceiveFolder);
            Thread serverThread = new Thread(ServerWork);
            serverThread.Start();

            string strLine;
            while (true)
            {
                strLine = Console.ReadLine();

                if (strLine.Equals("QUIT"))
                {
                    listener.Close();
                    break;
                }
            }

            serverThread.Join();
        }

        static void ServerWork()
        {
            const int BUF_SIZE = 4096;
            const int DEFAULT_PORT = 27015;

            // Data buffer for incoming data.
            byte[] bytes = new Byte[BUF_SIZE];

            // Establish the local endpoint for the socket.
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, DEFAULT_PORT);

            // Create a TCP/IP socket.
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.
            try
            {
                while (true)
                {
                    // Program is suspended while waiting for an incoming connection.
                    Socket handler = listener.Accept();

                    NetworkStream ns = new NetworkStream(handler);
                    BinaryReader br = new BinaryReader(ns);
                    FileStream fs = null;

                    try
                    {
                        string filename;
                        // 파일이름 수신
                        while ((filename = br.ReadString()) != null)
                        {
                            // 파일크기 수신
                            int length = (int)br.ReadInt64();

                            fs = new FileStream(ReceiveFolder + "/" + filename, FileMode.Create);
                            while (length > 0)
                            {
                                // 파일내용 수신
                                int nReadLen = br.Read(bytes, 0, Math.Min(BUF_SIZE, length));
                                fs.Write(bytes, 0, nReadLen);
                                length -= nReadLen;
                            }
                            fs.Close();
                            Console.WriteLine(filename + " is received.");
                        }

                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                    catch (EndOfStreamException e)
                    {
                        Console.WriteLine("Finish Recieve...");
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
            }
            catch (SocketException e)
            {
                //Console.WriteLine(e.ToString());
                Console.WriteLine("Finish Server Work...");
            }

            listener.Close();
        }
    }
}
