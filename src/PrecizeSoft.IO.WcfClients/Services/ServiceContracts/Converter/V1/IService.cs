using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Services.ServiceContracts.Converter.V1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Name = "Service", Namespace = "http://api.getpdf.online/converter/v1/")]
    public interface IService
    {
        [WebInvoke(Method = "POST", UriTemplate = "convert/{message.fileExtension}", BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        ConvertResultMessage Convert(ConvertMessage message);
        //Stream Convert(Stream source, string fileExtension);

        [WebGet(UriTemplate = "test")]
        [OperationContract]
        string Test();
    }

    [MessageContract]
    public class ConvertMessage
    {
        [MessageBodyMember]
        public string fileExtension { get; set; }

        [MessageBodyMember]
        public Stream source { get; set; }
    }

    [MessageContract]
    public class ConvertResultMessage
    {
        [MessageBodyMember]
        public Stream result { get; set; }
    }
}
