using JFlepp.Epub.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub
{
    public static class EPubReader
    {
        public async static Task<Book> ReadFromFile(string filePath, EpubReadingOptions options = 0)
        {
            using (var stream = System.IO.File.OpenRead(filePath))
            {
                return await ReadFromStream(stream, options).ConfigureAwait(false);
            }
        }

        public async static Task<Book> ReadFromStream(Stream stream, EpubReadingOptions options = 0)
        {
            var zip = await new IOCompressionZipReader().GetZipAsync(stream).ConfigureAwait(false);
            var extractorFactory = new ExtractorFactory(zip);
            var cleanerFactory = new CleanerImplementationsFactory();

            var structureFiles = await new StructureFilesFactory(extractorFactory)
                .CreateStructureFilesAsync(zip).ConfigureAwait(false);
            var manifestItems = extractorFactory
                .CreateManifestExtractor()
                .ExtractManifestItems(structureFiles.OpfDoc);
            var meta = extractorFactory
                .CreateMetaExtractor()
                .ExtractMeta(structureFiles.OpfDoc);
            IEnumerable<File> files = await extractorFactory
                .CreateFileExtractor(structureFiles.OpfPath)
                .ExtractFiles(manifestItems).ConfigureAwait(false);
            var navigationPoints = await extractorFactory
                .CreateNavigationExtractor(manifestItems)
                .ExtractNavigationPoints(structureFiles, files).ConfigureAwait(false);

            var builder = new BookBuilder();
            builder.ReadFromMeta(meta);
            builder.NavigationPoints = navigationPoints.ToList();
            builder.Files = files.ToList();

            if (options.HasFlag(EpubReadingOptions.ShortenPaths))
            {
                cleanerFactory.CreateFilePathShortener().ShortenFilePaths(builder);
            }

            return builder.ToBook();
        }
    }
}
