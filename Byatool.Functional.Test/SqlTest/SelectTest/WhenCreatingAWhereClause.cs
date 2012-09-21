using Byatool.Functional.ToSql;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Functional.Test.SqlTest.SelectTest
{
    [TestFixture]
    public class WhenCreatingAWhereClause : WhenCreatingAStatement
    {
        #region Fields

        private const int FirstValue = 1;
        private const int SecondValue = 2;
        private const string ThirdColumn = "column3";
        private const int ThirdValue = 3;

        #endregion

        #region Test Methods

        [Test]
        public void ItCanContainAnEqualsClause()
        {
            var expectedText = string.Format("WHERE {0} = {1}", FirstColumn, FirstValue);

            new Where()
                [
                    FirstColumn.IsEqualTo(FirstValue)
                ]
                .Should()
                .Be(expectedText);
        }

        [Test]
        public void ItCanContainAnInStatementWithASubQuery()
        {
            const string clause = "WHERE {0} IN (SELECT {1} FROM {2})";

            var innerSelect =
                new Select()
                    [
                        SecondColumn
                    ].From(SomeTable);

            new Where()
                [
                    FirstColumn.In(innerSelect)
                ]
                .Should()
                .Be(string.Format(clause, FirstColumn, SecondColumn, SomeTable));
        }

        [Test]
        public void ItCanContainAnInStatementWithAnActualList()
        {
            const string clause = "WHERE {0} IN (1,2,3)";

            new Where()
               [
                   FirstColumn.In(new [] { 1, 2, 3})
               ]
               .Should()
               .Be(string.Format(clause, FirstColumn));
        }

        [Test]
        public void ItCanContainMultipleAnds()
        {

            var expectedText = 
                string.Format("WHERE ({0} = {1} AND ({2} = {3} AND {4} = {5}))",
                    FirstColumn, FirstValue, SecondColumn, SecondValue, ThirdColumn, ThirdValue);

            new Where()
               [
                   FirstColumn.IsEqualTo(FirstValue).And(SecondColumn.IsEqualTo(SecondValue).And(ThirdColumn.IsEqualTo(ThirdValue)))
               ]
               .Should()
               .Be(expectedText);
        }

        [Test]
        public void ItCanContainMultipleOrs()
        {
            var expectedText = string.Format("WHERE ({0} = {1} OR ({2} = {3} OR {4} = {5}))", FirstColumn, FirstValue, SecondColumn, SecondValue, ThirdColumn, ThirdValue);

            new Where()
               [
                   FirstColumn.IsEqualTo(FirstValue).Or(SecondColumn.IsEqualTo(SecondValue).Or(ThirdColumn.IsEqualTo(ThirdValue)))
               ]
               .Should()
               .Be(expectedText);
        }

        [Test]
        public void ItCanContainMultipleOrsAndAnds()
        {
            var expectedText =
               string.Format("WHERE ({0} = {1} AND ({2} = {3} OR {4} = {5}))",
                   FirstColumn, FirstValue, SecondColumn, SecondValue, ThirdColumn, ThirdValue);

            new Where()
               [
                   FirstColumn.IsEqualTo(FirstValue)
                    .And(SecondColumn.IsEqualTo(SecondValue)
                    .Or(ThirdColumn.IsEqualTo(ThirdValue)))
               ]
               .Should()
               .Be(expectedText);
        }

        [Test]
        public void ItCanContainMultipleWhereStatements()
        {
            var expectedText =
              string.Format("WHERE ({0} = {1} AND {2} = {3}) OR ({0} = {1} OR {4} = {5})",
                  FirstColumn, FirstValue, SecondColumn, SecondValue, ThirdColumn, ThirdValue);

            new Where()
                [
                    FirstColumn.IsEqualTo(FirstValue).And(SecondColumn.IsEqualTo(SecondValue)),
                    With.Or(FirstColumn.IsEqualTo(FirstValue).Or(ThirdColumn.IsEqualTo(ThirdValue)))
                ]
                .Should()
                .Be(expectedText);
        }

        #endregion
    }
}   