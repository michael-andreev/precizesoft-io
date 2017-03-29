using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public class SourceBytesNullException : ArgumentNullException
    {
        public SourceBytesNullException(): base()
        {

        }

        public SourceBytesNullException(string paramName): base(paramName)
        {

        }
    }
}
