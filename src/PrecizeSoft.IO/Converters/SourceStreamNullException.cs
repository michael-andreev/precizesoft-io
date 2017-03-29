using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public class SourceStreamNullException : ArgumentNullException
    {
        public SourceStreamNullException(): base()
        {

        }

        public SourceStreamNullException(string paramName): base(paramName)
        {

        }
    }
}
