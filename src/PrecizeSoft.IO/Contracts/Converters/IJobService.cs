using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Contracts.Converters
{
    public interface IJobService
    {
        void AddJob(IJob job);

        void EditJob(Guid jobId, byte? rating);

        IJob GetJob(Guid jobId);

        IEnumerable<IJob> GetJobsBySession(Guid sessionId);

        bool SessionExists(Guid sessionId);

        void DeleteSession(Guid sessionId);
    }
}
