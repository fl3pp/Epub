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
    public class EpubStructureTests
    {
        [TestMethod]
        public async Task Construct_ByDefault_SetsAlLValuesToProperties()
        {
            var opf = await XmlStructureFile.LoadFromTextAsync("test.opf", "<opf />");
            var container = await XmlStructureFile.LoadFromTextAsync("container.opf", "<container />");
            var zip = new Mock<IZip>().Object;

            var testee = new EpubStructure(opf, container, zip);

            var resultOpf = testee.Opf;
            var resultContainer = testee.Container;
            var resultZip = testee.Zip;
            Assert.AreEqual(opf, resultOpf);
            Assert.AreEqual(container, resultContainer);
            Assert.AreEqual(zip, resultZip);
        }
    }
}
