using System.Collections.Generic;
using System.Linq;

namespace Byatool.Functional.ToSql
{
    public class GroupBy
    {
        #region Constructors

        public GroupBy()
        {
            Columns = new List<string>();
        } 

        #endregion

        #region Methods

        public virtual string this[params string[] items]
        {
            get
            {
                foreach (var item in items)
                {
                    Columns.Add(item);
                }

                return "Group By " + string.Join(", ", Columns.ToArray());
            }
        } 

        #endregion

        #region Properties
        
        public IList<string> Columns { get; protected set; } 

        #endregion
    }
}