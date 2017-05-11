using PrecizeSoft.IO.Contracts.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.WebApi.Contracts.Converter.V1
{
    public class Job : IJob
    {
        public Guid JobId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid InputFileId { get; set; }
        public Guid OutputFileId { get; set; }
        public DateTime? ExpireDateUtc { get; set; }
        public byte? Rating { get; set; }
    }
}
