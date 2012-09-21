using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Byatool.Functional.ToSql
{
    public class InnerJoin
    {

        #region Constructors

        public InnerJoin()
        {
            Columns = new List<string>();
        } 

        #endregion

        #region Methods

        public virtual string this[params string[] items]
        {
            get
            {
                Func<StringBuilder, string, StringBuilder> theCurrentTextWithParentText = 
                    (builder, text) => builder.AppendLine("INNER JOIN " + text);

                foreach (var item in items)
                {
                    Columns.Add(item);
                }

                return Columns.Aggregate(new StringBuilder(), theCurrentTextWithParentText).ToString();
            }
        } 

        #endregion

        #region Properties
        
        public IList<string> Columns { get; protected set; }

        #endregion
    }
}