using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using PrecizeSoft.IO.Services.ServiceContracts.Converter.V1;

namespace PrecizeSoft.IO.Services.Configuration.Converter.V1
{
    public class ServiceConfigurationManager
    {
        protected readonly string defaultAddress = "http://api.getpdf.online/converter/v1/service.svc";

        public string DefauldAddress
        {
            get
            {
                return this.defaultAddress;
            }
        }

        public Binding CreateBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Namespace = "http://api.getpdf.online/converter/v1/";
            binding.MaxReceivedMessageSize = 100 * 1024 * 1024;
            //binding.MessageEncoding = WSMessageEncoding.Mtom;

            return binding;
        }

        public Binding CreateRestBinding()
        {
            WebHttpBinding binding = new WebHttpBinding();
            binding.Namespace = "http://api.getpdf.online/converter/v1/";
            binding.MaxReceivedMessageSize = 100 * 1024 * 1024;

            return binding;
        }

        public ServiceEndpoint CreateServiceEndpoint(string address)
        {
            //ContractDescription contract = new ContractDescription(typeof(IService).ToString());
            ContractDescription contract = ContractDescription.GetContract(typeof(IService));
            ServiceEndpoint endpoint = new ServiceEndpoint(contract, this.CreateBinding(), new EndpointAddress(address));
            
            return endpoint;
        }

        public ServiceEndpoint CreateRestServiceEndpoint(string address)
        {
            //ContractDescription contract = new ContractDescription(typeof(IService).ToString());
            ContractDescription contract = ContractDescription.GetContract(typeof(IService));
            ServiceEndpoint endpoint = new ServiceEndpoint(contract, this.CreateRestBinding(), new EndpointAddress(address));
            endpoint.EndpointBehaviors.Add(new WebHttpBehavior());

            return endpoint;
        }
    }
}
