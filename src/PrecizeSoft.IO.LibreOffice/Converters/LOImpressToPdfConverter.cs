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
    public class LOImpressToPdfConverter : LOToPdfConverterBase
    {
        private IEnumerable<string> supportedFormatCollection = new List<string>()
            { ".cgm", ".cwk", ".dps", ".dpt", ".fodp", ".key", ".odg", ".odp", ".otp", ".pot", ".potm", ".potx", ".pps",
            ".ppsx", ".ppt", ".pptm", ".pptx", ".sti", ".sxd", ".sxi", ".uof", ".uop", ".xml" };
        public override IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                return this.supportedFormatCollection;
            }
        }

        public LOImpressToPdfConverter() : base("impress_pdf_Export")
        {
        }
    }
}
