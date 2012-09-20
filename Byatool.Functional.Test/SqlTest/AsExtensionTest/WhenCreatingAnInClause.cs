using Byatool.Functional.ToSql;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Functional.Test.SqlTest.AsExtensionTest
{
    [TestFixture]
    public class WhenCreatingAnInClause
    {
        #region Fields

        private const string FirstClause = "FirstClause";
        private const string SecondClause = "SecondClause";

        #endregion

        #region Test Methods

        [Test]
        public void AndTheAddedItemIsSurroundedByParenthesis()
        {
            FirstClause
                .In(SecondClause)
                .Should()
                .Be(string.Format("{0} IN ({1})", FirstClause, SecondClause));
        }

        [Test]
        public void AndAListIsSentInSoTheClauseIsCreateCorrectly()
        {
            FirstClause.In(new[] {1, 2, 3})
                .Should()
                .Be(string.Format("{0} IN (1,2,3)", FirstClause));
        }

        #endregion
    }
}