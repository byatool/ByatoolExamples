using System;

namespace Byatool.Functional
{
    public class GuardClause
    {
        public static void IfNullThrowArgumentException<T>(T itemToCheck, string errorMessage) where T : class
        {
            if (itemToCheck == null)
            {
                throw new ArgumentException(errorMessage);
            }
        } 
    }
}