using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Processing
{
    internal interface ICleanerFactory
    {
        IFilePathShortener CreateFilePathShortener();
    }

    internal sealed class CleanerImplementationsFactory : ICleanerFactory
    {
        private readonly IFilePathShortener pathShortener = new FilePathShortener();

        public IFilePathShortener CreateFilePathShortener() => pathShortener;
    }

}
