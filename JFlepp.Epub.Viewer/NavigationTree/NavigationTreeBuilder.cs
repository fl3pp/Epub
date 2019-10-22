using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using JFlepp.Epub;

namespace JFlepp.Epub
{
    internal sealed class NavigationTreeBuilder
    {
        public IEnumerable<TreeViewItem> GetItems(Book book)
        {
            return book.NavigationPoints.Select(CreateTreeViewItemFromNavigationPoint).ToArray();
        }

        private TreeViewItem CreateTreeViewItemFromNavigationPoint(NavigationPoint point)
        {
            var item = new TreeViewItem();
            item.Header = point.Title;
            item.DataContext = point;
            
            foreach (var child in point.Children)
            {
                item.Items.Add(CreateTreeViewItemFromNavigationPoint(child));
            };

            return item;
        }
    }
}
