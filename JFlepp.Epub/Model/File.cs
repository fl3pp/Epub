using System.Collections.Generic;

namespace JFlepp.Epub
{
    public sealed class File
    {
        public string Name { get; }
        public string Path { get; }
        public ContentType ContentType { get; }

// Properties should not return arrays
// Reason: Many APIs require a byte array and cannot handle a byte enumerable, e.g. new MemoryStream
#pragma warning disable CA1819 
        public byte[] Content { get; }
#pragma warning restore CA1819 

        public File(
            string fileName,
            string filePath,
            ContentType contentType,
            byte[] content)
        {
            Name = fileName;
            Path = filePath;
            ContentType = contentType;
            Content = content;
        }

        public override string ToString() => Path;

        internal File WithPath(string fullPath) => new File(Name, fullPath, ContentType, Content);
        internal File WithContent(byte[] content) => new File(Name, Path, ContentType, content);
    }
}
