using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO.Contracts.Converters
{
    public interface ILogService
    {
        void LogRequest(RequestLog request);

        void LogResponse(ResponseLog response);
    }
}
