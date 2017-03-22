using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Services.ServiceContracts.Converter.V1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Name = "Service", Namespace = "http://api.getpdf.online/converter/v1/")]
    public interface IService
    {
        [OperationContract]
        byte[] ConvertToPdf(byte[] source, string fileExtension);

        [OperationContract]
        string Test();
    }
}
