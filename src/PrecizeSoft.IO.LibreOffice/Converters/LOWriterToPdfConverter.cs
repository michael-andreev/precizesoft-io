using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using unoidl.com.sun.star.frame;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.util;

namespace PrecizeSoft.IO.Converters
{
    public class LOWriterToPdfConverter : LOToPdfConverterBase
    {
        private IEnumerable<string> supportedFormatCollection = new List<string>()
            { ".602", ".abw", ".cwk", ".doc", ".docm", ".docx", ".dot", ".dotm", ".dotx", ".fb2", ".fodt", ".htm", ".html",
            ".hwp", ".lrf", ".lwp", ".mcw", ".mw", ".mwd", ".nx^d", ".odt", ".ott", ".pages", ".pdb", ".rtf", ".sdw", ".stw",
            ".sxw", ".txt", ".uof", ".uot", ".wn", ".wpd", ".wps", ".wpt", ".wri", ".xml", ".zabw", ".zip" };
        public override IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                return this.supportedFormatCollection;
            }
        }

        public LOWriterToPdfConverter() : base("writer_pdf_Export")
        {
        }
    }
}
