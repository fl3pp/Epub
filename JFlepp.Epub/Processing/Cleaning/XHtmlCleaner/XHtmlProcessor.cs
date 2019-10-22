using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal interface IXHtmlCleaner
    {
        Task<File> ProcessFile(File file, XDocument doc);
    }

    internal sealed class XHtmlCleaner : IXHtmlCleaner
    {
        private readonly IUnnecessaryNodeRemover nodeRemover;
        private readonly IUnnecessaryAttributeRemover attributeRemover;

        public XHtmlCleaner(
            IUnnecessaryNodeRemover nodeRemover,
            IUnnecessaryAttributeRemover attributeRemover)
        {
            this.nodeRemover = nodeRemover;
            this.attributeRemover = attributeRemover;
        }

        public Task<File> ProcessFile(File file, XDocument doc)
        {
            var root = nodeRemover.RemoveNodes(doc.Root);
            attributeRemover.RemoveAttributes(root);
            var newFile = file.WithContent(Encoding.UTF8.GetBytes(root.ToString()));
            return Task.FromResult(newFile);
        }
    }
}
