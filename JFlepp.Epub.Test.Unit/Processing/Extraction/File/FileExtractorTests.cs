﻿using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit.Processing.Extraction.File
{
    [TestClass]
    public class FileExtractorTests
    {
        [TestMethod]
        public async Task ExtractFiles_WithManifestItemsInContentDirectory_ReturnsFiles()
        {
            var zip = new Mock<IZip>();
            var content = Encoding.ASCII.GetBytes("test");
            var manifestItems = new[]
            {
                new ManifestItem(string.Empty, "chapter1.xml", string.Empty, ContentType.Unknown),
            };
            var rootDirectory = "content/";
            zip.Setup(x => x.GetFileContent("content/chapter1.xml")).Returns(content);
            var testee = new FileExtractor(rootDirectory, zip.Object);

            var result = (await testee.ExtractFiles(manifestItems)).Single();

            Assert.AreEqual(content, result.Content);
            Assert.AreEqual("chapter1.xml", result.Name);
            Assert.AreEqual("content/chapter1.xml", result.Path);
        }
    }
}