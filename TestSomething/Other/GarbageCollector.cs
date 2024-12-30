using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSomething.Other
{
    public class GarbageCollector
    {
        public static void ExecuteGarbageCollector()
        {
            unsafe
            {
                int* a;
                
            }
            
            
            try
            {
                int k = 0;
                int j = 0;
                for (int i = 0; i < 10; i++, k++, j++)
                {
                    Console.WriteLine($"Поколение {i} = {GC.CollectionCount(i)}");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                var paramName = e.ParamName;
                var message = e.Message;
                var actualValue = e.ActualValue;
                var innerExc = e.InnerException;
                var data = e.Data;
                var targetSite = e.TargetSite;

                Console.WriteLine(e.ParamName);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.ActualValue);
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Data);
                Console.WriteLine(e.TargetSite);
               
            }

        }
        
    }
}
