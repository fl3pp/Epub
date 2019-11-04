using System;
using System.Collections.Generic;
using System.Linq;

namespace JFlepp.Epub
{
    internal static class EpubPathHelper
    {
        private const char directorySeparator = '/';
        private const string directoryUpRelativePath = "..";

        public static string GetDirectoryName(string path)
        {
            if (string.IsNullOrEmpty(path)) return path;

            var parts = path.SplitAndRemoveEmptyEntries(directorySeparator);

            if (parts.Length <= 1)
            {
                return path.First() == directorySeparator ? directorySeparator.ToString() : string.Empty;
            }

            return parts
                .Take(parts.Length - 1)
                .JoinToString(directorySeparator.ToString());
        }

        public static string GetFileName(string path)
        {
            return path.Split(directorySeparator).Last();
        }

        public static string ExpandPath(
            string basePath, string relativePath)
        {
            return ExpandPath(basePath + directorySeparator + relativePath);
        }

        public static string ExpandPath(string path)
        {
            var pathBuilder = new Stack<string>();

            foreach (var element in path.SplitAndRemoveEmptyEntries(directorySeparator))
            {
                if (element.Equals(directoryUpRelativePath, StringComparison.OrdinalIgnoreCase))
                {
                    pathBuilder.Pop();
                    continue;
                }
                pathBuilder.Push(element);
            }

            return pathBuilder.Reverse().JoinToString(directorySeparator.ToString());
        }

        public static string Append(string basePath, string otherPath)
        {
            return
                basePath +
                (basePath.Last() == directorySeparator
                    ? string.Empty
                    : directorySeparator.ToString()) +
                otherPath;
        }
    }
}
