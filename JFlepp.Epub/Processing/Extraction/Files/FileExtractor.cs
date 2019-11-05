using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface IFileExtractor
    {
        Task<File[]> ExtractFiles(
            XmlStructureFile opf, IEnumerable<ManifestItem> manifestItems, IZip zip);
    }

    internal sealed class FileExtractor : IFileExtractor
    {
        public Task<File[]> ExtractFiles(
            XmlStructureFile opf, IEnumerable<ManifestItem> manifestItems, IZip zip)
        {
            return Task.WhenAll(manifestItems.Select(
                item => CreateFileFromManifestItem(item, opf.Path, zip)));
        }

        private Task<File> CreateFileFromManifestItem(ManifestItem item, string opfDirectory, IZip zip)
        {
            var path = EpubPathHelper.ExpandPath(opfDirectory, item.Href!);
            var fileName = EpubPathHelper.GetFileName(item.Href!);

            return Task.FromResult(
                new File(fileName, path, item.ContentType, zip.GetFileContent(path)));
        }
    }
}
