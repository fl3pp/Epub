using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit.Wrapper
{
    [TestClass]
    public class DictionaryZipWrapperTests
    {
        [TestMethod]
        public void GetFileContent_WithFileExists_ReturnsFile()
        {
            var path = "test.html";
            var content = Encoding.ASCII.GetBytes("content");
            var dictionary = new Dictionary<string, byte[]>
            {
                { path, content }
            };
            var testee = new DictionaryZipWrapper(dictionary);

            var result = testee.GetFileContent(path);

            Assert.AreEqual(content, result);
        }

        [TestMethod]
        public void GetFileStream_WithFileExists_ReturnsStream()
        {
            var path = "test.html";
            var content = Encoding.ASCII.GetBytes("content");
            var dictionary = new Dictionary<string, byte[]>
            {
                { path, content }
            };
            var testee = new DictionaryZipWrapper(dictionary);

            using var reader = new BinaryReader(testee.GetFileStream(path));
            var result = reader.ReadBytes(content.Length);

            CollectionAssert.AreEqual(content, result);
        }

        [TestMethod]
        public void AddFile_FileNotExists_AddsFile()
        {
            var path = "test.html";
            var content = Encoding.ASCII.GetBytes("content");
            var dictionary = new Dictionary<string, byte[]>();
            var testee = new DictionaryZipWrapper(dictionary);

            testee.AddFile(path, content);

            var result = dictionary[path];
            Assert.AreEqual(content, result);
        }

        [TestMethod]
        public void DeleteFile_FileExists_DeletesFile()
        {
            var path = "test.html";
            var content = Encoding.ASCII.GetBytes("content");
            var dictionary = new Dictionary<string, byte[]>
            {
                { path, content }
            };
            var testee = new DictionaryZipWrapper(dictionary);

            testee.DeleteFile(path);

            Assert.IsFalse(dictionary.Any());
        }

        [TestMethod]
        public void EnumerateFiles_FileExists_EnumeratesFiles()
        {
            var path = "test.html";
            var content = Encoding.ASCII.GetBytes("content");
            var dictionary = new Dictionary<string, byte[]>
            {
                { path, content }
            };
            var testee = new DictionaryZipWrapper(dictionary);

            var result = testee.EnumerateFiles().Single();

            Assert.AreEqual(result.Item1, path);
            Assert.AreEqual(result.Item2, content);
        }
    }
}
