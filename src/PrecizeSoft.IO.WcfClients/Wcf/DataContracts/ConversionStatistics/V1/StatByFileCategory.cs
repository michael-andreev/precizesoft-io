using PrecizeSoft.IO.Contracts.ConversionStatistics;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PrecizeSoft.IO.Wcf.DataContracts.ConversionStatistics.V1
{
    [DataContract(Namespace = "http://io.precizesoft.com/ConversionStatistics/V1/")]
    public class StatByFileCategory: IStatByFileCategory
    {
        [DataMember]
        public string FileCategoryCode { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public long FileSizeSum { get; set; }

        [DataMember]
        public int FileSizeAvg { get; set; }

        [DataMember]
        public int FileSizeMin { get; set; }

        [DataMember]
        public int FileSizeMax { get; set; }
    }
}
