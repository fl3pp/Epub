using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal sealed class NavigationExtractorEpub2 : INavigationExtractor
    {
        public Task<IEnumerable<NavigationPoint>> ExtractNavigationPoints(
            StructureFiles context, IEnumerable<File> files)
        {
            var root = context.NcxDoc.Root;
            var basePath = EpubPathHelper.GetDirectoryName(context.NcxPath);

            var navMap = root.Elements()
                .Single(element => element.Name.Equals(XmlNamespaces.Ncx + NcxXmlNames.NavMapElementName));
            var orderProcessor = NavigationOrderProcessor.Create(navMap);

            return Task.FromResult(FilterAndSelectChildNavPoints(navMap, orderProcessor, basePath, files));
        }

        private IEnumerable<NavigationPoint> FilterAndSelectChildNavPoints(
            XElement element, NavigationOrderProcessor orderProcessor, string basePath, IEnumerable<File> files)
        {
            return element.Elements()
                .Where(e => e.Name.Equals(XmlNamespaces.Ncx + NcxXmlNames.NavPointElementName))
                .Select(e => ProcessNavPoint(e, orderProcessor, basePath, files))
                .ToArray();
        }

        private NavigationPoint ProcessNavPoint(
            XElement element, NavigationOrderProcessor orderProcessor, string basePath, IEnumerable<File> files)
        {
            var title = element.Elements()
                .Single(e => e.Name.Equals(XmlNamespaces.Ncx + NcxXmlNames.NavLabelElementName))
                .Elements().Single(e => e.Name.Equals(XmlNamespaces.Ncx + NcxXmlNames.TextElementName))
                .Value;

            var contentText = element.Elements()
                .Single(e => e.Name.Equals(XmlNamespaces.Ncx + NcxXmlNames.ContentElementName))
                .GetAttributeValueOrNull(NcxXmlNames.SrcAttributeName)!;
            var filePath = SrcTextSplitter.GetFileName(contentText);
            var elementId = SrcTextSplitter.GetElementId(contentText);

            var fileFullPath = EpubPathHelper.ExpandPath(basePath, filePath);
            var file = files.Single(f => f.Path.EqualsIgnoreCaseWithNull(fileFullPath));

            var order = orderProcessor.GetOrder(element);

            var children = FilterAndSelectChildNavPoints(element, orderProcessor, basePath, files);

            return new NavigationPoint(title, file, elementId, order, children);
        }
    }
}
