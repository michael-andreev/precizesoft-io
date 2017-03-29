using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using PrecizeSoft.IO.Services.Configuration.Converter.V1;
using PrecizeSoft.IO.Services.ServiceContracts.Converter.V1;
using PrecizeSoft.IO.Services.MessageContracts.Converter.V1;
using System.ServiceModel.Description;
using System.IO;

namespace PrecizeSoft.IO.Services.Clients.Converter.V1
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

        public ConvertResponse Convert(MessageContracts.Converter.V1.Convert message)
        {
            return base.Channel.Convert(message);
        }

        public GetSupportedFormatsResponse GetSupportedFormats()
        {
            return base.Channel.GetSupportedFormats();
        }
    }
}
