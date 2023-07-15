/*
위의 예제에서는 세 개의 Task를 병렬로 실행하고, 그 결과를 처리하는 방법을 보여줍니다.
CalculateSum() 메서드는 두 개의 정수를 받아서 합을 계산하는 작업을 수행하며,
각 작업은 2초 동안 대기한 후 결과를 반환합니다.

Main() 메서드에서는 세 개의 Task를 Task.Run()을 통해 실행하고, Task.WhenAll() 메서드를
사용하여 모든 작업이 완료될 때까지 대기합니다. 그런 다음 ContinueWith() 메서드를 사용하여
작업이 완료된 후에 ProcessResults() 메서드를 호출하여 결과를 처리합니다.

위의 예제를 실행하면 세 개의 Task가 병렬로 실행되고, 모든 작업이 완료된 후에 결과가 출력됩니다.
이 예제는 여러 작업을 조율하고 결과를 처리하는 방법을 보여주며, Task의 강력한 기능을 활용할 수 있는 예입니다.
*/
using System;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        Task<int> task1 = Task.Run(() => CalculateSum(1, 2));
        Task<int> task2 = Task.Run(() => CalculateSum(3, 4));
        Task<int> task3 = Task.Run(() => CalculateSum(5, 6));

        Task<int[]> allTasks = Task.WhenAll(task1, task2, task3);
        allTasks.ContinueWith(t => ProcessResults(t.Result));

        Console.WriteLine("작업 진행 중...");

        Console.ReadLine();
    }

    static int CalculateSum(int a, int b)
    {
        Console.WriteLine($"Task: {Task.CurrentId} 시작");
        Task.Delay(2000).Wait(); // 2초 대기
        int sum = a + b;
        Console.WriteLine($"Task: {Task.CurrentId} 완료");
        return sum;
    }

    static void ProcessResults(int[] results)
    {
        Console.WriteLine("결과 처리 중...");
        foreach (int result in results)
        {
            Console.WriteLine("결과: " + result);
        }
    }
}
