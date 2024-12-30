using System;
using System.Threading.Tasks;

namespace TestSomethingWPF.Dispatcher
{
    public static class Tasks
    {
        public static Task TaskAsync1 { get; private set; }
        public static Task TaskAsync2 { get; private set; }
        public static Task TaskAsync3 { get; private set; }
        static Tasks()
        {
            TaskAsync1 = Task.Run(async () =>
            {
                Console.WriteLine($"Поток -> {Environment.CurrentManagedThreadId} начал задержку 1с");
                await Task.Delay(1000);
                Console.WriteLine($"Поток -> {Environment.CurrentManagedThreadId} закончил задержку 1с");
            });

            TaskAsync2 = Task.Run(async () =>
            {
                Console.WriteLine($"Поток -> {Environment.CurrentManagedThreadId} начал задержку 2с");
                await Task.Delay(2000);
                Console.WriteLine($"Поток -> {Environment.CurrentManagedThreadId} закончил задержку 2с");
            });

            TaskAsync3 = Task.Run(async () =>
            {
                Console.WriteLine($"Поток -> {Environment.CurrentManagedThreadId} начал задержку 3с");
                await Task.Delay(3000);
                Console.WriteLine($"Поток -> {Environment.CurrentManagedThreadId} закончил задержку 3с");
            });
        }
    }
}
