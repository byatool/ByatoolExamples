using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Shared.Test.RandomToolTest
{
    [TestFixture]
    public class WhenCreatingANegativeNumber
    {
        #region Test Methods

        [Test]
        public void AllResultsAreNegative()
        {
            TestUtility.CreateAUnionFromResults(TestUtility.CreateRandomList(RandomTool.CreateANegativeInt32, 10))
                .All(item => item < 0)
                .Should()
                .BeTrue();
        }

        [Test]
        public void NegativeInt32ItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults(TestUtility.CreateRandomList(RandomTool.CreateANegativeInt32))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        #endregion
    }
}