using System;
using System.IO;

namespace FileIO
{
    class Program
    {
        static void MyCopyFile(string path, string filename)
        {
            string inFile = ".\\INPUT\\" + path + "\\" + filename;
            string outPath = ".\\OUTPUT\\" + path;
            string outFile = outPath + "\\" + filename;

            System.IO.Directory.CreateDirectory(outPath);

            const int BUF_SIZE = 512;
            byte[] buffer = new byte[BUF_SIZE];
            int nFReadLen;

            FileStream fs_in = new FileStream(inFile, FileMode.Open, FileAccess.Read);
            FileStream fs_out = new FileStream(outFile, FileMode.Create, FileAccess.Write);
            while ((nFReadLen = fs_in.Read(buffer, 0, BUF_SIZE)) > 0)
            {
                fs_out.Write(buffer, 0, nFReadLen);
            }
            fs_in.Close();
            fs_out.Close();
        }

        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo("./INPUT");
            FileInfo[] fiArr = di.GetFiles("*.*", SearchOption.AllDirectories);
            foreach(var f in fiArr)
            {
                long fSize = f.Length;
                string fName = f.Name;
                string path = f.DirectoryName.Substring(di.FullName.Length);

                Console.WriteLine(".{0}\\{1}: {2}bytes.", path, fName, fSize);

                if (f.Length > 3*1024) // 3Kbyte
                {
                    MyCopyFile(path, fName);
                }
            }
        }
    }
}
