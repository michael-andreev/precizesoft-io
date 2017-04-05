using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using PrecizeSoft.IO.Services.Configuration.ConversionStatistics.V1;
using PrecizeSoft.IO.Services.ServiceContracts.ConversionStatistics.V1;
using PrecizeSoft.IO.Services.MessageContracts.ConversionStatistics.V1;

namespace PrecizeSoft.IO.Services.Clients.ConversionStatistics.V1
{
    public partial class ServiceClient : ClientBase<IService>, IService
    {

        public ServiceClient() :
            base(new ServiceConfigurationManager().CreateBinding(),
                new EndpointAddress((new ServiceConfigurationManager().DefauldAddress)))
        {
        }

        public ServiceClient(EndpointAddress remoteAddress) :
                base(new ServiceConfigurationManager().CreateBinding(), remoteAddress)
        {
        }

        public ServiceClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public ServiceClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public ServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public ServiceClient(Binding binding, EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public GetDailyStatResponseMessage GetDailyStat(GetDailyStatMessage message)
        {
            return base.Channel.GetDailyStat(message);
        }

        public GetStatByFileCategoriesResponseMessage GetStatByFileCategories(GetStatByFileCategoriesMessage message)
        {
            return base.Channel.GetStatByFileCategories(message);
        }

        public GetSummaryStatResponseMessage GetSummaryStat(GetSummaryStatMessage message)
        {
            return base.Channel.GetSummaryStat(message);
        }
    }
}
