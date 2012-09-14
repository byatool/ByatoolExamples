using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Shared.Test.RandomToolTest
{
    [TestFixture]
    public class WhenCreatingAString
    {
        #region Fields

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        #region Test Methods

        [Test]
        public void ItIsSufficientlyRandom()
        {
            TestUtility
                .CreateAUnionFromResults(TestUtility.CreateRandomList(RandomTool.CreateAString))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void ItIsTheCorrectLength()
        {
            RandomTool.CreateAString(10).Length.Should().Be(10);
        }

        [Test]
        public void ItContainsRandomCharacters()
        {
            TestUtility
                .CreateAUnionFromResults(RandomTool.CreateAString(100))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        #endregion
    }
}