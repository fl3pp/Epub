using JFlepp.Epub.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit.Model
{
    [TestClass] 
    public class XmlBookExtensionsTests
    {
        [TestMethod]
        public void BookToXml_WithNull_ThrowsArgumentNullException()
        {
            var result = Assert.ThrowsException<ArgumentNullException>(() => 
            {
                BookToXmlExtensions.ToXml((Book)null);
            });

            Assert.AreEqual("Value cannot be null. (Parameter 'book')", result.Message);
        }

        [TestMethod]
        public void NavigationPointsToXml_WithNull_ThrowsArgumentNullException()
        {
            var result = Assert.ThrowsException<ArgumentNullException>(() =>
            {
                BookToXmlExtensions.ToXml((IEnumerable<NavigationPoint>)null);
            });

            Assert.AreEqual("Value cannot be null. (Parameter 'navigationPoints')", result.Message);
        }

        [TestMethod]
        public void NavigationPointToXml_WithNull_ThrowsArgumentNullException()
        {
            var result = Assert.ThrowsException<ArgumentNullException>(() => 
            {
                BookToXmlExtensions.ToXml((NavigationPoint)null);
            });

            Assert.AreEqual("Value cannot be null. (Parameter 'navigationPoint')", result.Message);
        }

        [TestMethod]
        public void FilesToXml_WithNull_ThrowsArgumentNullException()
        {
            var result = Assert.ThrowsException<ArgumentNullException>(() => 
            {
                BookToXmlExtensions.ToXml((IEnumerable<File>)null);
            }, "Value cannot be null. (Parameter 'files').");

            Assert.AreEqual("Value cannot be null. (Parameter 'files')", result.Message);
        }

        [TestMethod]
        public void FileToXml_WithNull_ThrowsArgumentNullException()
        {
            var result = Assert.ThrowsException<ArgumentNullException>(() => 
            {
                BookToXmlExtensions.ToXml((File)null);
            }, "Value cannot be null. (Parameter 'book')");

            Assert.AreEqual("Value cannot be null. (Parameter 'file')", result.Message);
        }
        
        [TestMethod] 
        public void BookToXml_WithBook_ReturnsXmlRepresentationOfBook()
        {
            const string exampleString = "test";
            var book = new Book(
                exampleString, exampleString, exampleString, exampleString, exampleString, exampleString,
                Array.Empty<File>(), Array.Empty<NavigationPoint>());

            var result = book.ToXml().ToString();

            var expected = @"<Book xmlns=""http://JFlepp.Epub/Book/meta/xml/"">
  <Title>test</Title>
  <Author>test</Author>
  <Description>test</Description>
  <DatePublished>test</DatePublished>
  <Publisher>test</Publisher>
  <Language>test</Language>
  <Files />
  <NavigationPoints />
</Book>";
            Assert.AreEqual(expected, result);
        }

        [TestMethod] 
        public void BookMetaToXml_WithBook_ReturnsMeta()
        {
            const string exampleString = "test";
            var book = new Book(
                exampleString, exampleString, exampleString, exampleString, exampleString, exampleString,
                Array.Empty<File>(), Array.Empty<NavigationPoint>());

            var result = book.MetaToXml().ToString();

            var expected = @"<Book xmlns=""http://JFlepp.Epub/Book/meta/xml/"">
  <Title>test</Title>
  <Author>test</Author>
  <Description>test</Description>
  <DatePublished>test</DatePublished>
  <Publisher>test</Publisher>
  <Language>test</Language>
</Book>";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void NavigationPointsToXml_WithNavigationPoint_ReturnsNavigationPoint()
        {
            var navigationPoints = new[]
            {
                new NavigationPoint("1.", File.Empty, string.Empty, 1, new []
                { 
                    new NavigationPoint("1.1.", File.Empty, string.Empty, 2, Array.Empty<NavigationPoint>()),
                }),
                new NavigationPoint("2.", File.Empty, string.Empty, 3, Array.Empty<NavigationPoint>()),
            };

            var result = navigationPoints.ToXml().ToString();

            var expected = @"<NavigationPoints xmlns=""http://JFlepp.Epub/Book/meta/xml/"">
  <NavigationPoint>
    <Title>1.</Title>
    <File></File>
    <ElementId></ElementId>
    <Order>1</Order>
    <NavigationPoints>
      <NavigationPoint>
        <Title>1.1.</Title>
        <File></File>
        <ElementId></ElementId>
        <Order>2</Order>
        <NavigationPoints />
      </NavigationPoint>
    </NavigationPoints>
  </NavigationPoint>
  <NavigationPoint>
    <Title>2.</Title>
    <File></File>
    <ElementId></ElementId>
    <Order>3</Order>
    <NavigationPoints />
  </NavigationPoint>
</NavigationPoints>";
            Assert.AreEqual(expected, result);
        }

        [TestMethod] 
        public void FilesToXml_WithFile_ReturnsFiles()
        {
            var files = new[]
            {
                new File("test1.html", "test1.html", ContentType.Css, Encoding.UTF8.GetBytes("<html />")),
                new File("test2.html", "test2.html", ContentType.Css, Encoding.UTF8.GetBytes("<HTML />")),
            };

            var result = files.ToXml().ToString();

            var expected = @"<Files xmlns=""http://JFlepp.Epub/Book/meta/xml/"">
  <File>
    <Name>test1.html</Name>
    <Path>test1.html</Path>
    <ContentType>Css</ContentType>
    <ContentHash>976c91556d9c07b0d6c8da7292df4661d9357b1ee5840acba40c31a89d7916eb</ContentHash>
  </File>
  <File>
    <Name>test2.html</Name>
    <Path>test2.html</Path>
    <ContentType>Css</ContentType>
    <ContentHash>817d4cb067499bcafa9e29a71f188c7176fc1ef948076515fce4f99a754f8d95</ContentHash>
  </File>
</Files>";
            Assert.AreEqual(expected, result);
        }
        
    }
}
