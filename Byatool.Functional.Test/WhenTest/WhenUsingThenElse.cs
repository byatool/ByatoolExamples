using System;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Functional.Test.WhenTest
{
    [TestFixture]
    public class WhenUsingThenElse : BaseWhenTest
    {
        #region Fields

        private const int FalseResult = 0;
        private readonly Func<int> _returnFalseResult = () => FalseResult;

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        #region Test Methods

        [Test]
        public void AndThereIsNoTrueMethodAnExceptionIsThrown()
        {
            AssertionExtensions.ShouldThrow<ArgumentException>(() =>
                When<int>
                    .True(ThisIsTrue)
                    .Then(null)
                    .Else(_returnFalseResult));
        }

        [Test]
        public void AndThereIsNoElseMethodAnExceptionIsThrown()
        {
            AssertionExtensions.ShouldThrow<ArgumentException>(() =>
                When<int>
                    .True(ThisIsTrue)
                    .Then(ReturnTrueResult)
                    .Else(null));
        }
     
        [Test]
        public void AndTheCheckIsTrueSoTheCorrectValueIsReturned()
        {
            When<int>
                .True(ThisIsTrue)
                .Then(ReturnTrueResult)
                .Else(_returnFalseResult)
                .Should()
                .Be(TrueResult);
        }

        [Test]
        public void AndTheCheckIsFalseSoTheCorrectValueIsReturned()
        {
            When<int>
                .True(ThisIsFalse)
                .Then(ReturnTrueResult)
                .Else(_returnFalseResult)
                .Should()
                .Be(FalseResult);
        }

        #endregion
    }
}