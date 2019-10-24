using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface IExtractorFactory
    {
        IOpfPathExtractor CreateOpfPathExtractor();
        IManifestExtractor CreateManifestExtractor();
        IMetaExtractor CreateMetaExtractor();
        INavigationExtractor CreateNavigationExtractor(IEnumerable<ManifestItem> manifestItems);
        IFileExtractor CreateFileExtractor(string opfPath);
    }

    internal sealed class ExtractorFactory : IExtractorFactory
    {
        private readonly IOpfPathExtractor opfPathExtractor;
        private readonly IManifestExtractor manifestExtractor;
        private readonly IMetaExtractor metaExtractor;
        private readonly INavigationExtractor epub2NavigationExtractor;

        private readonly IZip zip;

        public ExtractorFactory(IZip zip)
        {
            opfPathExtractor = new OpfPathExtractor();
            manifestExtractor = new ManifestExtractor();
            metaExtractor = new MetaExtractor();
            epub2NavigationExtractor = new NavigationExtractorEpub2();
            this.zip = zip;
        } 

        public IOpfPathExtractor CreateOpfPathExtractor() => opfPathExtractor;
        public IManifestExtractor CreateManifestExtractor() => manifestExtractor;
        public IMetaExtractor CreateMetaExtractor() => metaExtractor;

        public INavigationExtractor CreateNavigationExtractor(IEnumerable<ManifestItem> manifestItems)
        {
            return manifestItems.Any(
                item => OpfXmlNames.NavPropertiesAttributeValue.EqualsIgnoreCaseWithNull(item.Properties)) 
                ? new NavigationExtractorEpub3(zip, manifestItems) 
                : epub2NavigationExtractor;
        }

        public IFileExtractor CreateFileExtractor(string opfPath)
        {
            return new FileExtractor(EpubPathHelper.GetDirectoryName(opfPath), zip);
        }
    }
}
