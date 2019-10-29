using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace JFlepp.Epub.Xml
{
    internal sealed class BookToXmlConverter
    {
        private static XNamespace xNamespace = "http://JFlepp.Epub/Book/meta/xml/";
        private const char pluralSuffix = 's';

        public static XDocument BookToXml(Book book)
        {
            var rootElement = BookMetaToXml(book);
            rootElement.Add(FilesToXml(book.Files));
            rootElement.Add(NavigationPointsToXml(book.NavigationPoints));
            return new XDocument(rootElement);;
        }

        public static XElement BookMetaToXml(Book book)
        {
            return new XElement(xNamespace + nameof(Book), new[] 
            { 
                new XElement(xNamespace + nameof(book.Title), book.Title),
                new XElement(xNamespace + nameof(book.Author), book.Author),
                new XElement(xNamespace + nameof(book.Description), book.Description),
                new XElement(xNamespace + nameof(book.DatePublished), book.DatePublished),
                new XElement(xNamespace + nameof(book.Publisher), book.Publisher),
                new XElement(xNamespace + nameof(book.Language), book.Language),
            });
        }

        public static XElement FilesToXml(IEnumerable<File> files)
        {
            return new XElement(xNamespace + (nameof(File) + pluralSuffix),
                files.Select(FileToXml).ToArray());
        }

        public static XElement FileToXml(File file)
        {
            return new XElement(xNamespace + nameof(File),
                new XElement(xNamespace + nameof(file.Name), file.Name),
                new XElement(xNamespace + nameof(file.Path), file.Path),
                new XElement(xNamespace + nameof(file.ContentType), file.ContentType.ToString()),
                new XElement(xNamespace + nameof(file.Content) + "Hash", file.Content.GetSHA256Hash()));
        }

        public static XElement NavigationPointsToXml(IEnumerable<NavigationPoint> navigationPoints)
        {
            return new XElement(xNamespace + (nameof(NavigationPoint) + pluralSuffix),
                navigationPoints.Select(NavigationPointToXml).ToArray());
        }

        public static XElement NavigationPointToXml(NavigationPoint navigationPoint)
        {
            return new XElement(
                xNamespace + nameof(NavigationPoint),
                new XElement(xNamespace + nameof(navigationPoint.Title),
                    navigationPoint.Title),
                new XElement(xNamespace + nameof(navigationPoint.File),
                    navigationPoint.File.ToString()),
                new XElement(xNamespace + nameof(navigationPoint.ElementId),
                    navigationPoint.ElementId),
                new XElement(xNamespace + nameof(navigationPoint.Order),
                    navigationPoint.Order),
                NavigationPointsToXml(navigationPoint.Children));
        }
    }

    public static class BookToXmlExtensions
    {
        public static XDocument ToXml(this Book book) { Ensure.NotNull(book, nameof(book)); return BookToXmlConverter.BookToXml(book); }
        public static XElement MetaToXml(this Book book) { Ensure.NotNull(book, nameof(book)); return BookToXmlConverter.BookMetaToXml(book); }
        public static XElement ToXml(this IEnumerable<File> files) { Ensure.NotNull(files, nameof(files)); return BookToXmlConverter.FilesToXml(files); }
        public static XElement ToXml(this File file) { Ensure.NotNull(file, nameof(file)); return BookToXmlConverter.FileToXml(file); }
        public static XElement ToXml(this IEnumerable<NavigationPoint> navigationPoints) { Ensure.NotNull(navigationPoints, nameof(navigationPoints)); return BookToXmlConverter.NavigationPointsToXml(navigationPoints); }
        public static XElement ToXml(this NavigationPoint navigationPoint) { Ensure.NotNull(navigationPoint, nameof(navigationPoint)); return BookToXmlConverter.NavigationPointToXml(navigationPoint); }
    }
}
