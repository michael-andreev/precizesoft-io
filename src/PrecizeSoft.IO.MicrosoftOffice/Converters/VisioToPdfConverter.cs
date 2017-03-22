using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Visio;
using System.Threading;

namespace PrecizeSoft.IO.Converters
{
    /// <summary>
    /// Word documents to PDF converter. Using Microsoft Word for converting.
    /// </summary>
    public class VisioToPdfConverter: OfficeInteropFileConverterBase
    {
        /// <summary>
        /// Returns a list of supported file formats
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<string> InitializeSupportedFormatCollection()
        {
            return new List<string>()
                { ".vdw", ".vdx", ".vsd", ".vsdm", ".vsdx", ".vss", ".vssm", ".vssx", ".vst", ".vstm", ".vstx", ".vsx", ".vtx" };
        }

        /// <summary>
        /// Opens file from disk, converts it to PDF and saves to disk
        /// </summary>
        /// <param name="sourceFileName">Source file name</param>
        /// <param name="destinationFileName">Destination file name</param>
        protected override void ConvertFile(string sourceFileName, string destinationFileName)
        {
            Application visioApplication = new Application() { Visible = false };
            try
            {
                Document visioDocument = null;

                visioDocument = visioApplication.Documents.OpenEx(sourceFileName, (short)VisOpenSaveArgs.visOpenRO
                    + (short)VisOpenSaveArgs.visOpenMacrosDisabled + (short)VisOpenSaveArgs.visOpenDeclineAutoRefresh
                    + (short)VisOpenSaveArgs.visOpenDontList + (short)VisOpenSaveArgs.visOpenHidden);
                visioDocument.ExportAsFixedFormat(VisFixedFormatTypes.visFixedFormatPDF, destinationFileName,
                    VisDocExIntent.visDocExIntentPrint, VisPrintOutRange.visPrintAll);
            }
            finally
            {
                visioApplication.Quit();
            }
        }
    }
}
