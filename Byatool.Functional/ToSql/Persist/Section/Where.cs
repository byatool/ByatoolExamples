using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Byatool.Functional.ToSql.Persist.Section
{
    public class Where
    {
        #region Fields
        
        private List<WhereItem> _whereItems;

        #endregion
        
        #region Methods

        public SqlParameter[] CreateParameters()
        {
            return WhereItems.Select(item => new SqlParameter("@" + item.Name + item.UniqueKey, item.Value)).ToArray();
        }

        public string CreateSql()
        {
            return "WHERE " + _whereItems.Aggregate(new StringBuilder(), (builder, item) => builder.Append(createSqlNeededForAWhereType(item)));
        }

        //BAD
        //  Really unhappy with appending a guid to the end of a parameter name,
        //      because it changes the name of the parameter everytime, which 
        //      most likely means each one is cached seperately by sql server.
        //      This negates one of the main reasons for paramterized sql.
        private string createSqlNeededForAWhereType(WhereItem item)
        {
            var parameterName = "@" + item.Name + item.UniqueKey;

            return
                new Match<WhereType, string>(item.WhereType)
                    .When(WhereType.None, () => item.Name + " = " + parameterName)
                    .When(WhereType.And, () => " AND (" + item.Name + " = " + parameterName + ")")
                    .When(WhereType.Or, () => " OR (" + item.Name + " = " + parameterName + ")")
                    .Go();
        }

        public Where this[params WhereItem[] items]
        {
            get
            {
                foreach (var item in items)
                {
                    WhereItems.Add(item);
                }

                return this;
            }
        }

        #endregion

        #region Properties

        private IList<WhereItem> WhereItems
        {
            get
            {
                return _whereItems ?? (_whereItems = new List<WhereItem>());
            }
        }

        #endregion
    }
}