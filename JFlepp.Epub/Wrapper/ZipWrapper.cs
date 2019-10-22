using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub
{
    public interface IZip
    {
        byte[] GetFileContent(string path);
        Stream GetFileStream(string path);
        void AddFile(string path, byte[] content);
        void DeleteFile(string path);
        IEnumerable<(string, byte[])> EnumerateFiles();
    }

    internal sealed class DictionaryZipWrapper : IZip
    {
        private readonly IDictionary<string, byte[]> files;

        public DictionaryZipWrapper(IDictionary<string, byte[]> files)
        {
            this.files = files;
        }

        public byte[] GetFileContent(string path) => files[path];

        public Stream GetFileStream(string path) => new MemoryStream(files[path]);

        public void AddFile(string path, byte[] content) => files.Add(path, content);

        public void DeleteFile(string path) => files.Remove(path);

        public IEnumerable<(string, byte[])> EnumerateFiles() => files.Select(f => (f.Key, f.Value));
    }
}
