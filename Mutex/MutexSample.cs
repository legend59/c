using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading; 

namespace ThreadTest
{
    class ThreadSample
    {
        private static Mutex mut = new Mutex();

        static void PrintNums(string str)
        {
            Console.WriteLine("[" + str + "]");

            for (int i = 1; i <= 30; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        public class Worker
        {
            // This method will be called when the thread is started. 
            public void DoWork()
            {
                mut.WaitOne();
                PrintNums(name);
                mut.ReleaseMutex();
            }
            public string name;
        }

        static void Main(string[] args)
        {
            // Create the thread object. This does not start the thread. 
            Worker workerObject1 = new Worker();
            workerObject1.name = "Thread1";
            Thread workerThread1 = new Thread(workerObject1.DoWork);

            Worker workerObject2 = new Worker();
            workerObject2.name = "Thread2";
            Thread workerThread2 = new Thread(workerObject2.DoWork);


            // Start the worker thread. 
            workerThread1.Start();
            workerThread2.Start();

            mut.WaitOne();
            PrintNums("Main");
            mut.ReleaseMutex();

            // Use the Join method to block the current thread  
            // until the object's thread terminates. 
            workerThread1.Join();
            workerThread2.Join();
        }
    }
}
