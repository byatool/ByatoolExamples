namespace Byatool.Functional.ToSql
{
    public class With
    {
        public static string Or(string isEqualTo)
        {
            return "OR " + isEqualTo;
        }

        public static string And(string statement)
        {
            return "AND " + statement;
        }
    }
}