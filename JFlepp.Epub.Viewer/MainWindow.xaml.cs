using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JFlepp.Epub;

namespace JFlepp.Epub
{
    public partial class MainWindow : Window
    {
        public static MainWindow CreateWindow(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            var window = new MainWindow();
            window.InitializeComponent();

            window.Title = book.Title;
            foreach (var item in new NavigationTreeBuilder().GetItems(book))
            {
                window.NavigationTree.Items.Add(item);
            }

            window.WebReader.IsBrowserInitializedChanged += (sender, args) =>
            {
                if (!(bool)args.NewValue) return;
                window.NavigateToNavigationPoint(book.NavigationPoints.First());
            }; 


            return window;
        }

        private void NavigationTree_SelectedItemChanged<T>(object sender, RoutedPropertyChangedEventArgs<T> e)
        {
            var viewItem = e.NewValue as TreeViewItem;
            var navigationPoint = (NavigationPoint)viewItem!.DataContext;
            NavigateToNavigationPoint(navigationPoint);
        }

        private void NavigateToNavigationPoint(NavigationPoint navigationPoint)
        {
            WebReader.Load("epub://handler/" + navigationPoint.File.Path + '#' + navigationPoint.ElementId);
        }
    }
}
