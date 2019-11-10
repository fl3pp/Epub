using System.Linq;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal interface IMetaExtractor
    {
        MetaData ExtractMeta(XmlStructureFile opf);
    }

    internal sealed class MetaExtractor : IMetaExtractor
    {
        public MetaData ExtractMeta(XmlStructureFile opf)
        {
            var root = opf.Doc.Root;
            var metaData = root.Elements().Single(
                element => element.Name.Equals(XmlNamespaces.Opf + OpfXmlNames.MetaDataElementName));

            return new MetaData(
                GetPropertyValueOrNull(metaData, OpfXmlNames.IdentifierElementName),
                GetPropertyValueOrNull(metaData, OpfXmlNames.TitleElementName),
                GetPropertyValueOrNull(metaData, OpfXmlNames.LanguageElementName),
                GetPropertyValueOrNull(metaData, OpfXmlNames.PublisherElementName),
                GetPropertyValueOrNull(metaData, OpfXmlNames.DescriptionElementName),
                GetPropertyValueOrNull(metaData, OpfXmlNames.DateElementName),
                GetPropertyValueOrNull(metaData, OpfXmlNames.CreatorElementName),
                GetPropertyValueOrNull(metaData, OpfXmlNames.RightsElementName));
        }

        private static string? GetPropertyValueOrNull(XElement metaData, string propertyName)
        {
            var elements = metaData.Elements()
                .Where(element => element.Name.Equals(XmlNamespaces.OpfMetaElements + propertyName));
            if (!elements.Any()) return null;
            return elements.First().Value;
        }
    }
}
