using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class OpfPathExtractorTests
    {
        [TestMethod]
        public async Task ExtractOpfPath_WithContainerXml_ReturnsOpfPath()
        {
            var resource = TestResources.ContainerXmlSimple;
            var testee = new OpfPathExtractor();

            var result = testee.ExtractOpfPath(await XmlStructureFile.LoadFromTextAsync("test.container", resource));

            Assert.AreEqual("OEBPS/content.opf", result);
        }
    }
}
