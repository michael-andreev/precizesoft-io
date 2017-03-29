using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public static class ConverterFactoryLOExtensions
    {
        private static void ConfigureEnvironment(string libreOfficeUnoPath)
        {
            if (string.IsNullOrEmpty(libreOfficeUnoPath))
            {
                if (!LOComponentLoader.Instance.IsAvailable)
                {
                    LOEnvironment.ConfigureFromRegistry();
                }
            }
            else
            {
                LOEnvironment.ConfigureByUnoPath(libreOfficeUnoPath);
            }
        }
        public static IFileConverter CreateLOToPdfConverter(this ConverterFactory factory)
        {
            return CreateLOToPdfConverter(factory, false, null);
        }
        public static IFileConverter CreateLOToPdfConverter(this ConverterFactory factory, bool configureEnvironment)
        {
            return CreateLOToPdfConverter(factory, configureEnvironment, null);
        }
        public static IFileConverter CreateLOToPdfConverter(this ConverterFactory factory, bool configureEnvironment, string customLibreOfficeUnoPath)
        {
            if (configureEnvironment) ConfigureEnvironment(customLibreOfficeUnoPath);
            return new LOToPdfConverter();
        }

        public static IFileConverter CreateLOToPdfCliConverter(this ConverterFactory factory)
        {
            return new LOToPdfCliConverter();
        }
        public static IFileConverter CreateLOToPdfCliConverter(this ConverterFactory factory, string libreOfficePath)
        {
            return new LOToPdfCliConverter(libreOfficePath);
        }

        public static IFileConverter CreateLOWriterToPdfConverter(this ConverterFactory factory)
        {
            return CreateLOWriterToPdfConverter(factory, false, null);
        }
        public static IFileConverter CreateLOWriterToPdfConverter(this ConverterFactory factory, bool configureEnvironment)
        {
            return CreateLOWriterToPdfConverter(factory, configureEnvironment, null);
        }
        public static IFileConverter CreateLOWriterToPdfConverter(this ConverterFactory factory, bool configureEnvironment, string customLibreOfficeUnoPath)
        {
            if (configureEnvironment) ConfigureEnvironment(customLibreOfficeUnoPath);
            return new LOWriterToPdfConverter();
        }

        public static IFileConverter CreateLOCalcToPdfConverter(this ConverterFactory factory)
        {
            return CreateLOCalcToPdfConverter(factory, false, null);
        }
        public static IFileConverter CreateLOCalcToPdfConverter(this ConverterFactory factory, bool configureEnvironment)
        {
            return CreateLOCalcToPdfConverter(factory, configureEnvironment, null);
        }
        public static IFileConverter CreateLOCalcToPdfConverter(this ConverterFactory factory, bool configureEnvironment, string customLibreOfficeUnoPath)
        {
            if (configureEnvironment) ConfigureEnvironment(customLibreOfficeUnoPath);
            return new LOCalcToPdfConverter();
        }

        public static IFileConverter CreateLOImpressToPdfConverter(this ConverterFactory factory)
        {
            return CreateLOImpressToPdfConverter(factory, false, null);
        }
        public static IFileConverter CreateLOImpressToPdfConverter(this ConverterFactory factory, bool configureEnvironment)
        {
            return CreateLOImpressToPdfConverter(factory, configureEnvironment, null);
        }
        public static IFileConverter CreateLOImpressToPdfConverter(this ConverterFactory factory, bool configureEnvironment, string customLibreOfficeUnoPath)
        {
            if (configureEnvironment) ConfigureEnvironment(customLibreOfficeUnoPath);
            return new LOImpressToPdfConverter();
        }
    }
}
