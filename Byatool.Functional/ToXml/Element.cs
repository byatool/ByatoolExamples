using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Byatool.Functional.ToXml
{
    public class Element : IElement
    {
        #region Fields

        private readonly string _name;
        private readonly string _value;

        private IList<IElement> _elements;

        #endregion

        #region Constructors

        public Element(string name, object value = null)
        {
            _name = name;
            _value = value == null ? null : value.ToString();
        }
        
        #endregion

        #region Support Methods
        
        private string CreateElementText(IEnumerable<IElement> elements)
        {
            return
                elements
                    .Where(item => item is Element)
                    .Select(item => item.Create())
                    .Aggregate(new StringBuilder(), (builder, text) => builder.AppendLine(text))
                    .ToString();
        }

        private string CreateThisElement(string name, string value)
        {
            var attributes = CompressTheAttributes(Elements.Where(item => item is XmlAttribute));

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

        private string CompressTheAttributes(IEnumerable<IElement> attributes)
        {
            return
                When<string>
                    .True(attributes.Any())
                    .Then(() =>
                        attributes
                        .Aggregate(new StringBuilder(), (builder, item) => builder.Append(item.Create()))
                        .ToString())
                    .Else(() => string.Empty);
        }

        #endregion

        #region Methods

        public virtual Element this[params IElement[] items]
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

        private IList<IElement> Elements
        {
            get { return _elements ?? (_elements = new List<IElement>()); }
        }

        #endregion
    }
}