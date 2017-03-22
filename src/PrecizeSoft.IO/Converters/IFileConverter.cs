using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public interface IFileConverter
    {
        void Convert(string sourceFileName, string destinationFileName);

        Stream Convert(Stream sourceStream, string fileExtension);

        byte[] Convert(byte[] sourceBytes, string fileExtension);

        IEnumerable<string> SupportedFormatCollection
        {
            get;
        }
    }
}
