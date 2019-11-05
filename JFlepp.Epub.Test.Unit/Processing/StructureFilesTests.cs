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
    public class StructureFilesTests
    {
        [TestMethod]
        public void Construct_ByDefault_SetsAllValues()
        {
            var opfPath = "opf";
            var opfDoc = XDocument.Parse("<Opf />");
            var ncxPath = "ncx";
            var ncxDoc = XDocument.Parse("<Ncx />");
            var containerDoc = XDocument.Parse("<Container />");

            var testee = new EpubStructure(
                opfPath, opfDoc, ncxPath, ncxDoc, containerDoc);

            Assert.AreEqual(opfPath, testee.OpfPath);
            Assert.AreEqual(opfDoc, testee.OpfDoc);
            Assert.AreEqual(ncxPath, testee.NcxPath);
            Assert.AreEqual(ncxDoc, testee.NcxDoc);
            Assert.AreEqual(containerDoc, testee.ContainerDoc);
        }

        [TestMethod]
        public void WithAdjustedPaths_ShortenPathBy1Lambda_ReturnsSameValuesWithShortenedPaths()
        {
            var opfPath = "/opf";
            var ncxPath = "/ncx";
            var doc = XDocument.Parse("<Opf />");
            var testee = new EpubStructure(
                opfPath, doc, ncxPath, doc, doc);

            var result = testee.WithAdjustedPaths(p => p.Substring(1));

            Assert.AreEqual("opf", result.OpfPath);
            Assert.AreEqual("ncx", result.NcxPath);
        }
    }
}
