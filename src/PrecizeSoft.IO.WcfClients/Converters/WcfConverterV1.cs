using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using PrecizeSoft.IO.Services.Clients.Converter.V1;
using PrecizeSoft.IO.Services.MessageContracts.Converter.V1;

namespace PrecizeSoft.IO.Converters
{
    public class WcfConverterV1 : BytesFileConverter
    {
        protected ServiceClient client = null;

        public WcfConverterV1()
        {
            this.client = new ServiceClient();
        }

        public WcfConverterV1(EndpointAddress remoteAddress)
        {
            this.client = new ServiceClient(remoteAddress);
        }

        public WcfConverterV1(string endpointConfigurationName)
        {
            this.client = new ServiceClient(endpointConfigurationName);
        }

        public WcfConverterV1(string endpointConfigurationName, string remoteAddress)
        {
            this.client = new ServiceClient(endpointConfigurationName, remoteAddress);
        }

        public WcfConverterV1(string endpointConfigurationName, EndpointAddress remoteAddress)
        {
            this.client = new ServiceClient(endpointConfigurationName, remoteAddress);
        }

        public WcfConverterV1(Binding binding, EndpointAddress remoteAddress)
        {
            this.client = new ServiceClient(binding, remoteAddress);
        }

        public override IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                return this.client.GetSupportedFormats().SupportedFormats;
            }
        }

        protected override byte[] InternalConvert(byte[] sourceBytes, string fileExtension)
        {
            Services.MessageContracts.Converter.V1.Convert message = new Services.MessageContracts.Converter.V1.Convert()
            {
                FileBytes = sourceBytes,
                FileExtension = fileExtension
            };
            return client.Convert(message).FileBytes;
        }
    }
}