using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using PrecizeSoft.IO.Services.FaultContracts.Converter.V1;
using PrecizeSoft.IO.Services.MessageContracts.ConversionStatistics.V1;
using PrecizeSoft.IO.Services.MessageContracts.Converter.V1;

namespace PrecizeSoft.IO.Services.ServiceContracts.ConversionStatistics.V1
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
