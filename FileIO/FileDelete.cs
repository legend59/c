using System;
using System.IO;

public class Program
{
    public static void Main()
    {
        string filePath = "경로\\파일명.ext"; // 삭제할 파일의 경로

        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath); // 파일 삭제
                Console.WriteLine("파일이 성공적으로 삭제되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("파일 삭제 중 오류가 발생했습니다: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("파일이 존재하지 않습니다.");
        }
    }
}
