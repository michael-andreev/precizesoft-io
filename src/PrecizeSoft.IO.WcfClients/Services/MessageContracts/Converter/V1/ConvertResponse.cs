using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Services.MessageContracts.Converter.V1
{
    [MessageContract]
    public class ConvertResponse
    {
        [MessageBodyMember]
        public byte[] FileBytes { get; set; }
    }
}
