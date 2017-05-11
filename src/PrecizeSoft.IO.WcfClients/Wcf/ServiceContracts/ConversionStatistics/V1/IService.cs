using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using PrecizeSoft.IO.Wcf.FaultContracts.Converter.V1;
using PrecizeSoft.IO.Wcf.MessageContracts.ConversionStatistics.V1;
using PrecizeSoft.IO.Wcf.MessageContracts.Converter.V1;

namespace PrecizeSoft.IO.Wcf.ServiceContracts.ConversionStatistics.V1
{
    [ServiceContract(Name = "Service", Namespace = "http://io.precizesoft.com/ConversionStatistics/V1/")]
    public interface IService
    {
        [OperationContract]
        GetSummaryStatResponseMessage GetSummaryStat(GetSummaryStatMessage message);

        [OperationContract]
        GetStatByFileCategoriesResponseMessage GetStatByFileCategories(GetStatByFileCategoriesMessage message);

        [OperationContract]
        GetDailyStatResponseMessage GetDailyStat(GetDailyStatMessage message);
    }
}
