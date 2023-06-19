// Map
// Dictionary
using System;
using System.Collections.Generic;
 
namespace Sample
{
  class Sample
  {
    static void Main()
    {
      var myTable = new Dictionary<string, string>();
      myTable.Add("Hokkaido", "Sapporo");
      myTable.Add("Iwate", "Morioka");
      myTable.Add("Miyagi", "Sendai");
      
      foreach(KeyValuePair<string, string> item in myTable) {
        Console.WriteLine("[{0}:{1}]", item.Key, item.Value);  
      }
      
      Console.ReadKey();
    }
  }
}

// SortedDictionary
// Key로 정렬하여 저장

// ConcurrentDictionary
// Thread-safe하게 사용
// https://zosystem.tistory.com/246
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
 
namespace ConsoleApp1
{
 
    public class Program
    {
        static Dictionary<string, int> _mydic = new Dictionary<string, int>();
        static ConcurrentDictionary<string, int> _mydictConcu = new ConcurrentDictionary<string, int>();
        static void Main()
        {
            Thread mythread1 = new Thread(new ThreadStart(InsertData));
            Thread mythread2 = new Thread(new ThreadStart(InsertData));
            mythread1.Start();
            mythread2.Start();
            mythread1.Join();
            mythread2.Join();
            Thread mythread11 = new Thread(new ThreadStart(InsertDataConcu));
            Thread mythread21 = new Thread(new ThreadStart(InsertDataConcu));
            mythread11.Start();
            mythread21.Start();
            mythread11.Join();
            mythread21.Join();
            Console.WriteLine(string.Format("Result in Dictionary : {0}", _mydic.Values.Count));
            Console.WriteLine("********************************************");
            Console.WriteLine(string.Format("Result in Concurrent Dictionary : {0}", _mydictConcu.Values.Count));
            Console.ReadKey();
        }
        static void InsertData()
        {
            for (int i = 0; i < 100; i++)
            {
                _mydic.Add(Guid.NewGuid().ToString(), i);
            }
        }
        static void InsertDataConcu()
        {
            for (int i = 0; i < 100; i++)
            {
                _mydictConcu.TryAdd(Guid.NewGuid().ToString(), i);
            }
        }
    }
 
}