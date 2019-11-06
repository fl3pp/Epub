using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class NcxNavigationExtractorTests
    {
        [TestMethod]
        public async Task ExtractNavigationPoints_TocNcxExample1Full_ReturnsNavigationPoints()
        {
            var docString = TestResources.TocNcxExample1;
            var doc = await XmlStructureFile.LoadFromTextAsync(string.Empty, docString);
            var preface1File = TestItemFactory.CreateFileFromString("preface01.html");
            var ch1File = TestItemFactory.CreateFileFromString("ch01.html");
            var indexFile = TestItemFactory.CreateFileFromString("ix01.html");
            var testee = new NcxNavigationExtractor();

            var result = testee.ExtractNavigationPoints(doc, new[] { preface1File, ch1File, indexFile });

            var expected = new[]
            {
                new NavigationPoint("Preface", preface1File, null, 1, new []
                {
                    new NavigationPoint("This book is written for", preface1File, "this_book_is_written_for", 2, new NavigationPoint[0]),
                    new NavigationPoint("About This Book", preface1File, "about_this_book", 3, new NavigationPoint[0]),
                    new NavigationPoint("Conventions", preface1File, "conventions", 4, new[]
                    {
                        new NavigationPoint("text", preface1File, "conventions_text", 5, new NavigationPoint[0]),
                    }),
                }),
                new NavigationPoint("1. Beginning", ch1File, "beginning", 6, new NavigationPoint[0]),
                new NavigationPoint("Index", indexFile, null, 7, new NavigationPoint[0]),
            };
            CustomAsserts.AssertNavigationPointsAreEqual(result, expected);
        }

        [TestMethod]
        public async Task ExtractNavigationPoints_TocNcxExample2WithoutPlayOrderAndElementIds_ReturnsNavigationPoints()
        {
            var docString = TestResources.TocNcxExample2;
            var doc = await XmlStructureFile.LoadFromTextAsync(string.Empty, docString);
            var preface1File = TestItemFactory.CreateFileFromString("preface01.html");
            var testee = new NcxNavigationExtractor();

            var result = testee.ExtractNavigationPoints(doc, new[] { preface1File });

            var expected = new[]
            {
                new NavigationPoint("Preface", preface1File, null, 1, new []
                {
                    new NavigationPoint("Conventions", preface1File, "conventions", 2, new[]
                    {
                        new NavigationPoint("text", preface1File, "conventions_text", 3, new NavigationPoint[0]),
                    }),
                }),
            };
            CustomAsserts.AssertNavigationPointsAreEqual(result, expected);
        }
    }
}
