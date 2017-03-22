using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public class ConverterFactory
    {
        public IFileConverter CreateComplexConverter(IEnumerable<IFileConverter> converterCollection)
        {
            return new FileConverterRouter(converterCollection);
        }
    }
}
