using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal static class XmlExtensionMethods
    {
        public static Task<XDocument> ParseXmlFromPathAsync(this IZip zip, string path)
        {
            using (var containerXmlStream = zip.GetFileStream(path))
            {
                return XDocument.LoadAsync(
                        containerXmlStream, LoadOptions.None, CancellationToken.None);
            }
        }

        public static string? GetAttributeValueOrNull(this XElement element, XName name)
        {
            var attributes = element.Attributes().Where(a => a.Name.Equals(name));
            if (!attributes.Any() || attributes.Skip(1).Any()) return null;
            return attributes.Single().Value;
        }
    }
}
