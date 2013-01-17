using System;
using Byatool.Functional.ToSql.Persist.Element;

namespace Byatool.Functional.ToSql.Persist
{
    public static class SqlStatementExtensions
    {
        public static ColumnItem WillBe(this string inner, object value)
        {
            return new ColumnItem(inner, value);
        }

        public static WhereItem IsEqualTo(this string inner, object value)
        {
            return inner.IsEqualTo(WhereType.None, value);
        }

        public static WhereItem IsEqualTo(this string inner, WhereType whereType, object value)
        {
            return new WhereItem(whereType, inner, value);
        }
    }

    public class WhereItem
    {
        public WhereItem(WhereType whereType, string name, object value)
        {
            WhereType = whereType;
            Name = name;
            Value = value;
            UniqueKey = Guid.NewGuid().ToString().Replace("-", "");
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
        public WhereType WhereType { get; private set; }
        public string UniqueKey { get; private set; }
    }

    public enum WhereType
    {
        None,
        And,
        Or
    }
}