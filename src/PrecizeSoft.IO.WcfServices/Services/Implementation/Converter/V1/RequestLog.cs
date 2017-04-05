﻿using System;
using System.Collections.Generic;

namespace PrecizeSoft.IO.Services.Implementation.Converter.V1
{
    public class RequestLog
    {
        public Guid RequestId { get; set; }

        public DateTime RequestDateUtc { get; set; }

        public string SenderIp { get; set; }

        public string FileExtension { get; set; }

        public int FileSize { get; set; }

        public Dictionary<string,string> CustomAttributes { get; set; }
    }
}
