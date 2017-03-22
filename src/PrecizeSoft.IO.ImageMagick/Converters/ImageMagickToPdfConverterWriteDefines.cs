using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace PrecizeSoft.IO.Converters
{
    internal class ImageMagickToPdfConverterWriteDefines : IWriteDefines
    {
        protected IEnumerable<IDefine> defines = new List<IDefine>();
        public IEnumerable<IDefine> Defines
        {
            get
            {
                return this.defines;
            }
        }

        public MagickFormat Format
        {
            get
            {
                return MagickFormat.Pdf;
            }
        }
    }
}
