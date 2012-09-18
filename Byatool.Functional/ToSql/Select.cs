using System.Collections.Generic;

namespace Byatool.Functional.ToSql
{
    public class Select
    {
        public Select()
        {
            Columns = new List<string>();
        }

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

        public IList<string> Columns { get; protected set; }

        public string From(string tableName)
        {
            return "SELECT " + string.Join(", ", Columns) + " FROM " + tableName;
        }
    }
}