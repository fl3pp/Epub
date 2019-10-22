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
    public class ExtractorFactoryTests
    {
        [TestMethod]
        public void CreateOpfPathExtractor_ByDefault_CreatesExtractor()
        {
            var testee = new ExtractorFactory(null);

            var result = testee.CreateOpfPathExtractor();

            Assert.IsInstanceOfType(result, typeof(OpfPathExtractor));
        }

        [TestMethod]
        public void CreateManifestExtractor_ByDefault_CreatesExtractor()
        {
            var testee = new ExtractorFactory(null);

            var result = testee.CreateManifestExtractor();

            Assert.IsInstanceOfType(result, typeof(ManifestExtractor));
        }

        [TestMethod]
        public void CreateMetaExtractor_ByDefault_CreatesExtractor()
        {
            var testee = new ExtractorFactory(null);

            var result = testee.CreateMetaExtractor();

            Assert.IsInstanceOfType(result, typeof(MetaExtractor));
        }

        [TestMethod]
        public void CreateNavigationExtractor_WithoutManifestItems_CreatesEpub2Extractor()
        {
            var testee = new ExtractorFactory(null);

            var result = testee.CreateNavigationExtractor(new ManifestItem[0]);

            Assert.IsInstanceOfType(result, typeof(NavigationExtractorEpub2));
        }

        [TestMethod]
        public void CreateNavigationExtractor_WithoutNavManifestItem_CreatesEpub3Extractor()
        {
            var testee = new ExtractorFactory(null);

            var result = testee.CreateNavigationExtractor(new[] { new ManifestItem(null, null, "nav", ContentType.Unknown) });

            Assert.IsInstanceOfType(result, typeof(NavigationExtractorEpub3));
        }
    }
}
