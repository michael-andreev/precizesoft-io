using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public abstract class BytesFileConverter : IFileConverter
    {
        public abstract IEnumerable<string> SupportedFormatCollection { get; }

        public void Convert(string sourceFileName, string destinationFileName)
        {
            new FileConverterValidator(this.SupportedFormatCollection)
                .ValidateConvertParameters(sourceFileName, destinationFileName);

            byte[] sourceFileBytes = File.ReadAllBytes(sourceFileName);

            byte[] destinationFileBytes = this.InternalConvert(sourceFileBytes, Path.GetExtension(sourceFileName));

            File.WriteAllBytes(destinationFileName, destinationFileBytes);
        }

        public Stream Convert(Stream sourceStream, string fileExtension)
        {
            new FileConverterValidator(this.SupportedFormatCollection)
                .ValidateConvertParameters(sourceStream, fileExtension);

            byte[] sourceFileBytes = new byte[sourceStream.Length];
            sourceStream.Position = 0;
            sourceStream.Read(sourceFileBytes, 0, (int)sourceStream.Length);

            byte[] destinationFileBytes = this.InternalConvert(sourceFileBytes, fileExtension);

            MemoryStream destinationMemoryStream = new MemoryStream(destinationFileBytes);

            return destinationMemoryStream;
        }

        public byte[] Convert(byte[] sourceBytes, string fileExtension)
        {
            new FileConverterValidator(this.SupportedFormatCollection)
                .ValidateConvertParameters(sourceBytes, fileExtension);

            return this.InternalConvert(sourceBytes, fileExtension);
        }

        protected abstract byte[] InternalConvert(byte[] sourceBytes, string fileExtension);
    }
}
