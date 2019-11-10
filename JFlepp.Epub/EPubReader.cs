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
        private static EpubFactory CreateFactory()
        {
            var manifestExtractor = new ManifestExtractor();
            return new EpubFactory(
                new IOCompressionZipReader(),
                new EpubStructureFactory(
                    new OpfPathExtractor()),
                new ModelBuilderFactory(
                    manifestExtractor,
                    new MetaExtractor(),
                    new FileExtractor(),
                    new UniversalNavigationExtractor(
                        manifestExtractor,
                        new NcxLoader(),
                        new NcxNavigationExtractor(),
                        new XHtmlTocLoader(),
                        new XHtmlNavigationExtractor())),
                new BookCleaner(
                    new FilePathShortener()));
        }

        public async static Task<Book> ReadFromFile(string filePath, EpubReadingOptions options = 0)
        {
            using (var stream = System.IO.File.OpenRead(filePath))
            {
                return await ReadFromStream(stream, options).ConfigureAwait(false);
            }
        }

        public static Task<Book> ReadFromStream(Stream stream, EpubReadingOptions options = 0)
        {
            return CreateFactory().ReadBookAsync(stream, options);
        }
    }
}
