using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO.Converters;
using PrecizeSoft.IO.Wcf.Configuration.ConversionStatistics.V1;
using PrecizeSoft.IO.Wcf.ServiceContracts.ConversionStatistics.V1;
using PrecizeSoft.IO.Wcf.MessageContracts.ConversionStatistics.V1;

namespace PrecizeSoft.IO.Wcf.Implementation.ConversionStatistics.V1
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFileConverter">Must have a default constructor, because the wcf service must have
    /// a default constructor too for scalability support</typeparam>
    [ServiceBehavior(Namespace = "http://io.precizesoft.com/ConversionStatistics/V1/", ConcurrencyMode = ConcurrencyMode.Multiple)]
    public abstract class WcfConversionStatisticsV1Service : IService
    {
        public static void Configure(ServiceConfiguration config)
        {
            string address = config.BaseAddresses.First().AbsoluteUri;

            ServiceConfigurationManager manager = new ServiceConfigurationManager();

            ContractDescription contract = ContractDescription.GetContract(typeof(IService));
            ServiceEndpoint endpoint = new ServiceEndpoint(contract, manager.CreateBinding(), new EndpointAddress(address));
            config.AddServiceEndpoint(endpoint);
            //config.AddServiceEndpoint(manager.CreateServiceEndpoint(address));

            config.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            config.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
        }

        public abstract GetDailyStatResponseMessage GetDailyStat(GetDailyStatMessage message);

        public abstract GetStatByFileCategoriesResponseMessage GetStatByFileCategories(GetStatByFileCategoriesMessage message);

        public abstract GetSummaryStatResponseMessage GetSummaryStat(GetSummaryStatMessage message);
    }
}
