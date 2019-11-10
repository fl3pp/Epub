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
    public class BookBuilderTest
    {
        [TestMethod]
        public void GetProperties_WithoutModifications_SetsUnknownValues()
        {
            var testee = new BookBuilder();

            var resultTitle = testee.Title;
            var resultPublisher = testee.Publisher;

            Assert.AreEqual("Unknown Title", resultTitle);
            Assert.AreEqual("Unknown Publisher", resultPublisher);
        }

        [TestMethod]
        public void AdjustNavigationPoints_FuncThatAddsPrefixToNavigationPoints_AdjustsNavigationPointsRecursive()
        {
            var testee = new BookBuilder();
            testee.NavigationPoints = new[]
            {
                new NavigationPoint("One", null, null, 0, new []
                { 
                    new NavigationPoint("One Two", null, null, 0, new NavigationPoint[0]),
                }),
                new NavigationPoint("Two", null, null, 0, new NavigationPoint[0]),
            };

            testee.AdjustNavigationPoints(point => new NavigationPoint(
                "Chapter: " + point.Title, point.File, point.ElementId, point.Order, point.Children));


            var resultChapterOne = testee.NavigationPoints.First();
            var resultChapterOneTwo = testee.NavigationPoints.First().Children.First();
            var resultChapterTwo = testee.NavigationPoints.Skip(1).First();
            Assert.AreEqual("Chapter: One", resultChapterOne.Title);
            Assert.AreEqual("Chapter: One Two", resultChapterOneTwo.Title);
            Assert.AreEqual("Chapter: Two", resultChapterTwo.Title);
        }

        [TestMethod]
        public void ReadFromMeta_SomeMetaValuesAreNull_ReadsNonNullValues()
        {
            const string customTitle = "Custom Title";
            var meta = new MetaData(
                null, customTitle, null, null, null, null, null, null);
            var testee = new BookBuilder();

            testee.ReadFromMeta(meta);

            var resultTitle = testee.Title;
            var resultPublisher = testee.Publisher;
            Assert.AreEqual("Custom Title", resultTitle);
            Assert.AreEqual("Unknown Publisher", resultPublisher);
        }

        [TestMethod]
        public void ToBook_AllValuesSet_CreatesBook()
        {
            var testee = new BookBuilder 
            { 
                Title = nameof(Book.Title),
                Author = nameof(Book.Author),
                Description = nameof(Book.Description),
                DatePublished = nameof(Book.DatePublished),
                Publisher = nameof(Book.Publisher),
                Language = nameof(Book.Language),
                Files = Array.Empty<File>(),
                NavigationPoints = Array.Empty<NavigationPoint>(),
            };

            var result = testee.ToBook();

            Assert.AreEqual(testee.Title, result.Title);
            Assert.AreEqual(testee.Author, result.Author);
            Assert.AreEqual(testee.Description, result.Description);
            Assert.AreEqual(testee.DatePublished, result.DatePublished);
            Assert.AreEqual(testee.Publisher, result.Publisher);
            Assert.AreEqual(testee.Language, result.Language);
            Assert.AreEqual(testee.Files, result.Files);
            Assert.AreEqual(testee.NavigationPoints, result.NavigationPoints);
        }
    }
}
