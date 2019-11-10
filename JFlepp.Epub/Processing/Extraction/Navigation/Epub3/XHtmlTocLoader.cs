using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface IXHtmlTocLoader
    {
        Task<XmlStructureFile> LoadXHtmlToc(
            IEnumerable<ManifestItem> manifestItems, string relativeFilePathsAnchor, IEnumerable<File> files);
    }

    internal sealed class XHtmlTocLoader : IXHtmlTocLoader
    {
        public Task<XmlStructureFile> LoadXHtmlToc(
            IEnumerable<ManifestItem> manifestItems, string relativeFilePathsAnchor, IEnumerable<File> files)
        {
            var xHtmlTocRelativePath = manifestItems.Single(
                i => OpfXmlNames.NavPropertiesAttributeValue.EqualsIgnoreCaseWithNull(i.Properties)).Href!;
            var xHtmlTocPath = EpubPathHelper.ExpandPath(
                EpubPathHelper.GetDirectoryName(relativeFilePathsAnchor), xHtmlTocRelativePath);
            return XmlStructureFile.LoadFromBytesAsync(
                xHtmlTocPath, files.Single(f => f.Path.EqualsIgnoreCaseWithNull(xHtmlTocPath)).Content);
        }
    }
}
