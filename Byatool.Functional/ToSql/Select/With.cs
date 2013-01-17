namespace Byatool.Functional.ToSql.Select
{
    public class With
    {
        #region Methods
        
        public static string Or(string isEqualTo)
        {
            return "OR " + isEqualTo;
        }

        public static string And(string statement)
        {
            return "AND " + statement;
        } 

        #endregion
    }
}