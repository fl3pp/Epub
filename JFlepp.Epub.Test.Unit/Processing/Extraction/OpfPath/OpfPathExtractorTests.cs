using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class OpfPathExtractorTests
    {
        [TestMethod]
        public void ExtractOpfPath_WithContainerXml_ReturnsOpfPath()
        {
            var resource = TestResources.ContainerXmlSimple;
            var xDoc = XDocument.Parse(resource);
            var testee = new OpfPathExtractor();

            var result = testee.ExtractOpfPath(xDoc);

            Assert.AreEqual("OEBPS/content.opf", result);
        }
    }
}
