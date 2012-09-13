using System;

namespace Byatool.Functional.Test.WhenTest
{
    public class BaseWhenTest
    {
        protected const int TrueResult = 2;
        protected const bool ThisIsTrue = 1 == 1;
        protected const bool ThisIsFalse = 1 == 2;
        protected readonly Func<int> ReturnTrueResult = () => TrueResult;
         
    }
}