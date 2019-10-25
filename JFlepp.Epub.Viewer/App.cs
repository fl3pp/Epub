using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using JFlepp.Epub;

namespace JFlepp.Epub
{
    public class App : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            AddDirectoryAssemblyLoading();

            var path = args.ElementAtOrDefault(0);
            if (path == null)
            {
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "EPub (*.epub)|*.epub"
                };
                if (dialog.ShowDialog() == true)
                    path = dialog.FileName;
                else
                    return;
            }
            var book = EPubReader.ReadFromFile(path, EpubReadingOptions.ShortenPaths).Result;
            // TODO: remove Task.Result

            var app = new App();
            using var cefRunner = CefRunner.Create(book);
            var window = Epub.MainWindow.CreateWindow(book);
            app.Run(window);
        }

        private static void AddDirectoryAssemblyLoading()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromCefSharpFolder);

            static Assembly? LoadFromCefSharpFolder(object? sender, ResolveEventArgs? args)
            {
                var folderPath = Path.GetDirectoryName(typeof(App).Assembly.Location)! + "/cefsharp";
                var assemblyPath = Path.Combine(folderPath, new AssemblyName(args!.Name!).Name + ".dll");
                if (!System.IO.File.Exists(assemblyPath)) return null;
                return Assembly.LoadFrom(assemblyPath);
            }
        }
    }
}
