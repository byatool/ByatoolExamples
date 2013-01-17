using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Byatool.Functional.ToSql.Persist.Element;
using Byatool.Functional.ToSql.Persist.Section;

namespace Byatool.Functional.ToSql.Persist.Operation
{
    public class Update 
    {
        #region Fields

        private IList<ColumnItem> _columns;
        private string _connection;
        private readonly string _tableName;
        private Where _whereContainer;

        #endregion

        #region Constructors
        
        public Update(string tableName)
        {
            _tableName = tableName;
        }

        #endregion

        #region Methods

        public Update this[params ColumnItem[] items]
        {
            get
            {
                var result = new Update(_tableName) {_connection = _connection, WhereContainer = _whereContainer};

                foreach (var item in items)
                {
                    result.Columns.Add(item);
                }

                return result;
            }
        }

        public Update ConnectTo(string connection)
        {
            return new Update(_tableName) { _connection = connection, WhereContainer = _whereContainer, Columns = _columns};
        }

        public string CreateSql()
        {
            return ("UPDATE " + _tableName + " SET " + string.Join(", ", Columns.Select(item => item.Name + " = @" + item.Name))).Trim()
                + (WhereContainer != null ? " " + WhereContainer.CreateSql() : string.Empty);
        }

        public void Run()
        {
            var createdConnection = new SqlConnection(_connection);
            var neededCommand = new SqlCommand(CreateSql(), createdConnection);

            var parameters = Columns.Select(item => new SqlParameter("@" + item.Name, item.Value)).ToArray();
            parameters = parameters.Union(WhereContainer.CreateParameters()).ToArray();

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

        public Update Where(Where where)
        {
            return new Update(_tableName) { _connection = _connection, Columns = _columns, WhereContainer = where};
        }

        #endregion

        #region Properties

        public IList<ColumnItem> Columns
        {
            get { return _columns ?? ( _columns = new List<ColumnItem>()); }
            set { _columns = value; }
        }

        private Where WhereContainer
        {
            get { return _whereContainer; }
            set { _whereContainer = value; }
        }

        #endregion
    }
}