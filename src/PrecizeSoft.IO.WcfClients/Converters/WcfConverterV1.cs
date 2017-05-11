using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Linq;
using PrecizeSoft.IO.Wcf.Clients.Converter.V1;
using PrecizeSoft.IO.Wcf.MessageContracts.Converter.V1;
using PrecizeSoft.IO.Wcf.DataContracts.Converter.V1;
using System.Collections;

namespace PrecizeSoft.IO.Converters
{
    public class WcfConverterV1 : BytesFileConverter
    {
        protected ServiceClient client = null;

        public IDictionary CustomAttributes { get; set; } = null;

        public WcfConverterV1()
        {
            this.client = new ServiceClient();
        }

        public WcfConverterV1(Dictionary<string,string> customAttributes)
        {
            this.client = new ServiceClient();
            this.CustomAttributes = customAttributes;
        }

        public WcfConverterV1(EndpointAddress remoteAddress)
        {
            this.client = new ServiceClient(remoteAddress);
        }

        public WcfConverterV1(EndpointAddress remoteAddress, IDictionary customAttributes)
        {
            this.client = new ServiceClient(remoteAddress);
            this.CustomAttributes = customAttributes;
        }

        public WcfConverterV1(string endpointConfigurationName)
        {
            this.client = new ServiceClient(endpointConfigurationName);
        }

        public WcfConverterV1(string endpointConfigurationName, IDictionary customAttributes)
        {
            this.client = new ServiceClient(endpointConfigurationName);
            this.CustomAttributes = customAttributes;
        }

        public WcfConverterV1(string endpointConfigurationName, string remoteAddress)
        {
            this.client = new ServiceClient(endpointConfigurationName, remoteAddress);
        }

        public WcfConverterV1(string endpointConfigurationName, string remoteAddress, IDictionary customAttributes)
        {
            this.client = new ServiceClient(endpointConfigurationName, remoteAddress);
            this.CustomAttributes = customAttributes;
        }

        public WcfConverterV1(string endpointConfigurationName, EndpointAddress remoteAddress)
        {
            this.client = new ServiceClient(endpointConfigurationName, remoteAddress);
        }

        public WcfConverterV1(string endpointConfigurationName, EndpointAddress remoteAddress, IDictionary customAttributes)
        {
            this.client = new ServiceClient(endpointConfigurationName, remoteAddress);
            this.CustomAttributes = customAttributes;
        }

        public WcfConverterV1(Binding binding, EndpointAddress remoteAddress)
        {
            this.client = new ServiceClient(binding, remoteAddress);
        }

        public WcfConverterV1(Binding binding, EndpointAddress remoteAddress, IDictionary customAttributes)
        {
            this.client = new ServiceClient(binding, remoteAddress);
            this.CustomAttributes = customAttributes;
        }

        public override IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                return this.client.GetSupportedFormats(new GetSupportedFormatsMessage()).SupportedFormats;
            }
        }

        protected override byte[] InternalConvert(byte[] sourceBytes, string fileExtension)
        {
            List<CustomAttribute> customAttributes = null;

            if (this.CustomAttributes != null)
            {
                customAttributes = new List<CustomAttribute>();

                foreach (var p in this.CustomAttributes.Keys)
                    customAttributes.Add(new CustomAttribute {
                        Name = p.ToString(),
                        Value = this.CustomAttributes[p].ToString()
                    });
            }

            ConvertMessage message = new ConvertMessage()
            {
                FileBytes = sourceBytes,
                FileExtension = fileExtension,
                CustomAttributes = customAttributes
            };

            return client.Convert(message).FileBytes;
        }
    }
}