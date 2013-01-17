using System.Collections.Generic;
using System.Linq;

namespace Byatool.Functional.ToSql.Select
{
    public class Where
    {
        #region Constructors
        
        public Where()
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

                return "WHERE " + string.Join(" ", Columns.ToArray());
            }
        } 

        #endregion

        #region Properties
        
        public IList<string> Columns { get; protected set; } 

        #endregion
    }
}