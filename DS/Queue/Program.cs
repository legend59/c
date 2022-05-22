using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    class Program
    {
        static Dictionary<string, MsgQueue> queues;
        static void Main(string[] args)
        {
            // Queue Name - Id - (size, status, msg)
            queues = new Dictionary<string, MsgQueue>();

            while (true)
            {
                string line = Console.ReadLine();
                string[] words = line.Split(' ');
                string command = words[0];
                string qname = words[1];
                switch(command)
                {
                    case "CREATE":
                        Console.WriteLine(QCreate(qname, int.Parse(words[2])));
                        break;
                    case "ENQUEUE":
                        string message = words[2];
                        Console.WriteLine(QEnqueue(qname, message));
                        break;
                    case "DEQUEUE":
                        Console.WriteLine(QDequeue(qname));
                        break;
                    case "GET":
                        Console.WriteLine(QGet(qname));
                        break;
                    case "SET":
                        Console.WriteLine(QSet(qname, int.Parse(words[2])));
                        break;
                    case "DEL":
                        Console.WriteLine(QDel(qname, int.Parse(words[2])));
                        break;
                    default:
                        break;
                }
            }
        }

        static string QCreate(string name, int size)
        {
            if (queues.ContainsKey(name))
                return "Queue Exist";

            var q = new MsgQueue(size);

            queues.Add(name, q);

            return "Queue Created";
        }

        static string QEnqueue(string name, string msg)
        {
            return queues[name].MsgEnqueue(msg);
        }

        static string QDequeue(string name)
        {
            return queues[name].MsgDequeue(); 
        }

        static string QGet(string name)
        {
            return queues[name].MsgGet();
        }

        static string QSet(string name, int id)
        {
            return queues[name].MsgSet(id);
        }
        static string QDel(string name, int id)
        {
            return queues[name].MsgDel(id);
        }
    }
}
