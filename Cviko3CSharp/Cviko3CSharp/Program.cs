using System.IO;
using static Cviko3CSharp.Stack;

namespace Cviko3CSharp
{
    internal class Program
    {
        private static async Task<int> StringLength()
        {
            //Task<string> task = File.ReadAllTextAsync("data.txt");
            string txt = await File.ReadAllTextAsync("data.txt");
            return txt.Length;
        }
        private static async Task<int> GetInt()
        {
            return 1;
        }
        private static Task<int> GetInt2()
        {
            return Task.FromResult(1);
        }
        private static Task<int> GetInt3()
        {
            return GetInt();
        }
        private static async Task Experiment()
        {
            Console.WriteLine("Start");
            await Task.Delay(1000);
            using StreamWriter sw = new StreamWriter("data.txt");
            await sw.WriteLineAsync("Jde to");
            await Task.Delay(1000);
            Console.WriteLine("End");
        }
        static async Task Main(string[] args)//Async void je big nono
        {
            Task task = File.WriteAllTextAsync("data.txt", "Můj obsah");
            Console.WriteLine("Pokračuju");
            await task;
            int result = await StringLength();
            Console.WriteLine("Dokončili jsme práci");
            await Experiment();
            Console.ReadLine();
            /*SimpleStack<int> stack = new SimpleStack<int>();
            Random r = new Random();
            List<Thread> listOfThreads = new List<Thread>();
            object obj = new object();
            Thread thread = new Thread(() =>
            {
                while(true)
                {
                    lock (obj)
                    {
                        stack.Push(r.Next(100));
                        Monitor.Pulse(obj);
                    }
                    Thread.Sleep(100);
                }
            });
            thread.Start();
            for (int i = 0; i < 5; i++)
            {
                Thread threadTMP = new Thread(() =>
                {
                    while (true)
                    {
                        Monitor.Enter(obj);
                        try
                        {
                            if (!stack.IsEmpty)
                            {
                                stack.Pop();
                            }
                        }
                        finally
                        {
                            Monitor.Exit(obj);
                        }
                        lock (obj)
                        {
                            if (!stack.IsEmpty)
                            {
                                int val = stack.Pop();
                                Console.WriteLine("Ziskana value: " + val + "Thread ID: " + Thread.CurrentThread.ManagedThreadId);
                            }
                            else
                            {
                                //Console.WriteLine("Empty");
                                Monitor.Wait(obj);
                            }
                        }

                    }
                }); 
                listOfThreads.Add(threadTMP);
                threadTMP.Start();
            }*/
        }
    }
}
