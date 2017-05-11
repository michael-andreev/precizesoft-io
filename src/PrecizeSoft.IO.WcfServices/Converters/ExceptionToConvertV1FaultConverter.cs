using PrecizeSoft.IO.Wcf.Implementation.Converter.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    internal static class ExceptionToConvertV1FaultConverter
    {
        public static FaultException ToV1FaultConverter(this Exception e)
        {
            return (new FaultExceptionFactory()).CreateFromException(e);
        }
    }
}
