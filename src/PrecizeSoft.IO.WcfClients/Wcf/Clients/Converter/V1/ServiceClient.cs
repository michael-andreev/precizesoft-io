using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using PrecizeSoft.IO.Wcf.Configuration.Converter.V1;
using PrecizeSoft.IO.Wcf.ServiceContracts.Converter.V1;
using PrecizeSoft.IO.Wcf.MessageContracts.Converter.V1;

namespace PrecizeSoft.IO.Wcf.Clients.Converter.V1
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

        public ConvertResponseMessage Convert(MessageContracts.Converter.V1.ConvertMessage message)
        {
            return base.Channel.Convert(message);
        }

        public GetSupportedFormatsResponseMessage GetSupportedFormats(GetSupportedFormatsMessage message)
        {
            return base.Channel.GetSupportedFormats(message);
        }
    }
}
