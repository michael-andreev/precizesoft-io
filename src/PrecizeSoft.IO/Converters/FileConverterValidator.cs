using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public class FileConverterValidator : IFileConverterValidator
    {
        protected IEnumerable<string> supportedFormatCollection;

        public FileConverterValidator(IEnumerable<string> supportedFormatCollection)
        {
            this.supportedFormatCollection = supportedFormatCollection;
        }

        public void ValidateConvertParameters(string sourceFileName, string destinationFileName)
        {
            if (string.IsNullOrEmpty(sourceFileName))
                throw new SourceFileNameNullException("sourceFileName");

            if (string.IsNullOrEmpty(destinationFileName))
                throw new DestinationFileNameNullException("destinationFileName");

            string sourceFileNameExtension = Path.GetExtension(sourceFileName);

            this.ValidateFileExtension(sourceFileNameExtension);
        }

        public void ValidateConvertParameters(Stream sourceStream, string fileExtension)
        {
            if (sourceStream == null)
                throw new SourceStreamNullException("sourceStream");

            this.ValidateFileExtension(fileExtension);
        }

        public void ValidateConvertParameters(byte[] sourceBytes, string fileExtension)
        {
            if (sourceBytes == null)
                throw new SourceBytesNullException("sourceBytes");

            this.ValidateFileExtension(fileExtension);
        }

        protected void ValidateFileExtension(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                throw new FileExtensionNullException("fileExtension");

            if (!PathHelper.IsValidExtension(fileExtension))
                throw new InvalidFileExtensionException();

            if (!this.supportedFormatCollection.Contains(fileExtension.ToLower()))
            {
                throw new FormatNotSupportedException();
            }
        }
    }
}
