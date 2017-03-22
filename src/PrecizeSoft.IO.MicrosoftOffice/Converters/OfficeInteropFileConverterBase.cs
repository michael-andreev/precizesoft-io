using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace PrecizeSoft.IO.Converters
{
    public abstract class OfficeInteropFileConverterBase : IFileConverter
    {
        protected IEnumerable<string> supportedFormatCollection;
        public IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                return this.supportedFormatCollection;
            }
        }

        protected abstract IEnumerable<string> InitializeSupportedFormatCollection();

        public OfficeInteropFileConverterBase()
        {
            this.supportedFormatCollection = this.InitializeSupportedFormatCollection();
        }

        public Stream Convert(Stream sourceStream, string fileExtension)
        {
            if (sourceStream == null)
                throw new ArgumentNullException("sourceStream");

            string sourceTempFileName = null;
            //Adding extension .pdf to the destination filename is required because office
            //can add extension manually and filename would be wrong. Bug detected on Microsoft Excel
            string destinationTempFileName = Path.GetTempFileName() + ".pdf";

            #region Converting file
            if (sourceStream is FileStream)
            {
                this.Convert(((FileStream)sourceStream).Name, destinationTempFileName);
            }
            else
            {
                sourceTempFileName = Path.GetTempFileName() + fileExtension;
                using (FileStream sourceFileStream = File.OpenWrite(sourceTempFileName))
                {
                    sourceStream.CopyTo(sourceFileStream);
                }
                this.Convert(sourceTempFileName, destinationTempFileName);
            }
            #endregion

            MemoryStream destinationMemoryStream = new MemoryStream();

            #region Copying result file to memory stream
            using (FileStream destinationFileStream = File.OpenRead(destinationTempFileName))
            {
                destinationFileStream.CopyTo(destinationMemoryStream);
            }
            #endregion

            #region Deleting temporary files
            {
                //Wait while Microsoft Office releasing files
                GC.WaitForPendingFinalizers();
                //Collect garbage from Microsoft Office
                GC.Collect();
                //Delete temp files
                if (!string.IsNullOrEmpty(sourceTempFileName))
                {
                    File.Delete(sourceTempFileName);
                }
                File.Delete(destinationTempFileName);
            }
            #endregion

            return destinationMemoryStream;
        }

        public byte[] Convert(byte[] sourceBytes, string fileExtension)
        {
            if (sourceBytes == null)
                throw new ArgumentNullException("sourceBytes");

            byte[] result;

            using (MemoryStream sourceMemomyStream = new MemoryStream(sourceBytes))
            {
                using (Stream destinationStream = this.Convert(sourceMemomyStream, fileExtension))
                {
                    result = new byte[destinationStream.Length];

                    destinationStream.Position = 0;
                    destinationStream.Read(result, 0, result.Length);
                }
            }

            return result;
        }

        public void Convert(string sourceFileName, string destinationFileName)
        {
            if (string.IsNullOrWhiteSpace(sourceFileName))
                throw new ArgumentNullException("sourceFileName");

            if (string.IsNullOrWhiteSpace(destinationFileName))
                throw new ArgumentNullException("destinationFileName");

            this.ConvertFile(sourceFileName, destinationFileName);
        }

        protected abstract void ConvertFile(string sourceFileName, string destinationFileName);
    }
}
