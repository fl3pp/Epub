using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal interface IManifestExtractor
    {
        IEnumerable<ManifestItem> ExtractManifestItems(XmlStructureFile opf);
    }

    internal sealed class ManifestExtractor : IManifestExtractor
    {
        public IEnumerable<ManifestItem> ExtractManifestItems(XmlStructureFile opf)
        {
            var rootItem = opf.Doc.Root;
            var manifestItem = rootItem
                .Elements().Single(e => e.Name.Equals(XmlNamespaces.Opf + OpfXmlNames.ManifestElementName));
            return manifestItem
                .Elements().Where(e => e.Name.Equals(XmlNamespaces.Opf + OpfXmlNames.ItemElementName))
                .Select(CreateFromXElement);
        }

        private static ManifestItem CreateFromXElement(XElement e)
        {
            return new ManifestItem(
                e.GetAttributeValueOrNull(OpfXmlNames.IdAttributeName),
                e.GetAttributeValueOrNull(OpfXmlNames.HrefAttributeName),
                e.GetAttributeValueOrNull(OpfXmlNames.PropertiesAttributeName),
                ContentTypeParser.GetContentTypeFromMimeType(e.GetAttributeValueOrNull(OpfXmlNames.MediaTypeAttributeName)));
        }
    }
}
