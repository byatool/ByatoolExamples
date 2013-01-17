using Byatool.Functional.ToSql;
using Byatool.Functional.ToSql.Select;
using FluentAssertions;
using NUnit.Framework;

namespace Byatool.Functional.Test.SqlTest.SelectTest
{
    [TestFixture]
    public class WhenCreatingSelectAStatement : WhenCreatingAStatement
    {
        #region Test Methods

        [Test]
        public void AndATableIsSet()
        {
            new Select()
                [
                    FirstColumn
                ]
                .From(SomeTable)
                .Should()
                .Be(string.Format("SELECT {0} FROM {1}", FirstColumn, SomeTable));
        }

        [Test]
        public void AndTheTableHasAnAlias()
        {
            const string tableAlias = "SomeTableAlias";

            new Select()
                [
                    FirstColumn
                ]
                .From(SomeTable.As(tableAlias))
                .Should()
                .Be(string.Format("SELECT {0} FROM {1} AS {2}", FirstColumn, SomeTable, tableAlias));
        }

        [Test]
        public void AndAColumnDoesNotNeedAnAlias()
        {
            new Select()
                [
                    FirstColumn, 
                    SecondColumn
                ]
                .From(SomeTable)
                .Should()
                .Be(string.Format("SELECT {0}, {1} FROM {2}", FirstColumn, SecondColumn, SomeTable));
        }

        [Test]
        public void AndAColumnMayHaveAnAlias()
        {
            new Select()
                 [
                     FirstColumn.As(FirstColumnAlias),
                     SecondColumn
                 ]
                 .From(SomeTable)
                 .Should()
                 .Be(string.Format("SELECT {0} AS {1}, {2} FROM {3}", FirstColumn, FirstColumnAlias, SecondColumn, SomeTable));
        }

        [Test]
        public void AndAColumnMayHaveAPrecedingTableName()
        {
            new Select()
             [
                 FirstColumn.From(SomeTable).As(FirstColumnAlias),
                 SecondColumn
             ]
             .From(SomeTable)
             .Should()
             .Be(string.Format("SELECT {0}.{1} AS {2}, {3} FROM {4}", SomeTable, FirstColumn, FirstColumnAlias, SecondColumn, SomeTable));
        }

        [Test]
        public void AndTheTopCanBeRetreived()
        {
            new Select()
                [
                    FirstColumn.Top(10).As(FirstColumnAlias)
                ]
                .From(SomeTable)
                .Should()
                .Be(string.Format("SELECT TOP 10 {0} AS {1} FROM {2}", FirstColumn, FirstColumnAlias, SomeTable));
        }

        #endregion
    }
}