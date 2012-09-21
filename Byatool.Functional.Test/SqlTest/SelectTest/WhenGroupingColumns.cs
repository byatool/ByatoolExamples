using Byatool.Functional.ToSql;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Functional.Test.SqlTest.SelectTest
{
    [TestFixture]
    public class WhenGroupingColumns : WhenCreatingAStatement
    {
        private const string FirstTable = "FirstTable";

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
        public void TheGroupCanHandleASingleItem()
        {
            const string expectedText = "Group By {0}";

            new GroupBy()
                [
                    FirstColumn
                ]
                .Should()
                .Be(string.Format(expectedText, FirstColumn));
        }

        [Test]
        public void TheGroupdCanHandleMoreInterestingGroupings()
        {
            const string expectedText = "Group By {0}, {1}.{2}";
            new GroupBy()
                [
                    FirstColumn,
                    SecondColumn.From(FirstTable)
                ]
                .Should()
                .Be(string.Format(expectedText, FirstColumn, FirstTable, SecondColumn));
        }

        #endregion
    }
}