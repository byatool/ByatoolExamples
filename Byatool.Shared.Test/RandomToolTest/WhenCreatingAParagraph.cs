using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Shared.Test.RandomToolTest
{
    [TestFixture]
    public class WhenCreatingAParagraph
    {
        #region Fields

        private const int BaseLength = 100;

        #endregion

        #region Test Methods

        [Test]
        public void ItIsSufficientlyRandom()
        {
            var paragraph = RandomTool.CreateAParagraph(BaseLength);

            TestUtility.CreateAUnionFromResults(paragraph.Split(' '))
                .Count()
                .Should()
                .BeGreaterOrEqualTo(10);
        }

        [Test]
        public void TheLengthIsCorrect()
        {
            RandomTool.CreateAParagraph(BaseLength)
                .Length
                .Should()
                .BeLessOrEqualTo(BaseLength);
        }

        #endregion
    }
}