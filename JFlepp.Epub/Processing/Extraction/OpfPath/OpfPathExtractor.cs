using System.Linq;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal interface IOpfPathExtractor
    {
        string ExtractOpfPath(XmlStructureFile container);
    }

    internal sealed class OpfPathExtractor : IOpfPathExtractor
    {
        public string ExtractOpfPath(XmlStructureFile container)
        {
            var rootElement = container.Doc.Root;

            var rootFiles = rootElement.Elements()
                .Single(element => element.Name.LocalName.EqualsIgnoreCaseWithNull(ContainerXmlNames.RootFilesElementName));

            var rootFile = rootFiles
                .Elements()
                .Where(element => element.Name.LocalName.EqualsIgnoreCaseWithNull(ContainerXmlNames.RootFileElementName))
                .Single(e => ContainerXmlNames.OpfMediaType
                    .EqualsIgnoreCaseWithNull(e.GetAttributeValueOrNull(ContainerXmlNames.MediaTypeAttributeName)))
;

            return rootFile.GetAttributeValueOrNull(ContainerXmlNames.FullPathAttributeName)!;
        }
    }
}
