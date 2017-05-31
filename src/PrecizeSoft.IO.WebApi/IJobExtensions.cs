using PrecizeSoft.IO.Contracts.Converters;
using PrecizeSoft.IO.WebApi.Contracts.Converter.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO
{
    internal static class IJobExtensions
    {
        public static JobInfo ToJobInfo(this IJob job, IFileInfo inputFile, IFileInfo outputFile)
        {
            return new JobInfo
            {
                JobId = job.JobId,
                ExpireDateUtc = job.ExpireDateUtc,
                SessionId = job.SessionId,
                Rating = job.Rating,
                InputFile = inputFile.ToStorageFileInfo(),
                OutputFile = outputFile?.ToStorageFileInfo(),
                ErrorType = job.ErrorType
            };
        }
    }
}
