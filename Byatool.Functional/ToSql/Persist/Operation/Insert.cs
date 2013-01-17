using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Byatool.Functional.ToSql.Persist.Element;

namespace Byatool.Functional.ToSql.Persist.Operation
{
    public class Insert
    {
        #region Fields

        private string _connection;
        private IList<ColumnItem> _columns;

        #endregion

        #region Constructors
        
        public Insert(string tableName)
        {
            TableName = tableName;
            Columns = new List<ColumnItem>();
        }

        #endregion

        #region Methods

        public Insert ConnectTo(string connection)
        {
            _connection = connection;

            return new Insert(TableName) {_connection = connection, Columns = Columns};
        }

        public string CreateSql()
        {
            var allNames = Columns.Select(item => item.Name).ToList();

            return ("INSERT INTO " + TableName + "(" + string.Join(", ", allNames) + ") VALUES (" + string.Join(", ", allNames.Select(item => "@" + item)) + ")").Trim();
        }

        public Insert this[params ColumnItem[] items]
        {
            get
            {
                var result = new Insert(TableName) { _connection = _connection, _columns = Columns };

                foreach (var item in items)
                {
                    _columns.Add(item);
                }

                return result;
            }
        }

        public void Run()
        {
            var createdConnection = new SqlConnection(_connection);
            var neededCommand = new SqlCommand(CreateSql(), createdConnection);

            var parameters = Columns.Select(item => new SqlParameter("@" + item.Name, item.Value ?? DBNull.Value)).ToArray();
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

        #endregion

        #region Properties

        public IList<ColumnItem> Columns
        {
            get { return _columns; }
            protected set { _columns = value; }
        }

        public string TableName { get; private set; } 

        #endregion
    }
}