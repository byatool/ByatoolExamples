using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Byatool.Functional.ToXml
{
    public class XmlAttribute
    {
        public string Name;
        public object Value;

        public XmlAttribute(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }

    public class Element
    {
        #region Fields

        private IEnumerable<XmlAttribute> _attributes;
        private readonly string _name;
        private readonly string _value;

        private IList<Element> _elements;

        #endregion

        #region Constructors

        public Element(string name, object value = null, IEnumerable<XmlAttribute> attributes = null)
        {
            _name = name;
            _attributes = attributes;
            _value = value == null ? null : value.ToString();
        }
        
        #endregion

        #region Support Methods
        
        private string CreateElementText(IEnumerable<Element> elements)
        {
            return
                elements
                    .Select(item => item.Create())
                    .Aggregate(new StringBuilder(), (builder, text) => builder.AppendLine(text))
                    .ToString();
        }

        private string CreateThisElement(string name, string value)
        {
            var attributes = CompressTheAttributes(Attributes);

            return
                When<string>
                    .True(string.IsNullOrEmpty(value) && Elements.Count == 0)
                    .Then(() => string.Format("<{0}{1} />", name, attributes))
                    .Else(() =>
                          When<string>
                              .True(!string.IsNullOrEmpty(value))
                              .Then(() => string.Format("<{0}{1}>{2}</{0}>", _name, attributes, _value))
                              .Else(() => string.Format("<{0}{1}>{2}</{0}>", _name, attributes, CreateElementText(Elements)))
                    );
        } 

        private string CompressTheAttributes(IEnumerable<XmlAttribute> attributes)
        {
            return
                When<string>
                    .True(attributes.Any())
                    .Then(() =>
                        attributes
                        .Aggregate(new StringBuilder(), (builder, item) => builder.Append(" " + item.Name + "=\"" + item.Value + "\""))
                        .ToString())
                    .Else(() => string.Empty);
        }

        #endregion

        #region Methods

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

        private IEnumerable<XmlAttribute> Attributes
        {
            get { return _attributes ?? (_attributes = new List<XmlAttribute>()); }
        }

        #endregion
    }
}