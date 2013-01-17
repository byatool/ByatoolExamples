using System.Data.SqlClient;
using System.Linq;
using Byatool.Functional.ToSql.Persist.Section;

namespace Byatool.Functional.ToSql.Persist.Operation
{
    public class Delete 
    {
        #region Fields
        
        private string _connection;
        private readonly string _tableName;
        private Where _whereContainer;

        #endregion
        
        #region Constructors
        
        public Delete(string tableName)
        {
            _tableName = tableName;
        }

        #endregion
        
        #region Methods

        public Delete ConnectTo(string connection)
        {
            return new Delete(_tableName) { _connection = connection, WhereContainer = _whereContainer };
        }

        public string CreateSql()
        {
            return "DELETE FROM " + _tableName + (WhereContainer != null ? " " + WhereContainer.CreateSql() : string.Empty);
        }

        public void Run()
        {
            var createdConnection = new SqlConnection(_connection);
            var neededCommand = new SqlCommand(CreateSql(), createdConnection);

            var parameters = WhereContainer != null ? WhereContainer.CreateParameters().ToArray() : new SqlParameter[0];

            neededCommand.Parameters.AddRange(parameters);

            try
            {
                createdConnection.Open();
                neededCommand.ExecuteNonQuery();
            }
            finally
            {
                createdConnection.Close();
            }
        }

        public Delete Where(Where where)
        {
            return new Delete(_tableName) {_connection = _connection, _whereContainer = where};
        }

        #endregion

        #region Properties

        private Where WhereContainer
        {
            get { return _whereContainer; }
            set { _whereContainer = value; }
        }

        #endregion
    }
}