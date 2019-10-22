using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal sealed class BookBuilder
    {
        private const string unknownPrefix = "Unknown ";

        public string Title { get; set; } = unknownPrefix + nameof(Title);
        public string Author { get; set; } = unknownPrefix + nameof(Author);
        public string Description { get; set; } = unknownPrefix + nameof(Description);
        public string DatePublished { get; set; } = unknownPrefix + nameof(DatePublished);
        public string Publisher { get; set; } = unknownPrefix + nameof(Publisher);
        public string Language { get; set; } = unknownPrefix + nameof(Language);
        public IEnumerable<File> Files { get; set; } = Enumerable.Empty<File>();
        public IEnumerable<NavigationPoint> NavigationPoints { get; set; } = Enumerable.Empty<NavigationPoint>();

        public void ReadFromMeta(MetaData meta)
        {
            Title = GetNewValueOrUnknown(meta.Title, nameof(Title));
            Author = GetNewValueOrUnknown(meta.Creator, nameof(Author));
            Description = GetNewValueOrUnknown(meta.Description, nameof(Description));
            DatePublished = GetNewValueOrUnknown(meta.Date, nameof(DatePublished));
            Publisher = GetNewValueOrUnknown(meta.Publisher, nameof(Publisher));
            Language = GetNewValueOrUnknown(meta.Language, nameof(Language));
        }

        public void AdjustNavigationPoints(Func<NavigationPoint, NavigationPoint> func)
        {
            NavigationPoints = NavigationPoints.Select(p => AdjustNavigationPointsRecursive(p, func)).ToArray();
        }

        private NavigationPoint AdjustNavigationPointsRecursive(
            NavigationPoint point, Func<NavigationPoint, NavigationPoint> func)
        {
            var newPoint = func(point);
            return newPoint.WithChildren(point.Children.Select(child => AdjustNavigationPointsRecursive(child, func)));
        }

        private static string GetNewValueOrUnknown(string? newValue, string propertyName)
        {
            return newValue != null ? newValue : unknownPrefix + propertyName;
        } 

        public Book ToBook()
        {
            return new Book(
                Title, Author, Description, DatePublished, Publisher, Language, Files, NavigationPoints);
        }
    }
}
