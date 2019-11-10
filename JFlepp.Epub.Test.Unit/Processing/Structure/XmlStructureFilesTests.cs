using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class XmlStructureFilesTests
    {
        [TestMethod]
        public async Task LoadFromBytesAsync_WithPathAndContent_ReturnsXmlStructureFile()
        {
            var path = "test";
            var docString = "<doc />";
            var content = Encoding.UTF8.GetBytes(docString);

            var result = await XmlStructureFile.LoadFromBytesAsync(path, content);

            Assert.AreEqual(path, result.Path);
            Assert.AreEqual(docString, result.Doc.ToString());
        }

        [TestMethod]
        public async Task LoadFromTextAsync_WithPathAndContent_ReturnsXmlStructureFile()
        {
            var path = "test";
            var docString = "<doc />";

            var result = await XmlStructureFile.LoadFromTextAsync(path, docString);

            Assert.AreEqual(path, result.Path);
            Assert.AreEqual(docString, result.Doc.ToString());
        }

        [TestMethod]
        public async Task LoadFromZipAsync_WithPathAndContent_ReturnsXmlStructureFile()
        {
            var path = "test";
            var docString = "<doc />";
            var zip = new Mock<IZip>();
            zip
                .Setup(x => x.GetFileStream(path))
                .Returns(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(docString)));

            var result = await XmlStructureFile.LoadFromZipAsync(path, zip.Object);

            Assert.AreEqual(path, result.Path);
            Assert.AreEqual(docString, result.Doc.ToString());
        }


        [TestMethod]
        public async Task LoadFromStreamAsync_WithPathAndContent_ReturnsXmlStructureFile()
        {
            var path = "test";
            var docString = "<doc />";

            using (var stream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(docString)))
            {
                var result = await XmlStructureFile.LoadFromStreamAsync(path, stream);

                Assert.AreEqual(path, result.Path);
                Assert.AreEqual(docString, result.Doc.ToString());
            }
        }
    }
}
