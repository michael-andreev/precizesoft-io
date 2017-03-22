using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.Threading;

namespace PrecizeSoft.IO.Converters
{
    /// <summary>
    /// Word documents to PDF converter. Using Microsoft Word for converting.
    /// </summary>
    public class WordToPdfConverter: OfficeInteropFileConverterBase
    {
        /// <summary>
        /// Returns a list of supported file formats
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<string> InitializeSupportedFormatCollection()
        {
            return new List<string>()
            { ".docx", ".doc", ".docm", ".dot", ".dotm", ".dotx", ".odt", ".wbk", ".wiz", ".rtf"};
        }

        /// <summary>
        /// Opens file from disk, converts it to PDF and saves to disk
        /// </summary>
        /// <param name="sourceFileName">Source file name</param>
        /// <param name="destinationFileName">Destination file name</param>
        protected override void ConvertFile(string sourceFileName, string destinationFileName)
        {
            Application wordApplication = new Application() { Visible = false };
            try
            {
                Document wordDocument = null;

                var confirmConversions = false;
                var readOnly = true;
                var addToRecentFiles = false;
                var passwordDocument = Type.Missing;
                var passwordTemplate = Type.Missing;
                var revert = true;
                var writePasswordDocument = Type.Missing;
                var writePasswordTemplate = Type.Missing;
                var format = Type.Missing;
                var encoding = Type.Missing;
                var visible = false;
                var openAndRepair = true;
                var documentDirection = Type.Missing;
                var noEncodingDialog = true;
                var xmlTransform = Type.Missing;

                wordDocument = wordApplication.Documents.Open(sourceFileName, confirmConversions,
                    readOnly, addToRecentFiles, passwordDocument, passwordTemplate, revert,
                    writePasswordDocument, writePasswordTemplate, format, encoding, visible,
                    openAndRepair, documentDirection, noEncodingDialog, xmlTransform);
                wordDocument.ExportAsFixedFormat(destinationFileName, WdExportFormat.wdExportFormatPDF);
            }
            finally
            {
                wordApplication.Quit(WdSaveOptions.wdDoNotSaveChanges);
            }
        }
    }
}
