using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Wpf;
using JFlepp.Epub;

namespace JFlepp.Epub
{
    internal sealed class CefRunner : IDisposable
    {
        public static CefRunner Create(Book book)
        {
            Cef.EnableHighDPISupport();
            CefSharpSettings.ShutdownOnExit = false;

            #pragma warning disable CA2000 // From Project Maintainer Example
            var settings = new CefSettings();
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = EpubResourceHandlerFactory.SchemeName,
                SchemeHandlerFactory = new EpubResourceHandlerFactory(book),
            });
            settings.LogSeverity = LogSeverity.Disable;
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            #pragma warning restore CA2000 

            return new CefRunner();
        }

        public void Dispose()
        {
            Cef.Shutdown();
        }
    }
}
