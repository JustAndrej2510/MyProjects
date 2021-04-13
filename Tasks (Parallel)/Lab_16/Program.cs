using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace Lab_16
{
    class Program
    {
        private static CancellationTokenSource cancelllationSource = new CancellationTokenSource();
        private static CancellationToken ct = cancelllationSource.Token;
        class Tasks
        {
            public static void PrimeNumbers()
            {
                //bool isPN = true;
                int n;
                Console.Write("Введите последнее число диапазона: ");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Простые числа в последовательности от 1 до " + n + " :");
                if (ct.IsCancellationRequested)
                {
                    Console.WriteLine("Подан запрос на отмену задачи");
                    return;
                }
                for (int i = 2; i < n; i++)
                {
                    
                    bool isPN = true;
                    for (int j = 2; j < i; j++)
                    {
                        if (i % j == 0 && i != 2)
                        {
                            isPN = false;
                            break;
                        }
                    }
                    if (isPN == true)
                    {
                        Console.Write(i + " ");
                    }
                }
                Console.WriteLine();
            }
            public static int Factorial(int x)
            {
                int result = 1;

                for (int i = 1; i <= x; i++)
                {
                    result *= i;
                }
                Console.WriteLine($"Факториал {x} равен: {result}");
                return result;
            }
        }

        private static int ProductCount = 0;
        private static BlockingCollection<string> products = new BlockingCollection<string>();
        private static void Put()
        {
            //int NumOfProducts = 2;
           
            for (int i = 0; i < 2; i++)
            {
                //ProductCount++;
                Thread.Sleep(100);
                products.Add("Товар №" + (ProductCount+1));
                Console.WriteLine($"Товар № {++ProductCount} добавлен");
            }
        }
        private static void Get()
        {
           
           while(!products.IsCompleted)
            { 
                Console.WriteLine("Покупатель забрал " + products.Take());
                Thread.Sleep(100);
            }
        }
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            //1
            for (int i = 0; i < 5; i++)
            {
                Task task1 = new Task(Tasks.PrimeNumbers);
                stopwatch.Start();
                task1.Start();
                Console.WriteLine($"ID задачи: {task1.Id}\nСтатус задачи: {task1.Status}");
                task1.Wait();
                stopwatch.Stop();
                if (task1.IsCompleted)
                {
                    Console.WriteLine($"Задача выполнена");
                }
                Console.WriteLine("Время выполнения задачи: " + stopwatch.Elapsed);
                stopwatch.Reset();
            }

            //2     CancellationToken
            
            //Task ts2 = Task.Factory.StartNew(Tasks.PrimeNumbers, ct);
            //ts2.Wait(1000);
            //cancelllationSource.Cancel();
            //ts2.Wait();

            //3_4.1   ContinueWith

            //Task<int> ts3 = Task.Factory.StartNew(() => Tasks.Factorial(5));
            //Task<int> ts4 = Task.Factory.StartNew(() => Tasks.Factorial(10));
            //Task<int> ts5 = Task.WhenAll(ts3, ts4).ContinueWith<int>(t => ts4.Result / ts3.Result);
            //Task.WaitAll(ts3, ts4, ts5);
            //Console.WriteLine("Результат деления факториала 10 на факториал 5: " + ts5.Result.ToString());

            //3_4.2 GetAwaiter & GetResult

            //Task<int> ts3 = Task.Factory.StartNew(() => Tasks.Factorial(5));
            //Task<int> ts4 = Task.Factory.StartNew(() => Tasks.Factorial(10));
            //var awaiter1 = ts3.GetAwaiter();
            //var awaiter2 = ts4.GetAwaiter();
            //awaiter1.OnCompleted(() => Console.WriteLine(awaiter1.GetResult()));
            //awaiter2.OnCompleted(() => Console.WriteLine(awaiter2.GetResult()));
            //Task.WaitAll(ts3, ts4);

            //5 Parallel For & For

            //stopwatch.Start();
            //for (int i = 0; i < 5; i++)
            //{
            //    int[] array = new int[1000000];
            //    for (int j = 0; j < array.Length; j++)
            //    {
            //        array[j] = j;
            //    }
            //}
            //stopwatch.Stop();
            //Console.WriteLine($"Время выполнения цикла for: {stopwatch.Elapsed}");
            //stopwatch.Reset();

            //stopwatch.Start();
            //Parallel.For(0, 5, (count) =>
            //{
            //    int[] array = new int[1000000];
            //    Parallel.ForEach(array, (el) =>
            //    {
            //        el++;
            //    });
            //});
            //stopwatch.Stop();
            //Console.WriteLine($"Время выполнения Parallel.For/Parallel.ForEach: {stopwatch.Elapsed}");
            //stopwatch.Reset();

            //6 Parallel.Invoke

            //try
            //{
            //    using (StreamWriter sw = new StreamWriter("Invoke.txt"))
            //    {
            //        string str1 = "Введено представление об общественноэкономической формации и естественно - историческом, закономерном процессе возникновения развития и смены формаций под влиянием развития производительных сил,\n степени соответствия им производственных отношений, противоречий между ними, как сторонами единого способа производства.\n";

            //        string str2 = "Общественное бытие (материальная жизнь общества, материальное общественное производство) обусловливает общественное сознание.\nВ соответствии с ролью больших масс людей в материальном производстве обоснована их решающая роль в истории(К.Маркс, Ф.Энгельс). ";
            //        string[] words1 = str1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //        string[] words2 = str2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //        Parallel.Invoke(() =>
            //        {
            //            foreach (string s in words1)
            //            {
            //                sw.Write(s + " ");
            //            }
            //        },
            //        () =>
            //        {
            //            foreach (string s in words2)
            //            {
            //                sw.Write(s + " ");
            //            }
            //        });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

           

            //7 async & await

            //Task8();
            //Thread.Sleep(1000);
        }

        private static async void Task8()
        {
            

            Console.WriteLine("async started");
            await Task.Run(func);
            Console.WriteLine("async finished");
        }

        private static void func()
        {
            int[] array = new int[100];
            for (int j = 0; j < array.Length; j++)
            {
                array[j] = j;
                Console.WriteLine(array[j]);
            }
            
        }
    }
}
