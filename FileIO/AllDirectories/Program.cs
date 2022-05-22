// See https://aka.ms/new-console-template for more information

namespace SP_EX01
{ 
    class Program
    {
        static void Main(string[] args)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(".", "*", System.IO.SearchOption.AllDirectories);
            foreach (string subdirectory in subdirectoryEntries)
            {
                Console.WriteLine("[{0}]", subdirectory);

                string[] fileEntries = Directory.GetFiles(subdirectory);

                foreach (string fileName in fileEntries)
                {
                    FileInfo fInfo = new FileInfo(fileName);

                    long fleSize = fInfo.Length;
                    string dirName = fInfo.DirectoryName;
                    string newDirNAme = dirName.Replace("\\INPUT\\", "\\OUTPUT\\");
                    Console.WriteLine("name = " + fileName + " size = " + fleSize);
                    Console.WriteLine("dir name" + fInfo.DirectoryName);
                    Console.WriteLine("dir name newDirNAme " + newDirNAme);
                }
            }
        }
    }
}