using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PrecizeSoft.IO.Contracts.ConversionStatistics
{
    public interface IStatByHour
    {
        int Hour { get; set; }

        int TotalCount { get; set; }

        long FileSizeSum { get; set; }

        long ResultFileSizeSum { get; set; }

        long TotalFileSizeSum { get; set; }
    }
}
