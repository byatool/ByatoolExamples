namespace Byatool.Functional.ToSql
{
    public static class AsExtention
    {
        public static string As(this string columnName, string alias)
        {
            return columnName + " AS " + alias;
        }

        public static string From(this string postFix, string tableName)
        {
            return tableName + "." + postFix;
        }

        public static string Top(this string inner, int count)
        {
            return string.Format("TOP {0} {1}", count, inner);
        }
    }
}