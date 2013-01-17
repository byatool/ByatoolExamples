using Byatool.Functional.ToSql.Persist;
using Byatool.Functional.ToSql.Persist.Operation;
using Byatool.Shared;
using FluentAssertions;
using NUnit.Framework;

namespace Byatool.Functional.Test.SqlTest.PersistTest.OperationTest
{
    public class WhenCreatingAnInsert : WhenXingAStatement
    {
        #region Fields

        private int _fakeValue;

        #endregion

        #region Support Methods
       
        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
            _fakeValue = RandomTool.CreateANegativeInt32();
        }

        #endregion

        #region Test Methods

        [Test]
        public void TheInsertCanCreateAValidStatement()
        {
            var insertStatement = 
                new Insert(SomeTable)
                    [
                        FirstColumn.WillBe(_fakeValue),
                        SecondColumn.WillBe(SecondValue)
                    ];

            insertStatement.CreateSql().Should().Be("INSERT INTO " + SomeTable + "(" + FirstColumn + ", " + SecondColumn + ") VALUES (@" + FirstColumn + ", @" + SecondColumn + ")");
        }

        [Test]
        public void ANewInsertIsCreatedWhenSettingTheColumns()
        {
            var insertStatement =
                 new Insert(SomeTable)
                     [
                         FirstColumn.WillBe(_fakeValue),
                         SecondColumn.WillBe(SecondValue)
                     ];

            var newStatement = insertStatement[FirstColumn.WillBe(_fakeValue)];

            insertStatement.Should().NotBe(newStatement);
        }

        [Test]
        public void ANewInsertIsCreatedWhenSettingTheColumnsAndTheConnectionIsTheSame()
        {
            const string firstConnection = "connectionA";
            const string secondConnection = "connectionB";

            var insertStatement = new Insert(SomeTable)[FirstColumn.WillBe(_fakeValue)].ConnectTo(firstConnection);
            var newStatement = insertStatement.ConnectTo(secondConnection);

            var connectionA = RetrieveValueFromObject(insertStatement, ConnectionKeyword);
            var connectionB = RetrieveValueFromObject(newStatement, ConnectionKeyword);

             connectionA.Should().Be(connectionB);
        }

        [Test]
        public void TheConnectionStringIsAllowedToBeSet()
        {
            const string connection = "some connection";

            var insertStatement =
                new Insert(SomeTable)
                    [
                        FirstColumn.WillBe(_fakeValue)
                    ]
                    .ConnectTo(connection);

            RetrieveValueFromObject(insertStatement, ConnectionKeyword).ToString().Should().Be(connection);
        }

        [Test]
        public void TheConnectionStringIsSetAndANewInsertIsCreated()
        {
            const string connection = "some connection";

            var insertStatement = new Insert(SomeTable).ConnectTo(connection);
            var newStatement = insertStatement.ConnectTo(connection);

            insertStatement.Should().NotBe(newStatement);
        }

        [Test]
        public void TheConnectionStringIsSetAndANewInsertHasTheSameColumns()
        {
            const string connection = "some connection";

            var insertStatement = new Insert(SomeTable)[FirstColumn.WillBe(FirstValue)].ConnectTo(connection);
            var newStatement = insertStatement.ConnectTo(connection);

            newStatement.Columns.Should().BeEquivalentTo(insertStatement.Columns);
        }

        [Test]
        public void TheItemIsSavedToTheDatabase()
        {
            new Insert(SomeTable)
                [
                    FirstColumn.WillBe(_fakeValue),
                    SecondColumn.WillBe(SecondValue)
                ]
                .ConnectTo(Connection)
                .Run();

            RetrieveCountOfRowsWithTheValue(_fakeValue).Should().Be(1);
        }

        #endregion
    }
}