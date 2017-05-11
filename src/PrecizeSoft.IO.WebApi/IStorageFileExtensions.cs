using PrecizeSoft.IO.Contracts.Converters;
using PrecizeSoft.IO.WebApi.Contracts.Converter.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO
{
    internal static class IStorageFileExtensions
    {
        public static StorageFileInfo ToStorageFileInfo(this IFileInfo storageFile)
        {
            return new StorageFileInfo
            {
                FileId = storageFile.FileId,
                FileName = storageFile.FileName,
                FileSize = storageFile.FileSize,
                CreateDateUtc = storageFile.CreateDateUtc
            };
        }
    }
}
