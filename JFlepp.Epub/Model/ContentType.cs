using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub
{
    public enum ContentType
    {
        Unknown = 0,
        XHtml,
        DTBook,
        Ncx,
        OEB1Document,
        Xml,
        Css,
        OEB1Css,
        ImageGIF,
        ImageJpeg,
        ImagePng,
        ImageSvg,
        FontTrueType,
        FontOpenType,
    }

    public static class MimeTypes
    {
        public const string XHTML = "APPLICATION/XHTML+XML";
        public const string DTBOOK = "APPLICATION/X-DTBOOK+XML";
        public const string DTBOOKNCX = "APPLICATION/X-DTBNCX+XML";
        public const string OEB1DOCUMENT = "TEXT/X-OEB1-DOCUMENT";
        public const string XML = "APPLICATION/XML";
        public const string BINARYSTREAM = "APPLICATION/OCTET-STEAM";
        public const string CSS = "TEXT/CSS";
        public const string OEB1CSS = "TEXT/X-OEB1-CSS";
        public const string IMAGEGIF = "IMAGE/GIF";
        public const string IMAGEJPEG = "IMAGE/JPEG";
        public const string IMAGEPNG = "IMAGE/PNG";
        public const string IMAGESVG = "IMAGE/SVG+XML";
        public const string FONTTRUETYPE = "FONT/TRUETYPE";
        public const string FONTXFONTTRUETYPE = "APPLICATION/X-FONT-TRUETYPE";
        public const string FONTXFONTOPENTYPE = "APPLICATiON/X-FONT-OTF";
        public const string FONTOPENTYPE = "FONT/OPENTYPE";
        public const string MSFONTOPENTYPE = "APPLICATION/VND.MS-OPENTYPE";
    }


    public static class ContentTypeParser
    {
        private static readonly IDictionary<string, ContentType> contentTypeMimeTypeValues = new Dictionary<string, ContentType>
        {
            { MimeTypes.XHTML, ContentType.XHtml },
            { MimeTypes.DTBOOK, ContentType.DTBook },
            { MimeTypes.DTBOOKNCX, ContentType.Ncx },
            { MimeTypes.OEB1DOCUMENT, ContentType.OEB1Document },
            { MimeTypes.XML, ContentType.Xml },
            { MimeTypes.CSS, ContentType.Css },
            { MimeTypes.OEB1CSS, ContentType.OEB1Css },
            { MimeTypes.IMAGEGIF, ContentType.ImageGIF },
            { MimeTypes.IMAGEJPEG, ContentType.ImageJpeg },
            { MimeTypes.IMAGEPNG, ContentType.ImagePng },
            { MimeTypes.IMAGESVG, ContentType.ImageSvg },
            { MimeTypes.FONTTRUETYPE, ContentType.FontTrueType },
            { MimeTypes.FONTXFONTTRUETYPE, ContentType.FontTrueType },
            { MimeTypes.FONTOPENTYPE, ContentType.FontOpenType },
            { MimeTypes.MSFONTOPENTYPE, ContentType.FontOpenType },
            { MimeTypes.FONTXFONTOPENTYPE, ContentType.FontOpenType },
        };

        public static ContentType GetContentTypeFromMimeType(string? contentMimeType)
        {
            if (contentMimeType == null) return ContentType.Unknown;
            var result = contentTypeMimeTypeValues.TryGetValue(contentMimeType.ToUpperInvariant(), out var value)
                ? value
                : ContentType.Unknown;
            return result;
        }

        public static string GetMimeTypeContentType(ContentType contentType)
        {
            return contentTypeMimeTypeValues.Any(kvp => kvp.Value.Equals(contentType))
                ? contentTypeMimeTypeValues.First(kvp => kvp.Value.Equals(contentType)).Key
                : MimeTypes.BINARYSTREAM;
        }
    }
}
