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
    public class SrcTestSplitterTests
    {
        [TestMethod]
        public void GetFileName_WithFileNameOnly_ReturnsFileName()
        {
            var str = "test.html";

            var result = SrcTextSplitter.GetFileName(str);

            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void GetFileName_WithFileNameAndId_ReturnsFileName()
        {
            var str = "test.html#id";

            var result = SrcTextSplitter.GetFileName(str);

            Assert.AreEqual("test.html", result);
        }

        [TestMethod]
        public void GetElementId_WithFileNameOnly_ReturnsNull()
        {
            var str = "test.html";

            var result = SrcTextSplitter.GetElementId(str);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetElementId_WithFileNameAndId_ReturnsElementId()
        {
            var str = "test.html#id";

            var result = SrcTextSplitter.GetElementId(str);

            Assert.AreEqual("id", result);
        }


        [TestMethod]
        public void GetElementId_WithFileNameAndEmptyId_ReturnsNull()
        {
            var str = "test.html#";

            var result = SrcTextSplitter.GetElementId(str);

            Assert.IsNull(result);
        }
    }
}
