using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub
{
    [Flags]
    public enum EpubReadingOptions
    {
        None = 0b_0000,
        ShortenPaths = 0b_0001,
    }
}
