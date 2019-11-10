using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface INcxLoader
    {
        Task<XmlStructureFile> LoadNcx(
            IEnumerable<ManifestItem> manifestItems, string relativeFilePathsAnchor, IEnumerable<File> files);
    }

    internal sealed class NcxLoader : INcxLoader
    {
        public Task<XmlStructureFile> LoadNcx(
            IEnumerable<ManifestItem> manifestItems, string relativeFilePathsAnchor, IEnumerable<File> files)
        {
            var ncxRelativePath = manifestItems
                .Single(item => item.ContentType == ContentType.Ncx).Href!;
            var ncxPath = EpubPathHelper.ExpandPath(
                EpubPathHelper.GetDirectoryName(relativeFilePathsAnchor), ncxRelativePath);
            return XmlStructureFile.LoadFromBytesAsync(
                ncxPath, files.Single(f => f.Path.EqualsIgnoreCaseWithNull(ncxPath)).Content);
        }
    }
}
