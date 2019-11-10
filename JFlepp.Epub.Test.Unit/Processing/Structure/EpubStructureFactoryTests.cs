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
    public class EpubStructureFactoryTests
    {
        [TestMethod]
        public async Task CreateStructureAsync_ZipContainsAllFilesNecessary_ReadsContainerAndOpfFiles()
        {
            var setup = new TestSetup();
            setup.ZipContent.Add("META-INF/container.xml", Encoding.UTF8.GetBytes("<container />"));
            setup.ZipContent.Add("book.opf", Encoding.UTF8.GetBytes("<opf />"));
            setup.OpfExtractorMockResult = "book.opf";
            var zip = setup.CreateZip();
            var testee = setup.CreateTestee();

            var result = await testee.CreateStructureAsync(zip);

            Assert.AreEqual("opf", result.Opf.Doc.Root.Name.LocalName);
            Assert.AreEqual("container", result.Container.Doc.Root.Name.LocalName);
            Assert.AreEqual(zip, result.Zip);
        }
        
        private class TestSetup
        {
            public IDictionary<string, byte[]> ZipContent { get; } = new Dictionary<string, byte[]>();
            public string OpfExtractorMockResult { get; set; }

            public EpubStructureFactory CreateTestee()
            {
                var opfExtractor = new Mock<IOpfPathExtractor>();
                opfExtractor.Setup(x => x.ExtractOpfPath(It.IsAny<XmlStructureFile>())).Returns(OpfExtractorMockResult);
                return new EpubStructureFactory(opfExtractor.Object);
            }

            public IZip CreateZip()
            {
                var mock = new Mock<IZip>();
                mock
                    .Setup(x => x.GetFileStream(It.IsAny<string>()))
                    .Returns<string>(path => new System.IO.MemoryStream(ZipContent[path]));
                return mock.Object;
            }
        }
    }
}
