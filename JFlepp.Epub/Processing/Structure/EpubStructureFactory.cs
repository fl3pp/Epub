using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal interface IEpubStructureFactory
    {
        Task<EpubStructure> CreateStructureAsync(IZip zip);
    }

    internal sealed class EpubStructureFactory : IEpubStructureFactory
    {
        private readonly IOpfPathExtractor opfPathExtractor;

        private const string containerXmlPath = "META-INF/container.xml";

        public EpubStructureFactory(IOpfPathExtractor opfPathExtractor)
        {
            this.opfPathExtractor = opfPathExtractor;
        }

        public async Task<EpubStructure> CreateStructureAsync(IZip zip)
        {
            var container = await GetContainerAsync(zip).ConfigureAwait(false);
            var opf = await GetOpfAsync(zip, container).ConfigureAwait(false);
            return new EpubStructure(opf, container, zip);
        }

        private Task<XmlStructureFile> GetContainerAsync(IZip zip)
        {
            return XmlStructureFile.LoadFromZipAsync(containerXmlPath, zip);
        }

        private Task<XmlStructureFile> GetOpfAsync(IZip zip, XmlStructureFile container)
        {
            var opfXmlPath = opfPathExtractor.ExtractOpfPath(container);
            return XmlStructureFile.LoadFromZipAsync(opfXmlPath, zip);
        }
    }
}
