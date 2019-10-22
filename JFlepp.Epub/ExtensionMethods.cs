using System;
using System.Collections.Generic;
using System.Linq;

namespace JFlepp.Epub
{
    internal static class ExtensionMethods
    {
        public static bool EqualsIgnoreCase(this string str1, string? str2)
        {
            if (str2 == null) return false;
            return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
        }

        public static string[] SplitAndRemoveEmptyEntries(this string str, char separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string JoinToString<T>(this IEnumerable<T> objects, string separator)
        {
            return string.Join(separator, objects);
        }


        public static string GetSHA256Hash(this byte[] bytes)
        {
            using (var crypt = new System.Security.Cryptography.SHA256Managed())
            {
                return crypt
                    .ComputeHash(bytes)
                    .Select(b => b.ToString("x2"))
                    .JoinToString(string.Empty);
            }
        }
    }
}
