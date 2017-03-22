using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public static class ConverterFactoryImageMagickExtensions
    {
        public static IFileConverter CreateImageMagickToPdfConverter(this ConverterFactory factory)
        {
            return new ImageMagickToPdfConverter();
        }
    }
}
