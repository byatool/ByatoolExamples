using System.Collections.Generic;
using System.Linq;

namespace Byatool.Functional.ToSql
{
    public class Where
    {
        public Where()
        {
            Columns = new List<string>();
        }

        public virtual string this[params string[] items]
        {
            get
            {
                foreach (var item in items)
                {
                    Columns.Add(item);
                }

                return "WHERE " + string.Join(" ", Columns.ToArray());
            }
        }

        public IList<string> Columns { get; protected set; }
    }
}