using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public interface IFileConverterValidator
    {
        void ValidateConvertParameters(string sourceFileName, string destinationFileName);

        void ValidateConvertParameters(Stream sourceStream, string fileExtension);

        void ValidateConvertParameters(byte[] sourceBytes, string fileExtension);
    }
}
