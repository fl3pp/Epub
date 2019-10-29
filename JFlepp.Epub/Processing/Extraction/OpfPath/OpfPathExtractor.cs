using System.Linq;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal interface IOpfPathExtractor
    {
        string ExtractOpfPath(XDocument containerXml);
    }

    internal sealed class OpfPathExtractor : IOpfPathExtractor
    {
        public string ExtractOpfPath(XDocument containerXml)
        {
            var rootElement = containerXml.Root;

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
