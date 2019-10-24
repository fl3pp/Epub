using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal sealed class NavigationExtractorEpub3 : INavigationExtractor
    {
        private readonly IZip zip;
        private readonly IEnumerable<ManifestItem> manifestItems;

        public NavigationExtractorEpub3(
            IZip zip,
            IEnumerable<ManifestItem> manifestItems)
        {
            this.zip = zip;
            this.manifestItems = manifestItems;
        }

        public async Task<IEnumerable<NavigationPoint>> ExtractNavigationPoints(
            StructureFiles structureFiles, IEnumerable<File> files)
        {
            var xHtmlTocPath = GetXHtmlTocPath(structureFiles);
            var xDoc = await zip.ParseXmlFromPathAsync(xHtmlTocPath).ConfigureAwait(false);
            var navigationOrderProcessor = NavigationOrderProcessor.Create();
            var navElement = GetRootOrderedList(xDoc);
            return ProcessOrderedListRecursive(
                navElement, navigationOrderProcessor, EpubPathHelper.GetDirectoryName(xHtmlTocPath), files).ToArray();
        }

        private string GetXHtmlTocPath(StructureFiles structureFiles)
        {
            var xHtmlTocRelativePath = manifestItems.Single(
                i => OpfXmlNames.NavPropertiesAttributeValue.EqualsIgnoreCaseWithNull(i.Properties)).Href!;
            return EpubPathHelper.ExpandPath(
                EpubPathHelper.GetDirectoryName(structureFiles.OpfPath), xHtmlTocRelativePath);
        }

        private XElement GetRootOrderedList(XDocument document)
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
