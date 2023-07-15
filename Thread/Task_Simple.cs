using System;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        Task task = Task.Run(DoWork);
        task.Wait(); // 작업이 완료될 때까지 대기
        
        Console.WriteLine("작업 완료!");
    }

    static void DoWork()
    {
        // 비동기로 실행될 작업
        Console.WriteLine("작업 중...");
        Task.Delay(2000).Wait(); // 2초 대기
        Console.WriteLine("작업 완료!");
    }
}
