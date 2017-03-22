using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.PowerPoint;
using System.Threading;
using Microsoft.Office.Core;

namespace PrecizeSoft.IO.Converters
{
    /// <summary>
    /// PowerPoint documents to PDF converter. Using Microsoft PowerPoint for converting.
    /// </summary>
    public class PowerPointToPdfConverter: OfficeInteropFileConverterBase
    {
        /// <summary>
        /// Returns a list of supported file formats
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<string> InitializeSupportedFormatCollection()
        {
            return new List<string>()
            { ".odp", ".pot", ".potm", ".potx", ".ppa", ".ppam", ".pps", ".ppsm", ".ppsx", ".ppt", ".pptm", ".pptx", ".pwz" };
        }

        /// <summary>
        /// Opens file from disk, converts it to PDF and saves to disk
        /// </summary>
        /// <param name="sourceFileName">Source file name</param>
        /// <param name="destinationFileName">Destination file name</param>
        protected override void ConvertFile(string sourceFileName, string destinationFileName)
        {
            Application powerPointApplication = new Application();
            try
            {
                Presentation presentation = null;

                var readOnly = MsoTriState.msoTrue;
                var untitled = MsoTriState.msoTrue;
                var withWindow = MsoTriState.msoFalse;

                presentation = powerPointApplication.Presentations.Open(sourceFileName, readOnly,
                    untitled, withWindow);
                presentation.ExportAsFixedFormat(destinationFileName, PpFixedFormatType.ppFixedFormatTypePDF);
            }
            finally
            {
                powerPointApplication.Quit();
            }
        }
    }
}
