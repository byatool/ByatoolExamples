namespace Byatool.Functional.ToXml
{
    public class XmlAttribute : IElement
    {
        #region Fields

        public string Name;
        public object Value;
 
        #endregion
        
        #region Constructors

        public XmlAttribute(string name, object value)
        {
            Name = name;
            Value = value;
        } 

        #endregion

        #region Implementations
        
        public string Create()
        {
            return string.Format(" {0}=\"{1}\"", Name, Value);
        } 

        #endregion
    }
}