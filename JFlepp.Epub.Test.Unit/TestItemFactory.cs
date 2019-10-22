using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test
{
    public static class TestItemFactory
    {
        public static File CreateFileFromString(string path, string content = "")
        {
            return new File(System.IO.Path.GetFileName(path), path, ContentType.Unknown, Encoding.UTF8.GetBytes(content));
        }
    }
}
