using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Byatool.Functional.Test.SqlTest.PersistTest.OperationTest;
using Byatool.Functional.ToSql.Persist;
using Byatool.Functional.ToSql.Persist.Section;
using FluentAssertions;
using NUnit.Framework;

namespace Byatool.Functional.Test.SqlTest.PersistTest.SectionTest
{
    public class WhenCreatingAWhereClause : WhenXingAStatement
    {
        private string ThirdColumn;

        #region Fields

        
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
        public void ItShouldAllowASingleEquals()
        {
            const string finalText = "WHERE {0} = @{0}{1}";

            var whereClause =
                new Where()
                    [
                        FirstColumn.IsEqualTo(1)
                    ];

            var whereItems = WhereDeconstruction.RetrieveTheWhereItemUniqueNames(whereClause).First();

            whereClause
                 .CreateSql()
                 .Should()
                 .Be(string.Format(finalText, FirstColumn, whereItems));
        }

        [Test]
        public void ItShouldAllowAnAnd()
        {
            const string finalText = "WHERE {0} = @{0}{2} AND ({1} = @{1}{3})";

            var whereClause =
                new Where()
                    [
                        FirstColumn.IsEqualTo(1),
                        Also.And(SecondColumn.IsEqualTo(1))
                    ];

            var whereItems = WhereDeconstruction.RetrieveTheWhereItemUniqueNames(whereClause);

            whereClause
                .CreateSql()
                .Should()
                .Be(string.Format(finalText, FirstColumn, SecondColumn, whereItems[0], whereItems[1]));
        }

        [Test]
        public void ItShouldAllowAnOr()
        {
            const string finalText = "WHERE {0} = @{0}{2} OR ({1} = @{1}{3})";

            var whereClause =
            new Where()
                [
                    FirstColumn.IsEqualTo(1),
                    Also.Or(SecondColumn.IsEqualTo(1))
                ];

            var whereItems = WhereDeconstruction.RetrieveTheWhereItemUniqueNames(whereClause);

            whereClause
                .CreateSql()
                .Should()
                .Be(string.Format(finalText, FirstColumn, SecondColumn, whereItems[0], whereItems[1]));
        }

        [Test]
        public void ItShouldAllowCompoundExpressions()
        {
            const string finalText = "WHERE {0} = @{0}{3} AND ({1} = @{1}{4}) OR ({2} = @{2}{5})";

            ThirdColumn = "ThirdColumn";
            var whereClause =
                new Where()
                    [
                        FirstColumn.IsEqualTo(1),
                        Also.And(SecondColumn.IsEqualTo(1)),
                        Also.Or(ThirdColumn.IsEqualTo(1))
                    ];

            var whereItems = WhereDeconstruction.RetrieveTheWhereItemUniqueNames(whereClause);

            whereClause
              .CreateSql()
              .Should()
              .Be(string.Format(finalText, FirstColumn, SecondColumn, ThirdColumn, whereItems[0], whereItems[1], whereItems[2]));
        }

        [Test]
        public void ItShouldCreateAValidParameterList()
        {
            var parameters = new[] { new SqlParameter("@" + FirstColumn, FirstValue), new SqlParameter("@" + SecondColumn, SecondValue) };

            var whereClause =
                new Where()
                    [
                        FirstColumn.IsEqualTo(FirstValue),
                        Also.And(SecondColumn.IsEqualTo(SecondValue))
                    ];

            var whereItems = WhereDeconstruction.RetrieveTheWhereItemUniqueNames(whereClause);

            whereClause
                .CreateParameters().Should().Match(
                    (SqlParameter[] x)
                        => x[0].Value.ToString() == parameters[0].Value.ToString() && x[0].ParameterName == parameters[0].ParameterName + whereItems[0]
                        && x[1].Value.ToString() == parameters[1].Value.ToString() && x[1].ParameterName == parameters[1].ParameterName + whereItems[1]);
        }

        #endregion
    }
}