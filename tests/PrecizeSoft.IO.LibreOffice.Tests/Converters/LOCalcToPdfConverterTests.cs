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
    public class LOCalcToPdfConverterTests: IClassFixture<LOFixture>
    {
        [Theory]
        [InlineData(@"..\..\..\..\samples\sample.xlsx", @"results\sample.xlsx.pdf")]
        [InlineData(@"..\..\..\..\samples\sample.xls", @"results\sample.xls.pdf")]
        [InlineData(@"..\..\..\..\samples\sample.ods", @"results\sample.ods.pdf")]
        public void ConvertTest(string sourceFileName, string destinationFileName)
        {
            LOCalcToPdfConverter converter = new LOCalcToPdfConverter();

            converter.Convert(sourceFileName, destinationFileName);

            FileInfo fi = new FileInfo(destinationFileName);

            Assert.True(fi.Length > 0);
        }

        [Theory]
        [InlineData(@"..\..\..\..\samples\bad.xlsx", @"results\bad.xlsx.pdf")]
        [InlineData(@"..\..\..\..\samples\bad.xls", @"results\bad.xls.pdf")]
        [InlineData(@"..\..\..\..\samples\bad.ods", @"results\bad.ods.pdf")]
        public void ConvertBadFileTest(string sourceFileName, string destinationFileName)
        {
            LOCalcToPdfConverter converter = new LOCalcToPdfConverter();

            converter.Convert(sourceFileName, destinationFileName);

            FileInfo fi = new FileInfo(destinationFileName);

            Assert.True(fi.Length > 0);
        }
    }
}
