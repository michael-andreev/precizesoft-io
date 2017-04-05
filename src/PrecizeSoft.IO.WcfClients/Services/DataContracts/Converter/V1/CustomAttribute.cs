using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Services.DataContracts.Converter.V1
{
    [DataContract(Namespace = "http://io.precizesoft.com/ConversionStatistics/V1/")]
    public class CustomAttribute
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
