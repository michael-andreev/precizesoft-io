using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Contracts.ConversionStatistics
{
    public interface ISummaryStat
    {
        DateTime? FirstRequestDateUtc { get; set; }

        DateTime? LastRequestDateUtc { get; set; }

        double DurationInSecondsAvg { get; set; }

        double DurationInSecondsMin { get; set; }

        double DurationInSecondsMax { get; set; }

        int TotalCount { get; set; }

        int PositiveResultCount { get; set; }

        int NegativeResultCount { get; set; }

        long FileSizeSum { get; set; }

        int FileSizeAvg { get; set; }

        int FileSizeMin { get; set; }

        int FileSizeMax { get; set; }

        long ResultFileSizeSum { get; set; }

        int ResultFileSizeAvg { get; set; }

        int ResultFileSizeMin { get; set; }

        int ResultFileSizeMax { get; set; }

        long TotalFileSizeSum { get; set; }
    }
}
