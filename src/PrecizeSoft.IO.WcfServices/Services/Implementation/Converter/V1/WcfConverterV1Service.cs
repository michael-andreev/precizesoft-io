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
using System.ServiceModel.Channels;

namespace PrecizeSoft.IO.Services.Implementation.Converter.V1
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFileConverter">Must have a default constructor, because the wcf service must have
    /// a default constructor too for scalability support</typeparam>
    [ServiceBehavior(Namespace = "http://io.precizesoft.com/Converter/V1/", ConcurrencyMode = ConcurrencyMode.Multiple)]
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

        public ConvertResponseMessage Convert(MessageContracts.Converter.V1.ConvertMessage message)
        {
            Guid requestId = Guid.NewGuid();

            if (this.LogRequestEvent != null)
            {
                RemoteEndpointMessageProperty endpointProperty =
                  OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

                RequestLog requestLog = new RequestLog
                {
                    RequestId = requestId,
                    RequestDateUtc = DateTime.UtcNow,
                    SenderIp = endpointProperty.Address,
                    FileExtension = message.FileExtension,
                    FileSize = message.FileBytes.Length,
                    CustomAttributes = message.CustomAttributes.ToDictionary(p => p.Name, p => p.Value)
                };

                this.LogRequestEvent(requestLog);
            }

            FaultExceptionFactory faultFactory = new FaultExceptionFactory();

            byte[] fileBytes = null;
            FaultException faultException = null;

            try
            {
                fileBytes = this.converter.Convert(message.FileBytes, message.FileExtension);
            }
            catch (SourceBytesNullException)
            {
                faultException = faultFactory.CreateFileBytesEmptyFaultException();
            }
            catch(FileExtensionNullException)
            {
                faultException = faultFactory.CreateFileExtensionEmptyFaultException();
            }
            catch(InvalidFileExtensionException)
            {
                faultException = faultFactory.CreateInvalidFileExtensionFaultException();
            }
            catch (FormatNotSupportedException)
            {
                faultException = faultFactory.CreateFormatNotSupportedFaultException();
            }

            if (this.LogResponseEvent != null)
            {
                ResponseLog responseLog = new ResponseLog
                {
                    RequestId = requestId,
                    ResponseDateUtc = DateTime.UtcNow,
                    ResultFileSize = fileBytes?.Length,
                    Fault = faultException
                };

                this.LogResponseEvent(responseLog);
            }

            if (faultException == null)
            {
                return new ConvertResponseMessage
                {
                    RequestId = requestId,
                    FileBytes = fileBytes
                };
            }
            else
            {
                throw faultException;
            }
        }

        public GetSupportedFormatsResponseMessage GetSupportedFormats(GetSupportedFormatsMessage message)
        {
            return new GetSupportedFormatsResponseMessage
            {
                SupportedFormats = this.converter.SupportedFormatCollection
            };
        }

        public delegate void LogRequestEventHandler(RequestLog request);

        public delegate void LogResponseEventHandler(ResponseLog response);

        public event LogRequestEventHandler LogRequestEvent;

        public event LogResponseEventHandler LogResponseEvent;
    }
}
