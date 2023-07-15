
/*
1
*/
using System;
using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        Process currentProcess = Process.GetCurrentProcess();

        Console.WriteLine("프로세스 ID: " + currentProcess.Id);
        Console.WriteLine("프로세스 이름: " + currentProcess.ProcessName);
        Console.WriteLine("시작 시간: " + currentProcess.StartTime);
        Console.WriteLine("실행 시간: " + (DateTime.Now - currentProcess.StartTime));
        //
        // Process.GetCurrentProcess().MainModule.FIleName;
    }
}


/*2
*/
using System;
using System.Reflection;

public class Program
{
    public static void Main()
    {
        string programPath = Assembly.GetEntryAssembly().Location;
        string programDirectory = System.IO.Path.GetDirectoryName(programPath);

        Console.WriteLine("프로그램 경로: " + programPath);
        Console.WriteLine("프로그램 디렉토리: " + programDirectory);
    }
}
