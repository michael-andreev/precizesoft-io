using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO.Converters;
using PrecizeSoft.IO.Services.Configuration.Converter.V1;
using PrecizeSoft.IO.Services.ServiceContracts.Converter.V1;
using PrecizeSoft.IO.Services.MessageContracts.Converter.V1;
using PrecizeSoft.IO.Services.FaultContracts.Converter.V1;

namespace PrecizeSoft.IO.Services.Implementation.Converter.V1
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFileConverter">Must have a default constructor, because the wcf service must have
    /// a default constructor too for scalability support</typeparam>
    [ServiceBehavior(Namespace = "http://io.precizesoft.com/converter/v1/", ConcurrencyMode = ConcurrencyMode.Multiple)]
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
            //config.AddServiceEndpoint(manager.CreateServiceEndpoint(address));

            config.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            config.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
        }

        public ConvertResponse Convert(MessageContracts.Converter.V1.Convert message)
        {
            FaultExceptionFactory faultFactory = new FaultExceptionFactory();

            try
            {
                return new ConvertResponse() { FileBytes = this.converter.Convert(message.FileBytes, message.FileExtension) };
            }
            catch (SourceBytesNullException)
            {
                throw faultFactory.CreateFileBytesEmptyFaultException();
            }
            catch(FileExtensionNullException)
            {
                throw faultFactory.CreateFileExtensionEmptyFaultException();
            }
            catch(InvalidFileExtensionException)
            {
                throw faultFactory.CreateInvalidFileExtensionFaultException();
            }
            catch (FormatNotSupportedException)
            {
                throw faultFactory.CreateFormatNotSupportedFaultException();
            }
        }

        public GetSupportedFormatsResponse GetSupportedFormats()
        {
            return new GetSupportedFormatsResponse
            {
                SupportedFormats = this.converter.SupportedFormatCollection
            };
        }
    }
}
