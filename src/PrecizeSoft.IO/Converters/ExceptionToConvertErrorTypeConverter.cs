using PrecizeSoft.IO.Contracts.Converters;
using PrecizeSoft.IO.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public static class ExceptionToConvertErrorTypeConverter
    {
        public static ConvertErrorType ToConvertErrorType(this Exception e)
        {
            if (e is SourceBytesNullException)
            {
                return ConvertErrorType.FileBytesEmpty;
            }
            else if (e is FileExtensionNullException)
            {
                return ConvertErrorType.FileExtensionEmpty;
            }
            else if (e is InvalidFileExtensionException)
            {
                return ConvertErrorType.InvalidFileExtension;
            }
            else if (e is FormatNotSupportedException)
            {
                return ConvertErrorType.FormatNotSupported;
            }
            else
            {
                return ConvertErrorType.Other;
            }
        }
    }
}
