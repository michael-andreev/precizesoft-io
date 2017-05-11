using Microsoft.AspNetCore.Http;
using PrecizeSoft.IO.Contracts.Converters;
using PrecizeSoft.IO.WebApi.Contracts.Converter.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO
{
    internal static class IFormFileExtensions
    {
        public static StorageFile ToStorageFile(this IFormFile formFile)
        {
            byte[] fileBytes;

            using (Stream stream = formFile.OpenReadStream())
            {
                fileBytes = new byte[stream.Length];
                stream.Read(fileBytes, 0, (int)stream.Length);
            }

            StorageFile file = new StorageFile
            {
                FileId = Guid.NewGuid(),
                CreateDateUtc = DateTime.UtcNow,
                FileName = formFile.FileName,
                FileSize = (int)formFile.Length,
                Bytes = fileBytes
            };

            return file;
        }
    }
}
