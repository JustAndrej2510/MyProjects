using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Lab_15
{
    class Proc
    {
        public static void CurrentProcesses()
        {
            Process[] curr = Process.GetProcesses();
            Console.WriteLine("\nИнформация о текущий процессах\n");
            foreach (var i in curr)
            {
                Console.WriteLine($"Name: {i.ProcessName}\nID: {i.Id}\nPriority: {i.BasePriority}\n Start time: {i.StartTime}\nTime of using processor: {i.UserProcessorTime}\nCurrent state: {i.StartInfo}");
            }

        }
    }

    class Program
    {
        public static void CurrentProcesses()
        {
            try
            {
                Process[] curr = Process.GetProcesses();

                using (StreamWriter writer = new StreamWriter("processes.txt"))
                {
                    writer.WriteLine("\nИнформация о текущий процессах\n");
                    foreach (var i in curr)
                    {
                        writer.WriteLine($"Name: {i.ProcessName}\nID: {i.Id}\nPriority: {i.BasePriority}\nCurrent state: {i.StartInfo}");
                        try
                        {
                            writer.WriteLine($"StartTime: {i.StartTime}");
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine($"StartTime: {ex.Message}");
                        }

                        try
                        {
                            writer.WriteLine($"TotalProcessorTime: {i.TotalProcessorTime}");
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine($"TotalProcessorTime: {ex.Message}");
                        }
                    }
                    Console.WriteLine("Данные о процессах записаны в файл");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static void DomainInfo()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            Assembly[] assemblies = domain.GetAssemblies();
            using (StreamWriter wr = new StreamWriter("domainInfo.txt"))
            {
                wr.WriteLine($"Name: {domain.FriendlyName}");
                wr.WriteLine($"Base Directory: {domain.BaseDirectory}");
                wr.WriteLine("\nAssemlies: ");
                foreach (Assembly asm in assemblies)
                    wr.WriteLine(asm.GetName().Name);
                Console.WriteLine("Информация о домене записана в файл");
            }

        }

        public static void WriteOnConsole(object _n)
        {
            int n = (int)_n;
            using (StreamWriter wr = new StreamWriter("Count.txt"))
            {
                for (var i = 1; i < n; i++)
                {
                    Thread.Sleep(10);
                    Console.WriteLine(i);
                    wr.WriteLine(i);
                    Thread.Sleep(500);
                }

            }

        }

        static Mutex mutex = new Mutex();
        static void Main(string[] args)
        {
            CurrentProcesses();
            DomainInfo();
            Console.Write("Введите число: ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Счетчик до n");
            Thread mythread = new Thread(new ParameterizedThreadStart(WriteOnConsole))
            {
                Name = "MyThread",
                Priority = ThreadPriority.BelowNormal,
            };
            try
            {

                ManualResetEvent me = new ManualResetEvent(true);
                mythread.Start(n);
                me.Reset();
                // mythread.Interrupt();
                Console.WriteLine($"Thread's name: {mythread.Name}");
                Console.WriteLine($"Thread's priority: {mythread.Priority}");
                Console.WriteLine($"Thread's state: {mythread.ThreadState}");
                Console.WriteLine($"ID: {Thread.GetDomainID()}");
                me.Reset();
                Thread.Sleep(100);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mythread.Join();
            }

            Console.WriteLine("Введите число n: ");
            int x = int.Parse(Console.ReadLine());
            Thread tr1 = new Thread(new ParameterizedThreadStart(Even))
            {
                Name = "EvenNumbers",
            };

            Thread tr2 = new Thread(new ParameterizedThreadStart(Odd))
            {
                Name = "OddNumbers",
            };
            tr2.Start(x);
            //Thread.Sleep(20);
            tr1.Start(x);
            tr1.Join();
            tr2.Join();
            //Thread.Sleep(5000);
            Console.WriteLine("\n");
            //Timer
            int num = 1;
            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(Count);
            // создаем таймер
            Timer timer = new Timer(tm, num, 0, 1000);
            Thread.Sleep(5000);


        }

        public static void Even(object obj)
        {
            int n = (int)obj;
            int i = 0;
            while (i < n)
            {
                mutex.WaitOne();
                using (StreamWriter wr = new StreamWriter("numbers.txt", true))
                {
                    i += 2;
                    Thread.Sleep(20);
                    Console.WriteLine(i);
                    wr.WriteLine(i);
                    Thread.Sleep(500);

                }
                mutex.ReleaseMutex();
            }
        }
            public static void Odd(object obj)
            {
                int n = (int)obj;
                int i = 1;
                while (i < n)
                {
                    mutex.WaitOne();
                    using (StreamWriter wr = new StreamWriter("numbers.txt", true))
                    {

                        Thread.Sleep(10);
                        Console.WriteLine(i);
                        wr.WriteLine(i);
                        Thread.Sleep(500);
                        i += 2;
                    }
                    mutex.ReleaseMutex();
                }
            }

        //static int temp = 0;
        //public static void Num(object _n)
        //{
        //    int n = (int)_n;
        //    while(true)
        //    {
        //        mutex.WaitOne();
        //        using (StreamWriter wr = new StreamWriter("numbers.txt", true))
        //        {
        //            temp++;
        //            Thread.Sleep(10);
        //            Console.WriteLine(temp);
        //            wr.WriteLine(temp);
        //            Thread.Sleep(500);
        //        }
        //        mutex.ReleaseMutex();
        //    }

        //}

        //mutex.WaitOne();
        //using (StreamWriter wr = new StreamWriter("numbers.txt", true))
        //{
        //    for (var i = 2; i < n; i += 2)
        //    {

        //        Thread.Sleep(10);
        //        Console.WriteLine(i);
        //        wr.WriteLine(i);
        //        Thread.Sleep(500);
        //        //Thread.Yield();
        //    }

        //}
        //mutex.ReleaseMutex();



        //mutex.WaitOne();
        //using (StreamWriter wr = new StreamWriter("numbers.txt", true))
        //{
        //    for (var i = 1; i < n; i += 2)
        //    {

        //        Thread.Sleep(10);
        //        Console.WriteLine(i);
        //        wr.WriteLine(i);
        //        Thread.Sleep(500);
        //        Thread.Yield();
        //    }

        //}
        //mutex.ReleaseMutex();

        public static void Count(object obj)
        {
            int x = (int)obj;
            for (int i = 1; i < 9; i++, x++)
            {
                Console.WriteLine($"{x * i}");
            }
        }

    }

    }

