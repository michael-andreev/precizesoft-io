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
    public class LODrawToPdfConverter : LOToPdfConverterBase
    {
        private IEnumerable<string> supportedFormatCollection = new List<string>()
            { ".bmp", ".cdr", ".cmx", ".cwk", ".dxf", ".emf", ".eps", ".fh", ".fh1", ".fh10", ".fh11", ".fh2",
            ".fh3", ".fh4", ".fh5", ".fh6", ".fh7", ".fh8", ".fh9", ".fodg", ".gif", ".jfif", ".jif", ".jpe",
            ".jpeg", ".jpg", ".met", ".mov", ".odg", ".odg", ".otg", ".p65", ".pbm", ".pcd", ".pct", ".pcx",
            ".pgm", ".pict", ".pm", ".pm6", ".pmd", ".png", ".ppm", ".psd", ".pub", ".ras", ".sda", ".sgf",
            ".sgv", ".std", ".svg", ".svgz", ".svm", ".sxd", ".tga", ".tif", ".tiff", ".vdx", ".vsd", ".vsdm",
            ".vsdx", ".wmf", ".wpg", ".xbm", ".xml", ".xpm", ".zmf" };

        public override IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                return this.supportedFormatCollection;
            }
        }

        public LODrawToPdfConverter() : base("draw_pdf_Export")
        {
        }
    }
}
