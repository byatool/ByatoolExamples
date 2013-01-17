using System;
using System.Linq;
using Byatool.Functional.ToSql.Persist;
using Byatool.Functional.ToSql.Persist.Operation;
using Byatool.Functional.ToSql.Persist.Section;
using Byatool.Shared;
using FluentAssertions;
using NUnit.Framework;

namespace Byatool.Functional.Test.SqlTest.PersistTest.OperationTest
{
    public class WhenCreatingAnUpdate : WhenXingAStatement
    {
        #region Fields

        /* Could create an Interface that allows for something to have Where in it
         * 
         * Update 
         * [
         *     C1.WillBe(V1),
         *     C2.WillBe(V2)
         * ]
         * .Where
         * [
         * 
         *      C1.IsEqualTo(V1),       //  (c1 == @c1)     WhereItem(C2, V2)
         *      And(C2.IsEqualTo(v2))   //  AND (c2 == @C2) WhereItem(And, C2, V2)
         *      Or(C3.IsEqualTo(v3))    //  OR (C3 == @C3)  WhereItem(And, C2, V2)
         * ]
         * 
         * 
         */
        private const string BasicQuery = "UPDATE {0} SET {1} = @{1}, {2} = @{2}";
        private const string WhereContainerKeyName = "_whereContainer";
        private const string ColumnsKeyword = "_columns";

        #endregion

        #region Support Methods

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        #region Test Methods

        [Test]
        public void ItCreatesACorrectQueryWithOnlyColumnSetters()
        {
            new Update(SomeTable)
                [
                    FirstColumn.WillBe(FirstValue),
                    SecondColumn.WillBe(SecondValue)
                ]
                .CreateSql().Should().Be(string.Format(BasicQuery, SomeTable, FirstColumn, SecondColumn));
        }

        [Test]
        public void ItCreatesANewUpdateWhenTheColumnsAreSet()
        {
            var original = new Update(SomeTable)[FirstColumn.WillBe(FirstValue)];
            var record = original[SecondColumn.WillBe("")];

            original.Should().NotBe(record);
        }

        [Test]
        public void ItCopiesTheConnectionWhenTheColumnsAreSet()
        {
            var original = new Update(SomeTable)[FirstColumn.WillBe(FirstValue)].ConnectTo("a");
            var record = original[SecondColumn.WillBe("")];

            RetrieveValueFromObject(original, ConnectionKeyword)
                .Should()
                .Be(RetrieveValueFromObject(record, ConnectionKeyword));
        }

        [Test]
        public void ItCopiesTheWhereContainerWhenTheColumnsAreSet()
        {
            var original = new Update(SomeTable)[FirstColumn.WillBe(FirstValue)].Where(new Where());
            var record = original[SecondColumn.WillBe("")];

            RetrieveValueFromObject(original, WhereContainerKeyName)
                .Should()
                .Be(RetrieveValueFromObject(record, WhereContainerKeyName));
        }

        [Test]
        public void ItCreatesACorrectQueryWithAWhereClause()
        {
            const string query = BasicQuery + " WHERE {1} = @{1}{3}";

            var firstColumnIsEqualToFirstValue = new Where()
                [
                    FirstColumn.IsEqualTo(FirstValue)
                ];

            var whereItems = WhereDeconstruction.RetrieveTheWhereItemUniqueNames(firstColumnIsEqualToFirstValue).First();


            var finalSql = 
                new Update(SomeTable)
                    [
                        FirstColumn.WillBe(FirstValue),
                        SecondColumn.WillBe(SecondValue)
                    ]
                    .Where(firstColumnIsEqualToFirstValue)
                    .CreateSql();

            finalSql.Should().Be(string.Format(query, SomeTable, FirstColumn, SecondColumn, whereItems));
        }

        [Test]
        public void ItCreatesANewUpdateWhenAWhereClauseIsAdded()
        {
            var original = new Update(SomeTable)[FirstColumn.WillBe(FirstValue)];
            var record = original.Where(new Where());

            original.Should().NotBe(record);
        }

        [Test]
        public void ItCopiesTheConnectionWhenAWhereClauseIsAdded()
        {
            var original = new Update(SomeTable)[FirstColumn.WillBe(FirstValue)].ConnectTo("A");
            var record = original.Where(new Where());

            RetrieveValueFromObject(original, ConnectionKeyword).Should().Be(RetrieveValueFromObject(record, ConnectionKeyword));
        }

        [Test]
        public void ItCopiesTheColumnsWhenAWhereClauseIsAdded()
        {
            var original = new Update(SomeTable)[FirstColumn.WillBe(FirstValue)].ConnectTo("A");
            var record = original.Where(new Where());

            RetrieveValueFromObject(original, ColumnsKeyword).Should().Be(RetrieveValueFromObject(record, ColumnsKeyword));
        }

        [Test]
        public void ItAllowsTheConnectionStringToBeSet()
        {
            const string connection = "some connection";

            var operation =
                new Update(SomeTable)
                    [
                        FirstColumn.WillBe(FirstValue)
                    ]
                    .ConnectTo(connection);

            operation.GetType().GetField("_connection", BindingFlagsToSeeAll).GetValue(operation).ToString().Should().Be(connection);
        }

        [Test]
        public void ItCreatesANewUpdateWhenTheConnectionStringIsSet()
        {
            var original = new Update(SomeTable)[FirstColumn.WillBe(FirstValue)];
            var record = original.ConnectTo("A");

            original.Should().NotBe(record);
        }

        [Test]
        public void ItCopiesTheColumnsWhenTheConnectionStringIsSet()
        {
            var original = new Update(SomeTable)[FirstColumn.WillBe(FirstValue)];
            var record = original.ConnectTo("A");

            RetrieveValueFromObject(original, ColumnsKeyword).Should().Be(RetrieveValueFromObject(record, ColumnsKeyword));
        }

        [Test]
        public void ItCopiesTheWhereContainerWhenTheConnectionStringIsSet()
        {
            var original = new Update(SomeTable)[FirstColumn.WillBe(FirstValue)].Where(new Where());
            var record = original.ConnectTo("A");

            RetrieveValueFromObject(original, WhereContainerKeyName)
                .Should()
                .Be(RetrieveValueFromObject(record, WhereContainerKeyName));
        }

        [Test]
        public void ItCommitsTheChangesToTheServer()
        {

            var insertFirstColumnValue = RandomTool.CreateAnInt32();
            new Insert(SomeTable)
            [
                FirstColumn.WillBe(insertFirstColumnValue),
                SecondColumn.WillBe(SecondValue)
            ]
            .ConnectTo(Connection)
            .Run();


            var whereTheFirstColumIsEqualToTheRandomValue = new Where()
                [
                    FirstColumn.IsEqualTo(insertFirstColumnValue)
                ];

            var newFirstColumnValue = RandomTool.CreateAnInt32();
            RetrieveCountOfRowsWithTheValue(newFirstColumnValue).Should().Be(0);

            new Update(SomeTable)
                [
                    FirstColumn.WillBe(newFirstColumnValue)
                ]
                .Where(whereTheFirstColumIsEqualToTheRandomValue)
                .ConnectTo(Connection)
                .Run();

            RetrieveCountOfRowsWithTheValue(newFirstColumnValue).Should().Be(1);
        }

        #endregion
    }
}