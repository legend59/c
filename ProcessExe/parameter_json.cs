using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string filePath = "C:\\Path\\To\\ExternalProgram.exe";
        string jsonString = "{\"key1\":\"value1\",\"key2\":\"value2\"}";
        
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = filePath;
        startInfo.Arguments = $"\"{jsonString}\""; // JSON 문자열을 인용 부호로 둘러싸서 전달
        
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("외부 프로세스 실행 중 오류가 발생했습니다: " + ex.Message);
        }
    }
}


using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string filePath = "C:\\Path\\To\\ExternalProgram.exe";
        string parameter1 = "value1";
        string parameter2 = "value2";
        
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = filePath;
        startInfo.Arguments = $"{parameter1} {parameter2}"; // 파라미터 설정
        
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("외부 프로세스 실행 중 오류가 발생했습니다: " + ex.Message);
        }
    }
}
