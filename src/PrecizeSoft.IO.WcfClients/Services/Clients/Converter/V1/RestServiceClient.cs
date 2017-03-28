using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using PrecizeSoft.IO.Services.Configuration.Converter.V1;
using PrecizeSoft.IO.Services.ServiceContracts.Converter.V1;
using System.ServiceModel.Description;
using System.IO;

namespace PrecizeSoft.IO.Services.Clients.Converter.V1
{
    public partial class RestServiceClient : ClientBase<IService>, IService
    {

        public RestServiceClient() :
            base(new ServiceConfigurationManager().CreateRestBinding(),
                new EndpointAddress((new ServiceConfigurationManager().DefauldAddress)))
        {
            this.Endpoint.EndpointBehaviors.Add(new WebHttpBehavior());
        }

        public RestServiceClient(EndpointAddress remoteAddress) :
                base(new ServiceConfigurationManager().CreateRestBinding(), remoteAddress)
        {
            this.Endpoint.EndpointBehaviors.Add(new WebHttpBehavior());
        }

        public RestServiceClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
            this.Endpoint.EndpointBehaviors.Add(new WebHttpBehavior());
        }

        public RestServiceClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
            this.Endpoint.EndpointBehaviors.Add(new WebHttpBehavior());
        }

        public RestServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
            this.Endpoint.EndpointBehaviors.Add(new WebHttpBehavior());
        }

        public RestServiceClient(Binding binding, EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
            this.Endpoint.EndpointBehaviors.Add(new WebHttpBehavior());
        }

        public ConvertResultMessage Convert(ConvertMessage message)
        {
            return base.Channel.Convert(message);
        }

        public string Test()
        {
            return base.Channel.Test();
        }
    }
}
