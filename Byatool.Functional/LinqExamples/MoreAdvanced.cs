using System.Collections.Generic;
using System.Linq;

namespace Byatool.Functional.LinqExamples
{
    public class MoreAdvanced
    {
        /*
         * Use was to combine these two:
            { "mon", "tue", "wed" };
            { "jan", "feb", "mar" };
         * 
         * Into this one:
            { "mon jan", "mon feb", "mon mar", "tue jan", "tue feb", "tue mar", "wed jan", "wed feb", "wed mar" }
         */
        public static IList<string> CombineTwoListsUnevenly(IList<string> firstList, IList<string> secondList )
        {
            return
                firstList
                    .Select(first => secondList.Select(second => first + " " + second))
                    .Aggregate(new List<string>(), (endList, currentList) => endList.Union(currentList).ToList());
        }
    }
}