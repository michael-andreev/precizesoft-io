using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Threading;

namespace PrecizeSoft.IO.Converters
{
    /// <summary>
    /// Excel documents to PDF converter. Using Microsoft Excel for converting.
    /// </summary>
    public class ExcelToPdfConverter: OfficeInteropFileConverterBase
    {
        /// <summary>
        /// Returns a list of supported file formats
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<string> InitializeSupportedFormatCollection()
        {
            return new List<string>()
            { ".csv", ".dqy", ".iqy", ".odc", ".ods", ".oqy", ".rqy", ".slk", ".xla", ".xlam", ".xld",
                ".xlk", ".xll", ".xlm", ".xls", ".xlsb", ".xlshtml", ".xlsm", ".xlsmhtml", ".xlsx", ".xlt",
                ".xlthtml", ".xltm", ".xltx", ".xlw", ".xlxml"};
        }

        /// <summary>
        /// Opens file from disk, converts it to PDF and saves to disk
        /// </summary>
        /// <param name="sourceFileName">Source file name</param>
        /// <param name="destinationFileName">Destination file name</param>
        protected override void ConvertFile(string sourceFileName, string destinationFileName)
        {
            Application excelApplication = new Application() { Visible = false };
            try
            {
                Workbook excelWorkbook = null;

                var updateLinks = 2; //Never update links for this workbook on opening
                var readOnly = true;
                var format = Type.Missing;
                var password = Type.Missing;
                var writeResPassword = Type.Missing;
                var ignoreReadOnlyRecommended = true;
                var origin = Type.Missing;
                var delimiter = Type.Missing;
                var editable = false;
                var notify = false;
                var converter = Type.Missing;
                var addToMru = false;
                var local = Type.Missing;
                var corruptLoad = Type.Missing;

                excelWorkbook = excelApplication.Workbooks.Open(sourceFileName, updateLinks,
                    readOnly, format, password, writeResPassword, ignoreReadOnlyRecommended,
                    origin, delimiter, editable, notify, converter, addToMru, local, corruptLoad);
                excelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, destinationFileName);
            }
            finally
            {
                excelApplication.Quit();
            }
        }
    }
}
