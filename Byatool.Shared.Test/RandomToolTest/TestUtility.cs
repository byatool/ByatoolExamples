using System;
using System.Collections.Generic;
using System.Linq;

namespace Byatool.Shared.Test.RandomToolTest
{
    public class TestUtility
    {
        public static IEnumerable<T> CreateAUnionFromResults<T>(IEnumerable<T> createdList)
        {
            IEnumerable<T> returnList = new List<T>();

            return createdList
                .Aggregate(returnList, (collection, item) => collection.Union<T>(new [] { item }));
        }

        public static IEnumerable<T> CreateRandomList<T>(Func<T> randomMethod, int amount = 100)
        {
            return Enumerable.Range(0, amount).Select(item => randomMethod()).ToList();
        }
    }
}