using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal sealed class MetaData
    {
        public string? Identifier { get; }
        public string? Title { get; }
        public string? Language { get; }
        public string? Publisher { get; }
        public string? Description { get; }
        public string? Date { get; }
        public string? Creator { get; }
        public string? Rights { get; }

        public MetaData(string? identifier, string? title, string? language, string? publisher, string? description, string? date, string? creator, string? rights)
        {
            Identifier = identifier;
            Title = title;
            Language = language;
            Publisher = publisher;
            Description = description;
            Date = date;
            Creator = creator;
            Rights = rights;
        }
    }
}
