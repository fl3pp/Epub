using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFlepp.Epub.Processing
{
    internal sealed class StructureFiles
    {
        public string OpfPath { get; }
        public XDocument OpfDoc { get; }
        public string NcxPath { get; }
        public XDocument NcxDoc { get; }
        public XDocument ContainerDoc { get; }

        public StructureFiles(
            string opfPath, XDocument opf, string ncxPath, XDocument ncx, XDocument container)
        {
            OpfPath = opfPath;
            OpfDoc = opf;
            NcxPath = ncxPath;
            NcxDoc = ncx;
            ContainerDoc = container;
        }

        public StructureFiles WithAdjustedPaths(Func<string,string> trim)
        {
            return new StructureFiles(
                trim(OpfPath), OpfDoc, trim(NcxPath), NcxDoc, ContainerDoc);
        }
    }
}
