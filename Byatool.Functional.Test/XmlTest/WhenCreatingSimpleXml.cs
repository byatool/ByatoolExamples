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
        private const string FirstText = "hihi";
        private const string SecondElement = "SecondElement";
        private const string SecondValue = "SecondValue";
        private const string SimpleElementText = "<{0}>{1}</{0}>";
        private const string ThirdValue = "ThirdValue";
        private const string ThirdElement = "ThirdElement";

        #endregion

        #region Test Methods

        [Test]
        public void ASingleElementIsPossible()
        {
            new Element(FirstElement, FirstText)
                .Create()
                .Should()
                .Be(string.Format(SimpleElementText, FirstElement,  FirstText));
        }

        [Test]
        public void NoValueIsRepresentedCorrectly()
        {
            new Element(FirstElement)
              .Create()
                .Should()
                .Be(string.Format(EmptyElement, FirstElement));
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