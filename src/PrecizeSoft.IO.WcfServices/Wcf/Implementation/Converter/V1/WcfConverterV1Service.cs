using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO.Converters;
using PrecizeSoft.IO.Wcf.Configuration.Converter.V1;
using PrecizeSoft.IO.Wcf.ServiceContracts.Converter.V1;
using PrecizeSoft.IO.Wcf.MessageContracts.Converter.V1;
using PrecizeSoft.IO.Wcf.FaultContracts.Converter.V1;
using System.ServiceModel.Channels;
using PrecizeSoft.IO.Contracts.Converters;

namespace PrecizeSoft.IO.Wcf.Implementation.Converter.V1
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFileConverter">Must have a default constructor, because the wcf service must have
    /// a default constructor too for scalability support</typeparam>
    [ServiceBehavior(Namespace = "http://io.precizesoft.com/Converter/V1/", ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WcfConverterV1Service<TFileConverter, TLogger> : IService
        where TFileConverter : IFileConverter, new()
        where TLogger: ILogService, new()
    {
        /* In future needs to implement Dependency Injecion with 3rd party libraries (WCF doesn't support DI from
         * the box)
         */

        protected TFileConverter converter = new TFileConverter();
        protected TLogger logger = new TLogger();

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

            if (this.logger != null)
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
                    CustomAttributes = message.CustomAttributes?.ToDictionary(p => p.Name, p => p.Value)
                };

                this.logger.LogRequest(requestLog);
            }

            byte[] fileBytes = null;
            Exception convertException = null;

            try
            {
                fileBytes = this.converter.Convert(message.FileBytes, message.FileExtension);
            }
            catch (Exception e)
            {
                convertException = e;
            }

            if (this.logger != null)
            {
                ResponseLog responseLog = new ResponseLog
                {
                    RequestId = requestId,
                    ResponseDateUtc = DateTime.UtcNow,
                    ResultFileSize = fileBytes?.Length,
                    ErrorType = convertException.ToConvertErrorType()
                };

                this.logger.LogResponse(responseLog);
            }

            if (convertException == null)
            {
                return new ConvertResponseMessage
                {
                    RequestId = requestId,
                    FileBytes = fileBytes
                };
            }
            else
            {
                FaultException fault;

                try
                {
                    fault = convertException.ToV1FaultConverter();
                }
                catch (NotSupportedException)
                {
                    throw convertException;
                }

                throw fault;
            }
        }

        public GetSupportedFormatsResponseMessage GetSupportedFormats(GetSupportedFormatsMessage message)
        {
            return new GetSupportedFormatsResponseMessage
            {
                SupportedFormats = this.converter.SupportedFormatCollection
            };
        }
    }
}
