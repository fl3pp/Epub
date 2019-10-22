using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface IFileExtractor
    {
        Task<File[]> ExtractFiles(IEnumerable<ManifestItem> manifestItems);
    }

    internal sealed class FileExtractor : IFileExtractor
    {
        private readonly string opfDirectory;
        private readonly IZip zip;

        public FileExtractor(string opfDirectory, IZip zip)
        {
            this.opfDirectory = opfDirectory;
            this.zip = zip;
        }

        public Task<File[]> ExtractFiles(IEnumerable<ManifestItem> manifestItems)
        {
            return Task.WhenAll(manifestItems.Select(CreateFileFromManifestItem));
        }

        private Task<File> CreateFileFromManifestItem(ManifestItem item)
        {
            var path = EpubPathHelper.ExpandPath(opfDirectory, item.Href!);
            var fileName = EpubPathHelper.GetFileName(item.Href!);

            return Task.FromResult(
                new File(fileName, path, item.ContentType, zip.GetFileContent(path)));
        }
    }
}
