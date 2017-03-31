using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO.Converters;

namespace PrecizeSoft.IO.Tests.Converters
{
    public class ThreadParameters
    {
        public int IterationsCount { get; set; }
        public IFileConverter Converter { get; set; }
        public byte[] SourceFileBytes { get; set; }
        public string SourceFileExtension { get; set; }
        public string DestinationFileNameTemplate { get; set; }
    }
}
