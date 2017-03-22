using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO.Converters;
using PrecizeSoft.IO.Services.Configuration.Converter.V1;
using PrecizeSoft.IO.Services.ServiceContracts.Converter.V1;

namespace PrecizeSoft.IO.Services.Implementation.Converter.V1
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFileConverter">Must have a default constructor, because the wcf service must have
    /// a default constructor too for scalability support</typeparam>
    [ServiceBehavior(Namespace = "http://api.getpdf.online/converter/v1/", ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WcfConverterV1Service<TFileConverter> : IService
        where TFileConverter : IFileConverter, new()
    {
        protected TFileConverter converter = new TFileConverter();

        public static void Configure(ServiceConfiguration config)
        {
            string address = config.BaseAddresses.First().AbsoluteUri;

            ServiceConfigurationManager manager = new ServiceConfigurationManager();

            ContractDescription contract = ContractDescription.GetContract(typeof(IService));
            ServiceEndpoint endpoint = new ServiceEndpoint(contract, manager.CreateBinding(), new EndpointAddress(address));

            config.AddServiceEndpoint(endpoint);
            config.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            config.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
        }

        public byte[] ConvertToPdf(byte[] source, string fileExtension)
        {
            return this.converter.Convert(source, fileExtension);
        }

        public string Test()
        {
            return "Hello!";
        }
    }
}
