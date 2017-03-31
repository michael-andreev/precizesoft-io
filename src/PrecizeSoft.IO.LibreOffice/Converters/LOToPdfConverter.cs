using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using unoidl.com.sun.star.frame;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.util;

namespace PrecizeSoft.IO.Converters
{
    public class LOToPdfConverter : FileConverterRouter
    {
        protected static IEnumerable<IFileConverter> CreateConverterCollection()
        {
            List<IFileConverter> collection = new List<IFileConverter>
            {
                new LOWriterToPdfConverter(),
                new LOCalcToPdfConverter(),
                new LOImpressToPdfConverter(),
                new LODrawToPdfConverter()
            };
            return collection;
        }

        public LOToPdfConverter() : base(CreateConverterCollection())
        {
        }
    }
}
