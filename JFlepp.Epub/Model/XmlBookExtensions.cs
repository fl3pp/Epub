using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub
{
    public static class XmlBookExtensions
    {
        private static XNamespace xNamespace = "http://JFlepp.Epub/Book/meta/xml/";

        private const string rootElementName = "Book";

        public static string MetaToXml(this Book book)
        {
            if (book is null) throw new ArgumentNullException(nameof(book));

            var rootElement = new XElement(xNamespace + rootElementName);
            var xDocument = new XDocument(rootElement);

            AddChildren(rootElement, CreatePropertyElements(book));
            CreateAndAddElementAndAddChildren(rootElement, xNamespace + nameof(book.NavigationPoints),
                book.NavigationPoints.Select(CreateNavigationPointElementRecursive));
            CreateAndAddElementAndAddChildren(rootElement, xNamespace + nameof(book.Files),
                book.Files.Select(CreateFileElement));

            return xDocument.ToString();
        }

        private static void CreateAndAddElementAndAddChildren(
            XElement root, XName elementName, IEnumerable<XElement> children)
        {
            var element = new XElement(elementName);
            foreach (var child in children) element.Add(child);
            root.Add(element);
        }

        private static void AddChildren(XElement root, IEnumerable<XElement> children)
        {
            foreach (var child in children) root.Add(child);
        }

        private static IEnumerable<XElement> CreatePropertyElements(Book book)
        {
            yield return new XElement(xNamespace + nameof(book.Title), book.Title);
            yield return new XElement(xNamespace + nameof(book.Author), book.Author);
            yield return new XElement(xNamespace + nameof(book.Description), book.Description);
            yield return new XElement(xNamespace + nameof(book.DatePublished), book.DatePublished);
            yield return new XElement(xNamespace + nameof(book.Publisher), book.Publisher);
            yield return new XElement(xNamespace + nameof(book.Language), book.Language);
        }

        private static XElement CreateFileElement(File file)
        {
            return new XElement(xNamespace + nameof(File),
                new XElement(xNamespace + nameof(file.Name), file.Name),
                new XElement(xNamespace + nameof(file.Path), file.Path),
                new XElement(xNamespace + nameof(file.ContentType), file.ContentType.ToString()),
                new XElement(xNamespace + nameof(file.Content) + "Hash", file.Content.GetSHA256Hash()));
        }

        private static XElement CreateNavigationPointElementRecursive(NavigationPoint current)
        {
            return new XElement(
                xNamespace + nameof(NavigationPoint),
                new XElement(xNamespace + nameof(current.Title), current.Title),
                new XElement(xNamespace + nameof(current.File), current.File.ToString()),
                new XElement(xNamespace + nameof(current.ElementId), current.ElementId),
                new XElement(xNamespace + nameof(current.Order), current.Order),
                new XElement(xNamespace + nameof(current.Children), current.Children.Select(CreateNavigationPointElementRecursive)));
        }
    }
}
