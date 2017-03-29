using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public class FileExtensionNullException: ArgumentNullException
    {
        public FileExtensionNullException(): base()
        {

        }

        public FileExtensionNullException(string paramName): base(paramName)
        {

        }
    }
}
