// See https://aka.ms/new-console-template for more information
public class Worker
{
    string p1;
    string p2;

    private Worker(string p1, string p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }

    public void DoWork()
    {
        Console.WriteLine("");
    }
}

Console.WriteLine("Hello, World!");

string fileName = "./NUM.TXT";

string line;
StreamReader sr = new StreamReader(fileName);

while((line = sr.ReadLine()) != null)
{
    string[] wordls = line.Split(' ');

    //Console.WriteLine(wordls[0]);
    //Console.WriteLine(wordls[1]);
    Worker workerObject1 = new Worker();
    Thread workerThread1 = new Thread(workerObject1.DoWork);
    workerThread1.Start();

    workerThread1.Join();
}