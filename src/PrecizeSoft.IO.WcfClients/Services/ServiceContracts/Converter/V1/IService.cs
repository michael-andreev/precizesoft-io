using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using PrecizeSoft.IO.Services.FaultContracts.Converter.V1;
using PrecizeSoft.IO.Services.MessageContracts.Converter.V1;

namespace PrecizeSoft.IO.Services.ServiceContracts.Converter.V1
{
    [ServiceContract(Name = "Service", Namespace = "http://io.precizesoft.com/Converter/V1/")]
    public interface IService
    {
        [OperationContract]
        [FaultContract(typeof(FileExtensionEmpty))]
        [FaultContract(typeof(FileBytesEmpty))]
        [FaultContract(typeof(InvalidFileExtension))]
        [FaultContract(typeof(FormatNotSupported))]
        ConvertResponseMessage Convert(MessageContracts.Converter.V1.ConvertMessage message);

        [OperationContract]
        GetSupportedFormatsResponseMessage GetSupportedFormats(GetSupportedFormatsMessage message);
    }
}
