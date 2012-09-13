using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Functional.Test.WhenTest
{
    [TestFixture]
    public class WhenUsingThenOtherwise : BaseWhenTest
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
        public void AndTheCheckIsTrueSoTheCorrectValueIsReturned()
        {
            When<int>
                .True(ThisIsTrue)
                .Then(ReturnTrueResult)
                .OtherwiseThrow(new AssertionException(""))
                .Should()
                .Be(TrueResult);
        }

        [Test]
        public void AndTheCheckIsFalseSoTheCorrectExceptionIsThrown()
        {

            AssertionExtensions.ShouldThrow<AssertionException>(() =>
                When<int>
                    .True(ThisIsFalse)
                    .Then(ReturnTrueResult)
                    .OtherwiseThrow(new AssertionException("")));
        }

        #endregion
    }
}