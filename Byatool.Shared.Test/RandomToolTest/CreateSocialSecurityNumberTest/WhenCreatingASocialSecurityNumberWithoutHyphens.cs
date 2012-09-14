using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Shared.Test.RandomToolTest.CreateSocialSecurityNumberTest
{
    [TestFixture]
    public class WhenCreatingASocialSecurityNumberWithoutHyphens
    {
        #region Fields

        private string _number;

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
            _number = RandomTool.CreateASocialSecurityNumber(false);
        }

        #endregion

        #region Test Methods

        [Test]
        public void ItIsSufficientlyRandom()
        {
            TestUtility
                .CreateAUnionFromResults(TestUtility.CreateRandomList(() => RandomTool.CreateASocialSecurityNumber(false)))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void ItIsOfTheCorrectLength()
        {
            _number.Length.Should().Be(9);
        }

        #endregion
    }
}