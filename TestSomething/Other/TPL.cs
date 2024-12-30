using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/*
 * Замечание 1!
 * Для async/await задач используется планировщик задач.
 * Есть пул потоков, каждый свободный поток оперирует одной задачей.
 * Среда сама изменяет пул, чтобы потоков хватило для задач(об их кол-во не нужно заботиться)
 * Поток, который выполнил задачу, возвращаеться обратно в пул и используется дальше в ходе программы для заданных задач.
 */

/*
 * Замечание 2!
 * Пример как работает код:
 * 
 * public static async Task DelayOperationAsync() // асинхронный метод
    {
        BeforeCall();
        Task task = Task.Delay(1000); //асинхронная операция
        AfterCall();
        await task;
        AfterAwait();
    } 
 * 
 * 1)Выполняется весь код, предшествующий вызову асинхронной операции. 
 *   В данном случае это метод BeforeCall.
 *   
 * 2)Выполняется вызов асинхронной операции. 
 *   На данном этапе поток не освобождается и не блокируется.
 *   Данная операция возвращает результат — 
 *   упомянутый объект задачи (как правило Task), который сохраняется в локальную переменную 
 * 
 * 3)Выполняется код после вызова асинхронной операции,
 * но до ожидания (await). В примере — AfterCall
 * 
 * 4)Ожидание завершения на объекте задачи 
 * (который сохранили в локальную переменную) — await task.
 * 
 * Если асинхронная операция к этому моменту завершена, 
 * то выполнение продолжается синхронно, в том же потоке.
 * 
 * Если асинхронная операция не завершена, 
 * то сохраняется код, который надо будет вызвать по завершении асинхронной операции 
 * (т.н. продолжение), а поток возвращается в пул потоков и становится доступен для использования.
 * 
 * 5)Выполнение операций после ожидания — AfterAwait
 * — выполняется или сразу же, в том же потоке, 
 * когда операция на момент ожидания была завершена,
 * или, по завершении операции, берется новый поток,
 * который выполнит продолжение (сохраненное на предыдущем шаге).
 * 
 */

/*
 * Замечание 3!
 * Заметил сходство между методами:
 * Thread.CurrentThread.ManagedThreadId и Thread.CurrentThread.GetHashCode() и Environment.CurrentManagedThreadId.
 * Они трое возвращают уникальный индетификатор рабочего потока, и при вызове они совпадают.
 * 
 */

namespace TestSomething.Other
{
    /// <summary>
    /// Класс для теста асинхронного программирования
    /// </summary>
    public class TPL
    {
        public static void ExecuteTPL()
        {
            StartMethodsAsync();
            Console.ReadLine();
        }


        public static async void StartMethodsAsync()
        {
            await Method1();

            await Method2();

            await Method3();
        }

        public static async Task Method1()
        {
            Console.WriteLine($"Зашёл в метод 1 и запустил задачу 1 поток ID Thread {Thread.CurrentThread.ManagedThreadId} | " +
                $"{Thread.CurrentThread.GetHashCode()} | {Environment.CurrentManagedThreadId}");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var task1 = Task.Factory.StartNew(
                () =>
                {
                    Console.WriteLine($"Зашёл в задачу №1 поток ID Thread = {Thread.CurrentThread.ManagedThreadId} | " +
                        $"{Thread.CurrentThread.GetHashCode()} | {Environment.CurrentManagedThreadId}");
                    for (int i = 0; i < 1_000_000; i++) { }
                    Console.WriteLine($"Выполнил задачу №1 поток ID Thread = {Thread.CurrentThread.ManagedThreadId} | " +
                        $"{Thread.CurrentThread.GetHashCode()} | {Environment.CurrentManagedThreadId}");
                });

            var task2 = Task.Factory.StartNew(
                () =>
                {
                    Console.WriteLine($"Зашёл в задачу №2 поток ID Thread = {Thread.CurrentThread.GetHashCode()} | " +
                        $"{Thread.CurrentThread.ManagedThreadId} | {Environment.CurrentManagedThreadId}");
                    for (int i = 0; i < 1_000_000; i++) { }
                    Console.WriteLine($"Выполнил задачу №2 поток ID Thread = {Thread.CurrentThread.GetHashCode()} | " +
                        $"{Thread.CurrentThread.ManagedThreadId} | {Environment.CurrentManagedThreadId}");
                });
            //LongTimeOperation();
            Console.WriteLine($"Ожидание метода 1, время -> {stopWatch.ElapsedMilliseconds}" +
                $" | {task1.Status} | | {task2.Status} | ID Thread = {Thread.CurrentThread.GetHashCode()} | " +
                $"{Thread.CurrentThread.ManagedThreadId} | {Environment.CurrentManagedThreadId}");
            await task1;
            Console.WriteLine("Сообщение между двумя await'ами");
            await task2;
            var currentThreads = Process.GetCurrentProcess().Threads;
            Console.WriteLine($"Метод 1 закончил свою работу -> {stopWatch.ElapsedMilliseconds}" +
                $" | {task1.Status} | {task2.Status} | ID Thread = {Thread.CurrentThread.GetHashCode()} | " +
                $"{Thread.CurrentThread.ManagedThreadId} | {Environment.CurrentManagedThreadId}\n");
        }
       
