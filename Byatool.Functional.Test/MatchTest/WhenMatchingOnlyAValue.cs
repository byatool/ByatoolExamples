using System;
using Byatool.Functional.Test.MatchTest.TestClasses;
using FluentAssertions;
using NUnit.Framework;

namespace Byatool.Functional.Test.MatchTest
{
    [TestFixture]
    public class WhenMatchingOnlyAValue
    {
        #region Fields
        
        private const string ToTestAgainst = "toTestAgainst";

        private const string TheDefaultResult = "DefaultResult";
        private const string ReturnFirstResult = "FirstResult";
        private const string ReturnSecondResult = "SecondResult";
        private const string ReturnThirdResult = "ThirdResult";

        private const string ItEqualsFirstCheck = "FirstCheck";
        private const string ItEqualsSecondCheck = "SecondCheck";
        private const string ItEqualsThirdCheck = "ThirdCheck"; 

        #endregion

        #region Test Methods

        [Test]
        public void AndThereIsAMatchTheReturnIsCorrect()
        {
            new Match<string, string>(ToTestAgainst)
                .When(ItEqualsFirstCheck, () => ReturnFirstResult)
                .When(ItEqualsSecondCheck, () => ReturnSecondResult)
                .When(ToTestAgainst, () => ReturnThirdResult)
                .Default(TheDefaultResult)
                .Should()
                .Be(ReturnThirdResult);
        }

        [Test]
        public void AndThereIsNoMatchTheDefaultIsReturned()
        {
            new Match<string, string>(ToTestAgainst)
                .When(ItEqualsFirstCheck, () => ReturnFirstResult)
                .When(ItEqualsSecondCheck, () => ReturnSecondResult)
                .When(ItEqualsThirdCheck, () => ReturnThirdResult)
                .Default(TheDefaultResult)
                .Should()
                .Be(TheDefaultResult);
        }

        [Test]
        public void AndThereIsNoDefaultRequirement()
        {
            new Match<string, string>(ToTestAgainst)
              .When(ItEqualsFirstCheck, () => ReturnFirstResult)
              .When(ItEqualsSecondCheck, () => ReturnSecondResult)
              .When(ToTestAgainst, () => ReturnThirdResult)
              .Go()
              .Should()
              .Be(ReturnThirdResult);
        }

        [Test]
        public void AndTheResultIsANewMethodItCanBeReturned()
        {
            Func<Yay, string> returnFirstName = (yay) => yay.FirstName;
            Func<Yay, string> returnSecondName = (yay) => yay.SecondName;
            Func<Yay, string> returnThirdName = (yay) => yay.ThirdName;

            new Match<string, Func<Yay, string>>(ToTestAgainst)
                .When(ItEqualsFirstCheck, () => returnFirstName)
                .When(ItEqualsSecondCheck, () => returnSecondName)
                .When(ToTestAgainst, () => returnThirdName)
                .Go()
                .Should()
                .Be(returnThirdName);
        }


        #endregion
    }
}