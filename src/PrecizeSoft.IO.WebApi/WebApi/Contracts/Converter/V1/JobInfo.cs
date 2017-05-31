using PrecizeSoft.IO.Contracts.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.WebApi.Contracts.Converter.V1
{
    public class JobInfo
    {
        public Guid JobId { get; set; }

        public Guid? SessionId { get; set; }

        public DateTime? ExpireDateUtc { get; set; }

        public StorageFileInfo InputFile { get; set; }

        public StorageFileInfo OutputFile { get; set; }

        public ConvertErrorType? ErrorType { get; set; }

        public byte? Rating { get; set; }
    }
}
