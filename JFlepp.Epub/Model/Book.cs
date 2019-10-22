using System;
using System.Collections.Generic;

namespace JFlepp.Epub
{
    public sealed class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string Description { get; }
        public string DatePublished { get; }
        public string Publisher { get; }
        public string Language { get; }
        public IEnumerable<File> Files { get; }
        public IEnumerable<NavigationPoint> NavigationPoints { get; }

        public Book(
            string title,
            string author,
            string description,
            string datePublished,
            string publisher,
            string language,
            IEnumerable<File> files,
            IEnumerable<NavigationPoint> navigationPoints)
        {
            Title = title;
            Author = author;
            Description = description;
            DatePublished = datePublished;
            Publisher = publisher;
            Language = language;
            Files = files;
            NavigationPoints = navigationPoints;
        }
    }
}
