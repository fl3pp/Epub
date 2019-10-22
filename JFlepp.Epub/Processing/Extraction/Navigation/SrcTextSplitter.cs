using System.Linq;

namespace JFlepp.Epub.Processing
{
    internal static class SrcTextSplitter
    {
        private const char separator = '#';

        public static string GetFileName(string src)
        {
            return src.SplitAndRemoveEmptyEntries(separator)[0];
        }

        public static string? GetElementId(string src)
        {
            var splittedSkip1 = src.SplitAndRemoveEmptyEntries(separator).Skip(1);
            if (!splittedSkip1.Any()) return null;
            return splittedSkip1.Single();
        }
    }
}
