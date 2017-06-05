using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Contracts.Converters
{
    public interface IFileService
    {
        void AddFile(IFile file);

        IFile GetFile(Guid fileId);

        IFileInfo GetFileInfo(Guid fileId);

        IEnumerable<IFileInfo> GetFilesInfo(IEnumerable<Guid> fileIds);

        void DeleteFiles(IEnumerable<Guid> fileIds);
    }
}
