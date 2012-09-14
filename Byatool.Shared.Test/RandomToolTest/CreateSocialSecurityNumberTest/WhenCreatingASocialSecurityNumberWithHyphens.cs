using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Shared.Test.RandomToolTest.CreateSocialSecurityNumberTest
{
    [TestFixture]
    public class WhenCreatingASocialSecurityNumberWithHyphens
    {
        #region Fields

        private string _number;

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
            _number = RandomTool.CreateASocialSecurityNumber(true);
        }

        #endregion

        #region Test Methods

        [Test]
        public void ThereAreTwoHyphens()
        {
            _number 
               .Split('-')
               .First()
               .Length
               .Should()
               .Be(3);
        }

        [Test]
        public void TheAreThreeSections()
        {
            _number
                .Split('-')
                .Length
                .Should()
                .Be(3);
        }
    
        [Test]
        public void TheFirstSectionIsTheCorrectLength()
        {
            _number
                .Split('-')
                .First()
                .Length
                .Should()
                .Be(3);
        }

        [Test]
        public void TheSecondSectionIsTheCorrectLength()
        {
            _number
                .Split('-')[1]
                .Length
                .Should()
                .Be(2);
        }

        [Test]
        public void TheThirdSectionIsTheCorrectLength()
        {
            _number
                .Split('-')[2]
                .Length
                .Should()
                .Be(4);
        }

        #endregion
    }
}