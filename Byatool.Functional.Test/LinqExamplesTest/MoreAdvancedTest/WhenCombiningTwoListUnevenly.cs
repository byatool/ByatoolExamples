using System.Collections.Generic;
using System.Linq;
using Byatool.Functional.LinqExamples;
using FluentAssertions;
using NUnit.Framework;

namespace Byatool.Functional.Test.LinqExamplesTest.MoreAdvancedTest
{
    public class WhenCombiningTwoListUnevenly
    {
        [Test]
        public void AndTheSecondListIsRepeatedForEveryItemInTheFirstList()
        {
            var days = new[] { "mon", "tue", "wed" };
            var months = new[] { "jan", "feb", "mar" };

            var testAgainst = new[] { "mon jan", "mon feb", "mon mar", "tue jan", "tue feb", "tue mar", "wed jan", "wed feb", "wed mar" };

            MoreAdvanced
                .CombineTwoListsUnevenly(days, months)
                .ToList()
                .Should()
                .BeEquivalentTo(testAgainst);
        } 
    }
}