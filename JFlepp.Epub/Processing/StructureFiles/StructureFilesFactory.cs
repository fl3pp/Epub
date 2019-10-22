using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal sealed class StructureFilesFactory
    {
        private readonly IExtractorFactory extractorFactory;

        private const string containerXmlPath = "META-INF/container.xml";

        public StructureFilesFactory(IExtractorFactory extractorFactory)
        {
            this.extractorFactory = extractorFactory;
        }

        public async Task<StructureFiles> CreateStructureFilesAsync(IZip zip)
        {
            var containerXml = await zip.ParseXmlFromPathAsync(containerXmlPath).ConfigureAwait(false);

            var opfXmlPath = extractorFactory.CreateOpfPathExtractor().ExtractOpfPath(containerXml);
            var opfXml = await zip.ParseXmlFromPathAsync(opfXmlPath).ConfigureAwait(false);

            var ncxXmlRelativePath = extractorFactory.CreateManifestExtractor().ExtractManifestItems(opfXml)
                .Single(item => item.ContentType == ContentType.Ncx).Href!;
            var ncxXmlPath = EpubPathHelper.ExpandPath(
                EpubPathHelper.GetDirectoryName(opfXmlPath), ncxXmlRelativePath);
            var ncxXml = await zip.ParseXmlFromPathAsync(ncxXmlPath).ConfigureAwait(false); 

            return new StructureFiles(opfXmlPath, opfXml, ncxXmlPath, ncxXml, containerXml);
        }
    }
}