        public static async Task Method2()
        {
            Console.WriteLine($"Зашёл в метод 2 и запустил задачу 2 поток ID Thread {Thread.CurrentThread.ManagedThreadId}");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var task1 = Task.Factory.StartNew(
                () =>
                {
                    Console.WriteLine($"Зашёл в задачу №1 поток ID Thread = {Thread.CurrentThread.ManagedThreadId}");
                    for (int i = 0; i < 2_000_000; i++) { }
                    Console.WriteLine($"Выполнил задачу №1 поток ID Thread = {Thread.CurrentThread.ManagedThreadId}");
                });

            var task2 = Task.Factory.StartNew(
               () =>
               {
                   Console.WriteLine($"Зашёл в задачу №2 поток ID Thread = {Thread.CurrentThread.ManagedThreadId}");
                   for (int i = 0; i < 2_000_000; i++) { }
                   Console.WriteLine($"Выполнил задачу №2 поток ID Thread = {Thread.CurrentThread.ManagedThreadId}");
               });
           

            //LongTimeOperation();
            Console.WriteLine($"Ожидание метода 2, время -> {stopWatch.ElapsedMilliseconds}" +
                $" | {task1.Status} | {task2.Status} |ID Thread = {Thread.CurrentThread.ManagedThreadId}");
            await task1;
            await task2;
            var currentThreads = Process.GetCurrentProcess().Threads;
            Console.WriteLine($"Метод 2 закончил свою работу -> {stopWatch.ElapsedMilliseconds}" +
                $" | {task1.Status} | {task2.Status} | ID Thread = {Thread.CurrentThread.ManagedThreadId}\n");
        }

        public static async Task Method3()
        {
            Console.WriteLine($"Зашёл в метод 3 и запустил задачу 3 поток ID Thread {Thread.CurrentThread.ManagedThreadId}");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var task1 = Task.Factory.StartNew(
                () =>
                {
                    Console.WriteLine($"Зашёл в задачу №1 поток ID Thread = {Thread.CurrentThread.ManagedThreadId}");
                    for (int i = 0; i < 3_000_00; i++) { }
                    Console.WriteLine($"Выполнил задачу №1 поток ID Thread = {Thread.CurrentThread.ManagedThreadId}");
                });

            var task2 = Task.Factory.StartNew(
                () =>
                {
                    Console.WriteLine($"Зашёл в задачу №2 поток ID Thread = {Thread.CurrentThread.ManagedThreadId}");
                    for (int i = 0; i < 3_000_000; i++) { }
                    Console.WriteLine($"Выполнил задачу №2 поток ID Thread = {Thread.CurrentThread.ManagedThreadId}");
                });
            

            //LongTimeOperation();
            Console.WriteLine($"Ожидание метода 3, время -> {stopWatch.ElapsedMilliseconds}" +
                $" | {task1.Status} | {task2.Status} | ID Thread = {Thread.CurrentThread.ManagedThreadId}");
            await task1;
            await task2;
            var currentThreads = Process.GetCurrentProcess().Threads;
            Console.WriteLine($"Метод 3 закончил свою работу -> {stopWatch.ElapsedMilliseconds}" +
                $" | {task1.Status} | {task2.Status} | ID Thread = {Thread.CurrentThread.ManagedThreadId}\n");
        }

        /// <summary>
        /// Метод, который показывает, что если он выполняется дольше чем ранее 
        /// поставленные задачи, то основной поток, вызвавший данные задачи продожит 
        /// выполнять свою работу не блокируясь и не освобождая его.
        /// </summary>
        /// <param name="nameMethod">Имя метода, в котором произошёл вызов исходного метода</param>
        private static void LongTimeOperation(string nameMethod = null)
        {
            for (var i = 0; i < 1e8; i++) { }
            Console.WriteLine($"Долгую операцию выполнил поток {Thread.CurrentThread.ManagedThreadId}");
        }

        public static Task<int> TaskFromResultAndException()
        {
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                if (random.Next(1, 6) == 5)
                    return Task.FromResult(5);
            }

            return Task.FromException<int>(new ArgumentOutOfRangeException());
        }

        /// <summary>
        /// Тестовый метод для класса Parallel
        /// </summary>
        public static void ParallelForEach()
        {
            var nums = Enumerable.Range(0, 1_000_000).ToArray();
            long total = 0;

            Parallel.ForEach<int, long>(
                nums,
                () => 0,
                (j, loop, subtotal) =>
                {
                    subtotal += j;
                    return subtotal;
                },
                (finalResult) => Interlocked.Add(ref total, finalResult));

            Console.WriteLine($"The total from Parallel.ForEach is {total}");
            Console.ReadLine();
        }

    }
}
