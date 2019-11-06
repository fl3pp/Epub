using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFlepp.Epub.Processing;
using System.IO;
using System.Xml.Linq;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class XmlExtensionMethodsTests
    {
        [TestMethod]
        public void GetAttributeValueOrNull_WithAttributeSet_ReturnsAttributeValue()
        {
            var element = XElement.Parse("<e test=\"value\" />");

            var result = element.GetAttributeValueOrNull("test");

            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void GetAttributeValueOrNull_WithAttributeNotSet_ReturnsNull()
        {
            var element = XElement.Parse("<e />");

            var result = element.GetAttributeValueOrNull("test");

            Assert.IsNull(result);
        }
    }
}
