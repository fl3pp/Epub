using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal sealed class ManifestItem
    {
        public string? Id { get; }
        public string? Href { get; }
        public string? Properties { get; }
        public ContentType ContentType { get; }

        public ManifestItem(string? id, string? href, string? properties, ContentType contentType)
        {
            Id = id;
            Href = href;
            Properties = properties;
            ContentType = contentType;
        }
    }
}
