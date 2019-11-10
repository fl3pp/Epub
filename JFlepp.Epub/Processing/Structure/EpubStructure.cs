using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal sealed class EpubStructure
    {
        public XmlStructureFile Opf { get; }
        public XmlStructureFile Container { get; }
        public IZip Zip { get; }

        public EpubStructure(
            XmlStructureFile opf,
            XmlStructureFile container,
            IZip zip)
        {
            Opf = opf;
            Container = container;
            Zip = zip;
        }
    }
}
