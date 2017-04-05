using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Services.MessageContracts.Converter.V1
{
    [MessageContract]
    public class GetSupportedFormatsResponseMessage
    {
        [MessageBodyMember]
        public IEnumerable<string> SupportedFormats { get; set; }
    }
}
