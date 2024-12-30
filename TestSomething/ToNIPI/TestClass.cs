using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSomething.ToNIPI
{

    public class A
    {
        public string A_prop
        {
            get => "A Class";
        }
    }

    public class B
    {
        public string B_prop
        {
            get => "B Class";
        }
    }

    public class C
    {
        public string C_prop
        {
            get => "C Class";
        }
    }

    public class TestClass
    {
        public TestClass()
        {

        }

        public static void Main()
        {
            var typeList = new List<Type>()
            {
                typeof(A),
                typeof(B),
                typeof(C)
            };

            var typeActions = new Dictionary<Type, Action<object>>();

            foreach (var type in typeList)
            {
                typeActions[type] = obj =>
                {
                    dynamic instance = Convert.ChangeType(obj, type);
                    
                };
            }
        }
    }
}
