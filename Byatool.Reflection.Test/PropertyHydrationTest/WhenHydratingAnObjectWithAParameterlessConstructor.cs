using Byatool.Reflection.Test.PropertyHydrationTest.TestClasses;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Reflection.Test.PropertyHydrationTest
{
    [TestFixture]
    public class WhenHydratingAnObjectWithAParameterlessConstructor
    {
        #region Test Classes

        #endregion

        #region Test Methods

        [Test]
        public void AndTheClassHasNoDefaultConstructorSoConstructorInfoIsUsed()
        {
            new PropertyHydration()
                .CreateAFilled<NonEmptyConstructorParent>()
                .NonEmptyConstructor.Should().NotBeNull();
        }

        [Test]
        public void AndTheChildClassWithoutADefaultConstructorIsPopulated()
        {
            new PropertyHydration()
                .CreateAFilled<NonEmptyConstructorParent>()
                .NonEmptyConstructor
                .ChildClass.Should().NotBeNull();
        }

        [Test]
        public void AndTheImmediateClassDoesNotHaveADefaultConstructorItStillCreatesIt()
        {
            new PropertyHydration()
                .CreateAFilled<NonEmptyConstructor>()
                .Should()
                .NotBeNull();
        }

        #endregion 
    }
}
