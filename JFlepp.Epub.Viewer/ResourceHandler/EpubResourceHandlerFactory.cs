using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using JFlepp.Epub;

namespace JFlepp.Epub
{
    internal sealed class EpubResourceHandlerFactory : ISchemeHandlerFactory
    {
        public const string SchemeName = "epub";

        private readonly Book book;

        public EpubResourceHandlerFactory(Book book)
        {
            this.book = book;
        }

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            var uri = new Uri(request.Url);
            var fileName = uri.AbsolutePath.TrimStart('/');

            var file = book.Files.Cast<File?>().SingleOrDefault(f => f!.Path.Equals(fileName, StringComparison.OrdinalIgnoreCase));

            if (file == null) return ResourceHandler.FromString("<html></html>", ".html");

            return ResourceHandler.FromByteArray(file.Content, ContentTypeParser.GetMimeTypeContentType(file.ContentType));
        }
    }
}
