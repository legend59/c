using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Question4_Server
{
    class CardSocketServer
    {
        public Socket listener;

        // This method will be called when the thread is started. 
        public void DoSocketWork()
        {
            Console.WriteLine("Socket Server Thread Start");

            byte[] buffer = new byte[4096];
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEP = new IPEndPoint(ipAddress, 27015);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEP);
                listener.Listen(10);

                // Start listening for connections. 
                while (true)
                {
                    // Program is suspended while waiting for an incoming connection. 
                    Socket handler = listener.Accept();

                    NetworkStream ns = new NetworkStream(handler);
                    BinaryReader br = new BinaryReader(ns);

                    string filename;
                    while ((filename = br.ReadString()) != null)
                    {
                        int length = (int)br.ReadInt64();

                        while (length > 0)
                        {
                            int nReadLen = br.Read(buffer, 0, Math.Min(4096, length));
                            SaveFile(filename, buffer, nReadLen);
                            length -= nReadLen;
                        }
                    }
                    Console.WriteLine("Received Files!");
                }
            }
            catch (Exception e)
            {

            }
        }
        //public void DoSocketWork()
        //{
        //    Console.WriteLine("Socket Server Thread Start");

        //    const int DEFAULT_BUFLEN = 4096;
        //    const int DEFAULT_PORT = 27015;
        //    const int FILENAME_LEN = 27;
        //    const int HEADER_LEN = (1 + FILENAME_LEN + 4);

        //    // Data buffer for incoming data.
        //    byte[] recvbuf = new byte[DEFAULT_BUFLEN + HEADER_LEN];
        //    int recvLen;
        //    string filename = "";

        //    // Establish the local endpoint for the socket.
        //    IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        //    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, DEFAULT_PORT);

        //    // Create a TCP/IP socket.
        //    listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //    // Bind the socket to the local endpoint and 
        //    // listen for incoming connections.
        //    try
        //    {
        //        listener.Bind(localEndPoint);
        //        listener.Listen(10);

        //        // Start listening for connections.
        //        while (true)
        //        {
        //            // Program is suspended while waiting for an incoming connection.
        //            Socket handler = listener.Accept();

        //            int nBufIndex = 0;  // 메시지를 담을 위치 인덱스
        //            int cnt = 0;        // 메시지 처리 인덱스
        //            int nDataSize = 0;	// 파일 데이터 크기
        //            while ((recvLen = handler.Receive(recvbuf, nBufIndex, DEFAULT_BUFLEN, SocketFlags.None)) > 0)
        //            {
        //                recvLen += nBufIndex;

        //                while (cnt < recvLen)
        //                {
        //                    if (recvbuf[cnt] == '*')    // 메시지 시작 문자
        //                    {
        //                        // 처리하고 남은 버퍼의 크기가 헤더 사이즈보다 작으면 receive를 다시 해서 버퍼를 채운다 
        //                        if (recvLen - cnt < HEADER_LEN)
        //                        {
        //                            Buffer.BlockCopy(recvbuf, cnt, recvbuf, 0, recvLen - cnt);  // 맨 끝에 남아있는 내용을 앞으로 이동.
        //                            nBufIndex = recvLen - cnt;                                  // 새로 받을 버퍼의 위치 표시 
        //                            cnt = 0;                                                    // 버퍼 처리는 맨 처음부터 
        //                            break;
        //                        }
        //                        cnt++;

        //                        filename = Encoding.Default.GetString(recvbuf, cnt, FILENAME_LEN);
        //                        cnt += FILENAME_LEN;

        //                        nDataSize = BitConverter.ToInt32(recvbuf, cnt);
        //                        cnt += sizeof(int);

        //                        if (cnt == recvLen)
        //                        { // 처리한 데이터가 받은 메시지의 크기에 도달하면 -> 헤더만 받은 경우
        //                            cnt = 0;
        //                            nBufIndex = 0;
        //                            break;
        //                        }
        //                    }

        //                    // 파일 데이터가 남아있는 버퍼보다 클 경우. 버퍼에 있는 것만 일단 쓰고 나머지는 다시receive하여 처리한다.
        //                    // 파일에 이미 쓴 크기를 nSize(파일크기)에서 빼서, 앞으로 쓸 크기만큼만 계산해 놓는다. 
        //                    if (recvLen - cnt <= nDataSize)
        //                    {
        //                        string szFullPath = "..\\SERVER\\" + filename;
        //                        SaveReceiveFile(szFullPath, recvbuf, cnt, recvLen - cnt);
        //                        nDataSize -= (recvLen - cnt);   // 처리한 (파일에 쓴) 크기만큼 줄인다.  
        //                        cnt = 0;
        //                        nBufIndex = 0;
        //                        break;
        //                    }
        //                    // 파일 데이터가 남아있는 버퍼에 모두들어있는 경우. 
        //                    // 데이터 다음에 *부터 다시 시작하는 데이터가 들어있는 것임, while(cnt < recvLen)문을 돌며 계속 처리.
        //                    else
        //                    {
        //                        string szFullPath = "..\\SERVER\\" + filename;
        //                        SaveReceiveFile(szFullPath, recvbuf, cnt, nDataSize);
        //                        cnt += nDataSize;
        //                        nDataSize = 0;
        //                    }
        //                }
        //            }

        //            handler.Shutdown(SocketShutdown.Both);
        //            handler.Close();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Close Socket Listener");
        //    }

        //    listener.Close();
        //}

        private void SaveFile(string fname, byte[] buf, int len)
        {
            string path = "..\\SERVER\\" + fname;
            FileStream fs = new FileStream(path, FileMode.Append);
            fs.Write(buf, 0, len);
            fs.Close();
        }

        private static void SaveReceiveFile(string fullPath, byte[] buf, int index, int length)
        {
            FileStream fs = new FileStream(fullPath, FileMode.Append);
            fs.Write(buf, index, length);
            fs.Close();
        }
    }
}
