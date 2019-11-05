using System.IO;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal sealed class EpubFactory
    {
        private readonly IZipReader zipReader;
        private readonly IEpubStructureFactory structureFactory;
        private readonly IModelBuilderFactory modelBuilderFactory;
        private readonly IBookCleaner bookCleaner;

        public EpubFactory(
            IZipReader zipReader,
            IEpubStructureFactory structureFactory,
            IModelBuilderFactory modelBuilderFactory,
            IBookCleaner bookCleaner)
        {
            this.zipReader = zipReader;
            this.structureFactory = structureFactory;
            this.modelBuilderFactory = modelBuilderFactory;
            this.bookCleaner = bookCleaner;
        }

        public async Task<Book> ReadBookAsync(Stream stream, EpubReadingOptions options)
        {
            var zip = await zipReader.GetZipAsync(stream).ConfigureAwait(false);
            var structure = await structureFactory.CreateStructureAsync(zip).ConfigureAwait(false);
            var modelBuilder = await modelBuilderFactory.CreateBuilder(structure).ConfigureAwait(false);
            bookCleaner.CleanBook(modelBuilder, options);
            return modelBuilder.ToBook();
        }
    }
}
