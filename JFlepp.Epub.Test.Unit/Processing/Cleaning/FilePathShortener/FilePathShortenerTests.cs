using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class FilePathShortenerTests
    {
        [TestMethod]
        public void ShortenFilePaths_FilePathsCanBeShortened_ShortensFilePaths()
        {
            var builder = new BookBuilder();
            builder.Files = new[]
            {
                TestItemFactory.CreateFileFromString("/test/file1.html"),
                TestItemFactory.CreateFileFromString("/test/file2.html"),
                TestItemFactory.CreateFileFromString("/test/content/file3.html"),
            };
            var testee = new FilePathShortener();

            testee.ShortenFilePaths(builder);

            var expected = new[]
            {
                "file1.html",
                "file2.html",
                "content/file3.html", 
            };
            var result = builder.Files.Select(f => f.Path).ToArray();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
