using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO.Wcf.FaultContracts.Converter.V1;
using PrecizeSoft.IO.Contracts.Converters;
using PrecizeSoft.IO.Converters;

namespace PrecizeSoft.IO.Wcf.Implementation.Converter.V1
{
    internal class FaultExceptionFactory
    {
        public FaultException CreateFromException(Exception e)
        {
            if (e is SourceBytesNullException)
            {
                return this.CreateFileBytesEmptyFaultException();
            }
            else if (e is FileExtensionNullException)
            {
                return this.CreateFileExtensionEmptyFaultException();
            }
            else if (e is InvalidFileExtensionException)
            {
                return this.CreateInvalidFileExtensionFaultException();
            }
            else if (e is FormatNotSupportedException)
            {
                return this.CreateFormatNotSupportedFaultException();
            }
            else
            {
                throw new NotSupportedException("Unknown exception", e);
            }
        }

        public FaultException CreateFormatNotSupportedFaultException()
        {
            return new FaultException<FormatNotSupported>(new FormatNotSupported(),
                "Input file format is not supported. Call GetSupportedFormatCollection operation to get the list of supported file formats.");
        }

        public FaultException CreateFileBytesEmptyFaultException()
        {
            return new FaultException<FileBytesEmpty>(new FileBytesEmpty(),
                "File bytes is empty.");
        }

        public FaultException CreateFileExtensionEmptyFaultException()
        {
            return new FaultException<FileExtensionEmpty>(new FileExtensionEmpty(),
                "File extension is empty.");
        }

        public FaultException CreateInvalidFileExtensionFaultException()
        {
            return new FaultException<InvalidFileExtension>(new InvalidFileExtension(),
                "File extension is invalid.");
        }
    }
}
