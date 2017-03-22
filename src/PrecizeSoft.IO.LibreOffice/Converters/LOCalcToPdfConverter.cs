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
    public class LOCalcToPdfConverter : LOToPdfConverterBase
    {
        private IEnumerable<string> supportedFormatCollection = new List<string>()
            { ".123", ".csv", ".cwk", ".dbf", ".dif", ".et", ".ett", ".fods", ".gnm", ".gnumeric", ".htm", ".html",
            ".numbers", ".ods", ".ots", ".rtf", ".sdc", ".slk", ".stc", ".sxc", ".sylk", ".uof", ".uos", ".wb2", ".wdb",
            ".wk1", ".wk3", ".wks", ".wps", ".wq1", ".wq2", ".xlc", ".xlk", ".xlm", ".xls", ".xlsb", ".xlsm", ".xlsx",
            ".xlt", ".xltm", ".xltx", ".xlw", ".xml" };
        public override IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                return this.supportedFormatCollection;
            }
        }

        public LOCalcToPdfConverter() : base("calc_pdf_Export")
        {
        }
    }
}
