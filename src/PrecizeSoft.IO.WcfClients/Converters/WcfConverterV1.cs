using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using PrecizeSoft.IO.Services.Clients.Converter.V1;

namespace PrecizeSoft.IO.Converters
{
    public class WcfConverterV1 : IFileConverter
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

        public IEnumerable<string> SupportedFormatCollection => throw new NotImplementedException();

        public void Convert(string sourceFileName, string destinationFileName)
        {
            byte[] sourceFileBytes = File.ReadAllBytes(sourceFileName);

            byte[] destinationFileBytes = client.ConvertToPdf(sourceFileBytes, Path.GetExtension(sourceFileName));

            File.WriteAllBytes(destinationFileName, destinationFileBytes);
        }

        public Stream Convert(Stream sourceStream, string fileExtension)
        {
            throw new NotImplementedException();
        }

        public byte[] Convert(byte[] sourceBytes, string fileExtension)
        {
            return client.ConvertToPdf(sourceBytes, fileExtension);
        }
    }
}
