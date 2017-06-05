using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using PrecizeSoft.IO.Converters;
using Microsoft.Net.Http.Headers;
using PrecizeSoft.IO.Contracts.Converters;
using PrecizeSoft.IO.WebApi.Contracts.Converter.V1;
using System.IO.Compression;

namespace PrecizeSoft.IO.WebApi.Controllers.Converter.V1
{
    //[Route("[controller]")]
    //[Route("rest/converter/v1")]
    //[Consumes("application/json", "application/json-patch+json", "multipart/form-data", "application/octet-stream")]
    //[Produces("application/octet-stream", "application/pdf", "application/json")]
    public abstract class ConverterV1ControllerBase : Controller
    {
        protected IFileConverter converter;

        protected IJobService jobService;

        protected IFileService fileService;

        protected StoreOptions options;

        protected ILogService logService;

        public ConverterV1ControllerBase(IFileConverter converter, IJobService jobService, IFileService fileService, StoreOptions options, ILogService logService = null)
        {
            this.converter = converter;
            this.jobService = jobService;
            this.fileService = fileService;
            this.options = options;
            this.logService = logService;
        }

        /// <summary>
        /// Convert file
        /// </summary>
        /// <param name="file">File to convert</param>
        /// <param name="sessionId">Session ID (GUID) for multiple conversion</param>
        /// <returns>Conversion job information</returns>
        [HttpPost("jobs", Name = "AddJob")]
        //[Consumes("multipart/form-data")]
        [Produces("application/json")]
        public virtual JobInfo AddJob([FromForm] IFormFile file, [FromForm] Guid? sessionId/*, [FromQuery] bool storeFile = false*/)
        {
            return this.AddJobInternal(sessionId, file);
        }

        /// <summary>
        /// Download file (source or converted) by ID
        /// </summary>
        /// <param name="id">File ID (GUID)</param>
        /// <returns>File (source or converted)</returns>
        [HttpGet("files/{id}", Name = "GetFile")]
        [Produces("application/octet-stream")]
        public virtual IActionResult GetFile(Guid id)
        {
            IFile file = this.fileService.GetFile(id);

            if (file == null)
            {
                return NotFound();
            }

            var contentDisposition = new ContentDispositionHeaderValue("attachment");
            contentDisposition.SetHttpFileName(file.FileName);
            //Fix for Swagger UI
            contentDisposition.FileName = contentDisposition.FileName.Replace('?','_').Replace(' ', '_').Replace("\"", "");
            Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            //Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition, Transfer-Encoding");

            return File(file.Bytes, "application/octet-stream");
        }

        /// <summary>
        /// Download all converted files from session in Zip arcive
        /// </summary>
        /// <param name="sessionId">Session ID (GUID)</param>
        /// <returns>Files in Zip archive</returns>
        [HttpGet("files/getBySession", Name = "GetFilesBySession")]
        [Produces("application/octet-stream")]
        public virtual IActionResult GetFilesBySession([FromQuery] Guid sessionId)
        {
            IEnumerable<IJob> jobs = this.jobService.GetJobsBySession(sessionId);

            if ((jobs == null) || (jobs.Count() == 0))
            {
                return NotFound();
            }

            IEnumerable<Guid> fileIds = jobs
                .Where(p => p.OutputFileId.HasValue)
                .Select(p => p.OutputFileId.Value)
                .ToList();

            byte[] zipBytes;

            using (MemoryStream stream = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    List<string> addedFileNames = new List<string>();

                    foreach (Guid fileId in fileIds)
                    {
                        IFile file = this.fileService.GetFile(fileId);

                        string fileName = file.FileName;

                        int copyNumber = 1;
                        while (addedFileNames.Where(p => p == fileName).Any())
                        {
                            copyNumber++;
                            fileName =
                                $"{Path.GetFileNameWithoutExtension(file.FileName)} ({copyNumber}){Path.GetExtension(file.FileName)}";
                        }

                        ZipArchiveEntry zipEntry = zip.CreateEntry(fileName, CompressionLevel.Fastest);

                        using (Stream entryStream = zipEntry.Open())
                        {
                            entryStream.Write(file.Bytes, 0, file.Bytes.Length);
                        }

                        addedFileNames.Add(fileName);
                    }
                }

                zipBytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(zipBytes, 0, (int)stream.Length);
            }

            return File(zipBytes, "application/octet-stream", "getpdf.online.zip");
        }

        /// <summary>
        /// Get conversion job information by ID
        /// </summary>
        /// <param name="id">Conversion job ID (GUID)</param>
        /// <returns>Conversion job information</returns>
        [HttpGet("jobs/{id}", Name = "GetJob")]
        [Produces("application/json")]
        public virtual IActionResult GetJob(Guid id)
        {
            JobInfo job = this.GetJobInternal(id);

            if (job == null)
            {
                return NotFound();
            }

            return new ObjectResult(job);
        }

        /// <summary>
        /// Add or update quality rating by conversion job ID
        /// </summary>
        /// <param name="id">Conversion job ID (GUID)</param>
        /// <param name="rating">Quality rating (Number from 1 to 5 or empty value)</param>
        /// <returns></returns>
        [HttpPut("jobs/{id}/rating", Name = "EditRating")]
        [Consumes("application/json")]
        public virtual IActionResult EditJob([FromRoute] Guid id, [FromBody] byte? rating)
        {
            if (this.jobService.GetJob(id) == null)
            {
                return NotFound();
            }

            this.jobService.EditJob(id, rating);

            return new NoContentResult();
        }

