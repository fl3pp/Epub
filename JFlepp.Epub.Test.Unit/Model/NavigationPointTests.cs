using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit
{
    [TestClass]
    public class NavigationPointTests
    {
        [TestMethod]
        public void Construct_ByDefault_SetsAllValues()
        {
            var title = "name";
            var file = TestItemFactory.CreateFileFromString("test.xhtml");
            string elementId = null;
            var order = 0;
            var children = new NavigationPoint[0];

            var result = new NavigationPoint(
                title, file, elementId, order, children);

            Assert.AreEqual(title, result.Title);
            Assert.AreEqual(file, result.File);
            Assert.AreEqual(elementId, result.ElementId);
            Assert.AreEqual(children, result.Children);
            Assert.AreEqual(order, result.Order);
        }

        [TestMethod]
        public void ToString_WithIdInFile_ReturnsStringRepresentationWithId()
        {
            var name = "Chapter 1";
            var file = TestItemFactory.CreateFileFromString("test.txt");
            var elementId = "ch1";
            var testee = new NavigationPoint(name, file, elementId, 0, new NavigationPoint[0]);

            var result = testee.ToString();

            Assert.AreEqual("Chapter 1 - test.txt#ch1", result);
        }

        [TestMethod]
        public void ToString_WithoutIdInFile_ReturnsStringRepresentationWithoutId()
        {
            var name = "Chapter 1";
            var file = TestItemFactory.CreateFileFromString("test.txt");
            string elementId = null;
            var testee = new NavigationPoint(name, file, elementId, 0, new NavigationPoint[0]);

            var result = testee.ToString();

            Assert.AreEqual("Chapter 1 - test.txt", result);
        }
    }
}
