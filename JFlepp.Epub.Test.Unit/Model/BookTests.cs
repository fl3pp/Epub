using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit
{ 
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        public void Construct_ByDefault_SetsAllValues()
        {
            var title = "title";
            var author = "author";
            var description = "description";
            var datePublished = "2019-01-01";
            var publisher = "press";
            var language = "en";
            var files = new [] { new File("", "", 0, new byte[0]) };
            var navigationPoints = new NavigationPoint[0];

            var result = new Book(
                title, author, description, datePublished, publisher, language, files, navigationPoints);

            Assert.AreEqual(title, result.Title);
            Assert.AreEqual(author, result.Author);
            Assert.AreEqual(description, result.Description);
            Assert.AreEqual(datePublished, result.DatePublished);
            Assert.AreEqual(publisher, result.Publisher);
            Assert.AreEqual(language, result.Language);
            Assert.AreEqual(files, result.Files);
            Assert.AreEqual(navigationPoints, result.NavigationPoints);
        } 
    }
}
