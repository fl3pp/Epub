using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit
{
    [TestClass]
    public class ContentTypeParserTests
    {
        [TestMethod]
        [DataRow(MimeTypes.XHTML, ContentType.XHtml)]
        [DataRow(MimeTypes.DTBOOK, ContentType.DTBook)]
        [DataRow(MimeTypes.DTBOOKNCX, ContentType.Ncx)]
        [DataRow(MimeTypes.OEB1DOCUMENT, ContentType.OEB1Document)]
        [DataRow(MimeTypes.XML, ContentType.Xml)]
        [DataRow(MimeTypes.CSS, ContentType.Css)]
        [DataRow(MimeTypes.OEB1CSS, ContentType.OEB1Css)]
        [DataRow(MimeTypes.IMAGEGIF, ContentType.ImageGIF)]
        [DataRow(MimeTypes.IMAGEJPEG, ContentType.ImageJpeg)]
        [DataRow(MimeTypes.IMAGEPNG, ContentType.ImagePng)]
        [DataRow(MimeTypes.IMAGESVG, ContentType.ImageSvg)]
        [DataRow(MimeTypes.FONTTRUETYPE, ContentType.FontTrueType)]
        [DataRow(MimeTypes.FONTXFONTTRUETYPE, ContentType.FontTrueType)]
        [DataRow(MimeTypes.FONTOPENTYPE, ContentType.FontOpenType)]
        [DataRow("", ContentType.Unknown)]
        public void GetContentTypeFromMimeType_WithMimeType_ReturnsContentType(
            string input, ContentType expected)
        {
            var result = ContentTypeParser.GetContentTypeFromMimeType(input);

            Assert.AreEqual(expected, result);
        }     

        [TestMethod]
        [DataRow(ContentType.XHtml, MimeTypes.XHTML)]
        [DataRow(ContentType.DTBook, MimeTypes.DTBOOK)]
        [DataRow(ContentType.Ncx, MimeTypes.DTBOOKNCX)]
        [DataRow(ContentType.OEB1Document, MimeTypes.OEB1DOCUMENT)]
        [DataRow(ContentType.Xml, MimeTypes.XML)]
        [DataRow(ContentType.Css, MimeTypes.CSS)]
        [DataRow(ContentType.OEB1Css, MimeTypes.OEB1CSS)]
        [DataRow(ContentType.ImageGIF, MimeTypes.IMAGEGIF)]
        [DataRow(ContentType.ImageJpeg, MimeTypes.IMAGEJPEG)]
        [DataRow(ContentType.ImagePng, MimeTypes.IMAGEPNG)]
        [DataRow(ContentType.ImageSvg, MimeTypes.IMAGESVG)]
        [DataRow(ContentType.FontTrueType, MimeTypes.FONTTRUETYPE)]
        [DataRow(ContentType.FontOpenType, MimeTypes.FONTOPENTYPE)]
        [DataRow(ContentType.Unknown, MimeTypes.BINARYSTREAM)]
        public void GetMimeTypeForContentType_WithContentType_ReturnsMimeType(
            ContentType input, string expected)
        {
            var result = ContentTypeParser.GetMimeTypeContentType(input);

            Assert.AreEqual(expected, result);
        }
    }
}
