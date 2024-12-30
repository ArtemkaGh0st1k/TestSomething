using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestSomething.Other
{

    /// <summary>
    /// Класс, предназначенный для примера многопоточности
    /// </summary>
    public class MutliThread
    {
        /*
        * Замечание 1!
        * Когда создаётся поток путём new Thread, то он автоматически становистся осноным потоком.
        * Свойство IsBackground отвечает является поток фоновым. 
        * Если завершатся все основные потоки, то автоматически завершаться и все фоновые.
        * Если к примеру выставить основной поток IsBackground = true, то после его выполнения все фоновые потоки прекратят своё выполнение
        * Если фоновый поток завершит свои операции, то но будет выкинут из пула (сверху в VS Code исчезнет название этого потока)
        */

        /*
         * Замечание 2!
         * По поводу приоритета потоков ThreadPriprity.
         * В начале стрта потоки запускаются по мере их вызова иерархически.
         * Дальше поток, помеченный с более высоким приоритетом, должен первее остальных обращаться к ресурсу, но так происходит не всегда.
         * Поэтому можно предположить, что если поток занят, то вызывается первый из пула свободный поток(но это не точно!)
        */

        /*
         * Замечание 3!
         * Снизу приведён пример создания 5 основных потоков + 1 основной в методе Main(), который запускает все остальные потоки.
         * После их создания и запуска, доступ к ресурсам имеет первый запущенный поток, остальные потоки ждут его завершения, т.к дейтсвует lock.
         * В данном потоке идёт инкрементации переменной _paramStatic до 5.
         * После того, как поток закончил своё выполнение, следубщий поток занимется его место и повторяет все те же операции предыдущего.
         * Смысл в чём? -> В переменной _paramStatic
         * Если переменная не помечена модификатором static и атрибутом ThreadStatic, то все потоки обращяются к одной и той же области памяти(к одному экземпляру перемнной)
         * Данный приём позволяет указать кажжому потоку, что такая перменная будет уникальная для каждого из них.
         * 
        
         /*
          * Замечание 4!
          * Есть основной поток, который запускает остальные потоки.
          * Если основной поток запустил другой поток, то другой поток с помощью метода Join() может сказать вызывающему потоку
          * подождать, пока данный поток не отработает.
          *
        
        */
        // Данный атрибут говорит о том, что для каждого потока 
        // данное поле уникальное (! поля должно быть с модификатором static)
        [ThreadStatic] public static int _paramStatic;
        private int _param;
        private object _locker;
         
        public MutliThread()
        {
            _paramStatic = 0;
            _locker = new object();
            _massiveColors = new List<ConsoleColor>()
            {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.White,
                ConsoleColor.Blue,
                ConsoleColor.Gray,
                ConsoleColor.DarkMagenta
            };
        }

        public MutliThread(int countInitialiezdThreads) : this()
        {
            _threads = new List<Thread>(countInitialiezdThreads);
            for (int i = 0; i < countInitialiezdThreads; i++)
            {
                _threads.Add(new Thread(Print));
                _threads[i].Name = $"Поток {i}";
                _threads[i].IsBackground = true;
            }
        }

        public MutliThread(int countInitialiezdThreads, List<ThreadPriority> threadPriorities) : this()
        {
            _threads = new List<Thread>(countInitialiezdThreads);
            for (int i = 0; i < countInitialiezdThreads; i++)
            {
                _threads.Add(new Thread(Print));
                _threads[i].Name = $"Поток {i}";
                _threads[i].Priority = threadPriorities[i];
                _threads[i].IsBackground = true;
            }
        }

        private List<ConsoleColor> _massiveColors;
        public List<ConsoleColor> MassiveColors { get => _massiveColors; }

        private List<Thread> _threads;
        public List<Thread> Threads { get => _threads; }

        public void StartThreads()
        {
            foreach (var thread in _threads)
            {
                thread.Start();
                thread.Join(); // блокирует вызывающий поток, пока не отработает текущий
            }
        }

        private void Print()
        {
            lock (_locker)
            {
                var massiveString = Thread.CurrentThread.Name.Split(' ');
                Console.ForegroundColor = _massiveColors[int.Parse(massiveString[1])];
               
                for (int i = 0; i < 6; i++)
                {

                    Console.WriteLine($"{Thread.CurrentThread.Name} -> param = {_paramStatic}" 
                        + " | " + $"{Thread.CurrentThread.Priority}");
                    _paramStatic++;
                    Thread.Sleep(500); // Когда поток приостанавливается, то система переключается на свободный поток либо основной(те точно!)
                    Console.WriteLine("------------------------------------------");
                }
            }
        }

        public long GetCurrentUsedMemory(bool forceFullCollection) => GC.GetTotalMemory(forceFullCollection);

        public void ZeroToFive()
        {
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine($"Поток 1 -> {i}\n");
                Thread.Sleep(500);
            }
        }

        public void FiveToTen()
        {
            for (int i = 5; i <= 10; i++)
            {
                Console.WriteLine($"Поток 2 -> {i}\n");
                Thread.Sleep(500);
            }

           // Thread.Yield();
        }

        public void TenToFifteen()
        {
            for (int i = 10; i <= 15; i++)
            {
                Console.WriteLine($"Поток 3 -> {i}\n");
                Thread.Sleep(500);
            }
        }
    }

    public class Execution
    {
        public static void ExecuteMutliThread()
        {
            Thread.CurrentThread.Name = "Основной поток";
            /*
            var currentMemory0 = GC.GetTotalMemory(false);
            var multiThread1 = new MutliThread();
            var thread1 = new Thread(multiThread1.ZeroToFive);
            var currentMemory1 = GC.GetTotalMemory(false);
            var thread2 = new Thread(multiThread1.FiveToTen);
            var currentMemory2 = GC.GetTotalMemory(false);
            //var thread3 = new Thread(multiThread1.TenToFifteen);
            var currentMemory3 = GC.GetTotalMemory(false);

            thread1.Priority = ThreadPriority.Normal;
            thread2.Priority = ThreadPriority.Highest;
            //thread3.Priority = ThreadPriority.Lowest;

            thread1.Start();
            thread2.Start();
            //thread2.Start();
            //thread3.Start();
            */
            
            //
            var countThreads = 5;
            var prioritiesThreads = new List<ThreadPriority>(countThreads)
            {
                ThreadPriority.BelowNormal,
                ThreadPriority.AboveNormal,
                ThreadPriority.Lowest,
                ThreadPriority.Highest,
                ThreadPriority.Normal
            };
            var multiThread = new MutliThread(countThreads, prioritiesThreads);
            multiThread.StartThreads();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(Thread.CurrentThread.Name +  " завершён!\n");
            //
            
            Console.ReadLine();
            
        }
    }
}
