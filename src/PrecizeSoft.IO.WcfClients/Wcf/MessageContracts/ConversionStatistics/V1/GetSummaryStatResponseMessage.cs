using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO.Wcf.DataContracts.ConversionStatistics.V1;

namespace PrecizeSoft.IO.Wcf.MessageContracts.ConversionStatistics.V1
{
    [MessageContract]
    public class GetSummaryStatResponseMessage
    {
        [MessageBodyMember]
        public SummaryStat Data { get; set; }
    }
}
