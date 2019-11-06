using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal sealed class XHtmlNavigationExtractor : IFileBasedNavigationExtractor
    {
        public IEnumerable<NavigationPoint> ExtractNavigationPoints(
            XmlStructureFile xHtmlToc, IEnumerable<File> files)
        {
            var navigationOrderProcessor = NavigationOrderProcessor.Create();
            var navElement = GetRootOrderedList(xHtmlToc.Doc);
            return ProcessOrderedListRecursive(
                navElement, navigationOrderProcessor, EpubPathHelper.GetDirectoryName(xHtmlToc.Path), files).ToArray();
        }

        private static XElement GetRootOrderedList(XDocument document)
        {
            return document
                .Descendants().Single(element =>
                    element.Name.Equals(XmlNamespaces.XHtml + XHtmlTocXmlNames.NavElementName) &&
                    element.Attributes().Any(attribute =>
                        attribute.Name.Equals(XmlNamespaces.Epub + XHtmlTocXmlNames.EpubTypeAttributeName) &&
                        attribute.Value.EqualsIgnoreCaseWithNull(XHtmlTocXmlNames.TocEpubTypeAttributeValue)))
                .Elements().Single(e => e.Name.Equals(XmlNamespaces.XHtml + XHtmlTocXmlNames.OrderedListElementName));
        }

        public IEnumerable<NavigationPoint> ProcessOrderedListRecursive(
            XElement element, NavigationOrderProcessor navigationOrderProcessor, string basePath, IEnumerable<File> files)
        {
            return element.Elements()
                .Where(e => e.Name.Equals(XmlNamespaces.XHtml + XHtmlTocXmlNames.ListItemElementName))
                .Select(e => ProcessListItemRecursive(e, navigationOrderProcessor, basePath, files));
        }

        private NavigationPoint ProcessListItemRecursive(
            XElement listItem, NavigationOrderProcessor navigationOrderProcessor, string basePath, IEnumerable<File> files)
        {
            var linkElement = listItem.Elements()
                .Single(element => element.Name.Equals(XmlNamespaces.XHtml + XHtmlTocXmlNames.LinkElementName));

            var title = linkElement.Value;
            var srcPath = linkElement.GetAttributeValueOrNull(XHtmlTocXmlNames.HrefAttributeName)!;
            var filePath = SrcTextSplitter.GetFileName(srcPath);
            var elementId = SrcTextSplitter.GetElementId(srcPath);
            var order = navigationOrderProcessor.GetOrder(listItem);

            var fileFullPath = EpubPathHelper.ExpandPath(basePath, filePath);
            var file = files.Single(f => f.Path.EqualsIgnoreCaseWithNull(fileFullPath));

            var childOrderedList = listItem.Elements()
                .Cast<XElement?>().SingleOrDefault(
                    e => e!.Name.Equals(XmlNamespaces.XHtml + XHtmlTocXmlNames.OrderedListElementName));
            var children = childOrderedList != null
                ? ProcessOrderedListRecursive(childOrderedList, navigationOrderProcessor, basePath, files).ToArray()
                : Array.Empty<NavigationPoint>();

            return new NavigationPoint(title, file, elementId, order, children);
        }
    }
}
