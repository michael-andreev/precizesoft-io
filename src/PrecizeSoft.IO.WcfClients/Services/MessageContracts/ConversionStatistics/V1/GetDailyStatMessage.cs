using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO.Services.DataContracts.ConversionStatistics.V1;

namespace PrecizeSoft.IO.Services.MessageContracts.ConversionStatistics.V1
{
    [MessageContract]
    public class GetDailyStatMessage
    {
        [MessageBodyMember]
        public DateTime ReportDate { get; set; }

        [MessageBodyMember]
        public short TimeZoneUtcOffsetInMinutes { get; set; }
    }
}
