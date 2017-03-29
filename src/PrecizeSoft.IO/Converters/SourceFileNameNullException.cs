using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public class SourceFileNameNullException : ArgumentNullException
    {
        public SourceFileNameNullException(): base()
        {

        }

        public SourceFileNameNullException(string paramName): base(paramName)
        {

        }
    }
}
