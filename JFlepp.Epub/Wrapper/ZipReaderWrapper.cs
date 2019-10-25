using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub
{
    public interface IZipReader
    {
        Task<IZip> GetZipAsync(Stream file);
    }

    internal sealed class IOCompressionZipReader : IZipReader
    {
        public Task<IZip> GetZipAsync(byte[] content)
        {
            using var stream = new MemoryStream(content);
            return GetZipAsync(stream);
        }

        public async Task<IZip> GetZipAsync(Stream file)
        {
            using var zip = new ZipArchive(file);

            var kvpTasks = zip.Entries.Select(async e => new
            {
                Name = GetFileNameFromEntry(e),
                Content = await GetContentFromEntry(e).ConfigureAwait(false),
            });
 
            // Task.WhenAll does not work because of thread safety problems in ZipArchive
            // see https://github.com/dotnet/corefx/issues/33596
            foreach (var task in kvpTasks)
            {
                await task.ConfigureAwait(false);
            }

            var entries = kvpTasks.Select(t => t.Result);

            return new DictionaryZipWrapper(entries.ToDictionary(
                e => e.Name, e => e.Content, StringComparer.OrdinalIgnoreCase));
        }

        private static string GetFileNameFromEntry(ZipArchiveEntry entry)
        {
            return entry.FullName;
        }

        private async static Task<byte[]> GetContentFromEntry(ZipArchiveEntry entry)
        {
            using var fileStream = entry.Open();
            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream).ConfigureAwait(false);
            return memoryStream.ToArray();
        }
    }
}
