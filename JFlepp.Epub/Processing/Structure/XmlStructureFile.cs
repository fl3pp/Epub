using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal sealed class XmlStructureFile
    {
        public XDocument Doc { get; }
        public string Path { get; }

        public XmlStructureFile(XDocument doc, string path)
        {
            Doc = doc;
            Path = path;
        }

        public static Task<XmlStructureFile> LoadFromBytesAsync(string path, byte[] bytes)
        {
            using var stream = new MemoryStream(bytes);
            return LoadFromStreamAsync(path, stream);
        }

        public static Task<XmlStructureFile> LoadFromTextAsync(string path, string content)
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            return LoadFromStreamAsync(path, stream);
        }

        public static Task<XmlStructureFile> LoadFromZipAsync(string path, IZip zip)
        {
            using var stream = zip.GetFileStream(path);
            return LoadFromStreamAsync(path, stream);
        }

        public static async Task<XmlStructureFile> LoadFromStreamAsync(string path, Stream stream)
        {
            var doc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None).ConfigureAwait(false);
            return new XmlStructureFile(doc, path);
        }
    }
}
