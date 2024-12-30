using System;
using System.Collections;
using System.Collections.Generic;

namespace TestSomething.Other
{
    public class LearnMoreClass
    {
        public static void ExectuteLearnMoreClass()
        {
            var clickArgs = AppDomain.CurrentDomain;

            var b = 3;
        }


    }

    public class PersonComparator : IEqualityComparer<LearnMoreClass>
    {
        public bool Equals(LearnMoreClass x, LearnMoreClass y)
        {
            throw new System.NotImplementedException();
        }

        public int GetHashCode(LearnMoreClass obj)
        {
            throw new System.NotImplementedException();
        }
    }


}
