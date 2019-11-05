using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface IModelBuilderFactory
    {
        Task<BookBuilder> CreateBuilder(EpubStructure structure);
    }

    internal sealed class ModelBuilderFactory : IModelBuilderFactory
    {
        private readonly IManifestExtractor manifestExtractor;
        private readonly IMetaExtractor metaExtractor;
        private readonly IFileExtractor fileExtractor;
        private readonly INavigationExtractor navigationExtractor;

        public ModelBuilderFactory(
            IManifestExtractor manifestExtractor,
            IMetaExtractor metaExtractor,
            IFileExtractor fileExtractor,
            INavigationExtractor navigationExtractor)
        {
            this.manifestExtractor = manifestExtractor;
            this.metaExtractor = metaExtractor;
            this.fileExtractor = fileExtractor;
            this.navigationExtractor = navigationExtractor;
        }

        public async Task<BookBuilder> CreateBuilder(EpubStructure structure)
        {
            var builder = new BookBuilder();
            var manifestItems = manifestExtractor.ExtractManifestItems(structure.Opf);
            var meta = metaExtractor.ExtractMeta(structure.Opf);
            var files = await fileExtractor.ExtractFiles(structure.Opf, manifestItems, structure.Zip).ConfigureAwait(false);
            var navigationPoints = await navigationExtractor.ExtractNavigationPoints(structure, files).ConfigureAwait(false);

            builder.ReadFromMeta(meta);
            builder.NavigationPoints = navigationPoints.ToList();
            builder.Files = files.ToList();
            return builder;
        }
    }
}
