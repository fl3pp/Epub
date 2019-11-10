using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class UnnecessaryAttributeRemoverTests
    {
        [TestMethod]
        public void RemoveAttributes_WithUnnecessaryAttributeInChild_RemovesAttribute()
        {
            var root = XElement.Parse("<root><child test=\"one\" /></root>");
            var testee = new UnnecessaryXmlAttributeRemover(new string[0]);

            testee.RemoveAttributes(root);

            var result = root.ToString();
            var expected = "<root>\r\n  <child />\r\n</root>";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RemoveAttributes_WithAttributeToKeepAndToRemove_LeavesAttributeToKeep()
        {
            var root = XElement.Parse("<root keep=\"1\" remove=\"1\" />");
            var testee = new UnnecessaryXmlAttributeRemover(new[] { "keep" });

            testee.RemoveAttributes(root);

            var result = root.ToString();
            var expected = "<root keep=\"1\" />";
            Assert.AreEqual(expected, result);
        }
    }
}
