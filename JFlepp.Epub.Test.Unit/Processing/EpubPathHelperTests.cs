using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit
{
    [TestClass]
    public class EpubPathHelperTests
    {
        [TestMethod]
        public void GetDirectoryName_WithDirectory_ReturnsDirectory()
        {
            const string fullPath = "test/one.html";

            var result = EpubPathHelper.GetDirectoryName(fullPath);

            Assert.AreEqual("test", result);
        }

        [TestMethod]
        public void GetDirectoryName_PathIsRootWithoutPrecedingSeparator_ReturnsEmpty()
        {
            const string fullPath = "one.html";

            var result = EpubPathHelper.GetDirectoryName(fullPath);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetDirectoryName_PathIsRootWithPrecedingSeparator_ReturnsSeparator()
        {
            const string fullPath = "/one.html";

            var result = EpubPathHelper.GetDirectoryName(fullPath);

            Assert.AreEqual("/", result);
        }

        [TestMethod]
        public void GetFileName_WithPath_ReturnsFileName()
        {
            const string fullPath = "test/one.html";

            var result = EpubPathHelper.GetFileName(fullPath);

            Assert.AreEqual("one.html", result);
        }

        [TestMethod]
        public void ExpandPath_WithRelativePath_ExpandsPath()
        {
            const string extension = "test/ch1/../ch2/ch2.html";

            var result = EpubPathHelper.ExpandPath(extension);

            Assert.AreEqual("test/ch2/ch2.html", result);
        }

        [TestMethod]
        public void ExpandPath_WithBaseAndRelativePath_ExpandsPath()
        {
            const string baseDirectory = "test/ch1";
            const string extension = "../ch2/ch2.html";

            var result = EpubPathHelper.ExpandPath(baseDirectory, extension);

            Assert.AreEqual("test/ch2/ch2.html", result);
        }


        [TestMethod]
        public void ExpandPath_WithMoreThanOneLevelUpInRelativePath_ExpandsPath()
        {
            const string baseDirectory = "test/ch1";
            const string extension = "../../ch2/ch2.html";

            var result = EpubPathHelper.ExpandPath(baseDirectory, extension);

            Assert.AreEqual("ch2/ch2.html", result);
        }


        [TestMethod]
        public void ExpandPath_BasePathHasDirectorySeparatorAtEnd_ExpandsPath()
        {
            const string baseDirectory = "test/ch1/";
            const string extension = "../../ch2/ch2.html";

            var result = EpubPathHelper.ExpandPath(baseDirectory, extension);

            Assert.AreEqual("ch2/ch2.html", result);
        }

        [TestMethod]
        public void ExpandPath_WithLevelUpInRelativeAndBasePath_ExpandsPath()
        {
            const string baseDirectory = "test/ch1/ch2/..";
            const string extension = "../../ch2/ch3/../ch2.html";

            var result = EpubPathHelper.ExpandPath(baseDirectory, extension);

            Assert.AreEqual("ch2/ch2.html", result);
        }

        [TestMethod]
        public void Append_WithFolderAndFile_ExpandsPath()
        {
            const string folderPath = "test/proj1/";
            const string fileName = "one.html";

            var result = EpubPathHelper.Append(folderPath, fileName);

            Assert.AreEqual(folderPath + fileName, result);
        }

        [TestMethod]
        public void Append_WithFolderWithoutTrailingDirectorySeparatorAndFile_ExpandsPath()
        {
            const string folderPath = "test/proj1";
            const string fileName = "one.html";

            var result = EpubPathHelper.Append(folderPath, fileName);

            Assert.AreEqual(folderPath + '/' + fileName, result);
        }
    }
}
