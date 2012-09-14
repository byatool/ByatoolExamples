using System;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Shared.Test.RandomToolTest
{
    [TestFixture]
    public class WhenCreatingARandom
    {
        #region Fields

        private enum FakeEnum
        {
            A,B,C,D,E,F,G,H,I,J,K
        }

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        #region Support Methods

        #endregion        
        
        #region Test Methods

        [Test]
        public void BooleanItIsSufficientlyRandom()
        {
            var createdList = TestUtility.CreateRandomList(RandomTool.CreateABoolean).ToList();

            createdList.Count(item => item).Should().BeInRange(30, 100);
            createdList.Count(item => !item).Should().BeInRange(30, 100);
        }

        [Test]
        public void CharItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults<char>(TestUtility.CreateRandomList(RandomTool.CreateAChar).ToList())
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void CashAmountItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults<decimal>(TestUtility.CreateRandomList(RandomTool.CreateACashAmount))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void DateItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults<DateTime>(TestUtility.CreateRandomList(RandomTool.CreateADate))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void DecimalItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults<decimal>(TestUtility.CreateRandomList(RandomTool.CreateADecimal))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void EmailItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults<string>(TestUtility.CreateRandomList(RandomTool.CreateAnEmail))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void EnumerationItIsSufficientlyRandom()
        {

            TestUtility.CreateAUnionFromResults<FakeEnum>(TestUtility.CreateRandomList(RandomTool.CreateAnEnumeration<FakeEnum>))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void Int16ItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults<short>(TestUtility.CreateRandomList(RandomTool.CreateAnInt16))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void Int32ItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults<int>(TestUtility.CreateRandomList(RandomTool.CreateAnInt32))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void Int64ItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults<object>(TestUtility.CreateRandomList(RandomTool.CreateAnInt64))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void NameItIsSufficientlyRandom()
        {
            TestUtility.CreateAUnionFromResults(TestUtility.CreateRandomList(RandomTool.CreateAName))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        #endregion
    }
}