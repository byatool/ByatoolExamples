using System;
using Byatool.Functional.Test.MatchTest.TestClasses;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Functional.Test.MatchTest
{
    [TestFixture]
    public class WhenMatchingAnObject
    {
        #region Fields

        public const string FirstString = "FirstString";
        public const string SecondString = "SecondString";
        public const string ThirdString = "ThirdString";

        public const string FirstResult = "FirstResult";
        public const string SecondResult = "SecondResult";
        public const string ThirdResult = "ThirdResult";

        #endregion

        #region Test Methods

        [Test]
        public void AndThereIsAMatchTheCorrectResultIsReturned()
        {
            var testA = new Yay();
            var testB = new Yay();
            var testC = new Yay();

            var testD = testA;

            new Match<Yay, string>(testA)
                .When(testB, () => FirstResult)
                .When(testC, () => SecondResult)
                .When(testD, () => ThirdResult)
                .Default(null)
                .Should()
                .Be(ThirdResult);
        }

        [Test]
        public void AndAChildClassIsMatchedTheCorrectResultIsReturned()
        {
            var testABaby = new BabyYay();
            var testBBaby = new BabyYay();
            var testCBaby = new BabyYay();

            var testA = new Yay(testABaby);
            var testB = new Yay(testBBaby);
            var testC = new Yay(testCBaby);

            var testD = testA;

            new Match<BabyYay, string>(testA.Baby)
                .When(testB.Baby, () => FirstResult)
                .When(testC.Baby, () => SecondResult)
                .When(testD.Baby, () => ThirdResult)
                .Default(null)
                .Should()
                .Be(ThirdResult);
        }
       
        #endregion
    }
}