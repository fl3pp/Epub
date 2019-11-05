using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface INavigationExtractor
    {
        Task<IEnumerable<NavigationPoint>> ExtractNavigationPoints(
            EpubStructure context, IEnumerable<File> files);
    }

    internal interface IFileBasedNavigationExtractor
    {
        IEnumerable<NavigationPoint> ExtractNavigationPoints(
            XmlStructureFile navigationFile, IEnumerable<File> files);
    }

    internal sealed class UniversalNavigationExtractor : INavigationExtractor
    {
        private readonly IManifestExtractor manifestExtractor;

        private readonly INcxLoader ncxLoader;
        private readonly IFileBasedNavigationExtractor ncxNavigationExtractor;

        private readonly IXHtmlTocLoader xHtmlTocLoader;
        private readonly IFileBasedNavigationExtractor xHtmlNavigationExtractor;

        public UniversalNavigationExtractor(
            IManifestExtractor manifestExtractor,
            INcxLoader ncxLoader,
            IFileBasedNavigationExtractor ncxNavigationExtractor,
            IXHtmlTocLoader xHtmlTocLoader,
            IFileBasedNavigationExtractor xHtmlNavigationExtractor)
        {
            this.manifestExtractor = manifestExtractor;
            this.ncxLoader = ncxLoader;
            this.ncxNavigationExtractor = ncxNavigationExtractor;
            this.xHtmlTocLoader = xHtmlTocLoader;
            this.xHtmlNavigationExtractor = xHtmlNavigationExtractor;
        }

        public Task<IEnumerable<NavigationPoint>> ExtractNavigationPoints(
            EpubStructure structure, IEnumerable<File> files)
        {
            var manifestItems = manifestExtractor.ExtractManifestItems(structure.Opf);
            return manifestItems.Any(
                item => OpfXmlNames.NavPropertiesAttributeValue.EqualsIgnoreCaseWithNull(item.Properties)) 
                ? ExtractEpub2NavigationPoints(structure, manifestItems, files)
                : ExtractEpub3NavigationPoints(structure, manifestItems, files);
        }

        public async Task<IEnumerable<NavigationPoint>> ExtractEpub2NavigationPoints(
            EpubStructure structure, IEnumerable<ManifestItem> manifestItems, IEnumerable<File> files)
        {
            return ncxNavigationExtractor.ExtractNavigationPoints(
                await ncxLoader.LoadNcx(manifestItems, structure.Opf.Path, files).ConfigureAwait(false),
                files);
        }

        public async Task<IEnumerable<NavigationPoint>> ExtractEpub3NavigationPoints(
            EpubStructure structure, IEnumerable<ManifestItem> manifestItems, IEnumerable<File> files)
        {
            return xHtmlNavigationExtractor.ExtractNavigationPoints(
                await xHtmlTocLoader.LoadXHtmlToc(manifestItems, structure.Opf.Path, files).ConfigureAwait(false),
                files);
        }
    }
}
