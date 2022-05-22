using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Question3_Client
{
    class Validator
    {
        public bool CheckIdPsw(string id, string psw)
        {
            bool bRet = false;
            String encPsw = CardUtility.passwordEncryption_SHA256(psw);

            string[] lines = System.IO.File.ReadAllLines(@"..\\CLIENT\\INSPECTOR.TXT");
            foreach (string line in lines)
            {
                String fileId = line.Substring(0, 8);
                String filePsw = line.Substring(9);

                if (id.Equals(fileId) && encPsw.Equals(filePsw))
                {
                    bRet = true;
                    break;
                }
            }

            return bRet;
        }

        // cardInfo : [카드ID(8)][버스번호(7)][승차/하차 코드(1)][최근 승차시각(14)]
        // sample : CARD_001BUS_001N20171019143610
        public void InspectCard(string startTime, string id, string busID, string cardInfo)
        {
            string strValidateCode;

            // cardInfo parsing 
            //string cardID = cardInfo.Substring(0, 8); 
            string cardBusID = cardInfo.Substring(8, 7);
            string code = cardInfo.Substring(15, 1);
            string rideTime = cardInfo.Substring(16);

            // Get Inspect Time
            string strInspectTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Validation
            // 1. Bus ID Match
            if (busID.Equals(cardBusID))
            {
                // 2. Check Aboard Code
                if (code.Equals("N"))
                {
                    // 3. Time Difference
                    if (CardUtility.HourDiff(strInspectTime, rideTime) < 3)
                    {
                        strValidateCode = "R1";
                    }
                    else
                    {
                        strValidateCode = "R4";
                    }
                }
                else
                {
                    strValidateCode = "R3";
                }
            }
            else
            {
                strValidateCode = "R2";
            }

            // Create Folder
            string destFolder = "..\\..\\..\\" + id;
            System.IO.Directory.CreateDirectory(destFolder);

            // File Writing
            string strFilename = destFolder + "\\" + id + "_" + startTime + ".TXT";
            string strWrite = id + "#" + busID + "#" + cardInfo + "#" + strValidateCode + "#" + strInspectTime + "\n";
            FileStream fs = new System.IO.FileStream(strFilename, FileMode.Append);
            fs.Write(Encoding.UTF8.GetBytes(strWrite), 0, strWrite.Length);
            fs.Close();
        }

        public void SendFiles(String id)
        {
            DirectoryInfo di = new DirectoryInfo(String.Format("..\\{0}", id));
            FileInfo[] fiArr = di.GetFiles();

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 27015);

            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(remoteEP);

                NetworkStream ns = new NetworkStream(sender);
                BinaryWriter bw = new BinaryWriter(ns);
                const int BufSize = 4096; 
                byte[] buffer = new byte[BufSize];

                foreach (FileInfo infoFile in fiArr)
                {
                    string onlyFilename = infoFile.FullName.Substring(infoFile.FullName.Length - 27, 27);
                    bw.Write(onlyFilename);
                    bw.Write(infoFile.Length);
                    long lSize = infoFile.Length;
                    FileStream fs = new FileStream(infoFile.FullName, FileMode.Open);
                    while (lSize > 0)
                    {
                        int nReadLen = fs.Read(buffer, 0, Math.Min(BufSize, (int)lSize));
                        bw.Write(buffer, 0, nReadLen);
                        lSize -= BufSize;
                    }
                    fs.Close();
                    File.Move(infoFile.FullName, "..\\BACKUP\\" + onlyFilename);
                }
                bw.Close();
                ns.Close();
                sender.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}