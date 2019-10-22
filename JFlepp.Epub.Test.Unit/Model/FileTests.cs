using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit
{
    [TestClass]
    public class FileTests
    {
        [TestMethod]
        public void Construct_ByDefault_SetsAllValues()
        {
            var fileName = "test.cs";
            var filePath = @"C:\test.cs";
            var contentType = ContentType.Css;
            byte content = 0_0000001;

            var result = new File(fileName, filePath, contentType, new[] { content });

            Assert.AreEqual(fileName, result.Name);
            Assert.AreEqual(filePath, result.Path);
            Assert.AreEqual(contentType, result.ContentType);
            Assert.AreEqual(content, result.Content.Single());
        }

        [TestMethod]
        public void ToString_WithFileName_ReturnsPath()
        {
            var path = "test/test.cs";
            var testee = new File(string.Empty, path, 0, new byte[0]);

            var result = testee.ToString();

            Assert.AreEqual(path, result);
        }

        [TestMethod]
        public void WithPath_ANewPath_ReturnsNewFileWithAdjustedPath()
        {
            var filePath = @"C:\test.cs";
            var testee = new File("", filePath, 0, new byte[0]);

            const string newPath = @"C:\test\test.cs";
            var result = testee.WithPath(newPath);

            Assert.AreEqual(newPath, result.Path);
        }
        
        [TestMethod]
        public void WithContent_NewContent_ReturnsNewFileWithAdjustedPath()
        {
            var testee = new File("", "", 0, new byte[0]);

            const byte initialContent = 0_1;
            var result = testee.WithContent(new[] { initialContent });

            Assert.AreEqual(initialContent, result.Content.Single());
        }
    }
}
