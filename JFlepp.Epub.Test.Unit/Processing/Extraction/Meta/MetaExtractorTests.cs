using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class MetaExtractorTests
    {
        [TestMethod]
        public async Task ExtractMeta_ExampleOpf1_ExtractsMetaData()
        {
            var docString = TestResources.OpfMetadataExample1;
            var doc = await XmlStructureFile.LoadFromTextAsync("/test.opf", docString);
            var testee = new MetaExtractor();

            var result = testee.ExtractMeta(doc);

            Assert.AreEqual("urn:isbn:1111111111111", result.Identifier);
            Assert.AreEqual("Some Book's Title", result.Title);
            Assert.AreEqual("Copyright © 1111 ", result.Rights);
            Assert.AreEqual("Some Press", result.Publisher);
            Assert.AreEqual("2000-10-10", result.Date);
            Assert.AreEqual("Some text <strong>Im strong!</strong> and some other text", result.Description);
            Assert.AreEqual("Anon ymous", result.Creator);
            Assert.AreEqual("en", result.Language);
        }


        [TestMethod]
        public async Task ExtractMeta_ExampleOpf2_ExtractsMetaData()
        {
            var docString = TestResources.OpfMetadataExample2;
            var doc = await XmlStructureFile.LoadFromTextAsync("/test.opf", docString);
            var testee = new MetaExtractor();

            var result = testee.ExtractMeta(doc);

            Assert.AreEqual("1111111111111", result.Identifier);
            Assert.AreEqual("Some Book's Title", result.Title);
            Assert.AreEqual(null, result.Rights);
            Assert.AreEqual("Some Press", result.Publisher);
            Assert.AreEqual("2010-10-10", result.Date);
            Assert.AreEqual("Some text and some other text", result.Description);
            Assert.AreEqual("Anon ymous", result.Creator);
            Assert.AreEqual("en", result.Language);
        }

        [TestMethod]
        public async Task ExtractMeta_TitleSetTwice_ReturnsFirst()
        {
            var docString = $@"<package xmlns=""{XmlNamespaces.Opf}"" xmlns:dc=""{XmlNamespaces.OpfMetaElements}""><metadata><dc:title>test1</dc:title><dc:title>test2</dc:title></metadata></package>";
            var doc = await XmlStructureFile.LoadFromTextAsync("/test.opf", docString);
            var testee = new MetaExtractor();

            var result = testee.ExtractMeta(doc).Title;

            Assert.AreEqual("test1", result);
        }
    }
}
