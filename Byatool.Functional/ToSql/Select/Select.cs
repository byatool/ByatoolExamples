using System.Collections.Generic;

namespace Byatool.Functional.ToSql.Select
{
    public class Select
    {
        #region Constructors
        
        public Select()
        {
            Columns = new List<string>();
        } 

        #endregion

        #region Methods

        public virtual Select this[params string[] items]
        {
            get
            {
                foreach (var item in items)
                {
                    Columns.Add(item);
                }
                return this;
            }
        }

        public string From(string tableName)
        {
            return "SELECT " + string.Join(", ", Columns) + " FROM " + tableName;
        }

        #endregion

        #region Properties
        
        public IList<string> Columns { get; protected set; } 

        #endregion
    }
}