using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public static class ConverterFactoryMicrosoftOfficeExtensions
    {
        public static IFileConverter CreateWordToPdfConverter(this ConverterFactory factory)
        {
            return new WordToPdfConverter();
        }

        public static IFileConverter CreateExcelToPdfConverter(this ConverterFactory factory)
        {
            return new ExcelToPdfConverter();
        }

        public static IFileConverter CreatePowerPointToPdfConverter(this ConverterFactory factory)
        {
            return new PowerPointToPdfConverter();
        }

        public static IFileConverter CreateVisioToPdfConverter(this ConverterFactory factory)
        {
            return new VisioToPdfConverter();
        }
    }
}
