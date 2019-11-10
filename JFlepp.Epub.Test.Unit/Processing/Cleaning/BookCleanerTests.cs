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
    public class BookCleanerTests
    {
        [TestMethod]
        public void CleanBook_ReadingOptionsIncludePathShortening_CallsFilePathShortener()
        {
            var setup = new TestSetup();
            var testee = setup.CreateTestee();

            testee.CleanBook(setup.BookBuilder, EpubReadingOptions.ShortenPaths);

            setup.FilePathShortener.Verify(
                s => s.ShortenFilePaths(setup.BookBuilder), Times.Once);
        }


        [TestMethod]
        public void CleanBook_ReadingOptionsDontIncludePathShortening_NotCallsFilePathShortener()
        {
            var setup = new TestSetup();
            var testee = setup.CreateTestee();

            testee.CleanBook(setup.BookBuilder, EpubReadingOptions.None);

            setup.FilePathShortener.Verify(
                s => s.ShortenFilePaths(It.IsAny<BookBuilder>()), Times.Never);
        }

        private class TestSetup
        {
            public BookBuilder BookBuilder { get; } = new BookBuilder();
            public Mock<IFilePathShortener> FilePathShortener { get; } = new Mock<IFilePathShortener>();

            public BookCleaner CreateTestee()
            {
                return new BookCleaner(FilePathShortener.Object);
            }
        }
    }
}
