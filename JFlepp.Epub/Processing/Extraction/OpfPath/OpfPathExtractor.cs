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
                .Single(element => element.Name.LocalName.EqualsIgnoreCase(ContainerXmlNames.RootFilesElementName));

            var rootFile = rootFiles
                .Elements()
                .Where(element => element.Name.LocalName.EqualsIgnoreCase(ContainerXmlNames.RootFileElementName))
                .Where(e => ContainerXmlNames.OpfMediaType
                    .EqualsIgnoreCase(e.GetAttributeValueOrNull(ContainerXmlNames.MediaTypeAttributeName)))
                .Single();

            return rootFile.GetAttributeValueOrNull(ContainerXmlNames.FullPathAttributeName)!;
        }
    }
}
