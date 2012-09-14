using System;
using System.Collections.Generic;
using System.Linq;
using Byatool.Functional;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Shared.Test
{
    [TestFixture]
    public class WhenCreatingARandom
    {
        #region Fields

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        #region Support Methods

		private static IEnumerable<T> CreateAUnionFromResults<T>(IEnumerable<T> createdList)
        {
            IEnumerable<T> returnList = new List<T>();

            return createdList
                .Aggregate(returnList, (collection, item) => collection.Union<T>(new [] { item }));
        }

        private IEnumerable<T> CreateRandomList<T>(Func<T> randomMethod)
        {
            return Enumerable.Range(0, 100).Select(item => randomMethod()).ToList();
        }


	#endregion        
        
        #region Test Methods

        [Test]
        public void BooleanItIsSufficientlyRandom()
        {
            var createdList = CreateRandomList(RandomTool.CreateABoolean).ToList();

            createdList.Count(item => item).Should().BeInRange(30, 100);
            createdList.Count(item => !item).Should().BeInRange(30, 100);
        }

        [Test]
        public void CharItIsSufficientlyRandom()
        {
            CreateAUnionFromResults(CreateRandomList(RandomTool.CreateAChar).ToList())
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        [Test]
        public void CashAmountItIsSufficientlyRandom()
        {
            CreateAUnionFromResults(CreateRandomList(RandomTool.CreateACashAmount))
                .Count()
                .Should()
                .BeGreaterThan(10);
        }

        #endregion
    }
}