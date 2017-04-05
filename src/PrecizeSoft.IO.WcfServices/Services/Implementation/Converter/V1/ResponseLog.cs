using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace PrecizeSoft.IO.Services.Implementation.Converter.V1
{
    public class ResponseLog
    {
        public Guid RequestId { get; set; }

        public DateTime ResponseDateUtc { get; set; }

        public int? ResultFileSize { get; set; }

        public FaultException Fault { get; set; }
    }
}
