using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Converters
{
    public class FileConverterRouter : IFileConverter
    {
        public FileConverterRouter()
        {

        }

        public FileConverterRouter(IEnumerable<IFileConverter> converterCollection)
        {
            this.converterCollection = converterCollection.ToList();
        }

        protected IList<IFileConverter> converterCollection = new List<IFileConverter>();
        public IList<IFileConverter> ConverterCollection
        {
            get
            {
                return this.converterCollection;
            }
        }

        public IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                var formats =
                    (from P in this.converterCollection
                     from Q in P.SupportedFormatCollection
                     select Q).Distinct().OrderBy((Q) => { return Q; });
                return formats;
            }
        }

        public Stream Convert(Stream sourceStream, string fileExtension)
        {
            this.ValidateFileExtension(fileExtension);

            return this.GetConverterByFileExtension(fileExtension).Convert(sourceStream, fileExtension);
        }

        public byte[] Convert(byte[] sourceBytes, string fileExtension)
        {
            this.ValidateFileExtension(fileExtension);

            return this.GetConverterByFileExtension(fileExtension).Convert(sourceBytes, fileExtension);
        }

        public void Convert(string sourceFileName, string destinationFileName)
        {
            string sourceFileNameExtension = Path.GetExtension(sourceFileName).ToLower();

            this.ValidateFileExtension(sourceFileNameExtension);

            this.GetConverterByFileExtension(sourceFileNameExtension).Convert(sourceFileName, destinationFileName);
        }

        protected void ValidateFileExtension(string fileExtension)
        {
            if (!PathHelper.IsValidExtension(fileExtension))
                throw new ArgumentException("File extension in invalid.");
        }

        protected IFileConverter GetConverterByFileExtension(string extension)
        {
            IFileConverter converter =
                (from P in this.converterCollection
                 where P.SupportedFormatCollection.Contains(extension)
                 select P).FirstOrDefault();

            if (converter == null)
            {
                throw new Exception(string.Format("File format \"{0}\" not supported.", extension));
            }

            return converter;
        }
    }
}
