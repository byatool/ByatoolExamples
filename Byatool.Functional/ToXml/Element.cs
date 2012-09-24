using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Byatool.Functional.ToXml
{
    public class Element
    {
        #region Fields
        
        private readonly string _name;
        private readonly string _value;

        private IList<Element> _elements;

        #endregion

        #region Constructors

        public Element(string name)
        {
            _name = name;
        }

        public Element(string name, string value)
        {
            _name = name;
            _value = value;
        } 
        
        #endregion
        
        #region Methods

        private string CreateElementText(IEnumerable<Element> elements )
        {
            return
                elements
                    .Select(item => item.Create())
                    .Aggregate(new StringBuilder(), (builder, text) => builder.AppendLine(text))
                    .ToString();
        }

        public string CreateThisElement(string name, string value)
        {
            return 
                When<string>
                    .True(string.IsNullOrEmpty(value) && Elements.Count == 0)
                    .Then(() => string.Format("<{0} />", name))
                    .Else(() =>
                        When<string>
                            .True(!string.IsNullOrEmpty(value))
                            .Then(() => string.Format("<{0}>{1}</{0}>", _name, _value))
                            .Else(() => string.Format("<{0}>{1}</{0}>", _name, CreateElementText(Elements)))
                    );
        }

        public virtual Element this[params Element[] items]
        {
            get
            {
                foreach (var item in items)
                {
                    Elements.Add(item);
                }

                return this;
            }
        } 

        public string Create()
        {
            return CreateThisElement(_name, _value);
        }

        #endregion
        
        #region Properties

        private IList<Element> Elements
        {
            get { return _elements ?? (_elements = new List<Element>()); }
        } 

        #endregion
    }
}