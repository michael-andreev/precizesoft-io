using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Contracts.Converters
{
    public interface IFile: IFileInfo
    {
        byte[] Bytes { get; set; }
    }
}
