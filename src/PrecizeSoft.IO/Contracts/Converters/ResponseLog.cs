using System;
using System.Collections.Generic;
using System.Text;

namespace PrecizeSoft.IO.Contracts.Converters
{
    public class ResponseLog
    {
        public Guid RequestId { get; set; }

        public DateTime ResponseDateUtc { get; set; }

        public int? ResultFileSize { get; set; }

        public ConvertErrorType? ErrorType { get; set; }
    }
}
