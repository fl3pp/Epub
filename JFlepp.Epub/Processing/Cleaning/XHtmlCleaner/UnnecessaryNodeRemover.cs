using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal interface IUnnecessaryNodeRemover
    {
        XElement RemoveNodes(XElement element);
    }

    internal sealed class UnnecessaryXmlNodeRemover : IUnnecessaryNodeRemover
    {
        private readonly string rootElement;
        private readonly string[] allowedElements;
        private readonly Predicate<XElement> skipNode;

        public UnnecessaryXmlNodeRemover(string rootElement, string[] allowedElements)
            : this(rootElement, allowedElements, (n) => false)
        { }

        public UnnecessaryXmlNodeRemover(string rootElement, string[] allowedElements, Predicate<XElement> skipNode)
        {
            this.rootElement = rootElement;
            this.allowedElements = allowedElements;
            this.skipNode = skipNode;
        }

        public XElement RemoveNodes(XElement element)
        {
            var root = GetRoot(element);

            IEnumerable<XElement> GetElementsToBeRemoved()
            {
                return root.Descendants()
                    .Where(d => NeedsToBeRemoved(d));
            }

            while (GetElementsToBeRemoved().Any())
            {
                RemoveElement(GetElementsToBeRemoved().First());
            }

            return root;
        }

        private XElement GetRoot(XElement element)
        {
            return element
                .DescendantsAndSelf()
                .Single(e => e.Name.LocalName.EqualsIgnoreCase(rootElement));
        }

        private bool NeedsToBeRemoved(XElement element)
        {
            return !allowedElements.Any(e => e.EqualsIgnoreCase(element.Name.LocalName))
                && !skipNode(element);
        }

        private void RemoveElement(XElement element)
        {
            element.ReplaceWith(element.Nodes());
        }
    }
}
