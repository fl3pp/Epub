using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class UnnecessaryNodeRemoverTests
    {
        [TestMethod]
        public void RemoveNodes_WithUnnecessaryNodes_RemovesNodesButKeepsMixedContent()
        {
            var root = XElement.Parse("<root><delete>I am a <bold>bold</bold> string. </delete>Im also here</root>");
            var testee = new UnnecessaryXmlNodeRemover("root", new[] { "bold" });

            var result = testee.RemoveNodes(root);

            var resultString = root.ToString();
            var expected = "<root>I am a <bold>bold</bold> string. Im also here</root>";
            Assert.AreEqual(expected, resultString);
        }

        [TestMethod]
        public void RemoveNodes_WithOtherRootNode_ReturnsRequestedRoot()
        {
            var root = XElement.Parse("<test><head /><root>test</root></test>");
            var testee = new UnnecessaryXmlNodeRemover("root", new string[0]);

            var result = testee.RemoveNodes(root);

            var resultString = result.ToString();
            var expected = "<root>test</root>";
            Assert.AreEqual(expected, resultString);
        }

        [TestMethod]
        public void RemoveNodes_WithMultipleNodesToBeRemoved_RemovesUnnecessaryNodes()
        {
            var root = XElement.Parse("<root>I <delete>am</delete> a <bold><delete>bold</delete></bold> string.</root>");
            var testee = new UnnecessaryXmlNodeRemover("root", new[] { "bold" });

            var result = testee.RemoveNodes(root);

            var resultString = result.ToString();
            var expected = "<root>I am a <bold>bold</bold> string.</root>";
            Assert.AreEqual(expected, resultString);
        }

        [TestMethod]
        public void RemoveNodes_WithSkip_LeavesNode()
        {
            var root = XElement.Parse("<root><leaveme /><removeme /></root>");
            var testee = new UnnecessaryXmlNodeRemover("root", new string[0], 
                (e) => e.Name.LocalName == "leaveme");

            var result = testee.RemoveNodes(root);

            var childString = result.Elements().Single().ToString();
            var expected = "<leaveme />";
            Assert.AreEqual(expected, childString);
        }
    }
}
