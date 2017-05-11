using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Contracts.Converters
{
    public interface IFileInfo
    {
        Guid FileId { get; set; }

        string FileName { get; set; }

        int FileSize { get; set; }

        DateTime CreateDateUtc { get; set; }
    }
}
