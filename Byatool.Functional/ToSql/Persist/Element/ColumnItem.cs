namespace Byatool.Functional.ToSql.Persist.Element
{
    public class ColumnItem
    {
        public ColumnItem(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }

    }
}