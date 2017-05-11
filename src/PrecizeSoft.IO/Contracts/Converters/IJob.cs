using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Contracts.Converters
{
    public interface IJob
    {
        Guid JobId { get; set; }

        Guid? SessionId { get; set; }

        Guid InputFileId { get; set; }

        Guid OutputFileId { get; set; }

        DateTime? ExpireDateUtc { get; set; }

        byte? Rating { get; set; }
    }
}
