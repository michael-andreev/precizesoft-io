using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using PrecizeSoft.IO.Services.FaultContracts.Converter.V1;
using PrecizeSoft.IO.Services.MessageContracts.Converter.V1;

namespace PrecizeSoft.IO.Services.ServiceContracts.Converter.V1
{
    [ServiceContract(Name = "Service", Namespace = "http://io.precizesoft.com/converter/v1/")]
    public interface IService
    {
        [OperationContract]
        [FaultContract(typeof(FormatNotSupported))]
        ConvertResponse Convert(MessageContracts.Converter.V1.Convert message);

        [OperationContract]
        GetSupportedFormatsResponse GetSupportedFormats();
    }
}
