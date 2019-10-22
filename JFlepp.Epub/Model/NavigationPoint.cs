using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub
{
    public sealed class NavigationPoint
    {
        public string Title { get; }
        public File File { get; }
        public string? ElementId { get; }
        public int Order { get; }
        public IEnumerable<NavigationPoint> Children { get; }

        public NavigationPoint(
            string title, File file, string? elementId, int order, IEnumerable<NavigationPoint> children)
        {
            Title = title;
            ElementId = elementId;
            File = file;
            Order = order;
            Children = children;
        }

        public override string ToString() => $"{Title} - {File}{(ElementId == null ? string.Empty : '#' + ElementId)}";

        internal NavigationPoint WithFile(File file)
            => new NavigationPoint(Title, file, ElementId, Order, Children);

        internal NavigationPoint WithChildren(IEnumerable<NavigationPoint> children)
            => new NavigationPoint(Title, File, ElementId, Order, children);
    }
}
