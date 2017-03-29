using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO.Services.FaultContracts.Converter.V1;

namespace PrecizeSoft.IO.Services.Implementation.Converter.V1
{
    public class FaultExceptionFactory
    {
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
