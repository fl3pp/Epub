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
    }
}
