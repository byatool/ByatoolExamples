using Byatool.Functional.ToSql;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Functional.Test.SqlTest.SelectTest
{
    [TestFixture]
    public class WhenCreatingAnInnerJoin : WhenCreatingAStatement
    {
        #region Fields

        private const string FirstTable = "FirstTable";
        private const string SecondTable = "SecondTable";

        #endregion

        #region Test Methods

        [Test]
        public void ItCanAcceptASimpleJoin()
        {
            const string expectedText = "INNER JOIN {0} ON ({1} = {2})\r\n";

            new InnerJoin()
                [
                    FirstTable.On(FirstColumn.IsEqualTo(SecondColumn))
                ]
                .Should()
                .Be(string.Format(expectedText, FirstTable, FirstColumn, SecondColumn));
        }

        [Test]
        public void ItCanAcceptMultipleJoins()
        {
            const string expectedText = "INNER JOIN {0} ON ({1} = {2})\r\nINNER JOIN {3} ON ({2} = {1})\r\n";

            new InnerJoin()
                [
                    FirstTable.On(FirstColumn.IsEqualTo(SecondColumn)),
                    SecondTable.On(SecondColumn.IsEqualTo(FirstColumn))

                ]
                .Should()
                .Be(string.Format(expectedText, FirstTable, FirstColumn, SecondColumn, SecondTable));
        }

        [Test]
        public void ItCanAcceptAMediumComplexityJoin()
        {
            const string expectedText = "INNER JOIN {0} ON ({1} = {2}) AND ({2} = {1})\r\n";

            new InnerJoin()
                [
                    FirstTable.On(FirstColumn.IsEqualTo(SecondColumn)).AndOn(SecondColumn.IsEqualTo(FirstColumn))
                ]
                .Should()
                .Be(string.Format(expectedText, FirstTable, FirstColumn, SecondColumn));
        }

        [Test]
        public void ItCanAcceptAJoinToASelectStatement()
        {
            const string expectedText = "INNER JOIN {0} ON ({1} = ({2}))\r\n";

            var statement = new Select()
                [
                    SecondColumn.Top(1)
                ].From(SecondTable);

            new InnerJoin()
                [
                    FirstTable.On(FirstColumn.Matches(statement))
                ]
                .Should()
                .Be(string.Format(expectedText, FirstTable, FirstColumn, statement));
        }

        #endregion
    }
}