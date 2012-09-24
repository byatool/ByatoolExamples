using System.Collections.Generic;
using System.Linq;
using System.Text;
using Byatool.Functional.ToXml;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Functional.Test.XmlTest
{
    [TestFixture]
    public class WhenCreatingSimpleXml
    {
        #region Fields

        private const string EmptyElement = "<{0} />";
        private const string FirstElement = "FirstElement";
        private const string FirstValue = "FirstValue";
        private const string SecondElement = "SecondElement";
        private const string SecondValue = "SecondValue";
        private const string SimpleElementText = "<{0}>{1}</{0}>";
        private const string ThirdValue = "ThirdValue";
        private const string ThirdElement = "ThirdElement";

        #endregion

        #region Test Methods

        [Test]
        public void NoValueIsRepresentedCorrectly()
        {
            new Element(FirstElement)
                .Create()
                .Should()
                .Be(string.Format(EmptyElement, FirstElement));
        }

        [Test]
        public void ASingleElementIsPossible()
        {
            new Element(FirstElement, FirstValue)
                .Create()
                .Should()
                .Be(string.Format(SimpleElementText, FirstElement,  FirstValue));
        }

        [Test]
        public void AndTheValueIsNotAStringItStillWorks()
        {
            const int value = 123;
            new Element(FirstElement, value)
               .Create()
               .Should()
               .Be(string.Format(SimpleElementText, FirstElement, value.ToString()));
        }

        [Test]
        public void AnAttributeListIsRepresentedCorrectly()
        {
            const string expectedText = "<FirstElement SecondElement=\"SecondValue\" ThirdElement=\"ThirdValue\">FirstValue</FirstElement>";

            new Element(FirstElement, FirstValue, new[] {new XmlAttribute(SecondElement, SecondValue), new XmlAttribute(ThirdElement, ThirdValue), })
                .Create()
                .Should()
                .Be(expectedText);
        }

        [Test]
        public void AnElementCanHoldOtherElements()
        {
            var innerText =
                new StringBuilder()
                    .AppendLine(string.Format(SimpleElementText, SecondElement, SecondValue))
                    .AppendLine(string.Format(SimpleElementText, ThirdElement, ThirdValue))
                    .ToString();

            var outer = string.Format(SimpleElementText, FirstElement, innerText);

            new Element(FirstElement)
                [
                    new Element(SecondElement, SecondValue),
                    new Element(ThirdElement, ThirdValue)
                ]
                .Create()
                .Should()
                .Be(outer);
        }

        [Test]
        public void AnElementTreeIsPossible()
        {
            var third = string.Format(SimpleElementText, ThirdElement, ThirdValue);
            var second = string.Format(SimpleElementText, SecondElement, third + "\r\n");
            var outer = string.Format(SimpleElementText, FirstElement, second + "\r\n");

            new Element(FirstElement)
                [
                    new Element(SecondElement)
                    [
                        new Element(ThirdElement, ThirdValue)
                    ]
                ]
                .Create()
                .Should()
                .Be(outer);
        }

        #endregion
    }
}