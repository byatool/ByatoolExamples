using System;
using Byatool.Functional.ToSql.Persist;
using Byatool.Functional.ToSql.Persist.Operation;
using Byatool.Functional.ToSql.Persist.Section;
using Byatool.Shared;
using FluentAssertions;
using NUnit.Framework;

namespace Byatool.Functional.Test.SqlTest.PersistTest.OperationTest
{
    public class WhenDeletingAnItem : WhenXingAStatement
    {
        #region Fields
        #endregion

        #region Support Methods

        private void InsertRow(int firstValue, string secondValue = "")
        {
            new Insert(SomeTable)
               [
                   FirstColumn.WillBe(firstValue),
                   SecondColumn.WillBe(secondValue)
               ]
               .ConnectTo(Connection)
               .Run();
        }

        private Tuple<int, string> SetUpTheFakeData()
        {
            var neededSecondValue = RandomTool.CreateAString(5);
            var notableFirstValue = RandomTool.CreateAnInt32();

            InsertRow(notableFirstValue, neededSecondValue);
            InsertRow(notableFirstValue, RandomTool.CreateAString(5));

            RetrieveCountOfRowsWithTheValue(notableFirstValue).Should().Be(2);

            return new Tuple<int, string>(notableFirstValue, neededSecondValue);
        }

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        #region Test Methods

        [Test]
        public void TheWhereContainerisChangedButANewDeleteIsCreated()
        {
            var firstColumnIsEqualToFirstValue =
               new Where()
                   [
                       FirstColumn.IsEqualTo(FirstValue)
                   ];

            var deleteStatement = new Delete(SomeTable).Where(firstColumnIsEqualToFirstValue).ConnectTo("a");
            var record = deleteStatement.Where(new Where()[FirstColumn.IsEqualTo(1)]);

            deleteStatement.Should().NotBe(record);
        }

        [Test]
        public void TheWhereContainerisChangedButTheConnectionStringIsTheSame()
        {
            var firstColumnIsEqualToFirstValue =
               new Where()
                   [
                       FirstColumn.IsEqualTo(FirstValue)
                   ];

            var deleteStatement = new Delete(SomeTable).Where(firstColumnIsEqualToFirstValue).ConnectTo("a");
            var record = deleteStatement.Where(new Where()[FirstColumn.IsEqualTo(1)]);

            RetrieveValueFromObject(deleteStatement, ConnectionKeyword).Should().Be(RetrieveValueFromObject(record, ConnectionKeyword));
        }

        [Test]
        public void TheConnectionStringIsAllowedToBeSet()
        {
            const string connection = "some connection";

            var deleteStatement = new Delete(SomeTable).ConnectTo(connection);

            deleteStatement.GetType().GetField(ConnectionKeyword, BindingFlagsToSeeAll).GetValue(deleteStatement).ToString().Should().Be(connection);
        }

        [Test]
        public void TheConnectionStringisChangedButANewDeleteIsCreated()
        {
            const string connection = "some connection";

            var deleteStatement = new Delete(SomeTable).ConnectTo(connection);
            var record = deleteStatement.ConnectTo(connection);
            deleteStatement.Should().NotBe(record);
        }        
        
        [Test]
        public void TheConnectionStringisChangedButTheWhereContainerIsTheSame()
        {
            const string whereContainerName = "_whereContainer";
            var whereClause = new Where()[FirstColumn.IsEqualTo(1)];

            var deleteStatement = new Delete(SomeTable).Where(whereClause).ConnectTo("A");
            var record = deleteStatement.ConnectTo("B");

            RetrieveValueFromObject(deleteStatement, whereContainerName).Should().Be(RetrieveValueFromObject(record, whereContainerName));
        }

        [Test]
        public void TheItemIsDeletedFromTheDatabaseWithASimpleWhereClause()
        {
            var value = RandomTool.CreateAnInt32();

            InsertRow(value);
            RetrieveCountOfRowsWithTheValue(value).Should().Be(1);

            var firstColumnIsEqualToFirstValue =
                new Where()
                    [
                        FirstColumn.IsEqualTo(value)
                    ];

            new Delete(SomeTable)
                .ConnectTo(Connection)
                .Where(firstColumnIsEqualToFirstValue)
                .Run();

            RetrieveCountOfRowsWithTheValue(FirstValue).Should().Be(0);
        }

        [Test]
        public void AllRecordsCanBeRemoved()
        {
            var neededInformation = SetUpTheFakeData();
            RetrieveCountOfRowsWithTheValue(neededInformation.Item1).Should().Be(2);

            new Delete(SomeTable)
               .ConnectTo(Connection)
               .Run();

            RetrieveCountOfRowsWithTheValue(neededInformation.Item1).Should().Be(0);
        }
       
        [Test]
        public void TheItemIsDeletedFromTheDatabaseWithALessSimpelWhereClause()
        {
            var neededInformation = SetUpTheFakeData();

            RetrieveCountOfRowsWithTheValue(neededInformation.Item1).Should().Be(2);

            var theFirstColumnIsEqualToVakueAndTheSecondColumnIsEqualToNeededSecondValue =
                new Where()
                    [
                        FirstColumn.IsEqualTo(neededInformation.Item1),
                        Also.And(SecondColumn.IsEqualTo(neededInformation.Item2))
                    ];

            new Delete(SomeTable)
                .Where(theFirstColumnIsEqualToVakueAndTheSecondColumnIsEqualToNeededSecondValue)
                .ConnectTo(Connection)
                .Run();

            RetrieveCountOfRowsWithTheValue(neededInformation.Item1).Should().Be(1);
        }
        
        #endregion
    }
}