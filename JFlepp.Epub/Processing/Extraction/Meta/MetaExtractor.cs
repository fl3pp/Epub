using System.Linq;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal interface IMetaExtractor
    {
        MetaData ExtractMeta(XDocument opfDoc);
    }

    internal sealed class MetaExtractor : IMetaExtractor
    {
        public MetaData ExtractMeta(XDocument opfDoc)
        {
            var root = opfDoc.Root;
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

        private string? GetPropertyValueOrNull(XElement metaData, string propertyName)
        {
            var elements = metaData.Elements()
                .Where(element => element.Name.Equals(XmlNamespaces.OpfMetaElements + propertyName));
            if (!elements.Any()) return null;
            return elements.First().Value;
        }
    }
}
