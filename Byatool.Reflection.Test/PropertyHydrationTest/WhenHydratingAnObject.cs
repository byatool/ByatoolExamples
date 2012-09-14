using System;
using Byatool.Reflection.Test.PropertyHydrationTest.TestClasses;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Reflection.Test.PropertyHydrationTest
{
    [TestFixture]
    public class WhenHydratingAnObject
    {
        #region Fields

        private PropertyFilledClass _createdClass;

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
            var hydrator = new PropertyHydration();

            _createdClass = hydrator.CreateAFilled<PropertyFilledClass>();
        }

        #endregion

        #region Test Methods

        [Test]
        public void TheBoolPropertyIsSetCorrectly()
        {
            //??? Not sure how to test against a random bool
        }

        [Test]
        public void TheChildClassIsHydrated()
        {
            _createdClass.ChildClass.IntProperty.Should().NotBe(0);
        }

        [Test]
        public void TheChildClassIsNotNull()
        {
            _createdClass.ChildClass.Should().NotBeNull();
        }

        [Test]
        public void AGenericTypeIsIgnored()
        {
            //Basically the main call doesn't blow up... Kind of a meta test.
        }

        [Test]
        public void APropertyWithoutASetterIsIgnored()
        {
            //This is a weird test in that being run means there was no exception.
        }

        [Test]
        public void TheSecondChildClassIsNotNull()
        {
            _createdClass.ChildClass.SecondChildClass.IntProperty.Should().NotBe(0);
        }

        [Test]
        public void TheDateTimePropertyIsSetCorrectly()
        {
            _createdClass.DateTimeProperty.Should().BeAtLeast(new TimeSpan(DateTime.Now.AddMinutes(-30).Ticks));
        }

        [Test]
        public void TheDateTimeNullablePropertyIsSetCorrectly()
        {
            _createdClass.DateTimeNullableProperty.Should().BeAtLeast(new TimeSpan(DateTime.Now.AddMinutes(-30).Ticks));
        }

        [Test]
        public void TheDecimalPropertyIsSetCorrectly()
        {
            _createdClass.DecimalProperty.Should().NotBe(0);
        }

        [Test]
        public void TheIntPropertyIsSetCorrectly()
        {
            _createdClass.IntProperty.Should().NotBe(0);
        }

        [Test]
        public void TheInt64PropertyIsSetCorrectly()
        {
            _createdClass.LongProperty.Should().NotBe(0);
        }

        [Test]
        public void TheShortPropertyIsSet()
        {
            _createdClass.ShortProperty.Should().NotBe(0);
        }

        [Test]
        public void TheStringPropertyIsSetCorrectly()
        {
            _createdClass.StringProperty.Should().NotBeNullOrEmpty();
        }

        #endregion
    }
}