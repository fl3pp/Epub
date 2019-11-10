using JFlepp.Epub.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit.Processing
{
    [TestClass]
    public class XHtmlTocLoaderTests
    {
        [TestMethod]
        public async Task LoadXHtmlToc_PathSpecifiedInManifestItem_LoadsXHtmlToc()
        {
            var testee = new XHtmlTocLoader();
            var manifestItem = new ManifestItem(null, "toc.xhtml", "nav", ContentType.XHtml);
            var file = TestItemFactory.CreateFileFromString("content/toc.xhtml", "<toc />");

            var result = await testee.LoadXHtmlToc(new[] { manifestItem }, "content/book.opf", new[] { file } );

            Assert.AreEqual("content/toc.xhtml", result.Path);
            Assert.AreEqual("<toc />", result.Doc.ToString());
        }
    }
}
