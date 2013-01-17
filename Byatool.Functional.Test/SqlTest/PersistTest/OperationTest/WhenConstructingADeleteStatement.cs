using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Byatool.Functional.ToSql.Persist;
using Byatool.Functional.ToSql.Persist.Operation;
using Byatool.Functional.ToSql.Persist.Section;
using FluentAssertions;
using NUnit.Framework;

namespace Byatool.Functional.Test.SqlTest.PersistTest.OperationTest
{
    public class WhenConstructingADeleteStatement
    {
        #region Fields

        protected const string FirstColumn = "FirstColumn";
        protected const string FirstColumnAlias = "ColumnOne";
        protected const int FirstValue = 1;
        protected const string SomeTable = "SomeTable";

        protected const BindingFlags BindingFlagsToSeeAll =
          BindingFlags.Static | BindingFlags.FlattenHierarchy |
          BindingFlags.Instance | BindingFlags.NonPublic |
          BindingFlags.Public;


        #endregion

        #region Support Methods

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        #endregion

        #region Test Methods

        [Test]
        public void TheDeleteCanCreateAValidStatement()
        {
            var deleteStatement = new Delete(SomeTable);

            deleteStatement.CreateSql().Should().Be("DELETE FROM " + SomeTable);
        }

        [Test]
        public void TheDeleteCanCreateAValidStatementWithConditions()
        {
            var firstColumnIsEqualToFirstValue =
                new Where()
                    [
                        FirstColumn.IsEqualTo(FirstValue)
                    ];

            var deleteStatement =
                new Delete(SomeTable)
                    .Where(firstColumnIsEqualToFirstValue);


            var whereItem =
                firstColumnIsEqualToFirstValue.GetType().GetProperty("WhereItems", BindingFlagsToSeeAll)
                    .GetValue(firstColumnIsEqualToFirstValue, BindingFlagsToSeeAll, null, null, null)
                    .As<IList<WhereItem>>()
                        .First();

            deleteStatement.CreateSql().Should().Be("DELETE FROM " + SomeTable + " WHERE " + FirstColumn + " = @" + whereItem.Name + whereItem.UniqueKey);
        }

        [Test]
        public void TheDeleteCanCreateAValidWithAnAlsoThatContainsTheSameName()
        {
            var firstColumnIsEqualToFirstValue =
                new Where()
                    [
                        FirstColumn.IsEqualTo(FirstValue),
                        Also.And(FirstColumn.IsEqualTo(FirstValue))
                    ];

            var deleteStatement =
                new Delete(SomeTable)
                    .Where(firstColumnIsEqualToFirstValue);

            deleteStatement.CreateSql().Should().NotBe("DELETE FROM " + SomeTable + " WHERE " + FirstColumn + " = @where_" + FirstColumn + " AND (" + FirstColumn + " = @where_" + FirstColumn + ")");
        }

        #endregion
    }
}