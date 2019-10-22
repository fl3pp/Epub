using System;
using System.Linq;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal interface IUnnecessaryAttributeRemover
    {
        void RemoveAttributes(XElement element);
    }

    internal sealed class UnnecessaryXmlAttributeRemover : IUnnecessaryAttributeRemover
    {
        private readonly string[] attributesToKeep;

        public UnnecessaryXmlAttributeRemover(string[] attributesToKeep)
        {
            this.attributesToKeep = attributesToKeep;
        }

        public void RemoveAttributes(XElement element)
        {
            foreach (var child in element.DescendantsAndSelf())
            {
                foreach (var attribute in child.Attributes().ToArray())
                {
                    if (attributesToKeep.Contains(attribute.Name.LocalName))
                        continue;

                    attribute.Remove();
                }
                child.Name = child.Name.LocalName;
            }
        }
    }
}
