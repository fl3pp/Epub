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
    public class NcxLoaderTests
    {
        [TestMethod]
        public async Task LoadNcx_PathSpecifiedInManifestItem_LoadsXHtmlToc()
        {
            var testee = new NcxLoader();
            var manifestItem = new ManifestItem(null, "toc.ncx", string.Empty, ContentType.Ncx);
            var file = TestItemFactory.CreateFileFromString("content/toc.ncx", "<toc />");

            var result = await testee.LoadNcx(new[] { manifestItem }, "content/book.opf", new[] { file } );

            Assert.AreEqual("content/toc.ncx", result.Path);
            Assert.AreEqual("<toc />", result.Doc.ToString());
        }
    }
}
