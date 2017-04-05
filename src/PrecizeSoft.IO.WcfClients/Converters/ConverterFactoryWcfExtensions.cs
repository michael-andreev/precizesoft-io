using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public static class ConverterFactoryWcfExtensions
    {
        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory)
        {
            return new WcfConverterV1();
        }

        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, EndpointAddress remoteAddress)
        {
            return new WcfConverterV1(remoteAddress);
        }

        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, EndpointAddress remoteAddress, IDictionary customAttributes)
        {
            return new WcfConverterV1(remoteAddress, customAttributes);
        }


        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, string endpointConfigurationName)
        {
            return new WcfConverterV1(endpointConfigurationName);
        }

        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, string endpointConfigurationName, IDictionary customAttributes)
        {
            return new WcfConverterV1(endpointConfigurationName, customAttributes);
        }

        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, string endpointConfigurationName, string remoteAddress)
        {
            return new WcfConverterV1(endpointConfigurationName, remoteAddress);
        }

        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, string endpointConfigurationName, string remoteAddress, IDictionary customAttributes)
        {
            return new WcfConverterV1(endpointConfigurationName, remoteAddress, customAttributes);
        }

        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, string endpointConfigurationName, EndpointAddress remoteAddress)
        {
            return new WcfConverterV1(endpointConfigurationName, remoteAddress);
        }

        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, string endpointConfigurationName, EndpointAddress remoteAddress, IDictionary customAttributes)
        {
            return new WcfConverterV1(endpointConfigurationName, remoteAddress, customAttributes);
        }

        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, Binding binding, EndpointAddress remoteAddress)
        {
            return new WcfConverterV1(binding, remoteAddress);
        }

        public static IFileConverter CreateWcfConverterV1(this ConverterFactory factory, Binding binding, EndpointAddress remoteAddress, IDictionary customAttributes)
        {
            return new WcfConverterV1(binding, remoteAddress, customAttributes);
        }
    }
}
