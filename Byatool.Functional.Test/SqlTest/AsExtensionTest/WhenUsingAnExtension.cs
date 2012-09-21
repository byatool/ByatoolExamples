using System.Linq;
using Byatool.Functional.ToSql;
using NUnit.Framework;
using FluentAssertions;

namespace Byatool.Functional.Test.SqlTest.AsExtensionTest
{
    [TestFixture]
    public class WhenUsingAnExtension
    {
        #region Fields

        private const string Firstcolumn = "FirstColumn";
        private const string SecondColumn = "SecondColumn";
        private const string TableName = "TableName";

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        #endregion

        #region Test Methods

        [Test]
        public void AndItIsAndTheCreatedTextIsCorrect()
        {
            Firstcolumn
                .And(SecondColumn)
                .Should()
                .Be(string.Format("({0} AND {1})", Firstcolumn, SecondColumn));
        }

        [Test]
        public void AndItIsAndOnTheCreatedTextIsCorrect()
        {
            Firstcolumn
                .On(SecondColumn)
                .AndOn("sda")
                .Should()
                .Be(string.Format("{0} ON ({1}) AND (sda)", Firstcolumn, SecondColumn));
        }

        [Test]
        public void AndItIsAsTheCreatedTextIsCorrect()
        {
            Firstcolumn
                .As(SecondColumn)
                .Should()
                .Be(string.Format("{0} AS {1}", Firstcolumn, SecondColumn));
        }

        [Test]
        public void AndItIsFromTheCreatedTextIsCorrect()
        {
            Firstcolumn
                .From(TableName)
                .Should()
                .Be(string.Format("{0}.{1}", TableName, Firstcolumn));
        }

        [Test]
        public void AndItIsIsEqualToTheCreatedTextIsCorrect()
        {
            Firstcolumn
                .IsEqualTo(SecondColumn)
                .Should()
                .Be(string.Format("{0} = {1}", Firstcolumn, SecondColumn));
        }

        [Test]
        public void AndItIsMatchesTheCreatedTextIsCorrect()
        {
            Firstcolumn
                .Matches(SecondColumn)
                .Should()
                .Be(string.Format("{0} = ({1})", Firstcolumn, SecondColumn));
        }

        [Test]
        public void AndItIsOnTheCreatedTextIsCorrect()
        {
            Firstcolumn
                .On(SecondColumn)
                .Should()
                .Be(string.Format("{0} ON ({1})", Firstcolumn, SecondColumn));
        }

        [Test]
        public void AndItIsOrTheCreatedTextIsCorrect()
        {
            Firstcolumn
                .Or(SecondColumn)
                .Should()
                .Be(string.Format("({0} OR {1})", Firstcolumn, SecondColumn));
        }

        [Test]
        public void AndItIsTopTheCreatedTextIsCorrect()
        {
            Firstcolumn
                .Top(1)
                .Should()
                .Be(string.Format("TOP {0} {1}", 1, Firstcolumn));
        }

        #endregion
    }
}