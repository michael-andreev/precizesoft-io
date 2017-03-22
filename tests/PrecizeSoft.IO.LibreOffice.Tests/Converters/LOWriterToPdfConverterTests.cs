using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PrecizeSoft.IO.Converters;
using System.IO;

namespace PrecizeSoft.IO.Tests.Converters
{
    public class LOWriterToPdfConverterTests: IClassFixture<LOFixture>
    {
        [Theory]
        [InlineData(@"..\..\..\..\samples\sample.docx", @"results\sample.docx.pdf")]
        [InlineData(@"..\..\..\..\samples\sample.doc", @"results\sample.doc.pdf")]
        [InlineData(@"..\..\..\..\samples\sample.rtf", @"results\sample.rtf.pdf")]
        [InlineData(@"..\..\..\..\samples\sample.odt", @"results\sample.odt.pdf")]
        public void ConvertTest(string sourceFileName, string destinationFileName)
        {
            LOWriterToPdfConverter converter = new LOWriterToPdfConverter();

            converter.Convert(sourceFileName, destinationFileName);

            FileInfo fi = new FileInfo(destinationFileName);

            Assert.True(fi.Length > 0);
        }

        [Theory]
        [InlineData(@"..\..\..\..\samples\bad.docx", @"results\bad.docx.pdf")]
        [InlineData(@"..\..\..\..\samples\bad.doc", @"results\bad.doc.pdf")]
        [InlineData(@"..\..\..\..\samples\bad.rtf", @"results\bad.rtf.pdf")]
        [InlineData(@"..\..\..\..\samples\bad.odt", @"results\bad.odt.pdf")]
        public void ConvertBadFileTest(string sourceFileName, string destinationFileName)
        {
            LOWriterToPdfConverter converter = new LOWriterToPdfConverter();

            converter.Convert(sourceFileName, destinationFileName);

            FileInfo fi = new FileInfo(destinationFileName);

            Assert.True(fi.Length > 0);
        }

        [Theory]
        [InlineData(@"..\..\..\..\samples\sample.docx", @"results\sampleBytes.docx.pdf")]
        public void ConvertBytesTest(string sourceFileName, string destinationFileName)
        {
            byte[] sourceFileBytes = File.ReadAllBytes(sourceFileName);
            string sourceFileExtension = Path.GetExtension(sourceFileName);

            LOWriterToPdfConverter converter = new LOWriterToPdfConverter();

            File.WriteAllBytes(destinationFileName, converter.Convert(sourceFileBytes, sourceFileExtension));

            FileInfo fi = new FileInfo(destinationFileName);

            Assert.True(fi.Length > 0);
        }
    }
}
