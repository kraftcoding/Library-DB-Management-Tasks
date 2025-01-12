using LibraryDBManagement.Tasks.Model;
using System;
using LibraryDBManagement.Tasks.Core.Managers;
using LibraryDBManagement.Tasks.Core;
using System.Threading;
using System.Diagnostics;

namespace LibraryDBManagement.Tasks
{
    internal class Program
    {

        static readonly object _locker = new object();
        static bool _go;
        //static int _threads = 2; 
        static void Main(string[] args)
        {
            Console.WriteLine("Insert num of threads");
            int.TryParse( Console.ReadLine(), out int result);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            WebAPIManager webAPIManager = new WebAPIManager();

            #region Get Test
            //Console.WriteLine("step 1");
            //User Usr = WebAPIManager.GetUser(1).Result;
            //Console.WriteLine(Usr.Email);

            //Console.WriteLine("step 2");
            //Usr = WebAPIManager.GetUser(2).Result;
            //Console.WriteLine(Usr.Email);

            //Console.WriteLine("step 3");
            //Usr = WebAPIManager.GetUser(3).Result;
            //Console.WriteLine(Usr.Email);

            #endregion

            new Thread(Work).Start();
            //Console.WriteLine("Press Enter to pulse worker thread");
            //Console.ReadLine();

            lock (_locker)
            {// setting _go=true and pulsing.
                _go = true;
                Monitor.Pulse(_locker);
            }

            //Console.WriteLine("Demo of Producer/Consumer Queue with monitor wait/pulse");
            var q = new PCQueue(result);
            //Console.WriteLine("Enqueuing 10 items...");

            for (int i = 0; i < 10000; i++)
            {
                int itemNumber = i;      // To avoid the captured variable trap
                q.EnqueueItem(() =>
                {
                    //Thread.Sleep(1000);          // Simulate time-consuming work
                    Console.Write(" Task" + itemNumber);
                    User Usr = EntityManager.GetUserObject($"usr{itemNumber}", $"usr{itemNumber}@email.com", "1234", "test");
                    Uri uri = WebAPIManager.CreateUser(Usr).Result;
                });
            }

            q.Shutdown(true);
            Console.WriteLine();
            Console.WriteLine("Workers complete!");

            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            //delay
            Console.ReadLine();

        }

        static void Work()
        {
            Console.WriteLine("work thread started");
            lock (_locker)
            {
                while (!_go)
                {
                    Monitor.Wait(_locker);    // Lock is released while we’re waiting
                    // lock is regained
                }
            }
            Console.WriteLine("Woken!!!");
        }
    }
}
