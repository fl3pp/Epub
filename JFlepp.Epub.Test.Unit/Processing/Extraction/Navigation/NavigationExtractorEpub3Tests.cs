using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class NavigationExtractorEpub3Tests
    {
        [TestMethod]
        public async Task ExtractNavigationPoints_TocXHtmlExample1_ExtractsNavigation()
        {
            var fixture = new TestFixture();
            fixture.ZipDic["toc.xhtml"] = Encoding.UTF8.GetBytes(TestResources.TocXHtmlExample1);
            fixture.ManifestItems.Add(new ManifestItem(null, "toc.xhtml", "nav", ContentType.XHtml));
            var structureFiles = new EpubStructure("/", null, string.Empty, null, null);
            var preface1File = TestItemFactory.CreateFileFromString("preface01.html");
            var ch1File = TestItemFactory.CreateFileFromString("ch01.html");
            var indexFile = TestItemFactory.CreateFileFromString("index.html");
            var testee = fixture.CreateTestee();

            var result = await testee
                .ExtractNavigationPoints(structureFiles, new[] { preface1File, ch1File, indexFile }).ConfigureAwait(false); 

            var expected = new[]
            {
                new NavigationPoint("Preface", preface1File, null, 1, new []
                {
                    new NavigationPoint("This book is written for", preface1File, "this_book_is_written_for", 2, new NavigationPoint[0]),
                    new NavigationPoint("About", preface1File, "about", 3, new NavigationPoint[0]),
                }),
                new NavigationPoint("1. Beginning", ch1File, "beginning", 4, new []
                {
                    new NavigationPoint("1.1. Why", ch1File, "why", 5, new NavigationPoint[0]),
                }),
                new NavigationPoint("Index", indexFile, null, 6, new NavigationPoint[0]),
            };
            CustomAsserts.AssertNavigationPointsAreEqual(result, expected);
        } 


        [TestMethod]
        public async Task ExtractNavigationPoints_TocXHtmlExample2_ExtractsNavigation()
        {
            var fixture = new TestFixture();
            fixture.ZipDic["toc.xhtml"] = Encoding.UTF8.GetBytes(TestResources.TocXHtmlExample2);
            fixture.ManifestItems.Add(new ManifestItem(null, "toc.xhtml", "nav", ContentType.XHtml));
            var coverFile = TestItemFactory.CreateFileFromString("cover.xhtml");
            var introFile = TestItemFactory.CreateFileFromString("intro.xhtml");
            var structureFiles = new EpubStructure(string.Empty, null, string.Empty, null, null);
            var testee = fixture.CreateTestee();

            var result = await testee.ExtractNavigationPoints(structureFiles, new[] { coverFile, introFile });

            var expected = new[]
            {
                new NavigationPoint("Cover", coverFile, null, 1, new NavigationPoint[0]),
                new NavigationPoint("INTRODUCTION", introFile, "intro", 2, new []
                { 
                    new NavigationPoint("About", introFile, "lev1", 3, new NavigationPoint[0]),
                }),
            };
            CustomAsserts.AssertNavigationPointsAreEqual(result, expected);
        } 

        private class TestFixture
        {

            public IDictionary<string, byte[]> ZipDic { get; } = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
            public IList<ManifestItem> ManifestItems { get; } = new List<ManifestItem>();

            public XHtmlNavigationExtractor CreateTestee()
            {
                var zip = new DictionaryZipWrapper(ZipDic);
                return new XHtmlNavigationExtractor(zip, ManifestItems);
            }
        }
    }
}
