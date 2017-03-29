using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public class DestinationFileNameNullException : ArgumentNullException
    {
        public DestinationFileNameNullException(): base()
        {

        }

        public DestinationFileNameNullException(string paramName): base(paramName)
        {

        }
    }
}
