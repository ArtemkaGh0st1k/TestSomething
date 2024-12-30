using System;

namespace TestSomething.Other
{
    delegate void Method(int param1, int param2);

    public class Program
    {
        public static Method Method;

        public static Program()
        {
            Method = new Method();
        }

        public static void Sum(int x, int y) => Console.WriteLine(x + y);
        public static void Mult(int x, int y) => Console.WriteLine(x * y);
        public static void Diff(int x, int y) => Console.WriteLine(x - y);
        public static void Div(int x, int y) => Console.WriteLine(x / y);
        /// <summary>
        /// Добавление методов в делегат и их поочерёдный вызов
        /// </summary>
        public static void GetResultDelegate()
        {
            Method method = new Method(Sum);
            Console.WriteLine($"{Method.Invoke(3, 4)}, {method.Method.Name}");

            method += Mult;
            Console.WriteLine($"{method.Invoke(3, 4)}, {method.Method.Name}");

            method += Diff;
            Console.WriteLine($"{method.Invoke(3, 4)}, {method.Method.Name}");

            method += Div;
            Console.WriteLine($"{method.Invoke(3, 4)}, {method.Method.Name}");

            Console.ReadLine();
        }

        public static void OnSubcribe()
        {
            Method += Sum;
            Method += Mult;
            Method += Diff;
            Method += Div;
        }

        public static void InvoveMethods() => Method.Invoke(6, 3);
        
        public static void ExecuteDelegateSample(string[] args)
        {
            //Method method = new Method(Sum);
            //method += Sum(3,4);

            //Program.GetResultDelegate();

            Program.OnSubcribe();
            Program.InvoveMethods();
            Console.ReadLine();
        }
    }
}
