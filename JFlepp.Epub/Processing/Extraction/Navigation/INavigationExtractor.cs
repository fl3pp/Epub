using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface INavigationExtractor
    {
        Task<IEnumerable<NavigationPoint>> ExtractNavigationPoints(StructureFiles context, IEnumerable<File> files);
    }
}
