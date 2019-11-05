using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface IBookCleaner
    {
        void CleanBook(BookBuilder builder, EpubReadingOptions options);
    }

    internal sealed class BookCleaner : IBookCleaner
    {
        private readonly IFilePathShortener pathShortener;

        public BookCleaner(IFilePathShortener pathShortener)
        {
            this.pathShortener = pathShortener;
        }

        public void CleanBook(BookBuilder builder, EpubReadingOptions options)
        {
            if (options.HasFlag(EpubReadingOptions.ShortenPaths))
            {
                pathShortener.ShortenFilePaths(builder);
            }
        }
    }
}
