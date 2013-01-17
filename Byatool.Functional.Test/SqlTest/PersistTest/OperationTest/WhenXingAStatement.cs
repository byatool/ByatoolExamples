using System.Data.SqlClient;
using System.Reflection;
using Byatool.Functional.ToSql.Persist.Operation;
using NUnit.Framework;

namespace Byatool.Functional.Test.SqlTest.PersistTest.OperationTest
{
    public class WhenXingAStatement
    {
        #region Fields

        protected const BindingFlags BindingFlagsToSeeAll =
          BindingFlags.Static | BindingFlags.FlattenHierarchy |
          BindingFlags.Instance | BindingFlags.NonPublic |
          BindingFlags.Public;

        protected const string Connection = @"Data Source=.\SqlExpress;Integrated Security=True";
        protected const string FirstColumn = "FirstColumn";
        protected const string FirstColumnAlias = "ColumnOne";
        protected const int FirstValue = 1;
        protected const string SecondColumn = "SecondColumn";
        protected const string SecondValue = "test";
        protected const string SomeTable = "SomeTable";
        protected const string ConnectionKeyword = "_connection";

        #endregion

        #region Methods

        protected int RetrieveCountOfRowsWithTheValue(int firstValue)
        {
            var createdConnection = new SqlConnection(Connection);
            var neededCommand = new SqlCommand("SELECT COUNT(SomeId) FROM SomeTable WHERE FirstColumn = @FirstColumn", createdConnection);

            var parameters = new[] { new SqlParameter("@FirstColumn", firstValue) };
            var result = 0;

            neededCommand.Parameters.AddRange(parameters);
            try
            {
                createdConnection.Open();
                result = ((int)neededCommand.ExecuteScalar());
            }
            finally
            {
                createdConnection.Close();
            }

            return result;
        } 

        #endregion

        #region Test Hooks

        [TearDown]
        public void TearDown()
        {
            new Delete(SomeTable)
                .ConnectTo(Connection)
                .Run();

        }

        #endregion

        protected object RetrieveValueFromObject(object toCheck, string fieldName)
        {
            return toCheck.GetType().GetField(fieldName, BindingFlagsToSeeAll).GetValue(toCheck);
        }
    }
}