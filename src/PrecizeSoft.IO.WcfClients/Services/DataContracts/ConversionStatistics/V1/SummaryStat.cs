using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Services.DataContracts.ConversionStatistics.V1
{
    [DataContract(Namespace = "http://io.precizesoft.com/ConversionStatistics/V1/")]
    public class SummaryStat
    {
        [DataMember]
        public DateTime? FirstRequestDateUtc { get; set; }

        [DataMember]
        public DateTime? LastRequestDateUtc { get; set; }

        [DataMember]
        public double DurationInSecondsAvg { get; set; }

        [DataMember]
        public double DurationInSecondsMin { get; set; }

        [DataMember]
        public double DurationInSecondsMax { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public int PositiveResultCount { get; set; }

        [DataMember]
        public int NegativeResultCount { get; set; }

        [DataMember]
        public long FileSizeSum { get; set; }

        [DataMember]
        public int FileSizeAvg { get; set; }

        [DataMember]
        public int FileSizeMin { get; set; }

        [DataMember]
        public int FileSizeMax { get; set; }

        [DataMember]
        public long ResultFileSizeSum { get; set; }

        [DataMember]
        public int ResultFileSizeAvg { get; set; }

        [DataMember]
        public int ResultFileSizeMin { get; set; }

        [DataMember]
        public int ResultFileSizeMax { get; set; }

        [DataMember]
        public long TotalFileSizeSum { get; set; }
    }
}
