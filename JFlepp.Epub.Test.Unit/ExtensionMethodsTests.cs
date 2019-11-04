using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void EqualsIgnoreCaseWithNull_WithSameStringSameCase_ReturnsTrue()
        {
            var str1 = "test";
            var str2 = "test";

            var result = str1.EqualsIgnoreCaseWithNull(str2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualsIgnoreCaseWithNull_WithSameStringDifferentCase_ReturnsTrue()
        {
            var str1 = "test";
            var str2 = "Test";

            var result = str1.EqualsIgnoreCaseWithNull(str2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualsIgnoreCaseWithNull_WithDifferentStrings_ReturnsFalse()
        {
            var str1 = "test";
            var str2 = "Test";

            var result = str1.EqualsIgnoreCaseWithNull(str2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualsIgnoreCaseWithNull_WithOnlyFirstStringNull_ReturnsFalse()
        {
            var str1 = "test";
            string str2 = null;

            var result = str1.EqualsIgnoreCaseWithNull(str2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualsIgnoreCaseWithNull_WithOnlySecondStringNull_ReturnsFalse()
        {
            string str1 = null;
            var str2 = "test";

            var result = str1.EqualsIgnoreCaseWithNull(str2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualsIgnoreCaseWithNull_WithBothStringsNull_ReturnsTrue()
        {
            string str1 = null;
            string str2 = null;

            var result = str1.EqualsIgnoreCaseWithNull(str2);

            Assert.IsTrue(result);
        }
    }
}
