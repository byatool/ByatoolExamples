namespace Byatool.Functional.ToSql.Persist.Section
{
    public class Also
    {
        #region Methods
        
        public static WhereItem And(WhereItem isEqualTo)
        {
            return new WhereItem(WhereType.And, isEqualTo.Name, isEqualTo.Value);
        }

        public static WhereItem Or(WhereItem isEqualTo)
        {
            return new WhereItem(WhereType.Or, isEqualTo.Name, isEqualTo.Value);
        } 

        #endregion
    }
}