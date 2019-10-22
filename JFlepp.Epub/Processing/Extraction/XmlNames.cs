using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal static class XmlNamespaces
    {
        public static XNamespace Container { get; } = "urn:oasis:names:tc:opendocument:xmlns:container";
        public static XNamespace Ncx { get; } = "http://www.daisy.org/z3986/2005/ncx/";
        public static XNamespace Opf { get; } = "http://www.idpf.org/2007/opf";
        public static XNamespace Epub { get; } = "http://www.idpf.org/2007/ops";
        public static XNamespace OpfMetaElements { get; } = "http://purl.org/dc/elements/1.1/";
        public static XNamespace OpfTerms { get; } = "http://purl.org/dc/terms/";
        public static XNamespace XHtml { get; } = "http://www.w3.org/1999/xhtml";
    }

    internal static class ContainerXmlNames
    {
        public const string RootFilesElementName = "rootfiles";
        public const string RootFileElementName = "rootfile";
        public const string FullPathAttributeName = "full-path";
        public const string OpfMediaType = "application/oebps-package+xml";
        public const string MediaTypeAttributeName = "media-type";
    }

    internal static class OpfXmlNames
    {
        public const string ManifestElementName = "manifest";
        public const string ItemElementName = "item";
        public const string IdAttributeName = "id";
        public const string HrefAttributeName = "href";
        public const string MediaTypeAttributeName = "media-type";
        public const string PropertiesAttributeName = "properties";
        public const string NavPropertiesAttributeValue = "nav";

        public const string MetaDataElementName = "metadata";
        public const string IdentifierElementName = "identifier";
        public const string TitleElementName = "title";
        public const string PublisherElementName = "publisher";
        public const string DateElementName = "date";
        public const string DescriptionElementName = "description";
        public const string CreatorElementName = "creator";
        public const string LanguageElementName = "language";
        public const string RightsElementName = "rights";
    }

    internal static class NcxXmlNames
    {
        public const string NavMapElementName = "navMap";
        public const string NavPointElementName = "navPoint";
        public const string NavLabelElementName = "navLabel";
        public const string TextElementName = "text";
        public const string ContentElementName = "content";
        public const string SrcAttributeName = "src";
        public const string PlayOrderAttributeName = "playOrder";
    }

    internal static class XHtmlTocXmlNames
    {
        public const string EpubTypeAttributeName = "type";
        public const string TocEpubTypeAttributeValue = "toc";

        public const string NavElementName = "nav";
        public const string LinkElementName = "a";
        public const string OrderedListElementName = "ol";
        public const string ListItemElementName = "li";
        public const string HrefAttributeName = "href";
    }
}