        /// <summary>
        /// Get supported input file formats
        /// </summary>
        /// <returns>List of supported formats</returns>
        [HttpGet("formats", Name = "GetFormats")]
        [Produces("application/json")]
        public virtual IEnumerable<string> GetFormats()
        {
            return this.converter.SupportedFormatCollection;
        }

        /// <summary>
        /// Get all conversion jobs by session ID
        /// </summary>
        /// <param name="sessionId">Session ID (GUID)</param>
        /// <returns>List of conversion jobs</returns>
        [HttpGet("jobs/getBySession", Name = "GetJobsBySession")]
        [Produces("application/json")]
        public virtual IEnumerable<JobInfo> GetJobsBySession([FromQuery] Guid sessionId)
        {
            IEnumerable<IJob> jobs = this.jobService.GetJobsBySession(sessionId);
            IEnumerable<Guid> fileIds = this.GetFileIdsFromJobs(jobs);
            IEnumerable<IFileInfo> files = this.fileService.GetFilesInfo(fileIds);

            return jobs.Select(p => p.ToJobInfo(
                files.Single(q => q.FileId == p.InputFileId),
                p.OutputFileId.HasValue ? files.Single(r => r.FileId == p.OutputFileId.Value) : null));
        }

        private IEnumerable<Guid> GetFileIdsFromJobs(IEnumerable<IJob> jobs)
        {
            return jobs.Select(p => p.InputFileId)
                .Concat(jobs.Where(p => p.OutputFileId.HasValue)
                .Select(p => p.OutputFileId.Value));
        }

        /// <summary>
        /// Remove session with all files by ID
        /// </summary>
        /// <param name="id">Session ID (GUID)</param>
        /// <returns></returns>
        [HttpDelete("sessions/{id}", Name = "DeleteSession")]
        public virtual IActionResult Delete(Guid id)
        {
            if (!this.jobService.SessionExists(id))
            {
                return NotFound();
            }

            IEnumerable<IJob> jobs = this.jobService.GetJobsBySession(id);
            IEnumerable<Guid> fileIds = this.GetFileIdsFromJobs(jobs);

            this.jobService.DeleteSession(id);
            this.fileService.DeleteFiles(fileIds);

            return new NoContentResult();
        }

        protected virtual JobInfo AddJobInternal(Guid? sessionId, IFormFile file)
        {
            Guid jobId = Guid.NewGuid();

            if (this.logService != null)
            {
                RequestLog requestLog = new RequestLog
                {
                    RequestId = jobId,
                    RequestDateUtc = DateTime.UtcNow,
                    SenderIp = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    FileExtension = Path.GetExtension(file.FileName),
                    FileSize = (int)file.Length,
                    CustomAttributes = null
                };

                this.logService.LogRequest(requestLog);
            }

            byte[] inFileBytes = new byte[file.Length];

            using (Stream stream = file.OpenReadStream())
            {
                stream.Read(inFileBytes, 0, (int)file.Length);
            }

            byte[] outFileBytes = null;
            Exception convertException = null;

            try
            {
                outFileBytes = this.converter.Convert(inFileBytes, Path.GetExtension(file.FileName));
            }
            catch (Exception e)
            {
                convertException = e;
            }

            if (this.logService != null)
            {
                ResponseLog responseLog = new ResponseLog
                {
                    RequestId = jobId,
                    ResponseDateUtc = DateTime.UtcNow,
                    ResultFileSize = outFileBytes?.Length,
                    ErrorType = convertException?.ToConvertErrorType()
                };

                this.logService.LogResponse(responseLog);
            }

            {
                StorageFile inputFile = file.ToStorageFile();

                StorageFile outputFile = (convertException != null) ? null : new StorageFile
                {
                    FileId = Guid.NewGuid(),
                    FileName = Path.GetFileNameWithoutExtension(file.FileName) + ".pdf",
                    FileSize = outFileBytes.Length,
                    CreateDateUtc = DateTime.UtcNow,
                    Bytes = outFileBytes
                };

                Job job = new Job
                {
                    JobId = jobId,
                    ExpireDateUtc = (this.options?.FileLifeTime == null) ? (DateTime?)null : DateTime.UtcNow.Add(options.FileLifeTime),
                    SessionId = sessionId,
                    Rating = null,
                    InputFileId = inputFile.FileId,
                    OutputFileId = outputFile?.FileId,
                    ErrorType = convertException?.ToConvertErrorType()
                };

                this.fileService.AddFile(inputFile);
                if (outputFile != null ) this.fileService.AddFile(outputFile);
                this.jobService.AddJob(job);

                return job.ToJobInfo(inputFile, outputFile);
            }
        }

        protected JobInfo GetJobInternal(Guid id)
        {
            IJob job = this.jobService.GetJob(id);

            if (job == null) return null;

            IFileInfo inputFile = this.fileService.GetFileInfo(job.InputFileId);
            IFileInfo outputFile = (job.OutputFileId == null) ? null : this.fileService.GetFileInfo(job.OutputFileId.Value);

            return job.ToJobInfo(inputFile, outputFile);
        }
    }
}
