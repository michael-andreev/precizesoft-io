using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PrecizeSoft.IO.Contracts.ConversionStatistics
{
    public interface IStatByFileCategory
    {
        string FileCategoryCode { get; set; }

        int TotalCount { get; set; }

        long FileSizeSum { get; set; }

        int FileSizeAvg { get; set; }

        int FileSizeMin { get; set; }

        int FileSizeMax { get; set; }
    }
}
