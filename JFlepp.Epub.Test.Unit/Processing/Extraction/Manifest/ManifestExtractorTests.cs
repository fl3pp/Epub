using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Xml.Linq;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class ManifestExtractorTests
    {
        [TestMethod]
        public void ExtractManifestItems_WithExampleOpf_ReturnsManifestItems()
        {
            var opf = TestResources.OpfManifestExample;
            var xDoc = XDocument.Parse(opf);
            var testee = new ManifestExtractor();

            var result = testee.ExtractManifestItems(xDoc).ToArray();

            var expected = new[]
            {
                new ManifestItem("ncxtoc", "toc.ncx", null, ContentType.Ncx),
                new ManifestItem("css", "style.css", null, ContentType.Css),
                new ManifestItem("cover", "cover.html", null, ContentType.XHtml),
                new ManifestItem("epub.embedded.font.1", "CustomFont.otf", null, ContentType.FontOpenType ),
                new ManifestItem("id2558953", "index.html", null, ContentType.XHtml),
                new ManifestItem("cover-image", "cover.jpg", null, ContentType.ImageJpeg),
                new ManifestItem("id01", "ch1.html", null, ContentType.XHtml),
            };
            for (int index = 0; index <= 6; index++) Assert.IsTrue(AreManifestItemsEqual(expected[index], result[index]));
        }

        [TestMethod]
        public void ExtractManifestItems_WithExtendedOpf_ReturnsManifestItems()
        {
            var opf = TestResources.OpfManifestExtended;
            var xDoc = XDocument.Parse(opf);
            var testee = new ManifestExtractor();

            var result = testee.ExtractManifestItems(xDoc).ToArray();

            var expected = new[]
            {
                new ManifestItem("identifier", "reference", "properties", ContentType.Ncx),
                new ManifestItem("identifier", "reference", null, ContentType.Unknown),
                new ManifestItem("identifier", null, "properties", ContentType.Unknown),
                new ManifestItem(null, "reference", "properties", ContentType.XHtml),
            };
            for (int index = 0; index <= 3; index++) Assert.IsTrue(AreManifestItemsEqual(expected[index], result[index]));
        }

        private bool AreManifestItemsEqual(ManifestItem first, ManifestItem second)
        {
            return first.Id == second.Id
                && first.Href == second.Href
                && first.Properties == second.Properties
                && first.ContentType == second.ContentType;
        }
    }
}
