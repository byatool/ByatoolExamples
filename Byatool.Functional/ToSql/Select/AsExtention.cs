namespace Byatool.Functional.ToSql.Select
{
    public static class AsExtention
    {
        #region Methods

        public static string And(this string statement, string statementInner)
        {
            return string.Format("({0} AND {1})", statement, statementInner);
        }

        public static string AndOn(this string statement, string statementInner)
        {
            return string.Format("{0} AND ({1})", statement, statementInner);
        }

        public static string As(this string columnName, string alias)
        {
            return columnName + " AS " + alias;
        }

        public static string CountOnly(this string columnName)
        {
            return string.Format(" count({0}) ", columnName);
        }

        public static string From(this string postFix, string tableName)
        {
            return tableName + "." + postFix;
        }

        public static string In(this string columnName, string innerListClause)
        {
            return columnName + " IN (" + innerListClause + ")";
        }

        public static string In(this string columnName, int[] innerListClause)
        {
            return columnName + " IN (" + string.Join(",", innerListClause) + ")";
        }

        public static string IsEqualTo(this string inner, object value)
        {

            return string.Format("{0} = {1}", inner, value);
        }

        public static string Matches(this string inner, string toMatch)
        {
            return string.Format("{0} = ({1})", inner, toMatch);
        }

        public static string On(this string inner, string condition)
        {
            return string.Format("{0} ON ({1})", inner, condition);
        } 

        public static string Or(this string statement, string statementInner)
        {
            return string.Format("({0} OR {1})", statement, statementInner);
        }

        public static string Top(this string inner, int count)
        {
            return string.Format("TOP {0} {1}", count, inner);
        } 

        #endregion
    }
}