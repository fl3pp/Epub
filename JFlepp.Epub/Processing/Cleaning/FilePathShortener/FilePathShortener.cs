using System.Collections.Generic;
using System.Linq;

namespace JFlepp.Epub.Processing
{

    internal interface IFilePathShortener
    {
        void ShortenFilePaths(BookBuilder builder);
    }

    internal sealed class FilePathShortener : IFilePathShortener
    {
        public void ShortenFilePaths(BookBuilder builder)
        {
            var longestCommonPath = FindLongestCommonPath(builder.Files.Select(f => f.Path).ToArray());

            var replacements = builder.Files
                .ToDictionary(f => f, f =>  f.WithPath(f.Path.Substring(longestCommonPath.Length)));

            builder.AdjustNavigationPoints(point => point.WithFile(replacements[point.File]));
            builder.Files = replacements.Values;
        }

        private static string FindLongestCommonPath(IEnumerable<string> paths)
        {
            return new string(
                paths.First().Substring(0, paths.Min(s => s.Length))
                .TakeWhile((c, i) => paths.All(s => s[i] == c)).ToArray());
        } 
    }
}
